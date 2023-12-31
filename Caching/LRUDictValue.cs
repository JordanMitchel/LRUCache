namespace Caching;
public class LRUDictValue<T>
{
    public LinkedListNode<T> node;
    public T value;

    public LRUDictValue(T value, LinkedListNode<T> node)
    {
        this.value = value;
        this.node = node;
    }
}

