import { TodoActionTypes } from "../action-types/todoActionTypes";
import { TodoAction } from "../actions/todoActions";
import { TodoState } from "../../types";

const initialState: TodoState = {
    todos: [],
    errors: undefined,
    loading: false
  }

const reducer = (state: TodoState = initialState, action: TodoAction) => {
    switch (action.type) {
        case TodoActionTypes.ADD_TODO_ENTRY:
            return {
                ...state,
                todos: [...state.todos, action.todo]
            }
        case TodoActionTypes.UPDATE_TODO_STATE:
            return {
                ...state,
                todos: state.todos.map(e => e.id === action.id ?
                    { ...e, isDone: action.isDone } : e)
            }
        case TodoActionTypes.GET_TODO_ENTRIES:
            return {
                ...state,
                todos: action.todos
            }
        case TodoActionTypes.DELETE_TODO_ENTRY:
            return {
                ...state,
                todos: state.todos.filter(e => e.id !== action.id)
            }
        default:
            return state;
    }
}

export default reducer;