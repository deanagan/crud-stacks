import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import styled from "styled-components";
import { ActionLink } from "../components/ActionLink";
import { ViewBox, Button } from "../design-system/atoms";
import { todoActionCreators, usersActionCreators, State } from "../store";
import { ToggleSwitch } from "../components/ToggleSwitch";
import { Table } from "../components/Table";
import { Modal } from "../components/Modal";
import { AddEntryForm } from "../components/AddEntryForm";
import { uuidv4Type } from "../types";
import { Entry, Dropdown } from "../components";

const Wrapper = styled(ViewBox)`
  justify-content: center;
  background-color: ${({ theme }) => theme.Colors.white};
  margin-top: 55px;
`;

export const Home = () => {
  const dispatch = useDispatch();
  const { todos } = useSelector((state: State) => state.todo);
  const { users } = useSelector((state: State) => state.user);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [idForDeletion, setIdForDeletion] = useState<uuidv4Type | null>(null);

  const [newSummary, setNewSummary] = useState("");
  const [newDetail, setNewDetail] = useState("");

  const { addTodo, deleteTodo, updateTodoState, updateTodoAssignee } = bindActionCreators(
    todoActionCreators,
    dispatch
  );

  const deleteEntry = (uniqueId: uuidv4Type) => {
    setIdForDeletion(uniqueId);
    setShowDeleteModal(true);
  };

  const cancelDeletion = () => {
    setIdForDeletion(null);
    setShowDeleteModal(false);
  };

  const addEntryFormProp = {
    summary: newSummary,
    detail: newDetail,
    changeSummary: setNewSummary,
    changeDetail: setNewDetail,
  };

  useEffect(() => {
    dispatch(todoActionCreators.getTodos());
    dispatch(usersActionCreators.getUsers());
  }, [dispatch]);

  const concatName = (firstName: string, lastName: string) => [firstName, lastName].join(" ")

  return (
    <Wrapper w={80} h={100}>
      <Table
        rowData={todos.map((todo) => ({
          id: todo.uniqueId,
          summary: todo.summary,
          detail: todo.detail,
          isDone: todo.isDone ? "True" : "False",
          switch: (
            <ToggleSwitch
              switchUniqueId={todo.uniqueId as uuidv4Type}
              isDone={todo.isDone}
              updateSwitchStage={updateTodoState}
            />
          ),
          deleter: (
            <ActionLink
              color="red"
              message="delete"
              deleteFn={() => deleteEntry(todo.uniqueId as uuidv4Type)}
            />
          ),
          assignee: (
            <Dropdown
              itemUniqueId={todo.uniqueId as uuidv4Type}
              currentEntry={ !!todo.assignee ? concatName(todo.assignee?.firstName, todo.assignee?.lastName) : 'Unassigned' }
              possibleEntries={users.map((u) => ({
                uniqueId: u.uniqueId as uuidv4Type,
                name: concatName(u.firstName, u.lastName)
              }))}

              onSelect={function (entry: Entry): void {
                const [firstName, lastName] = entry.name.split(' ');
                updateTodoAssignee(todo.uniqueId as uuidv4Type, entry.uniqueId as uuidv4Type, firstName, lastName);
              } }
              />
          ),
        }))}
        columnLabels={[
          "Summary",
          "Detail",
          "Completed",
          "Update",
          "Remove Todo",
          "Assignee",
        ]}
        rowFields={[
          "summary",
          "detail",
          "isDone",
          "switch",
          "deleter",
          "assignee",
        ]}
      />

      <Button onClick={() => setShowAddModal(true)}>Add Request</Button>
      <Modal
        onCancel={() => {
          setShowAddModal(false);
          setNewDetail("");
          setNewSummary("");
        }}
        onOk={() => {
          setShowAddModal(false);
          addTodo({ summary: newSummary, detail: newDetail, isDone: false });
        }}
        show={showAddModal}
        title="Add New Request"
        okText="Ok"
        cancelText="Cancel"
        children={<AddEntryForm {...addEntryFormProp} />}
        showFooter={!!(newSummary && newDetail)}
        showClose
      />
      <Modal
        onCancel={() => cancelDeletion()}
        onOk={() => idForDeletion !== null && deleteTodo(idForDeletion)}
        show={showDeleteModal}
        title="Delete Request"
        okText="Ok"
        cancelText="Cancel"
        children={<h4>Are you sure you want to delete this request?</h4>}
        showFooter
        showClose={false}
      />
    </Wrapper>
  );
};
