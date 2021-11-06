// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { memo, FC, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { ActionLink, Dropdown, Table, ToggleSwitch } from "../design-system/molecules";
import { State, todoActionCreators } from "../store";
import { emptyGuid, uuidv4Type } from "../types";

export const TodoTable: FC = memo(() => {
  //export const TodoTable: FC = (() => { // using this line will cause the table to re-render
  const dispatch = useDispatch();
  const { updateTodoState } = bindActionCreators(todoActionCreators, dispatch);
  const { todos } = useSelector((state: State) => state.todo);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [idForDeletion, setIdForDeletion] = useState<uuidv4Type | null>(null);
  const nTodos = todos.length;

  const deleteEntry = (uniqueId: uuidv4Type) => {
    setIdForDeletion(uniqueId);
    setShowDeleteModal(true);
  };

  return nTodos > 0 ? (
    <Table
      rowData={todos.map((todo) => ({
        id: todo.uniqueId,
        summary: todo.summary,
        detail: todo.detail,
        isDone: todo.isDone ? "True" : "False",
        switch: (
            <ToggleSwitch
              switchUniqueId={todo.uniqueId as uuidv4Type}
              initialState={todo.isDone}
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
      }))}
      columnLabels={["Summary", "Detail", "Completed", "Status"]}
      rowFields={["summary", "detail", "isDone", "switch"]}
    />
  ) : (
    <h1>No todos!</h1>
  );
});
