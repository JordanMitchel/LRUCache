namespace Caching
{
    public interface ILRUCache<K,V>
    {
        public void Add(K cacheKey,V cacheval);
        public bool Remove(K cacheKey);
        public (CacheItem<K, V>, bool) Peek(K cacheKey);
        public void ClearCache();
        public CacheItem<K,V>? PeakTop();
        public CacheItem<K, V>? PeakBottom();
        public int Length();
    }
}