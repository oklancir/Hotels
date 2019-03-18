var API = {
    Rooms: {
        getAvailableRooms: function (startDate, endDate, success, error) {
            $.ajax({
                url: `/api/rooms?startDate=${startDate}&endDate=${endDate}`,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        get: function (id, success, error) {
            $.ajax({
                url: "/api/rooms/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        update: function (room, success, error) {
            $.ajax({
                url: `/api/rooms/${room.id}`,
                method: "PUT",
                data: room,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/rooms/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        create: function (room, success, error) {
            $.ajax({
                url: "/api/rooms",
                method: "POST",
                data: room,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        }
    },
    Guests: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/guests/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        update: function (guest, success, error) {
            $.ajax({
                url: `/api/guests/${guest.Id}`,
                method: "PUT",
                data: guest,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/guests/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        create: function (guest, success, error) {
            $.ajax({
                url: "/api/guests",
                method: "POST",
                data: guest,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        }
    },
    Reservations: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        update: function (reservation, success, error) {
            $.ajax({
                url: `/api/reservations/${reservation.Id}`,
                method: "PUT",
                data: reservation,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        create: function (reservation, success, error) {
            $.ajax({
                url: "/api/reservations",
                method: "POST",
                data: reservation,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        }
    },
    Items: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        update: function (item, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "PUT",
                data: item,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        create: function (item, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "POST",
                data: item,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        }
    },
    Invoices: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/invoices/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        update: function (invoice, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "PUT",
                data: invoice,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        create: function (invoice, success, error) {
            $.ajax({
                url: "/api/items",
                method: "POST",
                data: invoice,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        }
    },
    ServiceProducts: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/serviceproducts/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        update: function (serviceProduct, success, error) {
            $.ajax({
                url: "/api/serviceproducts/" + id,
                method: "PUT",
                data: serviceProduct,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/serviceproducts/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        },
        create: function (serviceProduct, success, error) {
            $.ajax({
                url: "/api/serviceproducts/" + id,
                method: "POST",
                data: serviceProduct,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { if (error) { error(errorThrown); }  }
            });
        }
    }
}