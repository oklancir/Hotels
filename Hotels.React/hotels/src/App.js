import React, { Component } from "react";
import { connect } from "react-redux";

import { apiRoomsGetAll } from "./modules/rooms/actions";

import "./App.css";
import Room from "./modules/rooms/components/Room";

class App extends Component {
  componentDidMount() {
    const { apiRoomsGetAll } = this.props;
    apiRoomsGetAll();
  }

  render() {
    const { rooms } = this.props;

    return (
      <div className="App">
        <p>
          Edit <code>src/App.js</code> and save to reload.
          </p>
        {
          rooms && rooms.map(room => (
            <Room
              id={room.id}
              name={room.name}
              availability={room.isAvailable}
              roomType={room.roomTypeId} />
          ))
        }
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
  apiRoomsGetAll: () => dispatch(apiRoomsGetAll())
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);