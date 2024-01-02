# LRUCache

A Cache with the ability,to add items via a unique key.\
You can set the capacity within the constructor of the main class LRUCache.\
Based on that limit, if you set it to 5 and you add a 6th item, then the first item added items will be evicted. \
when you call the add Item the 6th time the return object will have variables associated with the key and value for the evicted item.

This LRUCache is intended to be used as a Singleton, it as assumed in your program.cs you would have implemenation similar to builder.Services.AddSingleton<LRUCache,ILRUCache>.\
At this time there is no internal singleton design pattern. 

## ToDos,

- [x] Implement Cache to store unique values
- [x] Implement Cahce with unique key and value
- [x] Unit Tests for Cache
- [x] Implement IComparable on the key and value to check for existing values to reduce further action
- [ ] Implement lazy loading, to reduce waste on initial construction of cache.
- [ ] Enforce Singleton pattern internally
- [x] Make LRU Cache thread safe to avoid race conditions
- [x] Notify users of eviction on the event the cache is full and a new item has been added
- [ ] Implement Controller to attempt manual tests of LRUCache
- [ ] Implement UI to display LRUCache in grid
- [ ] Display retrieval times of items in cache vs database

## Methods implemented
- [x] Peek Method
- [x] PeekTop Method
- [x] PeekBottom
- [x] ClearCache
- [x] Length
- [x] Add
- [x] Remove
- [ ] RemoveBottom
- [ ] RemoveTop
- [ ] DisplayCache
- [ ] ConvertToJson
