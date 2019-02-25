function Reservation(object) {
    if (object) {
        this.id = object.id;
        this.roomId = object.roomId;
        this.startDate = object.startDate;
        this.endDate = object.endDate;
        this.guestId = object.guestId;
        this.discount = object.discount
    } else {
        this.id = null;
        this.roomId = null;
        this.startDate = null;
        this.endDate = null;
        this.guestId = null;
        this.discount = null;
    }
}

Reservation.prototype.toString = function () {
    return this.id + ": " + this.startDate + " - " + this.endDate;
}