import React, { Component } from "react";

import { Container } from "react-bootstrap";

import "./App.css";
import RoomList from "./modules/rooms/components/RoomList";
import GuestList from "./modules/guests/components/GuestList";
import ReservationList from "./modules/reservations/components/ReservationList";

class App extends Component {
  render() {
    return (
      <div className="App">
        <Container>
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <RoomList />
          {/* <GuestList /> */}
          {/* <ReservationList /> */}
        </Container>
      </div>
    );
  }
}

export default App;