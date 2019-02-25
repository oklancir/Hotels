var API = {
    Rooms: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/rooms/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        update: function (room, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "PUT",
                data: room,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        create: function (room, success, error) {
            $.ajax({
                url: "/api/reservations",
                method: "POST",
                data: room,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        }
    },
    Guests: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/guests/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "PUT",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "POST",
                data: data,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        }
    },
    Reservations: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "PUT",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "POST",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        }
    },
    Items: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "PUT",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "POST",
                data: data,
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        }
    },
    Invoices: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/invoices/" + id,
                method: "GET",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "PUT",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "DELETE",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "POST",
                success: function (data) { success(data); },
                error: function (xhr, options, errorThrown) { error(errorThrown); }
            });
        }
    }
}

var testSuccess = function (data) {
    console.log("Reservation " + data.id + " successfully returned");
};

var testError = function (data) {
    console.log("Reservation " + data.id + "does not exist!");
};

API.Rooms.get(1, testSuccess, testError);