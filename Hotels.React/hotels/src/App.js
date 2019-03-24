import React, {Component} from "react";

import {Container} from "react-bootstrap";

import "./App.css";
import RoomList from "./modules/rooms/components/RoomList";

class App extends Component {
  render() {
    return (
      <div className="App">
        <Container>
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <RoomList />
        </Container>
      </div>
    );
  }
}

export default App;
