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

// instancirati AXIOS konfiguracijom
export const apiRoomsGetAll = () => (dispatch, getState, axios) => {
  return axios
    .get('/rooms')
    .then(response =>
      dispatch({
        type: API_ROOMS_GET_ALL_LOADED,
        payload: response
      })
    )
    .catch(response =>
      dispatch({
        type: API_ROOMS_GET_ALL_ERROR,
        error: response.error
      })
    );
}

export const apiRoomsGetAllLoading = () => dispatch => {
  dispatch({
    type: API_ROOMS_GET_ALL_LOADING
  });
};
