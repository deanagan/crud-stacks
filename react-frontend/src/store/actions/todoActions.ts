import { TodoActionTypes } from '../action-types/todoActionTypes';
import { Todo, uuidv4Type } from '../../types';


interface AddTodoAction {
    type: TodoActionTypes.ADD_TODO_ENTRY;
    todo: Todo;
}

interface UpdateTodoAction {
    type: TodoActionTypes.UPDATE_TODO_STATE;
    uniqueId: uuidv4Type;
    isDone: boolean;
}

interface GetTodoEntriesAction {
    type: TodoActionTypes.GET_TODO_ENTRIES;
    todos: Todo[];
}

interface deleteTodoAction {
    type: TodoActionTypes.DELETE_TODO_ENTRY;
    uniqueId: uuidv4Type;
}

export type TodoAction = AddTodoAction | UpdateTodoAction | GetTodoEntriesAction | deleteTodoAction;
