using Caching;

namespace LRUTests;

public class CacheTest
{
    TestObject f;
    public CacheTest()
    {
        var f = new TestObject();
    }
    private static List<string> listData() => new List<string>() { "2" };

    [Theory]
    [InlineData(12)]
    [InlineData("testString")]
    public void When_you_call_the_add_method_and_the_cache_is_empty_then_you_can_add_a_value<T>(T value)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        _lruCache.add(value);
        Assert.True(_lruCache.length() == 1);
    }

    [Fact]
    public void When_you_call_the_add_method_and_the_cache_is_empty_then_you_can_add_an_object()
    {
        var _lruCache = new LRUCache<TestObject>(4);
        _lruCache.clearCache();
        _lruCache.add(new TestObject(4));
        Assert.True(_lruCache.length() == 1);
    }


    [Theory]
    [InlineData(12, 13, 14)]
    [InlineData("test1", "test2", "test3")]
    public void When_you_call_the_add_method_and_the_cache_contains_the_value_then_it_goes_to_the_top<T>(T value_1, T value_2, T value_3)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        _lruCache.add(value_1);
        _lruCache.add(value_2);
        _lruCache.add(value_3);
        bool inital_cache_is_four = _lruCache.peakTop().Equals(value_3);

        _lruCache.add(value_1);

        bool final_cache_is_twelve = _lruCache.peakTop().Equals(value_1);

        Assert.True(inital_cache_is_four && final_cache_is_twelve);
    }


    [Theory]
    [InlineData(12, 13, 14,18,20)]
    [InlineData("test1", "test2", "test3","test4","test5")]
    public void When_you_call_the_add_method_and_the_cache_is_full_then_the_last_value_is_removed<T>(T value_1, T value_2, T value_3, T value_4, T value_5)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        _lruCache.add(value_1);
        _lruCache.add(value_2);
        _lruCache.add(value_3);
        _lruCache.add(value_4);
        _lruCache.add(value_5);

        Assert.True(_lruCache.peakBottom().Equals(value_2));
    }

    [Theory]
    [InlineData(12)]
    [InlineData("testString")]
    public void When_you_call_the_remove_method_on_a_value_that_exists_then_true_should_be_returned<T>(T value)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        _lruCache.add(value);
        Assert.True(_lruCache.remove(value));
    }

    [Theory]
    [InlineData(12)]
    [InlineData("testString")]
    public void When_you_call_the_remove_method_on_a_value_that_does_not_exist_then_false_should_be_returned<T>( T value)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        Assert.False(_lruCache.remove(value));
    }

    [Theory]
    [InlineData(12)]
    [InlineData("testString")]
    public void When_you_call_the_peek_method_on_a_value_that_exists_then_the_value_and_true_should_be_returned<T>(T value)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        _lruCache.add(value);
        var result = _lruCache.peek(value);
        Assert.True(result.Item1.Equals(value) && result.Item2.Equals(true));
    }

    [Theory]
    [InlineData(12)]
    [InlineData("testString")]
    public void When_you_call_the_peeko_method_on_a_value_that_does_not_exist_then_the_value_and_false_should_be_returned<T>(T value)
    {
        var _lruCache = new LRUCache<T>(4);
        _lruCache.clearCache();
        var result = _lruCache.peek(value);
        Assert.True(result.Item1.Equals(value) && result.Item2 == false);
    }
}
