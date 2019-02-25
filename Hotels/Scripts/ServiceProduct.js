function ServiceProduct(object) {
    if (object) {
        this.id = object.id;
        this.name = object.name;
        this.price = object.price;
    } else {
        this.id = null;
        this.name = null;
        this.price = null;
    }
}

ServiceProduct.prototype.toString = function () {
    return this.name + " - " + this.price;
}