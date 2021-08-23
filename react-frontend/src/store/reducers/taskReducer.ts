import { TaskActionTypes } from "../action-types/TaskActionTypes";
import { TaskAction } from "../actions/taskActions";
import { TaskState } from "../../types";

const initialState: TaskState = {
    tasks: [],
    errors: undefined,
    loading: false
  }

const reducer = (state: TaskState = initialState, action: TaskAction) => {
    switch (action.type) {
        case TaskActionTypes.ADD_TASK_ENTRY:
            return {
                ...state,
                tasks: [...state.tasks, action.task]
            }
        case TaskActionTypes.UPDATE_TASK_STATE:
            return {
                ...state,
                tasks: state.tasks.map(e => e.id === action.id ?
                    { ...e, fixed: action.fixed } : e)
            }
        case TaskActionTypes.GET_TASK_ENTRIES:
            return {
                ...state,
                tasks: action.tasks
            }
        case TaskActionTypes.DELETE_TASK_ENTRY:
            return {
                ...state,
                tasks: state.tasks.filter(e => e.id !== action.id)
            }
        default:
            return state;
    }
}

export default reducer;