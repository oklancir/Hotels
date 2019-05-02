import React, { Component } from "react";

import { Container, ButtonToolbar, Button } from "react-bootstrap";

import "./App.css";

import ControlledTabs from "./components/ControlledTabs";
import ConfirmDeleteModal from "./components/ConfirmDeleteModal";

class App extends Component {
  state = { modalShow: false };
  render() {
    let modalClose = () => this.setState({ modalShow: false });
    return (
      <div className="App">
        <ButtonToolbar>
          <Button
            variant="primary"
            onClick={() => this.setState({ modalShow: true })}
          >
            Launch vertically centered modal
        </Button>

          <ConfirmDeleteModal
            show={this.state.modalShow}
            onHide={modalClose}
          />
        </ButtonToolbar>
        <Container>
          <h1>Rusky Hotels</h1>
          <ControlledTabs />
        </Container> 
        <ConfirmDeleteModal />
      </div>
    );
  }
}

export default App;
