import React, { Component } from "react";

import { connect } from "react-redux";

class ConfirmDelete extends Component {
  render() {
    return (
      <div>
        <p>Are you sure you want to delete this {this.props.objectType}?</p>
        <button
          onClick={e => this.props.onConfirm(this.props.objectId)}
          className="btn btn-primary"
        >
          YES
        </button>
        <button onClick={this.props.onCancel} className="btn btn-default">
          NO
        </button>
      </div>
    );
  }
}

const mapStateToProps = (state, ownProps) => ({
  objectId: ownProps.objectId
});

const mapDispatchToProps = (dispatch, ownProps) => ({
  onCancel: () => dispatch(ownProps.cancelAction()),
  onConfirm: id => dispatch(ownProps.deleteAction(id))
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ConfirmDelete);
