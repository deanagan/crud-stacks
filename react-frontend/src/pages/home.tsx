import styled from "styled-components";
import { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { ViewBox, Button } from "../design-system/atoms";
import { todoActionCreators, usersActionCreators, State } from "../store";
import { emptyGuid, uuidv4Type } from "../types";
import { Entry, Dropdown, ToggleSwitch, Table, ActionLink, Modal } from "../components";
import { AddEntryForm } from "../components/addentryform";

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

  const { addTodo, deleteTodo, updateTodoState, updateTodoAssignee } =
    bindActionCreators(todoActionCreators, dispatch);

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

  const concatName = (firstName: string, lastName: string) =>
    [firstName, lastName].join(" ");

  const onCancel = useCallback(() => {
    setShowAddModal(false);
    setNewDetail("");
    setNewSummary("");
  }, []);

  const addEntryForm = (<AddEntryForm {...addEntryFormProp} />);
  return (
    <Wrapper w={80} h={100}>
      {todos.length > 0 ? (
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
                currentEntry={
                  todo.assignee !== null
                    ? concatName(
                        todo.assignee?.firstName ?? "",
                        todo.assignee?.lastName ?? ""
                      )
                    : "Unassigned"
                }
                possibleEntries={[
                  {
                    uniqueId: emptyGuid,
                    name: "Unassigned",
                  },
                  ...users.map((u) => ({
                    uniqueId: u.uniqueId?.toString() as string,
                    name: concatName(u.firstName, u.lastName),
                  })),
                ]}
                onSelect={function (entry: Entry): void {
                  const entryGuidString =
                    entry.uniqueId?.toString() ?? emptyGuid;
                  updateTodoAssignee(
                    todo.uniqueId as uuidv4Type,
                    entryGuidString
                  );
                }}
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
      ) : <h1>No more todos!</h1>}
      <Button onClick={() => setShowAddModal(true)}>Add Todo</Button>
      <Modal
        onCancel={onCancel}
        onOk={() => {
          setShowAddModal(false);
          addTodo({ summary: newSummary, detail: newDetail, isDone: false });
        }}
        show={showAddModal}
        title="Add New Todo"
        okText="Ok"
        cancelText="Cancel"
        children={addEntryForm}
        showFooter={!!(newSummary && newDetail)}
        showClose
      />
      <Modal
        onCancel={() => cancelDeletion()}
        onOk={() => idForDeletion !== null && deleteTodo(idForDeletion)}
        show={showDeleteModal}
        title="Delete Todo"
        okText="Ok"
        cancelText="Cancel"
        children={<h4>Are you sure you want to delete this request?</h4>}
        showFooter
        showClose={false}
      />
    </Wrapper>
  );
};
