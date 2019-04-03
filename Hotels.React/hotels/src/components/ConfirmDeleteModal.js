import React from "react";

import { Modal, Button } from "react-bootstrap";

import ConfirmDelete from "./ConfirmDelete";

class ConfirmDeleteModal extends React.Component {
    render() {
        return (
            <Modal
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        {this.props.objectType}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ConfirmDelete />
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={this.props.onHide}>Close</Button>
                </Modal.Footer>
            </Modal>
        );
    }
}

export default ConfirmDeleteModal;