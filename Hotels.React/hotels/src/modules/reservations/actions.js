
export const API_RESERVATIONS_GET_ALL = "API_RESERVATIONS_GET_ALL";
export const API_RESERVATIONS_GET_ALL_LOADING = "API_RESERVATIONS_GET_ALL_LOADING";
export const API_RESERVATIONS_GET_ALL_LOADED = "API_RESERVATIONS_GET_ALL_LOADED";
export const API_RESERVATIONS_GET_ALL_ERROR = "API_RESERVATIONS_GET_ALL_ERROR";

export const API_RESERVATION_DELETE = "API_RESERVATION_DELETE";
export const API_RESERVATION_DELETE_LOADING = "API_RESERVATION_DELETE_LOADING";
export const API_RESERVATION_DELETE_LOADED = "API_RESERVATION_DELETE_LOADED";
export const API_RESERVATION_DELETE_ERROR = "API_RESERVATION_DELETE_ERROR";


export const apiReservationsGetAll = () => (dispatch, getState, axios) => {
    return axios.get("reservations").then(response => {
        dispatch({
            type: API_RESERVATIONS_GET_ALL_LOADED,
            payload: response.data
        })
    });
}

export const apiReservationDelete = (id) => (dispatch, getState, axios) => {
    return axios.delete(`reservations/${id}`).then(response => {
        dispatch({
            type: API_RESERVATION_DELETE_LOADED,
            payload: id
        })
    });
}