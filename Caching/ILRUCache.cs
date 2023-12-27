namespace Caching
{
    public interface ILRUCache<T>
    {
        public void add(T val);
        public bool remove(T val);
        public (T, bool) peek(T val);
        public void clearCache();
        public T? peakTop();
        public T? peakBottom();
        public int length();
    }
}