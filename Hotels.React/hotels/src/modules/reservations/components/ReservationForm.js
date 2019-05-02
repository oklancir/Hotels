import React from "react";
import {connect} from "react-redux";

import {Form} from "react-bootstrap";

import {apiGuestsGetAll} from "../../guests/actions";
import {apiRoomsGetAvailable} from "../../rooms/actions";

class ReservationForm extends React.Component {
  state = {};

  onDateChange = name => ({target}) => {
    this.setState({[name]: target.value});

    const {startDate, endDate} = this.state;
    if (!!startDate && !!endDate) {
      // TODO: Stavi breakpoint i vidi zašto se ne događa!!
      apiRoomsGetAvailable(startDate, endDate);
    }
  };

  render() {
    const {availableRooms, guests} = this.props;
    return (
      <Form style={{textAlign: "left"}}>
        <Form.Group controlId="reservationGuestId">
          <Form.Label>Guest</Form.Label>
          <Form.Control as="select">
            {guests.map(guest => (
              <option value={guest.id}>{guest.firstName}</option>
            ))}
          </Form.Control>
        </Form.Group>
        <Form.Group controlId="reservationStartDate">
          <Form.Label>Start Date</Form.Label>
          <Form.Control onChange={this.onDateChange("startDate")} type="date" />
        </Form.Group>
        <Form.Group controlId="reservationEndDate">
          <Form.Label>End Date</Form.Label>
          <Form.Control onChange={this.onDateChange("endDate")} type="date" />
        </Form.Group>
        <Form.Group controlId="reservationRoomId">
          <Form.Label>Room</Form.Label>
          <Form.Control as="select">
            {availableRooms.map(room => (
              <option value={room.id}>{room.name}</option>
            ))}
          </Form.Control>
        </Form.Group>
      </Form>
    );
  }
}

const mapStateToProps = state => {
  return {
    guests: state.guests.data,
    availableRooms: state.rooms.availableRooms.data
  };
};

const mapDispatchToProps = dispatch => ({
  apiGuestsGetAll: () => dispatch(apiGuestsGetAll()),
  apiRoomsGetAvailable: (startDate, endDate) =>
    dispatch(apiRoomsGetAvailable(startDate, endDate))
});

export default connect(mapStateToProps, mapDispatchToProps)(ReservationForm);
