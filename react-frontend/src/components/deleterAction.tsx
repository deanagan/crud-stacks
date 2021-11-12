import { FC, useState } from "react";
import { Modal } from ".";
import { ActionLink } from "../design-system/molecules";
import { uuidv4Type } from "../types";
import { todoActionCreators } from "../store";
import { useDispatch } from "react-redux";
import { bindActionCreators } from "redux";
import React from "react";

export interface DeleterActionProp {
  uniqueId: uuidv4Type;
}

//export const DeleterAction: FC<DeleterActionProp> = ({ uniqueId }) => {
export const DeleterAction: FC<DeleterActionProp> = React.memo(({ uniqueId }) => {
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [idForDeletion, setIdForDeletion] = useState<uuidv4Type | null>(null);
  const dispatch = useDispatch();
  const { deleteTodo } = bindActionCreators(todoActionCreators, dispatch);

  const deleteEntry = (uniqueId: uuidv4Type) => {
    setIdForDeletion(uniqueId);
    setShowDeleteModal(true);
  };

  const cancelDeletion = () => {
    setIdForDeletion(null);
    setShowDeleteModal(false);
  };

  return (
    <>
      <ActionLink
        color="red"
        message="delete"
        deleteFn={() => deleteEntry(uniqueId)}
      />

      <Modal
        onCancel={() => cancelDeletion()}
        onOk={() => idForDeletion !== null && deleteTodo(idForDeletion)}
        show={showDeleteModal}
        title="Delete Todo"
        okText="Ok"
        cancelText="Cancel"
        children={<h4>Are you sure you want to delete this request?</h4>}
        showClose={false}
      />
    </>
  );
});
