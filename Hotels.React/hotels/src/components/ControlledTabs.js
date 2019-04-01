import React, {Component} from "react";

import { Tabs, Tab } from "react-bootstrap";
import RoomList from "../modules/rooms/components/RoomList";
import GuestList from "../modules/guests/components/GuestList";
import ReservationList from "../modules/reservations/components/ReservationList";

class ControlledTabs extends Component {
    constructor(props, context) {
        super(props, context);
        this.state = {
            key: 'rooms',
        };
    }

    render() {
        return (
            <Tabs
                id="controlled-tab"
                activeKey={this.state.key}
                onSelect={key => this.setState({ key })}
            >
                <Tab eventKey="rooms" title="Rooms">
                    <RoomList/>
                </Tab>
                <Tab eventKey="guests" title="Guests">
                    <GuestList />
                </Tab>
                <Tab eventKey="reservations" title="Reservations">
                    <ReservationList />
                </Tab>
            </Tabs>
        );
    }
}

export default ControlledTabs;