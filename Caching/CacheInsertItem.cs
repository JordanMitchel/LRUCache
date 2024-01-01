using System;
namespace Caching
{
	public class CacheInsertItem<K,V>
	{
		private readonly bool _successfullyInserted;
		private readonly (K, V) _cacheItemInserted;
		public CacheInsertItem((K, V) insertedItem, bool successfullyInserted = false)
		{
			_cacheItemInserted = insertedItem;
			_successfullyInserted = successfullyInserted;
		}
	}
}

