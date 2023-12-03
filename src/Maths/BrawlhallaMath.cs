namespace WallyMapSpinzor2;

public static class BrawlhallaMath
{
    //i have no fucking idea how this works
    //brawlhalla uses this to compute the weight for lerping
    public static double EaseWeight(double weight, bool easeIn, bool easeOut, int easePower)
    {
        if(!easeIn && !easeOut) return weight;
        
        if(easeIn && easeOut) weight *= 2;
        int num1 = easePower == 2?0:1;
        double num2 = 1;
        if(easeIn && easeOut)
        {
            num1++;
            easeIn = weight < 1;
            num2 = 0.5;
        }

        if(easeIn)
        {
            weight = Math.Pow(weight, easePower);
        }
        else if(easeOut)
        {
            if(easePower == 2)
            {
                weight = (weight-num1)*(weight-num1-2)-num1;
            }
            else if(easePower > 2)
            {
                weight = ((easePower&1)<<num1) + Math.Pow(weight - num1, easePower) - num1;
            }

            if((easePower&1) == 0)
            {
                num2 = -num2;
            }
        }

        return num2 * weight;
    }
    
    //create a list of points used for collision anchors
    //does not include the starting point
    //when rendering pairs of points as a line, make sure to flip them so the first has a lower X
    //to get the correct collision normal
    public static IEnumerable<(double, double)> CollisionQuad(double X1, double Y1, double X2, double Y2, double XA, double YA)
    {
        int segments = (int)Math.Round((Math.Abs(X2-XA) + Math.Abs(X1-XA) + Math.Abs(Y2-YA) + Math.Abs(Y1-YA))/125);
        if(segments < 4) segments = 4;
        if(segments > 10) segments = 10;
        for(int i = 1; i <= segments; ++i)
        {
            double fraction = i / (double)segments;
            double offsetX0 = (XA - X1) * fraction;
            double offsetX1 = (XA - X2) * (1-fraction);
            double newX = (X1 + offsetX0) * (1-fraction) + (X2 + offsetX1) * fraction;
            double offsetY0 = (YA - Y1) * fraction;
            double offsetY1 = (YA - Y2) * (1-fraction);
            double newY = (Y1 + offsetY0) * (1-fraction) + (Y2 + offsetY1) * fraction;
            
            yield return (newX, newY);
        }
    }

    public static double Length(double X, double Y) => Math.Sqrt(X*X + Y*Y);
    public static (double, double) Normalize(double X, double Y)
    {
        double len = Length(X,Y);
        if(len == 0) return (0,0);
        return (X/len, Y/len);
    }

    public static (double, double) Lerp(double X1, double Y1, double X2, double Y2, double w) 
        => (X1 * (1-w) + X2 * w, Y1 * (1-w) + Y2 * w);
    
    public static double Dot(double X1, double Y1, double X2, double Y2) => X1*Y1 + X2*Y2;
    public static double Cross(double X1, double Y1, double X2, double Y2) => X1*Y2 - X2*Y1;

    public static double AngleBetween(double X1, double Y1, double X2, double Y2) =>
        Math.Atan2(Cross(X1,Y1,X2,Y2), Dot(X1,Y1,X2,Y2));
    
    public static (double, double) Rotated(double X, double Y, double t)
    {
        double sine = Math.Sin(t), cosi = Math.Cos(t);
        return (X*cosi - Y*sine, X*sine + Y*cosi);
    }

    public static (double, double) Slerp(double X1, double Y1, double X2, double Y2, double w)
    {
        (double _X1, double _Y1) = Normalize(X1, Y1);
        (double _X2, double _Y2) = Normalize(X2, Y2);
        double t = AngleBetween(_X1, _Y1, _X2, _Y2);
        (double _X, double _Y) = Rotated(_X1, _Y1, w*t);
        return (_X*Math.Abs(X1-X2), _Y*Math.Abs(Y1-Y2));
    }

    public static (double, double) LerpWithCenter(double X1, double Y1, double X2, double Y2, double XC, double YC, double w)
    {
        (double _X, double _Y) = Slerp(X1-XC, Y1-YC, X2-XC, Y2-YC, w);
        return (XC + _X, YC + _Y);
    }

    public static double SafeMod(double x, double m)
    {
        x %= m;
        if(x < 0) x += m;
        return x;
    }

    //horde random path generation
    public static IEnumerable<(double, double)> GenerateHordePath(
        BrawlhallaRandom rand, //random number generator
        double boundX, double boundY, double boundW, double boundH, //camera bounds
        double door1X, double door1Y, //door 1
        double door2X, double door2Y, //door 2
        DirEnum dir, PathEnum path, //type
        int idx //path index
    )
    {
        return _().SelectMany(_ => _);

        //hack: yield return multiple IEnumerable's, and then SelectMany them to flatten the list
        //please C# gods give us a yield all thingy
        //and the field keyword we've been waiting on that since C# 9
        IEnumerable<IEnumerable<(double, double)>> _()
        {
            idx %= 20;
            switch(dir)
            {
                case DirEnum.TOP:
                {
                    double jump = boundW / 10;
                    (double fromX, double fromY) = (boundX + idx * jump, boundY);
                    (double doorX, double doorY) = PickDoor(path, fromX < boundW/2)?(door1X,door1Y):(door2X,door2Y);
                    if(rand.Next() % 4 == 0)
                    {
                        bool idfk = Math.Abs(doorX - fromX) >= boundW/3;
                        (double midX, double midY) = (doorX + (((fromX > doorX)==idfk)?1:-1)*jump, 1000);
                        yield return GeneratePathSegment(fromX, fromY, midX, midY, 2, true, true);
                        yield return GeneratePathSegment(midX, midY, doorX, doorY, 2, false, false);
                    }
                    else
                    {
                        yield return GeneratePathSegment(fromX, fromY, doorX, doorY, 2, true, false);
                    }
                }
                break;
                case DirEnum.RIGHT:
                {
                    double jump = boundH / 10;
                    (double fromX, double fromY) = (boundX + boundW, boundY + idx * jump);
                    (double doorX, double doorY) = PickDoor(path, false)?(door1X,door1Y):(door2X,door2Y);
                    if(path == PathEnum.FAR && rand.Next() % 3 == 0)
                    {
                        (double midX1, double midY1) = (3220, 1050);
                        (double midX2, double midY2) = (2220, 850);
                        yield return GeneratePathSegment(fromX, fromY, midX1, midY1, 2, true, true);
                        yield return GeneratePathSegment(midX1, midY1, midX2, midY2, 2, false, true);
                        yield return GeneratePathSegment(midX2, midY2, doorX, doorY, 2, false, false);
                    }
                    else
                    {
                        (double midX, double midY) = (3220, 1300);
                        yield return GeneratePathSegment(fromX, fromY, midX, midY, 3, true, true);
                        yield return GeneratePathSegment(midX, midY, doorX, doorY, 3, false, false);
                    }
                }
                break;
                case DirEnum.BOTTOM:
                {
                    double jump = boundW / 20;
                    (double fromX, double fromY) = (boundX + idx * jump, boundY + boundH + 100);
                    double doorX, doorY;
                    double midX1, midY1 = 2800;
                    double midX2, midY2 = 1600;
                    if(fromX < boundX + boundW/3)
                    {
                        (doorX, doorY) = PickDoor(PathEnum.CLOSE, true)?(door1X,door1Y):(door2X,door2Y);
                        midX1 = -650;
                        midX2 = -550;
                    }
                    else if(fromX > boundX + 2*boundW/3)
                    {
                        (doorX, doorY) = PickDoor(PathEnum.CLOSE, false)?(door1X,door1Y):(door2X,door2Y);
                        midX1 = 3320;
                        midX2 = 3220;
                    }
                    else
                    {
                        (doorX, doorY) = PickDoor(PathEnum.CLOSE, fromX < boundX + boundW/2)?(door1X,door1Y):(door2X,door2Y);
                        bool chance50 = rand.Next()%2 == 0;
                        bool chance25 = rand.Next()%4 == 0;
                        if(chance50)
                        {
                            midX1 = 1201;
                            midX2 = chance25?1461:1201;
                        }
                        else
                        {
                            midX1 = 1461;
                            midX2 = chance25?1201:1461;
                        }
                    }

                    yield return GeneratePathSegment(fromX, fromY, midX1, midY1, 3, true, true);
                    yield return GeneratePathSegment(midX1, midY1, midX2, midY2, 3, false, true);
                    yield return GeneratePathSegment(midX2, midY2, doorX, doorY, 3, false, false);
                }
                break;
                case DirEnum.LEFT:
                {
                    double jump = boundH / 10;
                    (double fromX, double fromY) = (boundX, boundY + idx * jump);
                    (double doorX, double doorY) = PickDoor(path, true)?(door1X,door1Y):(door2X,door2Y);
                    if(path == PathEnum.FAR && rand.Next() % 3 == 0)
                    {
                        (double midX1, double midY1) = (-550, 1050);
                        (double midX2, double midY2) = (1450, 850);

                        yield return GeneratePathSegment(fromX, fromY, midX1, midY1, 2, true, true);
                        yield return GeneratePathSegment(midX1, midY1, midX2, midY2, 2, false, true);
                        yield return GeneratePathSegment(midX2, midY2, doorX, doorY, 2, false, false);
                    }
                    else
                    {
                        (double midX, double midY) = (-550, 1300);
                        yield return GeneratePathSegment(fromX, fromY, midX, midY, 3, true, true);
                        yield return GeneratePathSegment(midX, midY, doorX, doorY, 3, false, false);
                    }
                }
                break;
            }
        }

        bool PickDoor(PathEnum path, bool left) =>
            (path == PathEnum.ANY)
                ?(rand.Next()%2 == 0)
                :(left == (path == PathEnum.CLOSE));

        IEnumerable<(double, double)> GeneratePathSegment(double X1, double Y1, double X2, double Y2, int parts, bool first, bool middle)
        {
            if(first)
                yield return (X1, Y1);

            for(int i = 0; i < parts; ++i)
            {
                if(i == parts-1 && !middle)
                    yield return (X2, Y2);
                else
                {
                    double _X1 = (X2 - X1)/(parts - i);
                    double _Y1 = (Y2 - Y1)/(parts - i);
                    double _X2 = _X1 + X1;
                    double _Y2 = _Y1 + Y1;
                    double signX = _X1>=0?1:-1;
                    double signY = _Y1>=0?1:-1;
                    _X1 = Math.Abs(_X1);
                    _Y1 = Math.Abs(_Y1);
                    _X1 = (_X1 < 15)?15:(150 < _X1)?150:_X1;
                    _Y1 = (_Y1 < 15)?15:(150 < _Y1)?150:_Y1;
                    _X1 *= signX;
                    _Y1 *= signY;
                    double RX = _X2 - _X1 + (rand.Next() % (2*_X1));
                    double RY = _Y2 - _Y1 + (rand.Next() % (2*_Y1));
                    yield return (RX, RY);
                }
            }
        }
    }
}