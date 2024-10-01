add(item) {
    const hashValues = this.getHashValues(item);
    hashValues.forEach(val => this.storage[val] = true);
  }