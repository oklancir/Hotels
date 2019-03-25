import {
  API_ROOMS_GET,
  API_ROOMS_GET_LOADING,
  API_ROOMS_GET_LOADED,
  API_ROOMS_GET_ERROR,
  API_ROOMS_GET_ALL,
  API_ROOMS_GET_ALL_LOADING,
  API_ROOMS_GET_ALL_LOADED,
  API_ROOMS_GET_ALL_ERROR,
  API_ROOM_DELETE,
  API_ROOM_DELETE_LOADED
} from "./actions";

export default (
  state = {
    rooms: []
  },
  action
) => {
  switch (action.type) {
    case API_ROOMS_GET_ALL_LOADING:
      return {
        isLoading: true,
        isError: false,
        ...state
      };
    case API_ROOMS_GET_ALL_LOADED:
      return {
        isLoading: false,
        isError: false,
        data: action.payload,
        ...state
      };
    case API_ROOMS_GET_ALL_ERROR:
      return {
        isLoading: false,
        isError: true,
        data: [],
        ...state
      };
    case API_ROOMS_GET_LOADING:
      return {};
    case API_ROOMS_GET_LOADED:
      return {};
    case API_ROOM_DELETE_LOADED:
      return {
        ...state,
        data: state.data.filter((item) => item.id !== action.payload)
      };
    default:
      return state;
  }
};
