import React, {Component} from "react";
import {connect} from "react-redux";

import {Table} from "react-bootstrap";

import {
  apiReservationsGetAll,
  apiReservationDelete,
  reservationDeleteCancel
} from "../actions";

import Reservation from "./Reservation";
import ReservationForm from "./ReservationForm";
import ConfirmDeleteModal from "../../../components/ConfirmDeleteModal";

class ReservationList extends Component {
  componentDidMount() {
    const {apiReservationsGetAll} = this.props;
    apiReservationsGetAll();
  }

  render() {
    const {reservations, reservationToDelete} = this.props;

    return (
      <React.Fragment>
        <ReservationForm />
        <ConfirmDeleteModal
          show={Boolean(reservationToDelete)}
          objectType="reservation"
          objectId={reservationToDelete}
          cancelAction={reservationDeleteCancel}
          deleteAction={apiReservationDelete}
        />
        <h1>Reservation List</h1>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Id</th>
              <th>Start Date</th>
              <th>End Date</th>
              <th>Status</th>
              <th>Room</th>
              <th>Guest</th>
              <th>Invoice</th>
              <th>Discount</th>
              <th />
            </tr>
          </thead>
          <tbody>
            {reservations &&
              reservations.map(reservation => (
                <Reservation
                  key={reservation.id}
                  id={reservation.id}
                  startDate={reservation.startDate}
                  endDate={reservation.endDate}
                  reservationStatusId={reservation.reservationStatusId}
                  roomId={reservation.roomId}
                  guestId={reservation.guestId}
                  invoiceId={reservation.invoiceId}
                  discount={reservation.discount}
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
    reservationToDelete: state.reservations.reservationToDelete,
    reservations: state.reservations.data
  };
};

const mapDispatchToProps = dispatch => ({
  apiReservationsGetAll: () => dispatch(apiReservationsGetAll())
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ReservationList);
