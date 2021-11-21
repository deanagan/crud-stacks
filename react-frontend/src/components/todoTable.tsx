import { memo, FC, useEffect } from "react";
import { useDispatch } from "react-redux";
import { Table } from "../design-system/molecules";
import { todoActionCreators } from "../store";
import { Todo } from "../types";


const columnLabels = [
  "Summary",
  "Detail",
  "Completed",
  "Status",
  "Delete",
  "Assignee",
];

interface TodoTableProp {
  todos: Todo[];
}

export const TodoTable: FC<TodoTableProp> = memo(({ todos }) => {
  //export const TodoTable: FC = (() => { // using this line will cause the table to re-render
  const dispatch = useDispatch();

  const nTodos = todos.length;

  useEffect(() => {
    dispatch(todoActionCreators.getTodos());
  }, [dispatch]);


  return nTodos > 0 ? (
    <Table
      tableData={todos}
      columnLabels={
        columnLabels /*["Summary", "Detail", "Completed", "Status", "Delete", "Assignee"]*/
      }
    />
  ) : (
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
    <h1>No todos!</h1>
  );
});
