import React, { Component } from "react";

import { connect } from "react-redux";

import { roomDelete } from "../actions";

import { Button } from "react-bootstrap";

class Room extends Component {
    render() {
        const { id, name, roomType, onEditClick, onDeleteClick } = this.props;

        return (
            <tr>
                <td>{id}</td>
                <td>{name}</td>
                <td>{roomType}</td>
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
        rooms: state.rooms.data
    };
};

const mapDispatchToProps = dispatch => ({
    onDeleteClick: (e, id) => dispatch(roomDelete(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(Room);
