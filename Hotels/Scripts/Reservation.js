function Reservation(object) {
    if (object) {
        this.id = object.Id;
        this.startDate = object.StartDate;
        this.endDate = object.EndDate;
        this.roomId = object.RoomId;
        this.guestId = object.GuestId;
        this.discount = object.discount
    } else {
        this.id = null;
        this.startDate = null;
        this.endDate = null;
        this.roomId = null;
        this.guestId = null;
        this.discount = null;
    }
}

Reservation.prototype.toString = function () {
    return this.id + ": " + this.startDate + " - " + this.endDate;
}