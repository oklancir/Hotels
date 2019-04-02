import React, { Component } from "react";
import { connect } from "react-redux";

import { Table } from "react-bootstrap";

import { apiGuestsGetAll, apiGuestDelete, guestDeleteCancel } from "../actions";

import Guest from "./Guest";
import ConfirmDelete from "../../../components/ConfirmDelete";

class GuestList extends Component {
    componentDidMount() {
        const { apiGuestsGetAll } = this.props;
        apiGuestsGetAll();
    }

    render() {
        const { guests, guestToDelete } = this.props;

        return (
            <React.Fragment>
                {Boolean(guestToDelete) && (
                    <ConfirmDelete
                        objectType="guest"
                        objectId={guestToDelete}
                        cancelAction={guestDeleteCancel}
                        deleteAction={apiGuestDelete}
                    />
                )}
                <h1>Guest List</h1>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Address</th>
                            <th>Email</th>
                            <th>Phone Number</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {guests &&
                            guests.map(guest => (
                                <Guest
                                    key={guest.id}
                                    id={guest.id}
                                    firstName={guest.firstName}
                                    lastName={guest.lastName}
                                    address={guest.address}
                                    email={guest.email}
                                    phoneNumber={guest.phoneNumber}
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
        guestToDelete: state.guests.guestToDelete,
        guests: state.guests.data
    };
};

const mapDispatchToProps = dispatch => ({
    apiGuestsGetAll: () => dispatch(apiGuestsGetAll())
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(GuestList);
