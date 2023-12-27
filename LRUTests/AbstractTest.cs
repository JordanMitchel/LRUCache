using System;
using Caching;

namespace LRUTests
{
	public abstract class AbstractTest<T>
	{
        [Fact]
        public void When_you_call_the_add_method_and_the_cache_is_empty_then_you_can_add_a_value()
        {
            T valueToAdd = this.CreateSampleValue();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            _lruCache.add(valueToAdd);
            Assert.True(_lruCache.length() == 1);
        }

        [Fact]
        public void When_you_call_the_add_method_and_the_cache_contains_the_value_then_it_goes_to_the_top()
        {
            T valueToAdd = this.CreateSampleValue();
            T valueToAdd_2 = this.CreateSampleValue_2();
            T valueToAdd_3 = this.CreateSampleValue_3();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            _lruCache.add(valueToAdd);
            _lruCache.add(valueToAdd_2);
            _lruCache.add(valueToAdd_3);
            bool inital_cache_is_four = _lruCache.peakTop().Equals(valueToAdd_3);

            _lruCache.add(valueToAdd);

            bool final_cache_is_twelve = _lruCache.peakTop().Equals(valueToAdd);

            Assert.True(inital_cache_is_four && final_cache_is_twelve);
        }

        [Fact]
        public void When_you_call_the_add_method_and_the_cache_is_full_then_the_last_value_is_removed()
        {
            T valueToAdd = this.CreateSampleValue();
            T valueToAdd_2 = this.CreateSampleValue_2();
            T valueToAdd_3 = this.CreateSampleValue_3();
            T valueToAdd_4 = this.CreateSampleValue_4();
            T valueToAdd_5 = this.CreateSampleValue_5();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            _lruCache.add(valueToAdd);
            _lruCache.add(valueToAdd_2);
            _lruCache.add(valueToAdd_3);
            _lruCache.add(valueToAdd_4);
            _lruCache.add(valueToAdd_5);

            Assert.True(_lruCache.peakBottom().Equals(valueToAdd_2));
        }

        [Fact]
        public void When_you_call_the_remove_method_on_a_value_that_exists_then_true_should_be_returned()
        {
            T valueToAdd = this.CreateSampleValue();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            _lruCache.add(valueToAdd);
            Assert.True(_lruCache.remove(valueToAdd));
        }

        [Fact]
        public void When_you_call_the_remove_method_on_a_value_that_does_not_exist_then_false_should_be_returned()
        {
            T valueToAdd = this.CreateSampleValue();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            Assert.False(_lruCache.remove(valueToAdd));
        }

        [Fact]
        public void When_you_call_the_peek_method_on_a_value_that_exists_then_the_value_and_true_should_be_returned()
        {
            T valueToAdd = this.CreateSampleValue();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            _lruCache.add(valueToAdd);
            var result = _lruCache.peek(valueToAdd);
            Assert.True(result.Item1.Equals(valueToAdd) && result.Item2.Equals(true));
        }

        [Fact]
        public void When_you_call_the_peeko_method_on_a_value_that_does_not_exist_then_the_value_and_false_should_be_returned()
        {
            T valueToAdd = this.CreateSampleValue();
            var _lruCache = new LRUCache<T>(5);
            _lruCache.clearCache();
            var result = _lruCache.peek(valueToAdd);
            Assert.True(result.Item1.Equals(valueToAdd) && result.Item2.Equals(false));
        }

        protected abstract T CreateSampleValue();
        protected abstract T CreateSampleValue_2();
        protected abstract T CreateSampleValue_3();
        protected abstract T CreateSampleValue_4();
        protected abstract T CreateSampleValue_5();
    }
}

