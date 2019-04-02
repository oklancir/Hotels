import instance from "../api";

export const API_ROOMS_GET = "API_ROOMS_GET";
export const API_ROOMS_GET_LOADING = "API_ROOMS_GET_LOADING";
export const API_ROOMS_GET_LOADED = "API_ROOMS_GET_LOADED";
export const API_ROOMS_GET_ERROR = "API_ROOMS_GET_ERROR";

export const API_ROOMS_GET_ALL = "API_ROOMS_GET_ALL";
export const API_ROOMS_GET_ALL_LOADING = "API_ROOMS_GET_ALL_LOADING";
export const API_ROOMS_GET_ALL_LOADED = "API_ROOMS_GET_ALL_LOADED";
export const API_ROOMS_GET_ALL_ERROR = "API_ROOMS_GET_ALL_ERROR";

export const API_ROOMS_UPDATE = "API_ROOMS_UPDATE";
export const API_ROOMS_UPDATE_LOADING = "API_ROOMS_UPDATE_LOADING";
export const API_ROOMS_UPDATE_LOADED = "API_ROOMS_UPDATE_LOADED";
export const API_ROOMS_UPDATE_ERROR = "API_ROOMS_UPDATE_ERROR";

export const ROOM_DELETE = "ROOM_DELETE";
export const ROOM_DELETE_CANCEL = "ROOM_DELETE_CANCEL";
export const API_ROOM_DELETE = "API_ROOM_DELETE";
export const API_ROOM_DELETE_LOADING = "API_ROOM_DELETE_LOADING";
export const API_ROOM_DELETE_LOADED = "API_ROOM_DELETE_LOADED";
export const API_ROOM_DELETE_ERROR = "API_ROOM_DELETE_ERROR";

export const apiRoomsGetAll = () => (dispatch, getState, api) => {
  return api.get("rooms").then(response => {
    dispatch({
      type: API_ROOMS_GET_ALL_LOADED,
      payload: response.data
    })
  });
}

export const roomDelete = (id) => ({
  type: ROOM_DELETE,
  id: id
})

export const roomDeleteCancel = () => ({
  type: ROOM_DELETE_CANCEL
})

export const apiRoomDelete = (id) => (dispatch, getState, api) => {
  return api.delete(`rooms/${id}`).then(response => {
    dispatch({
      type: API_ROOM_DELETE_LOADED,
      payload: id
    })
  });
}

export const apiRoomsGetAllLoading = () => dispatch => {
  dispatch({
    type: API_ROOMS_GET_ALL_LOADING
  });
};


