namespace WallyMapSpinzor2;

//values are specific, to match Brawlhalla's
public enum BehaviorEnum
{
    //NOTE:
    //Ingame, _ (blue enemies) is stored by just not specifiying the behavior.
    //We give it a value in the enum for convenience
    _ = 0,
    FAST = 1,
    TANKY = 2,
    ANY = 3
}