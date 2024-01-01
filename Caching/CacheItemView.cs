using System;
namespace Caching
{
	public class CacheItemView<K,V>
	{
		public bool _itemViewable { get; private set; }
        public CacheItem<K, V>? _isInCache { get; private set; }
		public CacheItemView(K cacheKey,V cacheVal, bool viewable = true)
		{
            _itemViewable = viewable;
			_isInCache = new CacheItem<K, V>(cacheKey, cacheVal);
		}
		public CacheItemView()
        {
            _itemViewable = false;
		}
	}
}

