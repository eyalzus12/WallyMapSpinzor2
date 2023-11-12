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
    //when rendering pairs of points as a line, make sure to flip them so the first has a lower X
    //to get the correct collision normal
    public static IEnumerable<(double, double)> CollisionQuad(double fromX, double fromY, double toX, double toY, double anchorX, double anchorY)
    {
        int segments = (int)Math.Round((Math.Abs(toX-anchorX) + Math.Abs(fromX-anchorX) + Math.Abs(toY-anchorY) + Math.Abs(fromY-anchorY))/125);
        if(segments < 4) segments = 4;
        if(segments > 10) segments = 10;
        for(int i = 0; i <= segments; ++i)
        {
            double fraction = i / (double)segments;
            double offsetX0 = (anchorX - fromX) * fraction;
            double offsetX1 = (anchorX - toX) * (1-fraction);
            double newX = (fromX + offsetX0) * (1-fraction) + (toX + offsetX1) * fraction;
            double offsetY0 = (anchorY - fromY) * fraction;
            double offsetY1 = (anchorY- toY) * (1-fraction);
            double newY = (fromY + offsetY0) * (1-fraction) + (toY + offsetY1) * fraction;
            
            yield return (newX, newY);
        }
    }
}