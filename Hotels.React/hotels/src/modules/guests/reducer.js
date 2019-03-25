import {
    API_GUESTS_GET_ALL_LOADING,
    API_GUEST_DELETE,
    API_GUEST_DELETE_LOADED
} from "./actions";

export default (
    state = {
        guests: []
    },
    action
) => {
    switch (action.type) {
        case API_GUESTS_GET_ALL_LOADING:
            return {
                isLoading: true,
                isError: false,
                ...state
            };
        case API_GUEST_DELETE_LOADED:
            return {
                ...state,
                data: state.data.filter((item) => item.id !== action.payload)
            };
        default:
            return state;
    }
};
