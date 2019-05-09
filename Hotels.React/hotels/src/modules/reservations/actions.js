export const API_RESERVATIONS_GET_ALL = "API_RESERVATIONS_GET_ALL";
export const API_RESERVATIONS_GET_ALL_LOADING =
  "API_RESERVATIONS_GET_ALL_LOADING";
export const API_RESERVATIONS_GET_ALL_LOADED =
  "API_RESERVATIONS_GET_ALL_LOADED";
export const API_RESERVATIONS_GET_ALL_ERROR = "API_RESERVATIONS_GET_ALL_ERROR";

export const API_RESERVATION_DELETE = "API_RESERVATION_DELETE";
export const API_RESERVATION_DELETE_LOADING = "API_RESERVATION_DELETE_LOADING";
export const API_RESERVATION_DELETE_LOADED = "API_RESERVATION_DELETE_LOADED";
export const API_RESERVATION_DELETE_ERROR = "API_RESERVATION_DELETE_ERROR";

export const API_RESERVATION_CREATE_LOADED = "API_RESERVATION_CREATE_LOADED";

export const API_RESERVATION_EDIT_LOADED = "API_RESERVATION_EDIT_LOADED";

export const RESERVATION_DELETE = "RESERVATION_DELETE";
export const RESERVATION_DELETE_CANCEL = "RESERVATION_DELETE_CANCEL";

export const RESERVATION_CREATE = "RESERVATION_CREATE";
export const RESERVATION_CREATE_CANCEL = "RESERVATION_CREATE_CANCEL";

export const RESERVATION_EDIT = "RESERVATION_EDIT";
export const RESERVATION_EDIT_CANCEL = "RESERVATION_EDIT_CANCEL";

export const apiReservationsGetAll = () => (dispatch, getState, api) => {
  return api.get("reservations").then(response => {
    dispatch({
      type: API_RESERVATIONS_GET_ALL_LOADED,
      payload: response.data
    });
  });
};

export const createReservation = () => ({
  type: RESERVATION_CREATE
});

export const createReservationCancel = () => ({
  type: RESERVATION_CREATE_CANCEL
});

export const editReservation = id => ({
  type: RESERVATION_EDIT,
  id: id
});

export const editReservationCancel = () => ({
  type: RESERVATION_EDIT_CANCEL
});

export const reservationDelete = id => ({
  type: RESERVATION_DELETE,
  id: id
});

export const reservationDeleteCancel = () => ({
  type: RESERVATION_DELETE_CANCEL
});

export const apiReservationDelete = id => (dispatch, getState, api) => {
  return api.delete(`reservations/${id}`).then(response => {
    dispatch({
      type: API_RESERVATION_DELETE_LOADED,
      payload: id
    });
  });
};

export const apiReservationCreate = (
  startDate,
  endDate,
  guestId,
  roomId,
  discount
) => (dispatch, getState, api) => {
  return api
    .post("reservations", {
      data: {
        startDate,
        endDate,
        guestId,
        roomId,
        discount
      }
    })
    .then(response => {
      dispatch({
        type: API_RESERVATION_CREATE_LOADED,
        payload: response.data
      });
    });
};

export const apiReservationEdit = (
  id,
  startDate,
  endDate,
  guestId,
  roomId,
  discount
) => (dispatch, getState, api) => {
  return api
    .post(`reservations/${id}`, {
      data: {
        startDate,
        endDate,
        guestId,
        roomId,
        discount
      }
    })
    .then(response => {
      dispatch({
        type: API_RESERVATION_EDIT_LOADED,
        payload: response.data
      });
    });
};
