$(document).ready(function () {
    $("#rooms").DataTable({
        ajax: {
            url: "/api/rooms",
            dataSrc: ""
        },
        buttons: [
            "add"
        ],
        rowId: function (data) { return `DT_room_${data.id}`; },
        columns: [
            {
                data: "id",
            },
            {
                data: "name",
            },
            {
                data: "roomTypeId",
                render: function (data) {
                    if (data === 1) {
                        return "<td>Single bed</td>";
                    } else if (data === 2) {
                        return "<td>Double bed</td>";
                    } else if (data === 3) {
                        return "<td>Triple bed</td>";
                    } else {
                        return "<td>Penthouse</td>";
                    };
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-room-id=" + data + ">Delete</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-edit' data-toggle='modal' data-target='#edit-room-modal' data-room-id=" + data + ">Edit</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-details' data-room-id=" + data + ">Details</button>";
                }
            },
        ]
    });

    $("#rooms").on("click", ".js-delete", function () {
        var button = $(this);
        var roomId = button.attr("data-room-id");

        bootbox.confirm("Are you sure you want to delete this room?", function (result) {
            if (result) {
                API.Rooms.delete(
                    roomId,
                    function () {
                        button.parents("tr").remove();
                    },
                    function (xhr, options, error) {
                        alert(error);
                    }
                );
            }
        });
    });

    $("#add-room-modal #addRoom").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#add-room-modal");

        var room = {
            Name: modal.find("input#name")[0].value,
            RoomTypeId: modal.find("input#roomTypeId")[0].value,
        };

        API.Rooms.create(room, function (data) {
            var table = $('#rooms').DataTable();

            table
                .row
                .add(data)
                .draw();

            $("#add-room-modal").modal('hide');
        }, function () {
            bootbox.alert("Something went wrong with adding room.");
        });
    });

    $("#edit-room-modal #updateRoom").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#edit-room-modal");

        var roomId = parseInt(modal.find("input#roomId")[0].value);

        var room = {
            Id: roomId,
            Name: modal.find("input#name")[0].value,
            RoomTypeId: modal.find("input#roomTypeId")[0].value,
        };

        API.Rooms.update(room, function (data) {
            var table = $('#rooms').DataTable();
            var row = $(`#DT_room_${guestId}`);

            table.row(row)
                .data(data);

            table.row(row).invalidate();

            $("#edit-room-modal").modal('hide')
        }, function (guest) {
            bootbox.alert("Something went wrong with updating room " + guest.Id + ".");
        });
    });

    $("#edit-room-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        var row = button.parents("tr");
        var table = row.parents("table");
        var room = table.DataTable().rows(row).data()[0];

        var modal = $(this);
        modal.find(".modal-body input#roomId").val(room.id);
        modal.find(".modal-body input#name").val(room.name);
        modal.find(".modal-body input#roomTypeId").val(room.roomTypeId);
    });

    $("#rooms").on("click", ".js-details", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to view this room?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/rooms/" + button.attr("data-room-id"),
                    method: "GET",
                    success: function () {
                    }
                });
            }
        });
    });
});