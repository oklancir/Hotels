$(document).ready(function () {
    $("#invoices").DataTable({
        ajax: {
            url: "/api/invoices",
            dataSrc: ""
        },
        buttons: [
            "add"
        ],
        rowId: function (data) { return `DT_invoice_${data.id}`; },
        columns: [
            {
                data: "id"
            },
            {
                data: "totalAmount"
            },
            {
                data: "isPaid",
                render: function (data) {
                    if (data === true) {
                        return "<td>Paid</td>";
                    } else {
                        return "<td>Awaiting Payment</td>";
                    };
                }
            },
            {
                data: "reservationId"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-invoice-id=" + data + ">Delete</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn-link js-edit' data-toggle='modal' data-target='#edit-invoice-modal' data-invoice-id=" + data + ">Edit</button>";
                }
            }
        ]
    });

    $("#invoices").on("click", ".js-delete", function () {
        var button = $(this);
        var invoiceId = button.attr("data-invoice-id");

        bootbox.confirm("Are you sure you want to delete this invoice?", function (result) {
            if (result) {
                API.Invoices.delete(
                    invoiceId,
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

    $("#add-invoice-modal #addInvoice").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#add-invoice-modal");

        var invoice = {
            TotalAmount: parseInt($("#addTotalAmount").val()),
            IsPaid: $("#addIsPaid").val(),
            ReservationId: parseInt($("#addInvoiceReservationId").val())
        };

        API.Invoices.create(invoice, function (data) {
            var table = $('#invoices').DataTable();

            table
                .row
                .add(data)
                .draw();

            $("#add-invoice-modal").modal('hide');
        }, function () {
            bootbox.alert("Something went wrong with adding invoice.");
        });
    });

    $("#edit-invoice-modal #updateInvoice").on("click", function (event) {
        var button = $(this);
        var modal = button.parents("#edit-invoice-modal");

        var invoiceId = parseInt(modal.find("input#invoiceId")[0].value);

        var invoice = {
            Id: invoiceId,
            TotalAmount: parseInt($("#editTotalAmount").val()),
            IsPaid: $("#editIsPaid").val(),
            ReservationId: parseInt(("#editInvoiceReservationId").val())
        };

        API.Invoices.update(invoice, function (data) {
            var table = $('#invoices').DataTable();
            var row = $(`#DT_invoice_${invoiceId}`);

            table.row(row)
                .data(data);

            table.row(row).invalidate();

            $("#edit-invoice-modal").modal('hide')
        }, function (invoice) {
            bootbox.alert("Something went wrong with updating invoice " + invoice.Id + ".");
        });
    });

    $("#edit-invoice-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        var row = button.parents("tr");
        var table = row.parents("table");
        var invoice = table.DataTable().rows(row).data()[0];

        var modal = $(this);
        $("#editInvoiceId").val(invoice.id);
        $("#editTotalAmount").val(invoice.totalAmount);
        $("#editIsPaid").val(invoice.isPaid);
        $("#editInvoiceReservationId").val(invoice.reservationId);
    });
});