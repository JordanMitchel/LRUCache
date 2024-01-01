namespace Caching;
public class LRUDictValue<V>
{
    public LinkedListNode<V> node;
    public V cacheValue;

    public LRUDictValue(V value, LinkedListNode<V> node)
    {
        this.cacheValue = value;
        this.node = node;
    }
}

