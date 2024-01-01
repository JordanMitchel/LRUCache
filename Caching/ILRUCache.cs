namespace Caching
{
    public interface ILRUCache<K,V>
    {
        public CacheEvictedItem<K,V> Add(K cacheKey,V cacheval);
        public CacheRemoveItem<K,V> Remove(K cacheKey);
        public CacheItemView<K, V> Peek(K cacheKey);
        public void ClearCache();
        public CacheItemView<K,V> PeakTop();
        public CacheItemView<K, V> PeakBottom();
        public int Length();
    }
}