
export const API_GUESTS_GET_ALL = "API_GUESTS_GET_ALL";
export const API_GUESTS_GET_ALL_LOADING = "API_GUESTS_GET_ALL_LOADING";
export const API_GUESTS_GET_ALL_LOADED = "API_GUESTS_GET_ALL_LOADED";
export const API_GUESTS_GET_ALL_ERROR = "API_GUESTS_GET_ALL_ERROR";

export const API_GUEST_DELETE = "API_GUEST_DELETE";
export const API_GUEST_DELETE_LOADING = "API_GUEST_DELETE_LOADING";
export const API_GUEST_DELETE_LOADED = "API_GUEST_DELETE_LOADED";
export const API_GUEST_DELETE_ERROR = "API_GUEST_DELETE_ERROR";


export const apiGuestsGetAll = () => (dispatch, getState, axios) => {
    return axios.get("guests").then(response => {
        dispatch({
            type: API_GUESTS_GET_ALL_LOADED,
            payload: response.data
        })
    });
}

export const apiGuestDelete = (id) => (dispatch, getState, axios) => {
    return axios.delete(`guests/${id}`).then(response => {
        dispatch({
            type: API_GUEST_DELETE_LOADED,
            payload: id
        })
    });
}