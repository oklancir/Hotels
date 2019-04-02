import {createStore, applyMiddleware, combineReducers, compose} from "redux";
import thunk from "redux-thunk";

import api from "./api";
import roomsReducer from "./rooms/reducer";
import guestsReducer from "./guests/reducer";
import reservationsReducer from "./reservations/reducer";

const rootReducer = combineReducers({
  rooms: roomsReducer,
  guests: guestsReducer,
  reservations: reservationsReducer
});

export default function configureStore() {
  const composeEnhancers =
    window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
  return createStore(
    rootReducer,
    composeEnhancers(applyMiddleware(thunk.withExtraArgument(api)))
  );
}
