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
                data: "discount"
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
                    return "<button class='btn-link js-edit' data-toggle='modal' data-target='#edit-reservation-modal' data-reservation-id=" + data + ">Edit</button>";
                }
            }
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
        var selectedGuest = parseInt($("#addSelectGuestId option:selected").val());
        var selectedRoom = parseInt($("#addSelectRoomId option:selected").val());
        
        var reservation = {
            StartDate: $("#addStartDate").val(),
            EndDate: $("#addEndDate").val(),
            GuestId: selectedGuest,
            RoomId: selectedRoom,
            Discount: parseInt($("#addDiscount").val()),
            ReservationStatusId: 1
        };

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
        reservationId = parseInt($("#edit-reservation-modal #editReservationId").val());

        var reservation = {
            Id: parseInt($("#editReservationId").val()),
            ReservationStatusId: parseInt($("#editReservationStatusId").val()),
            StartDate: $("#editStartDate").val(),
            EndDate: $("#editEndDate").val(),
            GuestId: parseInt($("#editGuestId").val()),
            RoomId: parseInt($("#editRoomId").val()),
            Discount: parseInt($("#editDiscount").val())
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

    $("#add-reservation-modal").on("show.bs.modal", function (event) {
        var selectGuest = $("#addSelectGuestId");
        var startDateInput = $("#add-reservation-modal #addStartDate");
        var endDateInput = $("#add-reservation-modal #addEndDate");
        var selectRoom = $("#addSelectRoomId");

        var startDateValue;
        var endDateValue;

        
        selectGuest.empty();
        selectRoom.empty();

        $.ajax({
            url: "/api/guests",
            method: "GET",
            success: function (data) {
                $.each(data, function (i, value) {
                    selectGuest.append(`<option value=${value.id}>${value.firstName} ${value.lastName}</option>`);
                });
            },
            error: function (xhr, options, errorThrown) { error(errorThrown); }
        });

        startDateInput.change(function () {
            startDateValue = startDateInput[0].val
            selectRoom.empty();
            $.ajax({
                url: `/api/rooms?startDate=${startDateValue}&${endDateValue}`,
                method: "GET",
                success: function (data) {
                    $.each(data, function (i, value) {
                        selectRoom.append(`<option value=${value.id}>${value.name}</option>`);
                    });
                },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        });

        endDateInput.change(function () {
            startDateValue = endDateInput[0].val
            selectRoom.empty();
            $.ajax({
                url: `/api/rooms?startDate=${startDateValue}&${endDateValue}`,
                method: "GET",
                success: function (data) {
                    $.each(data, function (i, value) {
                        selectRoom.append(`<option value=${value.id}>${value.name}</option>`);
                    });
                },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        });
    });

    $("#edit-reservation-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        console.log(button);
        var row = button.parents("tr");
        var table = row.parents("table");
        var reservation = table.DataTable().rows(row).data()[0];

        var modal = $(this);
        $("#editReservationId").val(reservation.id);
        $("#editReservationStatusId").val(reservation.reservationStatusId);
        $("#editStartDate").val(reservation.startDate);
        $("#editEndDate").val(reservation.endDate);
        $("#editRoomId").val(reservation.roomId);
        $("#editGuestId").val(reservation.guestId);
        $("#editDiscount").val(reservation.discount);
    });
});