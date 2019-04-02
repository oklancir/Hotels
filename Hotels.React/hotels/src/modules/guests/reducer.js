import {
  API_GUESTS_GET_ALL_LOADING,
  API_GUEST_DELETE,
  API_GUEST_DELETE_LOADED,
  API_GUESTS_GET_ALL_LOADED,
  GUEST_DELETE,
  GUEST_DELETE_CANCEL
} from "./actions";

const initialState = {
  isLoading: false,
  isError: false,
  data: [],
  guestToDelete: null
};

export default (state = initialState, action) => {
  switch (action.type) {
    case GUEST_DELETE:
      return {
        ...state,
        guestToDelete: action.id
      };
    case GUEST_DELETE_CANCEL:
      return {
        ...state,
        guestToDelete: null
      }
    case API_GUESTS_GET_ALL_LOADING:
      return {
        ...state,
        isLoading: true,
        isError: false
      };
    case API_GUESTS_GET_ALL_LOADED:
      return {
        ...state,
        isLoading: false,
        isError: false,
        data: action.payload
      };
    case API_GUEST_DELETE_LOADED:
      return {
        ...state,
        guestToDelete: null,
        data: state.data.filter(item => item.id !== action.payload)
      };
    default:
      return state;
  }
};
