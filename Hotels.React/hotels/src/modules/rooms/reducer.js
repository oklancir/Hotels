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
  API_ROOM_DELETE_LOADED,
  ROOM_DELETE,
  ROOM_DELETE_CANCEL,
  API_ROOMS_GET_AVAILABLE_LOADED
} from "./actions";

const initialState = {
  isLoading: false,
  isError: false,
  data: [],
  availableRooms: {
    startDate: null,
    endDate: null,
    data: []
  },
  roomToDelete: null
};

export default (state = initialState, action) => {
  switch (action.type) {
    case ROOM_DELETE:
      return {
        ...state,
        roomToDelete: action.id
      };
    case ROOM_DELETE_CANCEL:
      return {
        ...state,
        roomToDelete: null
      };
    case API_ROOMS_GET_ALL_LOADING:
      return {
        ...state,
        isLoading: true,
        isError: false
      };
    case API_ROOMS_GET_ALL_LOADED:
      return {
        ...state,
        isLoading: false,
        isError: false,
        data: action.payload
      };
    case API_ROOMS_GET_ALL_ERROR:
      return {
        ...state,
        isLoading: false,
        isError: true,
        data: []
      };
    case API_ROOMS_GET_AVAILABLE_LOADED:
      return {
        ...state,
        availableRooms: {
          startDate: action.startDate,
          endDate: action.endDate,
          data: action.payload
        }
      }
    case API_ROOMS_GET_LOADING:
      return {};
    case API_ROOMS_GET_LOADED:
      return {};
    case API_ROOM_DELETE_LOADED:
      return {
        ...state,
        roomToDelete: null,
        data: state.data.filter(item => item.id !== action.payload)
      };
    default:
      return state;
  }
};
