using System;
using System.Collections.Concurrent;

namespace Caching
{
    public class LRUCache<K,V> : ILRUCache<K,V> where K:notnull
    {
        private readonly int _capacity;
        private readonly LinkedList<K> _cacheList;
        private readonly ConcurrentDictionary<K, LRUDictItem<K,V>> _cacheDict;
        private readonly object _lock = new();

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cacheList = new LinkedList<K>();
            _cacheDict = new ConcurrentDictionary<K, LRUDictItem<K,V>>(Environment.ProcessorCount,capacity);
        }

        public CacheEvictedItem<K,V> Add(K cacheKey, V cacheVal)
        {
            //Add lock
            lock (_lock)
            {
                LRUDictItem<K, V> evictedItem;
                if (_cacheDict.ContainsKey(cacheKey))
                {
                    var node = _cacheDict[cacheKey].CacheNode;
                    UpdateLocationOfVal(cacheKey,cacheVal, node);
                }
                else
                {

                    if (_cacheDict.Count >= _capacity)
                    {
                        var oldKey = _cacheList.Last!.Value;
                        _cacheList.RemoveLast();
                        _cacheDict.Remove(oldKey, out evictedItem!);
                        var node = new LinkedListNode<K>(cacheKey);
                        _cacheList.AddFirst(node);
                        _cacheDict.GetOrAdd(cacheKey, new LRUDictItem<K, V>(cacheKey, cacheVal, node));
                        return new CacheEvictedItem<K,V>(oldKey, evictedItem.CacheItem.CacheVal);
                    }
                    else
                    {
                        var node = new LinkedListNode<K>(cacheKey);
                        _cacheList.AddFirst(node);
                        _cacheDict.GetOrAdd(cacheKey, new LRUDictItem<K, V>(cacheKey, cacheVal, node));
                    }

                }
                return new CacheEvictedItem<K,V>();
            }
        }

        public CacheRemoveItem<K,V> Remove(K cacheKey)
        {
            //addLock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(cacheKey))
                {
                    var cacheVal = _cacheDict[cacheKey].CacheItem.CacheVal;
                    var nodeToRemove = _cacheDict[cacheKey].CacheNode;
                    var DictValueToRemove = new LRUDictItem<K,V>(cacheKey, cacheVal, nodeToRemove);
                    _cacheList.Remove(nodeToRemove);
                    _cacheDict.Remove(cacheKey, out DictValueToRemove);
                    return new CacheRemoveItem<K,V>(cacheKey,cacheVal,true);
                }
                return new CacheRemoveItem<K,V>(cacheKey, default, false);
            }

        }

        public CacheItemView<K,V> Peek(K cacheKey)
        {
            //add lock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(cacheKey))
                {
                    var cacheVal = _cacheDict[cacheKey].CacheItem.CacheVal;
                    var node = _cacheDict[cacheKey].CacheNode;
                    UpdateLocationOfVal(cacheKey,cacheVal, node);
                    return new CacheItemView<K,V>(cacheKey, cacheVal, true);
                }
                else
                {
                    //TODO Logging.Warn("Could not view value, does not exist");

                    return new CacheItemView<K,V>(cacheKey,default,false);
                }
            }

        }

        public void ClearCache()
        {
            //add lock
            lock (_lock)
            {
                _cacheDict.Clear();
                _cacheList.Clear();
            }

        }

        public CacheItemView<K,V> PeakTop()
        {
            //add lock
            lock (_lock)
            {
                if (!_cacheDict.IsEmpty)
                {
                    var topKey = _cacheList.FirstOrDefault();
                    var cacheItem = _cacheDict[topKey!].CacheItem;
                    return new CacheItemView<K, V>(cacheItem.CacheKey, cacheItem.CacheVal);
                }
                return new CacheItemView<K, V>();
            }


        }

        public CacheItemView<K,V> PeakBottom()
        {
            //add lock
            lock (_lock)
            {
                if (!_cacheDict.IsEmpty)
                {
                    var topKey = _cacheList.LastOrDefault();
                    var cacheItem = _cacheDict[topKey!].CacheItem;
                    return new CacheItemView<K, V>(cacheItem.CacheKey, cacheItem.CacheVal);
                }
                return new CacheItemView<K, V>();
            }
        }

        public int Length()
        {
            lock (_lock)
                return _cacheDict.Count;
        }

        private void UpdateLocationOfVal(K cacheKey,V? cacheVal, LinkedListNode<K> node)
        {
            lock (_lock)
            {
                _cacheList.Remove(node);
                _cacheList.AddFirst(node);
                _cacheDict[cacheKey] = new LRUDictItem<K,V>(cacheKey, cacheVal!, node);
            }

        }
    }
}

