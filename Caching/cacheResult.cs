using System;
namespace Caching
{
	public class cacheResult<K,V>
	{
		private readonly CacheInsertItem<K,V>? _cacheInsert;
		private readonly CacheEvictedItem<K,V>? _cacheEviction;
		private readonly CacheRemoveItem<K, V>? _cacheRemoved;
		public cacheResult()
		{
		}
	}
}

