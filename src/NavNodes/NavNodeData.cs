namespace WallyMapSpinzor2;

public readonly record struct NavNodeData(int NavID, NavNodeData.NavNodeType Type)
{
    public override string ToString() => Type.NavNodeTypeToString() + NavID;

    public enum NavNodeType
    {
        None, A, D, G, L, W
    }
}