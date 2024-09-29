Here is a simple implementation of a Bloom filter in JavaScript:

```javascript
class BloomFilter {
  constructor(size = 100) {
    this.size = size;
    this.storage = new Array(size).fill(false);
  }

  add(item) {
    const hashValues = this.getHashValues(item);
    hashValues.forEach(val => this.storage[val] = true);
  }

  contains(item) {
    const hashValues = this.getHashValues(item);
    for (let i = 0; i < hashValues.length; i++) {
      if (!this.storage[hashValues[i]]) {
        return false;
      }
    }
    return true;
  }

  hashOne(item) {
    let hash = 0;
    for (let i = 0; i < item.length; i++) {
      hash = (hash << 5) + hash + item.charCodeAt(i);
      hash = hash & hash;
      hash = Math.abs(hash);
    }
    return hash % this.size;
  }

  hashTwo(item) {
    let hash = 5381;
    for (let i = 0; i < item.length; i++) {
      hash = (hash << 5) + hash + item.charCodeAt(i);
    }
    return Math.abs(hash % this.size);
  }

  getHashValues(item) {
    return [this.hashOne(item), this.hashTwo(item)];
  }
}

// Usage
const bloomFilter = new BloomFilter();
bloomFilter.add('item1');
console.log(bloomFilter.contains('item1')); // true
console.log(bloomFilter.contains('item2')); // false
```

This Bloom filter uses two hash functions to determine the indices in the storage array where an item should be stored or checked. The `add` method adds an item to the filter, and the `contains` method checks if an item is in the filter. Note that Bloom filters can have false positives, but not false negatives.