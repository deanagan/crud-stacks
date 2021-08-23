import { TaskActionTypes } from "../action-types/TaskActionTypes";
import { Dispatch } from "redux";
import { Task } from "../../types";
import { TaskAction } from "../actions/taskActions";
import { HttpClient } from "../action-apis/commonActionApi";

export const addTask = (task: Task) => {
  return (dispatch: Dispatch<TaskAction>) => {
    new HttpClient().post<Task>( {url: 'http://localhost:1337/tasks', requiresToken: false, payload: task})
      .then((task) => {
        dispatch({
          type: TaskActionTypes.ADD_TASK_ENTRY,
          task: { ...task, id: task.id }
        });
      })
      .catch((e) => {
        console.log(e);
      });
  };
};

export const updateTaskState = (id: number, fixed: boolean) => {
  const fixedState = { fixed: fixed };
  return (dispatch: Dispatch<TaskAction>) => {
    new HttpClient().put<Task | typeof fixedState>({url: `http://localhost:1337/tasks/${id}`, requiresToken: false, payload: fixedState} )
    .then((data) => {
      const task = data as Task;
      dispatch({
        type: TaskActionTypes.UPDATE_TASK_STATE,
        id: task.id as number,
        fixed: task.fixed,
      });
    });
  };
};

export const deleteTask = (id: number) => {
  return (dispatch: Dispatch<TaskAction>) => {
    new HttpClient().delete<Task | number>({url: `http://localhost:1337/tasks/${id}`, requiresToken: false, payload: id} )
    .then((data) => {
      const task = data as Task;
      dispatch({
        type: TaskActionTypes.DELETE_TASK_ENTRY,
        id: task.id as number,
        fixed: task.fixed,
      });
    });
  };
};

export const getTasks = () => {
  return (dispatch: Dispatch<TaskAction>) => {
    new HttpClient().get<Task[]>( {url: 'http://localhost:1337/tasks', requiresToken: false})
    .then((tasks) => {
      dispatch({
        type: TaskActionTypes.GET_TASK_ENTRIES,
        tasks: tasks.map((task: Task) => {
          return {
            id: task.id,
            name: task.name,
            detail: task.detail,
            fixed: task.fixed,
          };
        }),
      });
    });
  };
};
