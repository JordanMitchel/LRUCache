using System;
using System.Collections.Concurrent;

namespace Caching
{
    public class LRUCache<T> : ILRUCache<T>
    {
        private readonly int _capacity;
        private readonly LinkedList<T> _cacheList;
        private readonly ConcurrentDictionary<T, LRUDictValue<T>> _cacheDict;
        private object _lock;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cacheList = new LinkedList<T>();
            _cacheDict = new ConcurrentDictionary<T, LRUDictValue<T>>(Environment.ProcessorCount,capacity);
        }

        public void add(T val)
        {
            //Add lock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(val))
                {
                    var node = _cacheDict[val].node;
                    updateLocationOfVal(val, node);
                }
                else
                {
                    var node = new LinkedListNode<T>(val);
                    if (_cacheDict.Count >= _capacity)
                    {
                        var oldNode = _cacheList.Last;
                        _cacheList.RemoveLast();
                        var oldVal = _cacheList.Last.Value;
                        var oldDictValue = new LRUDictValue<T>(oldVal, oldNode);
                        _cacheDict.Remove(oldVal, out oldDictValue);
                    }
                    _cacheList.AddFirst(node);
                    _cacheDict.GetOrAdd(val, new LRUDictValue<T>(val, node));
                }
            }
        }

        public bool remove(T val)
        {
            //addLock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(val))
                {
                    var nodeToRemove = _cacheDict[val].node;
                    var DictValueToRemove = new LRUDictValue<T>(val, nodeToRemove);
                    _cacheList.Remove(nodeToRemove);
                    _cacheDict.Remove(val, out DictValueToRemove);
                    return true;
                }
                return false;
            }

        }

        public (T, bool) peek(T val)
        {
            //add lock
            lock (_lock)
            {
                if (_cacheDict.ContainsKey(val))
                {
                    var node = _cacheDict[val].node;
                    updateLocationOfVal(val, node);
                    return (val, true);
                }
                else
                {
                    //Logging.Warn("Could not view value, does not exist");
                    return (val, false);
                }
            }

        }

        public void clearCache()
        {
            //add lock
            lock (_lock)
            {
                _cacheDict.Clear();
                _cacheList.Clear();
            }

        }

        public T? peakTop()
        {
            //add lock
            lock(_lock)
                return _cacheDict.Count != 0 ? _cacheList.FirstOrDefault() : default;
        }

        public T? peakBottom()
        {
            //add lock
            lock (_lock)
                return _cacheDict.Count != 0 ? _cacheList.LastOrDefault() : default;
        }

        public int length()
        {
            //add lock
            lock (_lock)
                return _cacheDict.Count;
        }

        private void updateLocationOfVal(T? val, LinkedListNode<T> node)
        {
            //add lock
            lock (_lock)
            {
                _cacheList.Remove(node);
                _cacheList.AddFirst(node);
                _cacheDict[val] = new LRUDictValue<T>(val, node);
            }

        }
    }
}

