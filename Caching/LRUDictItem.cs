namespace Caching;
public class LRUDictItem<K,V>
{
    public LinkedListNode<K> CacheNode { get; private set; }
    public CacheItem<K, V> CacheItem { get; private set; }

    public LRUDictItem(K key, V value, LinkedListNode<K> node)
    {
        CacheNode = node;
        CacheItem = new CacheItem<K, V>(key, value);
    }
}

