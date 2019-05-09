import {
  API_RESERVATIONS_GET_ALL_LOADING,
  API_RESERVATIONS_GET_ALL_LOADED,
  API_RESERVATION_DELETE,
  API_RESERVATION_DELETE_LOADED,
  RESERVATION_DELETE,
  RESERVATION_DELETE_CANCEL,
  RESERVATION_EDIT,
  RESERVATION_EDIT_CANCEL,
  RESERVATION_CREATE,
  RESERVATION_CREATE_CANCEL
} from "./actions";

const initialState = {
  isLoading: false,
  isError: false,
  data: [],
  reservationToDelete: null
};

export default (state = initialState, action) => {
  switch (action.type) {
    case RESERVATION_CREATE:
      return {
        ...state,
        createReservation: true
      };
    case RESERVATION_CREATE_CANCEL:
      return {
        ...state,
        createReservation: false
      };
    case RESERVATION_EDIT:
      return {
        ...state,
        reservationToEdit: action.id
      };
    case RESERVATION_EDIT_CANCEL:
      return {
        ...state,
        reservationToEdit: null
      };
    case RESERVATION_DELETE:
      return {
        ...state,
        reservationToDelete: action.id
      };
    case RESERVATION_DELETE_CANCEL:
      return {
        ...state,
        reservationToDelete: null
      };
    case API_RESERVATIONS_GET_ALL_LOADING:
      return {
        ...state,
        isLoading: true,
        isError: false
      };
    case API_RESERVATIONS_GET_ALL_LOADED:
      return {
        ...state,
        isLoading: false,
        isError: false,
        data: action.payload
      };
    case API_RESERVATION_DELETE_LOADED:
      return {
        ...state,
        data: state.data.filter(item => item.id !== action.payload),
        reservationToDelete: null
      };
    default:
      return state;
  }
};
