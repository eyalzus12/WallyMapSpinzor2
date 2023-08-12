namespace WallyMapSpinzor2;

public class Cache<T>
{
    public T Value
    {
        get
        {
            if(!_IsCached)
            {
                _CachedValue = _CacheFunc();
                _IsCached = true;
            }
            return _CachedValue!;
        }
    }
    
    public T? _CachedValue{get; set;}
    public bool _IsCached{get; set;} = false; //set to false when you need a recache
    public Func<T> _CacheFunc{get; set;}
    
    public Cache(Func<T> cacheFunc)
    {
        _CacheFunc = cacheFunc;
    }
}