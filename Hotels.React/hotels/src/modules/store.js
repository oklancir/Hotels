import { createStore, applyMiddleware, combineReducers } from "redux";
import thunk from "redux-thunk";

import axios from "./axios"
import roomsReducer from "./rooms/reducer";
import guestsReducer from "./guests/reducer";
import reservationsReducer from "./reservations/reducer";

const rootReducer = combineReducers({
    rooms: roomsReducer,
    guests: guestsReducer,
    reservations: reservationsReducer
});

export default function configureStore() {
    return createStore(
        rootReducer,
        applyMiddleware(thunk.withExtraArgument(axios))
    );
}
