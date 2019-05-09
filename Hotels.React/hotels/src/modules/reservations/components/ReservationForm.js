import React from "react";
import { connect } from "react-redux";

import { Form, Modal } from "react-bootstrap";

import { apiGuestsGetAll } from "../../guests/actions";
import { apiRoomsGetAvailable } from "../../rooms/actions";
import { apiReservationCreate, apiReservationEdit } from "../actions";

class ReservationForm extends React.Component {
  state = {};

  onDateChange = name => ({ target }) => {
    this.setState({ [name]: target.value });

    const { startDate, endDate } = this.state;
    const { apiRoomsGetAvailable } = this.props;
    if (!!startDate && !!endDate) {
      apiRoomsGetAvailable(startDate, endDate);
    }
  };

  render() {
    const {
      availableRooms,
      guests,
      createReservation,
      reservationToEdit,
      apiReservationCreate,
      apiReservationEdit
    } = this.props;

    const { startDate, endDate, guestId, roomId, discount } = this.state;
    return (
      <Modal show={createReservation || Boolean(reservationToEdit)}>
        <Modal.Header>Create new reservation</Modal.Header>
        <Modal.Body>
          <Form style={{ textAlign: "left" }}>
            <Form.Group controlId="reservationGuestId">
              <Form.Label>Guest</Form.Label>
              {/* TODO: Connect to state */}
              <Form.Control as="select">
                {guests.map(guest => (
                  <option key={guest.id} value={guest.id}>
                    {guest.firstName}
                  </option>
                ))}
              </Form.Control>
            </Form.Group>
            <Form.Group controlId="reservationStartDate">
              <Form.Label>Start Date</Form.Label>
              <Form.Control
                onChange={this.onDateChange("startDate")}
                type="date"
              />
            </Form.Group>
            <Form.Group controlId="reservationEndDate">
              <Form.Label>End Date</Form.Label>
              <Form.Control
                onChange={this.onDateChange("endDate")}
                type="date"
              />
            </Form.Group>
            <Form.Group controlId="reservationRoomId">
              <Form.Label>Room</Form.Label>
              {/* TODO: Connect to state */}
              <Form.Control onChange={10} as="select">
                {availableRooms.map(room => (
                  <option key={room.id} value={room.id}>
                    {room.name}
                  </option>
                ))}
              </Form.Control>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          {createReservation && (
            <button
              onClick={e =>
                apiReservationCreate(
                  startDate,
                  endDate,
                  guestId,
                  roomId,
                  discount
                )
              }
              className="btn btn-primary"
            >
              SAVE
            </button>
          )}
          {reservationToEdit && (
            <button
              onClick={e =>
                apiReservationEdit(
                  reservationToEdit,
                  startDate,
                  endDate,
                  guestId,
                  roomId,
                  discount
                )
              }
              className="btn btn-primary"
            >
              UPDATE
            </button>
          )}
          {/* Stavi u React.Fragment za svaki modal, dva gumba, uvjetno*/}
          <button onClick={this.props.onCancel} className="btn btn-default">
            CANCEL
          </button>
        </Modal.Footer>
      </Modal>
    );
  }
}

const mapStateToProps = state => {
  return {
    guests: state.guests.data,
    availableRooms: state.rooms.availableRooms.data,

    reservationToEdit: state.reservations.reservationToEdit,
    createReservation: state.reservations.createReservation
  };
};

const mapDispatchToProps = dispatch => ({
  apiGuestsGetAll: () => dispatch(apiGuestsGetAll()),
  apiRoomsGetAvailable: (startDate, endDate) =>
    dispatch(apiRoomsGetAvailable(startDate, endDate)),
  apiReservationCreate: (startDate, endDate, guestId, roomId, discount) =>
    dispatch(
      apiReservationCreate(startDate, endDate, guestId, roomId, discount)
    ),
  apiReservationEdit: (id, startDate, endDate, guestId, roomId, discount) =>
    dispatch(
      apiReservationEdit(id, startDate, endDate, guestId, roomId, discount)
    )
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ReservationForm);
