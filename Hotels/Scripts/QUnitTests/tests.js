QUnit.test("hello test", function (assert) {
    assert.ok(1 == "1", "Passed!");
});

QUnit.test("API Rooms POST Test", function (assert) {
    var room = {
        roomName: "QUNIT ROOM 101",
        roomTypeId: 1,
    }

    var success = function () {
        return "Room successfully inserted.";
    }

    var error = function () {
        return "Something went wrong with posting the room.";
    }

    var roomCreate = API.Rooms.create(room, success, error);
    assert.equal(roomCreate, success);
});