$(document).ready(function () {
    $("#reservations").DataTable({
        ajax: {
            url: "/api/reservations",
            dataSrc: ""
        },
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
                data: "reservationStatusId"
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
                    function() {
                        button.parents('tr').remove();
                    },
                    function(xhr, options, error) {
                        alert(error);
                    }
                );
            }
        });
    });

    $('#edit-reservation-modal #saveChanges').on('click', function (event) {
        var button = $(this);
        var modal = button.parents('#edit-reservation-modal');
        var reservationId = parseInt(modal.find('input#reservationId')[0].value);
        console.log(reservationId);
        // API.Reservations.update()
    });

    $('#edit-reservation-modal').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget);
        var row = button.parents('tr');
        var table = row.parents('table');
        var reservation = table.DataTable().rows(row).data()[0];

        var modal = $(this);
        modal.find('.modal-body input#reservationId').val(reservation.id);
        modal.find('.modal-body input#startDate').val(reservation.startDate);
        modal.find('.modal-body input#endDate').val(reservation.endDate);

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