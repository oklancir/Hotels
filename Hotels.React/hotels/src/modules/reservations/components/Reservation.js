import React, { Component } from "react";

import { connect } from "react-redux";

import { apiReservationDelete } from "../actions";

import { Button } from "react-bootstrap";

class Reservation extends Component {
    render() {
        const { id, startDate, endDate, roomId, guestId, invoiceId, discount, onEditClick, onDeleteClick } = this.props;

        return (
            <tr>
                <td>{id}</td>
                <td>{startDate}</td>
                <td>{endDate}</td>
                <td>{roomId}</td>
                <td>{guestId}</td>
                <td>{invoiceId}</td>
                <td>{discount}</td>
                <td>
                    <Button disabled onClick={e => onEditClick(e, id)}>Edit</Button>
                    <Button onClick={e => onDeleteClick(e, id)}>Delete</Button>
                </td>
            </tr>
        );
    }
}

const mapStateToProps = state => {
    return {
        reservations: state.reservations.data
    };
};

const mapDispatchToProps = dispatch => ({
    onDeleteClick: (e, id) => dispatch(apiReservationDelete(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(Reservation);
