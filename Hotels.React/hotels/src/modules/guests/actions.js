
export const API_GUESTS_GET_ALL = "API_GUESTS_GET_ALL";
export const API_GUESTS_GET_ALL_LOADING = "API_GUESTS_GET_ALL_LOADING";
export const API_GUESTS_GET_ALL_LOADED = "API_GUESTS_GET_ALL_LOADED";
export const API_GUESTS_GET_ALL_ERROR = "API_GUESTS_GET_ALL_ERROR";

export const GUEST_DELETE = "GUEST_DELETE";
export const GUEST_DELETE_CANCEL = "GUEST_DELETE_CANCEL";
export const API_GUEST_DELETE = "API_GUEST_DELETE";
export const API_GUEST_DELETE_LOADING = "API_GUEST_DELETE_LOADING";
export const API_GUEST_DELETE_LOADED = "API_GUEST_DELETE_LOADED";
export const API_GUEST_DELETE_ERROR = "API_GUEST_DELETE_ERROR";


export const apiGuestsGetAll = () => (dispatch, getState, api) => {
    return api.get("guests").then(response => {
        dispatch({
            type: API_GUESTS_GET_ALL_LOADED,
            payload: response.data
        })
    });
}

export const guestDelete = (id) => ({
    type: GUEST_DELETE,
    id: id
})

export const guestDeleteCancel = () => ({
    type: GUEST_DELETE_CANCEL
})

export const apiGuestDelete = (id) => (dispatch, getState, api) => {
    return api.delete(`guests/${id}`).then(response => {
        dispatch({
            type: API_GUEST_DELETE_LOADED,
            payload: id
        })
    });
}

export const apiGuestsGetAllLoading = () => dispatch => {
    dispatch({
        type: API_GUESTS_GET_ALL_LOADING
    });
};