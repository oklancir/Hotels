function Guest(object) {
    if (object) {
        this.id = object.Id;
        this.firstName = object.firstName;
        this.lastName = object.lastName;
        this.address = object.address;
        this.email = object.email;
        this.phoneNumber = object.phoneNumber;
    } else {
        this.id = null;
        this.firstName = null;
        this.lastName = null;
        this.address = null;
        this.email = null;
        this.phoneNumber = null;
    }
}

Guest.prototype.toString = function () {
    return this.id + ": " + this.firstName + " " + this.lastName;
}