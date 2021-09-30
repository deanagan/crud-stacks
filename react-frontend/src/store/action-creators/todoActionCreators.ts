import { TodoActionTypes } from "../action-types/todoActionTypes";
import { Dispatch } from "redux";
import { Todo, uuidv4Type } from "../../types";
import { TodoAction } from "../actions/todoActions";
import { HttpClient } from "../action-apis/commonActionApi";
import { apiVersion, server } from "../../Appsettings";

const backendBaseUrl = server;
const backendType = 'api';

export const addTodo = (todo: Todo) => {
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().post<Todo>( {url: `${backendBaseUrl}/${apiVersion}/${backendType}/todos`, requiresToken: false, payload: todo})
      .then((todo) => {
        dispatch({
          type: TodoActionTypes.ADD_TODO_ENTRY,
          todo: { ...todo, uniqueId: todo.uniqueId }
        });
      })
      .catch((e) => {
        console.log(e);
      });
  };
};

export const updateTodoState = (uniqueId: uuidv4Type, isDone: boolean) => {
  const isDoneState = { isDone: isDone };
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().put<Todo | typeof isDoneState>({url: `${backendBaseUrl}/${apiVersion}/${backendType}/todos/${uniqueId}`, requiresToken: false, payload: isDoneState} )
    .then((data) => {
      const todo = data as Todo;
      dispatch({
        type: TodoActionTypes.UPDATE_TODO_STATE,
        uniqueId: todo.uniqueId as uuidv4Type,
        isDone: todo.isDone,
      });
    });
  };
};

export const deleteTodo = (uniqueId: uuidv4Type) => {
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().delete<Todo | uuidv4Type>({url: `${backendBaseUrl}/${apiVersion}/${backendType}/todos/${uniqueId}`, requiresToken: false, payload: uniqueId} )
    .then(() => {
      dispatch({
        type: TodoActionTypes.DELETE_TODO_ENTRY,
        uniqueId: uniqueId,
      });
    });
  };
};

export const getTodos = () => {
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().get<Todo[]>( {url: `${backendBaseUrl}/${apiVersion}/${backendType}/todos`, requiresToken: false})
    .then((todos) => {
      dispatch({
        type: TodoActionTypes.GET_TODO_ENTRIES,
        todos: todos.map((todo: Todo) => {
          return {
            uniqueId: todo.uniqueId,
            summary: todo.summary,
            detail: todo.detail,
            isDone: todo.isDone,
            assignee: todo.assignee
          };
        }),
      });
    });
  };
};
