$(document).ready(function () {
    $("#guests").DataTable({
        ajax: {
            url: "/api/guests",
            dataSrc: ""
        },
        columns: [
            {
                data: "firstName",
            },
            {
                data: "lastName"
            },
            {
                data: "address"
            },
            {
                data: "email"
            },
            {
                data: "phoneNumber"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-guest-id=" + data + ">Delete</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-edit' data-guest-id=" + data + ">Edit</button>";
                }
            }
        ]
    });

    $("#guests").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this guest?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/guests/" + button.attr("data-guest-id"),
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                    }
                });
            }
        });
    });

    $("#guests").on("click", ".js-edit", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to edit this guest?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/guests/" + button.attr("data-guest-id"),
                    method: "GET",
                    success: function () {
                    }
                });
            }
        });
    });
});