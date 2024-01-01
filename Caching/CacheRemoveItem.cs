namespace Caching
{
    internal class CacheRemoveItem<K, V>
    {
        private readonly bool _successfullyRemoved;
        private readonly (K, V) _cacheItemRemoved;
        public CacheRemoveItem((K, V) removedItem, bool successfullyRemoved = false)
        {
            _cacheItemRemoved = removedItem;
            _successfullyRemoved = successfullyRemoved;
        }
    }
}