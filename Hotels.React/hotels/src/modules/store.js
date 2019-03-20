import { createStore, applyMiddleware, combineReducers } from "redux";
import thunk from "redux-thunk";

import axios from "./axios"
import roomsReducer from "./rooms/reducer";

const rootReducer = combineReducers({
    rooms: roomsReducer
});

export default function configureStore() {
    return createStore(
        rootReducer,
        applyMiddleware(thunk.withExtraArgument(axios))
    );
}
