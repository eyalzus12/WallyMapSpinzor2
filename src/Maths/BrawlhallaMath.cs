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
        Math.Atan2(Y2-Y1, X2-X1);
    
    public static (double, double) Rotated(double X, double Y, double t)
    {
        double sine = Math.Sin(t), cosi = Math.Cos(t);
        return (X*cosi - Y*sine, X*sine + Y*cosi);
    }

    public static (double, double) Slerp(double X1, double Y1, double X2, double Y2, double w)
    {
        (X1, Y1) = Normalize(X1, Y1);
        (X2, Y2) = Normalize(X2, Y2);
        (double _X, double _Y) = Rotated(X1, Y1, w*AngleBetween(X1, Y1, X2, Y2));
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
}