namespace WallyMapSpinzor2;

public static class BrawlhallaMath
{
    //i have no fucking idea how this works
    //brawlhalla uses this to compute the weight for lerping
    public static double EaseWeight(double weight, bool easeIn, bool easeOut, int easePower)
    {
        if (!easeIn && !easeOut) return weight;

        if (easeIn && easeOut) weight *= 2;
        int num1 = easePower == 2 ? 0 : 1;
        double num2 = 1;
        if (easeIn && easeOut)
        {
            num1++;
            easeIn = weight < 1;
            num2 = 0.5;
        }

        if (easeIn)
        {
            weight = Math.Pow(weight, easePower);
        }
        else if (easeOut)
        {
            if (easePower == 2)
            {
                weight = (weight - num1) * (weight - num1 - 2) - num1;
            }
            else if (easePower > 2)
            {
                weight = ((easePower & 1) << num1) + Math.Pow(weight - num1, easePower) - num1;
            }

            if ((easePower & 1) == 0)
            {
                num2 = -num2;
            }
        }

        return num2 * weight;
    }

    // create a list of points used for collision anchors
    // does not include the starting point
    // when rendering pairs of points as a line, make sure to flip them so the first has a lower X
    // to get the correct collision normal
    public static IEnumerable<(double, double)> CollisionQuad(double x1, double y1, double x2, double y2, double xa, double ya)
    {
        int segments = (int)Math.Round((Math.Abs(x2 - xa) + Math.Abs(x1 - xa) + Math.Abs(y2 - ya) + Math.Abs(y1 - ya)) / 125);
        if (segments < 4) segments = 4;
        if (segments > 10) segments = 10;
        for (int i = 1; i <= segments; ++i)
        {
            double fraction = i / (double)segments;
            double offsetX0 = (xa - x1) * fraction;
            double offsetX1 = (xa - x2) * (1 - fraction);
            double newX = (x1 + offsetX0) * (1 - fraction) + (x2 + offsetX1) * fraction;
            double offsetY0 = (ya - y1) * fraction;
            double offsetY1 = (ya - y2) * (1 - fraction);
            double newY = (y1 + offsetY0) * (1 - fraction) + (y2 + offsetY1) * fraction;

            yield return (newX, newY);
        }
    }

    public static double RoundedCos(double x)
    {
        // im not using Math.TAU here because brawlhalla uses Math.PI * 2
        // which may introduce error. this will probably never matter but eh.
        x -= Math.Floor(x / (Math.PI * 2)) * (Math.PI * 2);
        x = Math.Round(x * 1000) / 1000;
        x = Math.Cos(x);
        x = Math.Round(x * 1000) / 1000;
        return x;
    }

    public static double RoundedSin(double x)
    {
        // im not using Math.TAU here because brawlhalla uses Math.PI * 2
        // which may introduce error. this will probably never matter but eh.
        x -= Math.Floor(x / (Math.PI * 2)) * (Math.PI * 2);
        x = Math.Round(x * 1000) / 1000;
        x = Math.Sin(x);
        x = Math.Round(x * 1000) / 1000;
        return x;
    }

    public static double Length(double x, double y) => Math.Sqrt(x * x + y * y);
    public static (double, double) Normalize(double x, double y)
    {
        double len = Length(x, y);
        if (len == 0) return (0, 0);
        return (x / len, y / len);
    }

    public static (double, double) Lerp(double x1, double y1, double x2, double y2, double w)
    {
        // uses the same rounding as brawlhalla
        double x = Math.Round((x1 * (1 - w) + x2 * w) * 100) / 100;
        double y = Math.Round((y1 * (1 - w) + y2 * w) * 100) / 100;
        return (x, y);
    }

    /*
    public static double Dot(double X1, double Y1, double X2, double Y2) => X1*Y1 + X2*Y2;
    public static double Cross(double X1, double Y1, double X2, double Y2) => X1*Y2 - X2*Y1;

    public static double AngleBetween(double X1, double Y1, double X2, double Y2) =>
        Math.Atan2(Cross(X1,Y1,X2,Y2), Dot(X1,Y1,X2,Y2));
    
    public static (double, double) Rotated(double X, double Y, double t)
    {
        (double sine, double cosi) = Math.SinCos(t);
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
    */

    public static (double, double) LerpWithCenter(double x1, double y1, double x2, double y2, double xc, double yc, double w)
    {
        // code follows brawlhalla logic. brawlhalla does not do a proper Slerp
        // but instead assumes center will always be on the same X or Y as one of the keyframes

        double angle1;
        if (x1 == xc)
        {
            if (y1 > yc) angle1 = Math.PI * 0.5;
            else angle1 = Math.PI * 1.5;
        }
        else if (x1 < xc) angle1 = Math.PI;
        else angle1 = 0;

        double angle2;
        if (x2 == xc)
        {
            if (y2 > yc) angle2 = Math.PI * 0.5;
            else angle2 = Math.PI * 1.5;
        }
        else if (x2 < xc) angle2 = Math.PI;
        else if (angle1 == Math.PI * 1.5) angle2 = Math.PI * 2;
        else angle2 = 0;

        if (angle1 == 0 && angle2 == Math.PI * 1.5)
            angle1 = Math.PI * 2;

        double angle = angle1 * (1 - w) + angle2 * w;
        double x = Math.Round((xc + Math.Abs(x1 - x2) * RoundedCos(angle)) * 100) / 100;
        double y = Math.Round((yc + Math.Abs(y1 - y2) * RoundedSin(angle)) * 100) / 100;
        return (x, y);
    }

    public static double SafeMod(double x, double m)
    {
        x %= m;
        if (x < 0) x += m;
        return x;
    }

    // horde random path generation
    public static IEnumerable<(double, double)> GenerateHordePath(
        BrawlhallaRandom rand, // random number generator
        double boundX, double boundY, double boundW, double boundH, // camera bounds
        double door1X, double door1Y, // door 1
        double door2X, double door2Y, // door 2
        DirEnum dir, PathEnum path, // type
        int idx // path index
    )
    {
        return _().SelectMany(_ => _);

        // hack: yield return multiple IEnumerable's, and then SelectMany them to flatten the list
        IEnumerable<IEnumerable<(double, double)>> _()
        {
            idx %= 20;
            switch (dir)
            {
                case DirEnum.TOP:
                    {
                        double jump = boundW / 10;
                        (double fromX, double fromY) = (boundX + idx * jump, boundY);
                        (double doorX, double doorY) = PickDoor(path, fromX < boundW / 2) ? (door1X, door1Y) : (door2X, door2Y);
                        if (rand.Next() % 4 == 0)
                        {
                            bool idfk = Math.Abs(doorX - fromX) >= boundW / 3;
                            (double midX, double midY) = (doorX + (((fromX > doorX) == idfk) ? 1 : -1) * jump, 1000);
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
                        (double doorX, double doorY) = PickDoor(path, false) ? (door1X, door1Y) : (door2X, door2Y);
                        if (path == PathEnum.FAR && rand.Next() % 3 == 0)
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
                        if (fromX < boundX + boundW / 3)
                        {
                            (doorX, doorY) = PickDoor(PathEnum.CLOSE, true) ? (door1X, door1Y) : (door2X, door2Y);
                            midX1 = -650;
                            midX2 = -550;
                        }
                        else if (fromX > boundX + 2 * boundW / 3)
                        {
                            (doorX, doorY) = PickDoor(PathEnum.CLOSE, false) ? (door1X, door1Y) : (door2X, door2Y);
                            midX1 = 3320;
                            midX2 = 3220;
                        }
                        else
                        {
                            (doorX, doorY) = PickDoor(PathEnum.CLOSE, fromX < boundX + boundW / 2) ? (door1X, door1Y) : (door2X, door2Y);
                            bool chance50 = rand.Next() % 2 == 0;
                            bool chance25 = rand.Next() % 4 == 0;
                            if (chance50)
                            {
                                midX1 = 1201;
                                midX2 = chance25 ? 1461 : 1201;
                            }
                            else
                            {
                                midX1 = 1461;
                                midX2 = chance25 ? 1201 : 1461;
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
                        (double doorX, double doorY) = PickDoor(path, true) ? (door1X, door1Y) : (door2X, door2Y);
                        if (path == PathEnum.FAR && rand.Next() % 3 == 0)
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
            (path == PathEnum.ANY) ? (rand.Next() % 2 == 0) : (left == (path == PathEnum.CLOSE));

        IEnumerable<(double, double)> GeneratePathSegment(double x1, double y1, double x2, double y2, int parts, bool first, bool middle)
        {
            if (first)
                yield return (x1, y1);

            for (int i = 0; i < parts; ++i)
            {
                if (i == parts - 1 && !middle)
                    yield return (x2, y2);
                else
                {
                    double xseg1 = (x2 - x1) / (parts - i);
                    double yseg1 = (y2 - y1) / (parts - i);
                    double xseg2 = xseg1 + x1;
                    double yseg2 = yseg1 + y1;
                    double xSign = xseg1 >= 0 ? 1 : -1;
                    double ySign = yseg1 >= 0 ? 1 : -1;
                    xseg1 = Math.Abs(xseg1);
                    yseg1 = Math.Abs(yseg1);
                    xseg1 = (xseg1 < 15) ? 15 : ((150 < xseg1) ? 150 : xseg1);
                    yseg1 = (yseg1 < 15) ? 15 : ((150 < yseg1) ? 150 : yseg1);
                    xseg1 *= xSign;
                    yseg1 *= ySign;
                    double xResult = xseg2 - xseg1 + (rand.Next() % (2 * xseg1));
                    double yResult = yseg2 - yseg1 + (rand.Next() % (2 * yseg1));
                    yield return (xResult, yResult);
                }
            }
        }
    }
}
