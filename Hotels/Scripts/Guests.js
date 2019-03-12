$(document).ready(function () {
    $("#guests").DataTable({
        ajax: {
            url: "/api/guests",
            dataSrc: ""
        },
        buttons: [
            "add"
        ],
        rowId: function (data) { return `DT_guest_${data.id}`; },
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
            }
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

    $("#edit-guest-modal #updateGuest").on("click", function (event) {
        var guestModal = $("#edit-guest-modal");
        var guestFirstName = $("#editFirstName").val();
        var guestLastName = $("#editFirstName").val();
        var guestAddress = $("#editAddress").val();
        var guestEmail = $("#editEmail").val();
        var guestPhoneNumber = $("#editPhoneNumber").val()

        var guestId = parseInt($("#editGuestId").val());

        var guest = {
            FirstName: guestFirstName,
            LastName: guestLastName,
            Address: guestAddress,
            Email: guestEmail,
            PhoneNumber: guestPhoneNumber
        };
        
        if (!$.isNumeric(guestId)) {
            API.Guests.create(guest, function (data) {
                var table = $("#guests").DataTable();
                console.log(guest);
                table
                    .row
                    .add(data)
                    .draw();
                guestModal.modal("hide");
            }, function () {
                bootbox.alert("Something went wrong with adding guest.");
            });
        } else {
            console.log(guest);
            $.extend(guest, { Id: guestId });
            API.Guests.update(guest, function (data) {
                var table = $("#guests").DataTable();
                var row = $(`#DT_guest_${guestId}`);

                table.row(row)
                    .data(data);

                table.row(row).invalidate();

                guestModal.modal("hide");
            }, function (guest) {
                bootbox.alert("Something went wrong with updating guest " + guest.Id + ".");
            });
        }
    });

    $("#edit-guest-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        var buttonClassName = button[0].className;
        var modal = $(this);

        if (buttonClassName === "btn-link js-edit")
        {
            var row = button.parents("tr");
            var table = row.parents("table");
            var guest = table.DataTable().rows(row).data()[0];

            $("#editGuestModalLabel").text("Edit Guest")
            $("#editGuestId").val(guest.id);
            $("#editFirstName").val(guest.firstName);
            $("#editLastName").val(guest.lastName);
            $("#editAddress").val(guest.address);
            $("#editEmail").val(guest.email);
            $("#editPhoneNumber").val(guest.phoneNumber);
        }
        else
        {
            $("#editGuestModalLabel").text("Add New Guest");
            $("#editGuestId").val("");
            $("#editFirstName").val("");
            $("#editLastName").val("");
            $("#editAddress").val("");
            $("#editEmail").val("");
            $("#editPhoneNumber").val("");
        }
    });
});