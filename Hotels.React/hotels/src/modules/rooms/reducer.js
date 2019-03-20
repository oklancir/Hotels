import {
  API_ROOMS_GET,
  API_ROOMS_GET_LOADING,
  API_ROOMS_GET_LOADED,
  API_ROOMS_GET_ERROR,
  API_ROOMS_GET_ALL,
  API_ROOMS_GET_ALL_LOADING,
  API_ROOMS_GET_ALL_LOADED,
  API_ROOMS_GET_ALL_ERROR
} from "./actions";

export default (
  state = {
    rooms: {}
  },
  action
) => {
  switch (action.type) {
    case API_ROOMS_GET_ALL_LOADING:
      return {
        isLoading: true,
        isError: false,
        ...state.rooms
      };
    case API_ROOMS_GET_ALL_LOADED:
      return {
        isLoading: false,
        isError: false,
        data: action.payload,
        ...state.rooms
      };
    case API_ROOMS_GET_ALL_ERROR:
      return {
        isLoading: false,
        isError: true,
        data: {},
        ...state.rooms
      };
    case API_ROOMS_GET_LOADING:
      return {};
    case API_ROOMS_GET_LOADED:
      return {};
    default:
      return state;
  }
};
