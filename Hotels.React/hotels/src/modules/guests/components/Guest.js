import React, { Component } from "react";

import { connect } from "react-redux";

import { apiGuestDelete } from "../actions";

import { Button } from "react-bootstrap";

class Guest extends Component {
    render() {
        const { id, firstName, lastName, address, email, phoneNumber, onEditClick, onDeleteClick } = this.props;

        return (
            <tr>
                <td>{id}</td>
                <td>{firstName}</td>
                <td>{lastName}</td>
                <td>{address}</td>
                <td>{email}</td>
                <td>{phoneNumber}</td>
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
        guests: state.guests.data
    };
};

const mapDispatchToProps = dispatch => ({
    onDeleteClick: (e, id) => dispatch(apiGuestDelete(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(Guest);
