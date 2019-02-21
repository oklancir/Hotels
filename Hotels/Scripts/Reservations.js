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
            }

        ]
    });

    $("#reservations").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this reservation?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/reservations/" + button.attr("data-reservation-id"),
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    }
                });
            }
        });
    });
});