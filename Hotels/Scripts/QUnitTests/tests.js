QUnit.module("API.Rooms", function () {

    var latestRoomId = 0;

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
            assert.equal(room.hasOwnProperty("id"), true, "it has property 'id'");
            assert.equal(room.id > 0, true, "it's id is greater than 0");
            latestRoomId = room.id; // save value for later
            done();
        }

        var error = function (xhrError) {
            // error by default
            done();
            throw xhrError;
        }

        API.Rooms.create(room, success, error);
    });

    QUnit.test("When updating an existing Room", function (assert) {
        var done = assert.async();

        var room = {
            name: "QUNIT ROOM TO UPDATE",
            isAvailable: true,
            roomTypeId: 4
        }

        var success = function (room) {
            latestRoomId = room.id;
        }

        var error = function (xhrError) {
            done();
            throw xhrError;
        }

        API.Rooms.create(room, success, error);

        API.Rooms.get(latestRoomId, function () { console.log("Latest room id" + latestRoomId); }, error);

        var updatedRoomName = "Updated room";
        var updatedRoomId = latestRoomId;

        API.Rooms.get(
            latestRoomId,
            function (room) {
                room.id = updatedRoomId;
                room.name = updatedRoomName;
                API.Rooms.update(
                    room,
                    function (updatedRoom) {
                        assert.equal(updatedRoom.name, updatedRoomName, `Updated room id is ${updatedRoomId}`);
                        assert.equal(updatedRoom.id, updatedRoomId, `Updated room name is ${updatedRoomName}`);
                        done();
                    },
                    function (xhrError) {
                        done();
                        throw xhrError;
                    });
            },
            function (xhrError) {
                done();
                throw xhrError;
            }
        );
    });
})