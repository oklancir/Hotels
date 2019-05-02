import React from "react";

import { Modal } from "react-bootstrap";

import ConfirmDelete from "./ConfirmDelete";

class ConfirmDeleteModal extends React.Component {
    render() {
        const {
            cancelAction,
            deleteAction,
            objectId,
            objectType,
            onHide,
            ...otherProps
        } = this.props;

        return (
            <Modal
                {...otherProps}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered
            >
                <Modal.Header>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Confirm Action
                    </Modal.Title>
                </Modal.Header>
                <ConfirmDelete
                    objectType={objectType}
                    objectId={objectId}
                    cancelAction={cancelAction}
                    deleteAction={deleteAction} />
            </Modal>
        );
    }
}

export default ConfirmDeleteModal;