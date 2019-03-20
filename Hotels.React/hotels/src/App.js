import React, {Component} from "react";
import {connect} from "react-redux";

import {apiRoomsGetAll} from "./modules/rooms/actions";

import logo from "./logo.svg";
import "./App.css";

class App extends Component {
  componentDidMount() {
    const {apiRoomsGetAll} = this.props;
    apiRoomsGetAll();
  }

  render() {
    const {rooms} = this.props;

    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          {
            rooms && rooms.map(room => (<p>{room.name}</p>))
          }
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
        </header>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
  rooms: state.rooms.data
}
};

const mapDispatchToProps = dispatch => ({
  apiRoomsGetAll: () => dispatch(apiRoomsGetAll)
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);
