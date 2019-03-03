$(document).ready(function () {
    $("#reservations").DataTable({
        ajax: {
            url: "/api/reservations",
            dataSrc: ""
        },
        buttons: [
            "add"
        ],
        rowId: function (data) { return `DT_reservation_${data.id}`; },
        columns: [
            {
                data: "startDate",
            },
            {
                data: "endDate"
            },
            {
                data: "guestId"
            },
            {
                data: "roomId"
            },
            {
                data: "reservationStatusId",
                render: function (data) {
                    if (data === 1) {
                        return "<td>Pending</td>";
                    } else if (data === 2) {
                        return "<td>Processing</td>";
                    } else {
                        return "<td>Completed</td>";
                    };
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-reservation-id=" + data + ">Delete</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                  return "<button class='btn-link js-edit' data-toggle='modal' data-target='#edit-reservation-modal' data-reservation-id="
                    + data
                    + ">Edit</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-details' data-reservation-id=" + data + ">Details</button>";
                }
            },
        ]
    });

    $("#reservations").on("click", ".js-delete", function () {
        var button = $(this);
        var reservationId = button.attr("data-reservation-id");

        bootbox.confirm("Are you sure you want to delete this reservation?", function (result) {
            if (result) {
                API.Reservations.delete(
                    reservationId,
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

    $("#add-reservation-modal #addReservation").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#add-reservation-modal");
        
        var reservation = {
            StartDate: modal.find("input#startDate")[0].value,
            EndDate: modal.find("input#endDate")[0].value,
            GuestId: parseInt(modal.find("select#selectGuestId")[0].value),
            RoomId: parseInt(modal.find("select#selectRoomId")[0].value),
            Discount: parseInt(modal.find("input#discount")[0].value)
        };

        $("select#selectGuestId").each(function () {
            var select = $(this);
            select.empty();
            select.append("<option></option>");

            $.ajax({
                url: "api/guests",
            }).then(function (options) {
                options.map(function (option) {
                    var option = $("<option>");

                    option
                        .val(option[select.attr("data-valueKey")])
                        .text(option[select.attr("data-displayKey")]);

                    select.append(option);
                });
            });
        });

        API.Reservations.create(reservation, function (data) {
            var table = $("#reservations").DataTable();

            table
                .row
                .add(data)
                .draw();

            $("#add-reservation-modal").modal("hide");
        }, function () {
            bootbox.alert("Something went wrong with adding reservation.");
        });
    });

    $("#edit-reservation-modal #updateReservation").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#edit-reservation-modal");
        var reservationId = parseInt(modal.find("input#reservationId")[0].value);

        var reservation = {
            Id: reservationId,
            StartDate: modal.find("input#startDate")[0].value,
            EndDate: modal.find("input#endDate")[0].value,
            GuestId: parseInt(modal.find("input#guestId")[0].value),
            RoomId: parseInt(modal.find("input#roomId")[0].value),
            Discount: parseInt(modal.find("input#discount")[0].value)
        };

        API.Reservations.update(reservation, function (data) {
            var table = $("#reservations").DataTable();
            var row = $(`#DT_reservation_${reservationId}`);

            table.row(row)
                .data(data);

            table.row(row).invalidate();

            $("#edit-reservation-modal").modal("hide")
        }, function () {
            bootbox.alert("Something went wrong with updating reservation " + reservationId + ".");
        });
    });

    $("#edit-reservation-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        console.log(button);
        var row = button.parents("tr");
        var table = row.parents("table");
        var reservation = table.DataTable().rows(row).data()[0];

        var modal = $(this);
        modal.find(".modal-body input#reservationId").val(reservation.id);
        modal.find(".modal-body input#startDate").val(reservation.startDate);
        modal.find(".modal-body input#endDate").val(reservation.endDate);
        modal.find(".modal-body input#roomId").val(reservation.roomId);
        modal.find(".modal-body input#guestId").val(reservation.guestId);
        modal.find(".modal-body input#discount").val(reservation.discount);
    });

    $("#reservations").on("click", ".js-details", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to view this reservation?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/reservations/" + button.attr("data-reservation-id"),
                    method: "GET",
                    success: function () {
                    }
                });
            }
        });
    });
});