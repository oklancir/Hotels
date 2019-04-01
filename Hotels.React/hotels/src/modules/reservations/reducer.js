import {
  API_RESERVATIONS_GET_ALL_LOADING,
  API_RESERVATIONS_GET_ALL_LOADED,
  API_RESERVATION_DELETE,
  API_RESERVATION_DELETE_LOADED
} from "./actions";

export default (
  state = {
    reservations: []
  },
  action
) => {
  switch (action.type) {
    case API_RESERVATIONS_GET_ALL_LOADING:
      return {
        isLoading: true,
        isError: false,
        ...state
      };
    case API_RESERVATIONS_GET_ALL_LOADED:
      return {
        isLoading: false,
        isError: false,
        data: action.payload,
        ...state
      };
    case API_RESERVATION_DELETE_LOADED:
      return {
        ...state,
        data: state.data.filter(item => item.id !== action.payload)
      };
    default:
      return state;
  }
};
