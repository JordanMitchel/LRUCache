using Caching;

namespace LRUTests;

public class CacheTest
{
    [Theory]
    [InlineData("key", 12)]
    [InlineData(1,"testString")]
    public void When_you_call_the_Add_method_and_the_cache_is_empty_then_you_can_Add_a_value<K,V>(K key,V value)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        _lruCache.Add(key,value);
        Assert.True(_lruCache.Length() == 1);
    }

    [Fact]
    public void When_you_call_the_Add_method_and_the_cache_is_empty_then_you_can_Add_an_object()
    {
        var _lruCache = new LRUCache<String,TestObject>(4);
        _lruCache.ClearCache();
        _lruCache.Add("String",new TestObject(4));
        Assert.True(_lruCache.Length() == 1);
    }


    [Theory]
    [InlineData("key1",12, "key2", 13, "key3", 14)]
    [InlineData("test1", 1, "test2", 2, "test3", 3)]
    public void When_you_call_the_Add_method_and_the_cache_contains_the_value_then_it_goes_to_the_top<K,V>(K key1, V value1, K key2, V value2, K key3, V value3)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        _lruCache.Add(key1,value1);
        _lruCache.Add(key2,value2);
        _lruCache.Add(key3,value3);
        bool inital_cache_is_four = _lruCache.PeakTop().CacheVal.Equals(value3);

        _lruCache.Add(key1,value1);

        bool final_cache_is_twelve = _lruCache.PeakTop().CacheVal.Equals(value1);

        Assert.True(inital_cache_is_four && final_cache_is_twelve);
    }


    [Theory]
    [InlineData("key1",12,"key2", 13, "key3", 14, "key4", 18, "key5", 20)]
    [InlineData(100,"test1", 101, "test2", 102, "test3", 103,"test4", 104,"test5")]
    public void When_you_call_the_Add_method_and_the_cache_is_full_then_the_last_value_is_Removed<K,V>(K key1, V value1, K key2, V value2, K key3, V value3, K key4, V value4, K key5, V value5)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        _lruCache.Add(key1, value1);
        _lruCache.Add(key2, value2);
        _lruCache.Add(key3, value3);
        _lruCache.Add(key4, value4);
        _lruCache.Add(key5, value5);

        Assert.True(_lruCache.PeakBottom().CacheVal.Equals(value2));
    }

    [Theory]
    [InlineData(12,"testVal")]
    [InlineData("testString",1)]
    public void When_you_call_the_Remove_method_on_a_value_that_exists_then_true_should_be_returned<K,V>(K key, V value)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        _lruCache.Add(key, value);
        Assert.True(_lruCache.Remove(key));
    }

    [Theory]
    [InlineData(12, "testVal")]
    [InlineData("testString", 1)]
    public void When_you_call_the_Remove_method_on_a_value_that_does_not_exist_then_false_should_be_returned<K,V>( K key, V value)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        Assert.False(_lruCache.Remove(key));
    }

    [Theory]
    [InlineData(12, "testVal")]
    [InlineData("testString", 1)]
    public void When_you_call_the_Peek_method_on_a_value_that_exists_then_the_value_and_true_should_be_returned<K,V>(K key, V value)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        _lruCache.Add(key, value);
        var result = _lruCache.Peek(key);
        Assert.True(result.Item1.CacheVal.Equals(value) && result.Item2.Equals(true));
    }

    [Theory]
    [InlineData(12, "testVal")]
    [InlineData("testString", 1)]
    public void When_you_call_the_Peeko_method_on_a_value_that_does_not_exist_then_the_key_and_false_should_be_returned<K,V>(K key, V value)
    {
        var _lruCache = new LRUCache<K,V>(4);
        _lruCache.ClearCache();
        var result = _lruCache.Peek(key);

        Assert.True(result.Item1.CacheKey.Equals(key) && result.Item2 == false);
    }
}
