using System;
namespace Caching
{
	public class CacheEvictedItem<K,V>
	{
		public bool _itemEvicted { get; private set; }
        public CacheItem<K, V>? _cacheItemEvicted { get; private set; }
		public CacheEvictedItem(K cacheKey,V cacheVal, bool successfullyEvicted = true)
		{
			_itemEvicted = successfullyEvicted;
			_cacheItemEvicted = new CacheItem<K, V>(cacheKey, cacheVal);
		}
		public CacheEvictedItem()
        {
			_itemEvicted = false;
		}
	}
}

