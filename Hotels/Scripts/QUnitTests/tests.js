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
            // ovo se izvrsava ako je uspjesno izvedeno pa tu mozemo provjeravati
            // stavimo da prima _data_ parametar
            assert.equal(room.hasOwnProperty("id"), true, "it has property 'id'"); // provjera da li data objekt ima Id
            assert.equal(room.id > 0, true, "it's id is greater than 0"); // provjeris da li je Id > 0
            latestRoomId = room.id; // save for later
            done();
        }

        var error = function (xhrError) {
            // ovo je greska po defaultu
            done();
            throw xhrError;
        }

        API.Rooms.create(room, success, error);
    });

    QUnit.test("When updating an existing Room", function (assert) {
        var done = assert.async();

        // TODO: kreirati novu sobu
        // nakon toga se dohvaca soba
        // nakon toga se updatea soba

        var updatedRoomName = "Updated room";
        API.Rooms.get(
            latestRoomId,
            function(room) {
                room.name = updatedRoomName;
                API.Rooms.update(
                    room,
                    function(updatedRoom) {
                        assert.equal(updatedRoom.name, updatedRoomName);
                        done();
                    },
                    function(xhrError) {
                        done();
                        throw xhrError;
                    });
            },
            function(xhrError) {
                done();
                throw xhrError;
            }
        );
    }); 

})