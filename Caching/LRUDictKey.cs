namespace Caching;
public class LRUDictKey<T>
{
    public LinkedListNode<T> node;
    public T value;

    public LRUDictKey(T value, LinkedListNode<T> node)
    {
        this.value = value;
        this.node = node;
    }
}

