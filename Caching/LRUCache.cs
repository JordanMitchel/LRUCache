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

        public void Add(K cacheKey, V cacheVal)
        {
            //Add lock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(cacheKey))
                {
                    var node = _cacheDict[cacheKey].CacheNode;
                    UpdateLocationOfVal(cacheKey,cacheVal, node);
                }
                else
                {
                    var node = new LinkedListNode<K>(cacheKey);
                    if (_cacheDict.Count >= _capacity)
                    {
                        var oldNode = _cacheList.Last;
                        var oldDictItem = new LRUDictItem<K,V>(cacheKey,cacheVal, oldNode!);
                        _cacheList.RemoveLast();
                        _cacheDict.Remove(cacheKey, out oldDictItem);
                    }
                    _cacheList.AddFirst(node);
                    _cacheDict.GetOrAdd(cacheKey, new LRUDictItem<K, V>(cacheKey,cacheVal, node));
                }
            }
        }

        public bool Remove(K cacheKey)
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
                    return true;
                }
                return false;
            }

        }

        public (CacheItem<K,V>, bool) Peek(K cacheKey)
        {
            //add lock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(cacheKey))
                {
                    var cacheVal = _cacheDict[cacheKey].CacheItem.CacheVal;
                    var node = _cacheDict[cacheKey].CacheNode;
                    UpdateLocationOfVal(cacheKey,cacheVal, node);
                    return (new CacheItem<K,V>(cacheKey, cacheVal), true);
                }
                else
                {
                    //TODO Logging.Warn("Could not view value, does not exist");

                    return (new CacheItem<K,V>(cacheKey, default!), false);
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

        public CacheItem<K,V>? PeakTop()
        {
            //add lock
            lock (_lock)
            {
                if (!_cacheDict.IsEmpty)
                {
                    var topKey = _cacheList.FirstOrDefault();
                    return _cacheDict[topKey!].CacheItem;
                }
                return new CacheItem<K, V>(default!, default!);
            }


        }

        public CacheItem<K,V>? PeakBottom()
        {
            //add lock
            lock (_lock)
            {
                if (!_cacheDict.IsEmpty)
                {
                    var topKey = _cacheList.LastOrDefault();
                    return _cacheDict[topKey!].CacheItem;
                }
                return new CacheItem<K, V>(default!, default!);
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

