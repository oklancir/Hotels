import React, { Component } from "react";

import { connect } from "react-redux";

import { reservationDelete } from "../actions";

import { Button, ButtonToolbar } from "react-bootstrap";

class Reservation extends Component {
    render() {
        const { id, startDate, endDate, roomId, guestId, invoiceId, reservationStatusId, discount, onEditClick, onDeleteClick } = this.props;

        return (
            <tr>
                <td>{id}</td>
                <td>{startDate}</td>
                <td>{endDate}</td>
                <td>{roomId}</td>
                <td>{guestId}</td>
                <td>{invoiceId}</td>
                <td>{reservationStatusId}</td>
                <td>{discount}</td>
                <td>
                    <ButtonToolbar>
                        <Button size="sm" style={{ marginRight: 8 }} disabled onClick={e => onEditClick(e, id)}>Edit</Button>
                        <Button variant="outline-danger" size="sm" onClick={e => onDeleteClick(e, id)}>Delete</Button>
                    </ButtonToolbar>
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
    onDeleteClick: (e, id) => dispatch(reservationDelete(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(Reservation);