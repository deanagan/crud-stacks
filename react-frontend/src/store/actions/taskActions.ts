
import { TaskActionTypes } from '../action-types/TaskActionTypes';
import { Task } from '../../types';


interface AddTaskAction {
    type: TaskActionTypes.ADD_TASK_ENTRY;
    task: Task;
}

interface UpdateTaskAction {
    type: TaskActionTypes.UPDATE_TASK_STATE;
    id: number;
    fixed: boolean;
}

interface GetTaskEntriesAction {
    type: TaskActionTypes.GET_TASK_ENTRIES;
    tasks: Task[];
}

interface deleteTaskAction {
    type: TaskActionTypes.DELETE_TASK_ENTRY;
    id: number;
}

export type TaskAction = AddTaskAction | UpdateTaskAction | GetTaskEntriesAction | deleteTaskAction;
