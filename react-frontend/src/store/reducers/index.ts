import { combineReducers } from "redux";
import repairReducer from "./repairReducer";

const reducers = combineReducers({
    repair: repairReducer
});

export default reducers;

export type State = ReturnType<typeof reducers>;