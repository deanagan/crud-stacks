
import { TodoActionTypes } from '../action-types/todoActionTypes';
import { Todo } from '../../types';


interface AddTodoAction {
    type: TodoActionTypes.ADD_TODO_ENTRY;
    todo: Todo;
}

interface UpdateTodoAction {
    type: TodoActionTypes.UPDATE_TODO_STATE;
    id: number;
    isDone: boolean;
}

interface GetTodoEntriesAction {
    type: TodoActionTypes.GET_TODO_ENTRIES;
    todos: Todo[];
}

interface deleteTodoAction {
    type: TodoActionTypes.DELETE_TODO_ENTRY;
    id: number;
}

export type TodoAction = AddTodoAction | UpdateTodoAction | GetTodoEntriesAction | deleteTodoAction;
