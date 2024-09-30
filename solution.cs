class BloomFilter {
  constructor(size = 100) {
    this.size = size;
    this.storage = new Array(size).fill(false);
  }
}