﻿$(document).ready(function () {
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

    $("#add-guest-modal #addGuest").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#add-guest-modal");

        var guest = {
            FirstName: $("#addFirstName").val(),
            LastName: $("#addLastName").val(),
            Address: $("#addAddress").val(),
            Email: $("#addEmail").val(),
            PhoneNumber: $("#addPhoneNumber").val()
        };

        API.Guests.create(guest, function (data) {
            var table = $("#guests").DataTable();

            table
                .row
                .add(data)
                .draw();

            $("#add-guest-modal").modal('hide');
        }, function () {
            bootbox.alert("Something went wrong with adding guest.");
        });
    });

    $("#edit-guest-modal #updateGuest").on("click", function (event) {
        var button = $(this);
        console.log(button);
        var modal = button.parents("#edit-guest-modal");

        var guestId = parseInt($("#editGuestId")[0].val());
        var guest = {
            Id: guestId,
            FirstName: $("#editFirstName")[0].val(),
            LastName: $("#editLastName")[0].val(),
            Address: $("#editAddress")[0].val(),
            Email: $("#editEmail")[0].val(),
            PhoneNumber: $("#editPhoneNumber")[0].val()
        };

        if (guestId === null) {
            API.Guests.create(guest, function (data) {
                var table = $("#guests").DataTable();

                table
                    .row
                    .add(data)
                    .draw();
                $("#edit-guest-modal").modal('hide');
            }, function () {
                bootbox.alert("Something went wrong with adding guest.");
            });
        } else {

            API.Guests.update(guest, function (data) {
                var table = $('#guests').DataTable();
                var row = $(`#DT_guest_${guestId}`);

                table.row(row)
                    .data(data);

                table.row(row).invalidate();

                $("#edit-guest-modal").modal("hide");
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

    $("#edit-guest-modal").on("hidden.bs.modal", function () {
        $(this).find("#modal-body").children().val("");
    })
});