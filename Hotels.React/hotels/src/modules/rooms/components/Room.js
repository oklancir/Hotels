import React, { Component } from "react";

import room from "./Room.css";

class Room extends Component {
    render() {
        return (
            <div className={room}>
                <p onClick={this.props.click}>
                    Room: {this.props.name} RoomType: {this.props.roomType}
                </p>
                <p
                    type="text"
                    value={this.props.id} />
                <p
                    type="text"
                    value={this.props.name} />
                <p
                    type="text"
                    value={this.props.availability} />
                <p
                    type="text"
                    value={this.props.roomType} />
            </div>
        )
    }
};

export default Room;