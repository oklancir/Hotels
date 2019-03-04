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

    var reservation = {
      StartDate: modal.find("input#startDate")[0].value,
      EndDate: modal.find("input#endDate")[0].value,
      GuestId: parseInt(modal.find("select#selectGuestId")[0].value),
      RoomId: parseInt(modal.find("select#selectRoomId")[0].value),
      Discount: parseInt(modal.find("input#discount")[0].value),
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
    var reservationId = parseInt(modal.find("input#reservationId")[0].value);

    var reservation = {
      Id: reservationId,
      ReservationStatusId: parseInt(modal.find("input#reservationStatusId")[0].value),
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

  $("#add-reservation-modal").on("show.bs.modal", function (event) {
    var selectGuest = $("#selectGuestId");
    var startDateInput = $("#add-reservation-modal #startDate");
    var endDateInput = $("#add-reservation-modal #endDate");
    var selectRoom = $("#selectRoomId");

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
    modal.find(".modal-body input#reservationId").val(reservation.id);
    modal.find(".modal-body input#reservationStatusId").val(reservation.reservationStatusId);
    modal.find(".modal-body input#startDate").val(reservation.startDate);
    modal.find(".modal-body input#endDate").val(reservation.endDate);
    modal.find(".modal-body input#roomId").val(reservation.roomId);
    modal.find(".modal-body input#guestId").val(reservation.guestId);
    modal.find(".modal-body input#discount").val(reservation.discount);
  });
});