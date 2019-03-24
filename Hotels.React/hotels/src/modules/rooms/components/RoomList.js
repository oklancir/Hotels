import React, {Component} from "react";
import {connect} from "react-redux";

import {Table} from "react-bootstrap";

import {apiRoomsGetAll} from "../actions";

import Room from "./Room";

class RoomList extends Component {
  componentDidMount() {
    const {apiRoomsGetAll} = this.props;
    apiRoomsGetAll();
  }

  render() {
    const {rooms} = this.props;

    return (
      <React.Fragment>
        <h1>Room List</h1>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>id</th>
              <th>Name</th>
              <th>Room type</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {rooms &&
              rooms.map(room => (
                <Room
                  key={room.id}
                  id={room.id}
                  name={room.name}
                  availability={room.isAvailable}
                  roomType={room.roomTypeId}
                />
              ))}
          </tbody>
        </Table>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    rooms: state.rooms.data
  };
};

const mapDispatchToProps = dispatch => ({
  apiRoomsGetAll: () => dispatch(apiRoomsGetAll())
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(RoomList);
