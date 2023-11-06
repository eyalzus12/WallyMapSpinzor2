namespace WallyMapSpinzor2;

public class BrawlhallaRandom
{
    public int StateIndex{get; set;} = 0;
    public List<uint> State{get; set;} = new(16);
    
    public BrawlhallaRandom() {}
    public BrawlhallaRandom(uint seed) {Init(seed);}
    
    public void Init(uint seed)
    {
        StateIndex = 0;
        State = new(16){seed};
        for(int i=1;i<16;++i) State.Add((uint)(1812433253u*(State[i-1]^(State[i-1]>>30))+i));
    }
    
    public uint Next()
    {
        uint seed0 = State[StateIndex];
        uint seed1 = State[(StateIndex+13)&15];
        uint seed2 = seed0^seed1^(seed0<<16)^(seed1<<15);
        seed1 = State[(StateIndex+9)&15];
        seed1 ^= seed1>>11;
        State[StateIndex] = seed2^seed1;
        seed0 = State[StateIndex];
        uint seed3 = seed0^(seed0<<5)&3661901092u;
        StateIndex = (StateIndex+15)&15;
        seed0 = State[StateIndex];
        State[StateIndex] = seed0^seed2^seed3^(seed0<<2)^(seed2<<18)^(seed1<<28);
        return State[StateIndex];
    }
}