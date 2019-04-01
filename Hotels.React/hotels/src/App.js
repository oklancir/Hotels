import React, {Component} from "react";

import {Container} from "react-bootstrap";

import "./App.css";

import ControlledTabs from "./components/ControlledTabs";

class App extends Component {
  render() {
    return (
      <div className="App">
        <Container>
          <h1>Rusky Hotels</h1>
          <ControlledTabs />
        </Container>
      </div>
    );
  }
}

export default App;
