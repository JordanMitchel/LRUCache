using System;
using System.Collections.Concurrent;

namespace Caching
{
    public class LRUCache<T> : ILRUCache<T>
    {
        private readonly int _capacity;
        private readonly LinkedList<T> _cacheList;
        private readonly Dictionary<T, LRUDictKey<T>> _cacheDict;
        private readonly ConcurrentDictionary<T, LRUDictKey<T>> _cacheCDict;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cacheList = new LinkedList<T>();
            _cacheDict = new Dictionary<T, LRUDictKey<T>>(capacity);
        }

        public void add(T val)
        {
            //Add lock

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
                    _cacheList.RemoveLast();
                    var oldVal = _cacheList.Last.Value;
                    _cacheDict.Remove(oldVal);
                }
                _cacheList.AddFirst(node);
                _cacheDict.Add(val, new LRUDictKey<T>(val, node));
            }

        }

        public bool remove(T val)
        {
            //addLock
            if (_cacheDict.ContainsKey(val))
            {
                var node = _cacheDict[val].node;
                _cacheList.Remove(node);
                _cacheDict.Remove(val);
                return true;
            }
            return false;
        }

        public (T, bool) peek(T val)
        {
            //add lock
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

        public void clearCache()
        {
            //add lock
            _cacheDict.Clear();
            _cacheList.Clear();
        }

        public T? peakTop()
        {
            //add lock
            return _cacheDict.Count != 0 ? _cacheList.FirstOrDefault() : default;
        }

        public T? peakBottom()
        {
            //add lock
            return _cacheDict.Count != 0 ? _cacheList.LastOrDefault() : default;
        }

        public int length()
        {
            //add lock
            return _cacheDict.Count;
        }

        private void updateLocationOfVal(T? val, LinkedListNode<T> node)
        {
            //add lock
            _cacheList.Remove(node);
            _cacheList.AddFirst(node);
            _cacheDict[val] = new LRUDictKey<T>(val, node);
        }
    }
}

