import { combineReducers } from "redux";
import todoReducer from "./todoReducer";
import userReducer from "./userReducer";
import authReducer from "./authReducer";

const reducers = combineReducers({
    todo: todoReducer,
    user: userReducer,
    auth: authReducer,
});

export default reducers;

export type State = ReturnType<typeof reducers>;