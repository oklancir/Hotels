var API = {
    Rooms: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/rooms/" + id,
                method: "GET",
                success: success,
                error: error
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "PUT",
                success: success,
                error: error
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: succ,
                error: error
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/reservations",
                method: "POST",
                data: data,
                success: success,
                error: error
            });
        }
    },
    Guests: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/guests/" + id,
                method: "GET",
                success: success,
                error: error
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "PUT",
                success: success
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: success,
                error: error
            });
        },
        create: function (dat, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "POST",
                success: success,
                error: error
            });
        }
    },
    Reservations: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "GET",
                success: success,
                error: error
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "PUT",
                success: success,
                error: error
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "DELETE",
                success: success,
                error: error
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/reservations/" + id,
                method: "POST",
                success: success,
                error: error
            });
        }
    },
    Items: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "GET",
                success: success,
                error: error
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "PUT",
                success: success,
                error: error
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "DELETE",
                success: success,
                error: error
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "POST",
                data: data,
                success: success,
                error: error
            });
        }
    },
    Invoices: {
        get: function (id, success, error) {
            $.ajax({
                url: "/api/invoices/" + id,
                method: "GET",
                success: function () {
                }
            });
        },
        update: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "PUT",
                success: success,
                error
            });
        },
        delete: function (id, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "DELETE",
                success: success,
                error: error
            });
        },
        create: function (data, success, error) {
            $.ajax({
                url: "/api/items/" + id,
                method: "POST",
                success: success,
                data: data,
                error: error
            });
        }
    }
}