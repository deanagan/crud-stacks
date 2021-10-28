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

interface DeleteTodoAction {
    type: TodoActionTypes.DELETE_TODO_ENTRY;
    uniqueId: uuidv4Type;
}

interface UpdateTodoAssignee {
    type: TodoActionTypes.UPDATE_TODO_ASSIGNEE;
    uniqueId: uuidv4Type;
    assigneeGuid: uuidv4Type | null;
    firstName: string;
    lastName: string;

}

export type TodoAction = AddTodoAction | UpdateTodoAction | GetTodoEntriesAction | DeleteTodoAction | UpdateTodoAssignee;
