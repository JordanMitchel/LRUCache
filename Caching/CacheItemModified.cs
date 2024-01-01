using System;
namespace Caching
{
	public class CacheItemModified<K,V>
	{
        public bool _successfullymodified { get; private set; }
        public CacheItem<K, V> _cacheItemInserted { get; private set; }
        public CacheItemModified(CacheItem<K, V> insertedItem, bool successfullyInserted = false)
		{
			_cacheItemInserted = insertedItem;
            _successfullymodified = successfullyInserted;
		}
	}
}

