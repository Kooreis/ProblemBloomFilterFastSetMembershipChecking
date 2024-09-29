Here is a simple implementation of a Bloom filter in TypeScript:

```typescript
class BloomFilter {
    private size: number;
    private storage: Uint8Array;

    constructor(size: number) {
        this.size = size;
        this.storage = new Uint8Array(size);
    }

    add(item: string) {
        const hashValues = this.getHashValues(item);
        hashValues.forEach(val => this.storage[val] = 1);
    }

    check(item: string): boolean {
        const hashValues = this.getHashValues(item);
        for (let i = 0; i < hashValues.length; i++) {
            if (this.storage[hashValues[i]] === 0) {
                return false;
            }
        }
        return true;
    }

    private getHashValues(item: string): number[] {
        const hash1 = this.hash1(item);
        const hash2 = this.hash2(item);
        return [hash1 % this.size, hash2 % this.size];
    }

    private hash1(item: string): number {
        let hash = 0;
        for (let i = 0; i < item.length; i++) {
            hash = (hash << 5) - hash + item.charCodeAt(i);
            hash = hash & hash;
        }
        return hash;
    }

    private hash2(item: string): number {
        let hash = 0;
        for (let i = 0; i < item.length; i++) {
            hash = (hash * 33) ^ item.charCodeAt(i);
        }
        return hash;
    }
}

// Usage
const bloomFilter = new BloomFilter(256);
bloomFilter.add('item1');
console.log(bloomFilter.check('item1')); // true
console.log(bloomFilter.check('item2')); // false
```

This Bloom filter uses two simple hash functions to map items to positions in the filter's storage array. The `add` method hashes the item and sets the corresponding positions in the storage array to 1. The `check` method hashes the item and checks if all corresponding positions in the storage array are 1. If they are, the item is probably in the set; if not, the item is definitely not in the set.