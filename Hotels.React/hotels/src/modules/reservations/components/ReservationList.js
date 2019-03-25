import React, { Component } from "react";
import { connect } from "react-redux";

import { Table } from "react-bootstrap";

import { apiReservationsGetAll } from "../actions";

import Room from "./Room";

class ReservationList extends Component {
    componentDidMount() {
        const { apiRoomsGetAll } = this.props;
        apiRoomsGetAll();
    }

    render() {
        const { reservations } = this.props;

        return (
            <React.Fragment>
                <h1>Reservation List</h1>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>Status</th>
                            <th>Status</th>
                            <th>Discount</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {reservations &&
                            reservations.map(reservation => (
                                <Room
                                    key={reservation.id}
                                    id={reservation.id}
                                    startDate={reservation.startDate}
                                    endDate={reservation.endDate}
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
