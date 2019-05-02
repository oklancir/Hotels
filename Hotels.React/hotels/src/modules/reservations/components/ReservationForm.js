import React from "react";
import {Form} from "react-bootstrap";

class ReservationForm extends React.Component {
  state = {};

  render() {
    return (
      <Form style={{textAlign: "left"}}>
        <Form.Group controlId="reservationGuestId">
          <Form.Label>Guest</Form.Label>
          <Form.Control type="text" />
        </Form.Group>
        <Form.Group controlId="reservationStartDate">
          <Form.Label>Start Date</Form.Label>
          <Form.Control type="date" />
        </Form.Group>
        <Form.Group controlId="reservationEndDate">
          <Form.Label>End Date</Form.Label>
          <Form.Control type="date" />
        </Form.Group>
        <Form.Group controlId="reservationRoomId">
          <Form.Label>Room</Form.Label>
          <Form.Control type="text" />
        </Form.Group>
      </Form>
    );
  }
}

export default ReservationForm;
