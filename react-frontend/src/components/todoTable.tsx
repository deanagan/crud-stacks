// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { memo, FC, useEffect, useCallback, useRef, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { DeleterAction } from ".";
import { Table, ToggleSwitch } from "../design-system/molecules";
import { State, todoActionCreators } from "../store";
import { emptyGuid, Todo, uuidv4Type } from "../types";
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
  // This is added so our toggle switch doesn't re-render
  // why useRef? because we need to pass updateTodoState in our dependency list if we
  // use it in the callback
  const callback = useRef<typeof updateTodoState>(updateTodoState);
  const updateSwitchState = useCallback((id, isDone) => callback.current(id, isDone), []);
  // we use this to prevent header from re-rendering since it doesn't change at all
  const columnLabels = useMemo(() => ["Summary", "Detail", "Completed", "Status", "Delete", "Assignee"], []);

  const todoHandler = useCallback((todo: Todo) => {
    return {
      id: todo.uniqueId,
      summary: todo.summary,
      detail: todo.detail,
      isDone: todo.isDone ? "True" : "False",
      switch: (
          <ToggleSwitch
            switchUniqueId={todo.uniqueId as uuidv4Type}
            initialState={todo.isDone}
            updateSwitchStage={updateSwitchState/*updateTodoState*/}
          />
      ),
      deleter: (<DeleterAction uniqueId = {todo.uniqueId as uuidv4Type}/>),
      assignee: (<AssigneeDropDown assigneeUniqueId={todo.assignee?.uniqueId ?? emptyGuid} todoUniqueId={todo.uniqueId as uuidv4Type} />)
    }
  }, [updateSwitchState]);

  return nTodos > 0 ? (
    <Table
      rowData={todos.map(todoHandler)}
      columnLabels={columnLabels/*["Summary", "Detail", "Completed", "Status", "Delete", "Assignee"]*/}
      rowFields={["summary", "detail", "isDone", "switch", "deleter", "assignee"]}
    />
    // <Table
    //   rowData={todos.map((todo) => ({
    //     id: todo.uniqueId,
    //     summary: todo.summary,
    //     detail: todo.detail,
    //     isDone: todo.isDone ? "True" : "False",
    //     switch: (
    //         <ToggleSwitch
    //           switchUniqueId={todo.uniqueId as uuidv4Type}
    //           initialState={todo.isDone}
    //           updateSwitchStage={updateSwitchState/*updateTodoState*/}
    //         />
    //     ),
    //     deleter: (<DeleterAction uniqueId = {todo.uniqueId as uuidv4Type}/>),
    //     assignee: (<AssigneeDropDown assigneeUniqueId={todo.assignee?.uniqueId ?? emptyGuid} todoUniqueId={todo.uniqueId as uuidv4Type} />)
    //   }))}
    //   columnLabels={columnLabels/*["Summary", "Detail", "Completed", "Status", "Delete", "Assignee"]*/}
    //   rowFields={["summary", "detail", "isDone", "switch", "deleter", "assignee"]}
    // />
  ) : (
    <h1>No todos!</h1>
  );
});
