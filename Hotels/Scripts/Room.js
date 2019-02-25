function Room(object) {
    if (object) {
        this.id = object.id;
        this.name = object.name;
        this.isAvailable = object.isAvailable;
        this.roomTypeId = object.roomTypeId;
    } else {
        this.id = null;
        this.name = null;
        this.isAvailable = null;
        this.roomTypeId = null;
    }
}

Room.prototype.toString = function () {
    return this.name + " - " + this.roomTypeId;
}