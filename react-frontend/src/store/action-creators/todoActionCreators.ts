import { TodoActionTypes } from "../action-types/todoActionTypes";
import { Dispatch } from "redux";
import { Todo } from "../../types";
import { TodoAction } from "../actions/todoActions";
import { HttpClient } from "../action-apis/commonActionApi";

export const addTodo = (todo: Todo) => {
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().post<Todo>( {url: 'http://localhost:1337/todos', requiresToken: false, payload: todo})
      .then((todo) => {
        dispatch({
          type: TodoActionTypes.ADD_TODO_ENTRY,
          todo: { ...todo, id: todo.id }
        });
      })
      .catch((e) => {
        console.log(e);
      });
  };
};

export const updateTodoState = (id: number, isDone: boolean) => {
  const isDoneState = { isDone: isDone };
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().put<Todo | typeof isDoneState>({url: `http://localhost:1337/todos/${id}`, requiresToken: false, payload: isDoneState} )
    .then((data) => {
      const todo = data as Todo;
      dispatch({
        type: TodoActionTypes.UPDATE_TODO_STATE,
        id: todo.id as number,
        isDone: todo.isDone,
      });
    });
  };
};

export const deleteTodo = (id: number) => {
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().delete<Todo | number>({url: `http://localhost:1337/todos/${id}`, requiresToken: false, payload: id} )
    .then((data) => {
      const todo = data as Todo;
      dispatch({
        type: TodoActionTypes.DELETE_TODO_ENTRY,
        id: todo.id as number,
        isDone: todo.isDone,
      });
    });
  };
};

export const getTodos = () => {
  return (dispatch: Dispatch<TodoAction>) => {
    new HttpClient().get<Todo[]>( {url: 'http://localhost:1337/todos', requiresToken: false})
    .then((todos) => {
      dispatch({
        type: TodoActionTypes.GET_TODO_ENTRIES,
        todos: todos.map((todo: Todo) => {
          return {
            id: todo.id,
            name: todo.name,
            detail: todo.detail,
            isDone: todo.isDone,
          };
        }),
      });
    });
  };
};