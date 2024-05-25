using System.Collections.Generic;

namespace WallyMapSpinzor2;

public class RenderState
{
    internal BrawlhallaRandom Random { get; set; } = new();

    // tried making the keys weak refs to avoid GC-locking the objects.
    // but there's no simple way to do it.
    private readonly Dictionary<LevelAnimation, LevelAnimation.State> _levelAnimationState = [];

    internal LevelAnimation.State this[LevelAnimation @this]
    {
        get => _levelAnimationState.TryGetValue(@this, out LevelAnimation.State? state)
                ? state
                : _levelAnimationState[@this] = new();
        set => _levelAnimationState[@this] = value;
    }

    public void Reset()
    {
        Random = new();
        _levelAnimationState.Clear();
    }
}