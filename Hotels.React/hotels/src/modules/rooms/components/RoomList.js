import React, { Component } from "react";
import { connect } from "react-redux";

import { Table } from "react-bootstrap";

import { apiRoomsGetAll, apiRoomDelete, roomDeleteCancel } from "../actions";

import Room from "./Room";
import ConfirmDelete from "../../../components/ConfirmDelete";

class RoomList extends Component {
  componentDidMount() {
    const { apiRoomsGetAll } = this.props;
    apiRoomsGetAll();
  }

  render() {
    const { rooms, roomToDelete } = this.props;

    return (
      <React.Fragment>
        {Boolean(roomToDelete) && (
          <ConfirmDelete
            objectType="room"
            objectId={roomToDelete}
            cancelAction={roomDeleteCancel}
            deleteAction={apiRoomDelete}
          />
        )}
        <h1>Room List</h1>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Room type</th>
              <th />
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
    roomToDelete: state.rooms.roomToDelete,
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
