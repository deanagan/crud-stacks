import { TodoActionTypes } from "../action-types/todoActionTypes";
import { TodoAction } from "../actions/todoActions";
import { TodoState } from "../../types";
import { Reducer } from "redux";

const initialState: TodoState = {
  todos: [],
  errors: undefined,
  loading: false,
};

const reducer: Reducer<TodoState, TodoAction>  = (state: TodoState = initialState, action: TodoAction) => {
  switch (action.type) {
    case TodoActionTypes.ADD_TODO_ENTRY:
      return {
        ...state,
        todos: [...state.todos, action.todo],
      };
    case TodoActionTypes.UPDATE_TODO_STATE:
      return {
        ...state,
        todos: state.todos.map((e) =>
          e.uniqueId === action.uniqueId ? { ...e, isDone: action.isDone } : e
        ),
      };
    case TodoActionTypes.GET_TODO_ENTRIES:
      return {
        ...state,
        todos: action.todos,
      };
    case TodoActionTypes.DELETE_TODO_ENTRY:
      return {
        ...state,
        todos: state.todos.filter((e) => e.uniqueId !== action.uniqueId),
      };
    case TodoActionTypes.UPDATE_TODO_ASSIGNEE:
      return {
        ...state,
        todos: state.todos.map((e) =>
          e.uniqueId === action.uniqueId
            ? {
                ...e,
                assignee:
                  action.assigneeGuid !== null
                    ? {
                        uniqueId: action.assigneeGuid,
                        firstName: action.firstName,
                        lastName: action.lastName,
                      }
                    : null,
              }
            : e
        )
      };
    default:
      return state;
  }
};

export default reducer;
