function Item(object) {
    if (object) {
        this.id = object.id;
        this.quantity = object.quantity;
        this.invoiceId = object.invoiceId;
        this.serviceProductId = object.serviceProductId;
    } else {
        this.id = null;
        this.quantity = null;
        this.invoiceId = null;
        this.serviceProductId = null;
    }
}

Item.prototype.toString = function () {
    return this.id + ": " + this.quantity;
}