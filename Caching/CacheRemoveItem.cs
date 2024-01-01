namespace Caching
{
    public class CacheRemoveItem<K, V>
    {
        public bool _successfullyRemoved { get; private set; }
        public CacheItem<K,V> _cacheItemRemoved { get; private set; }
        public CacheRemoveItem(K cacheKey,V cacheVal, bool successfullyRemoved = false)
        {
            _cacheItemRemoved = new CacheItem<K, V>(cacheKey,cacheVal);
            _successfullyRemoved = successfullyRemoved;
        }
    }
}