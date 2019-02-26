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
                    return "<button class='btn-link js-edit' data-toggle='modal' data-target='#edit-guest-modal' data-guest-id=" + data + ">Edit</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-details' data-guest-id=" + data + ">Details</button>";
                }
            },
        ]
    });

    $("#guests").on("click", ".js-delete", function () {
        var button = $(this);
        var guestId = button.attr("data-guest-id");

        bootbox.confirm("Are you sure you want to delete this guest?", function (result) {
            if (result) {
                API.Guests.delete(
                    guestId,
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

    $("#edit-guest-modal #saveChanges").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#edit-guest-modal");
        var guestId = parseInt(modal.find("input#guestId")[0].value);
        console.log(guestId);

        var guest = {
            FirstName: modal.find("input#firstName")[0].value,
            LastName: modal.find("input#lastName")[0].value,
            Address: parseInt(modal.find("input#address")[0].value),
            Email: parseInt(modal.find("input#email")[0].value),
            PhoneNumber: parseInt(modal.find("input#phoneNumber")[0].value)
        };

        API.Guests.update(guestId ,guest, function () {
            bootbox.alert("Guest " + guest.id + " updated successfully.")
        }, function () {
            bootbox.alert("Something went wrong with updating guest " + guest.id + ".");
        });
    });

    $("#edit-guest-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        var row = button.parents("tr");
        var table = row.parents("table");
        var guest = table.DataTable().rows(row).data()[0];

        var modal = $(this);
        modal.find(".modal-body input#guestId").val(guest.id);
        modal.find(".modal-body input#firstName").val(guest.firstName);
        modal.find(".modal-body input#lastName").val(guest.lastName);
        modal.find(".modal-body input#address").val(guest.address);
        modal.find(".modal-body input#email").val(guest.email);
        modal.find(".modal-body input#phoneNumber").val(guest.phoneNumber);
    });

    $("#guests").on("click", ".js-details", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to view this guest?", function (result) {
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