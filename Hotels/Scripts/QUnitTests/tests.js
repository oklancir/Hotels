QUnit.test("hello test", function (assert) {
    assert.ok(1 == "1", "Passed!");
});

QUnit.test("When creating a new Room", function (assert) {
    var done = assert.async();
    var room = {
        name: "QUNIT ROOM 101",
        isAvailable: true,
        roomTypeId: 1
    }

    var success = function (room) {
        // ovo se izvrsava ako je uspjesno izvedeno pa tu mozemo provjeravati
        // stavimo da prima _data_ parametar
        assert.equal(room.hasOwnProperty("id"), true, "it has property 'id'"); // provjera da li data objekt ima Id
        assert.equal(room.id > 0, true, "it's id is greater than 0"); // provjeris da li je Id > 0
        done();
    }

    var error = function (xhrError) {
        // ovo je greska po defaultu
        done();
        throw xhrError;
    }

    API.Rooms.create(room, success, error);
}); 