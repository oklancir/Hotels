function Invoice(object) {
    if (object) {
        this.id = object.Id;
        this.reservationId = object.reservationId;
        this.isPaid = object.isPaid;
        this.totalAmount = object.totalAmount;
        this.items = object.items;
    } else {
        this.id = null;
        this.reservationId = null;
        this.isPaid = null;
        this.totalAmount = null;
        this.items = null;
    }
}

Invoice.prototype.toString = function () {
    return this.id + ": " + this.isPaid;
}