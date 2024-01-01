using System;
namespace Caching
{
	public class CacheItem<K,V> 
	{
		public K CacheKey { get; private set; }
		public V CacheVal { get; private set; }

		public CacheItem(K key, V val)
		{
			CacheKey = key;
			CacheVal = val;
		}
	}
}

