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

    public static double Lerp(double from, double to, double weight)
    {
        return from*(1-weight) + to*(weight);
    }
}