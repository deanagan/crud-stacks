// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { memo, FC, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { DeleterAction } from ".";
import { Table, ToggleSwitch } from "../design-system/molecules";
import { State, todoActionCreators } from "../store";
import { uuidv4Type } from "../types";
import { AssigneeDropDown } from "./assigneeDropDown";


export const TodoTable: FC = memo(() => {
  //export const TodoTable: FC = (() => { // using this line will cause the table to re-render
  const dispatch = useDispatch();
  const { updateTodoState } = bindActionCreators(todoActionCreators, dispatch);
  const { todos } = useSelector((state: State) => state.todo);
  const nTodos = todos.length;

  useEffect(() => {
    dispatch(todoActionCreators.getTodos());
  }, [dispatch]);



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
        deleter: (<DeleterAction uniqueId = {todo.uniqueId as uuidv4Type}/>),
        assignee: (<AssigneeDropDown uniqueId={todo.uniqueId as uuidv4Type} />)
      }))}
      columnLabels={["Summary", "Detail", "Completed", "Status", "Delete", "Assignee"]}
      rowFields={["summary", "detail", "isDone", "switch", "deleter", "assignee"]}
    />
  ) : (
    <h1>No todos!</h1>
  );
});
