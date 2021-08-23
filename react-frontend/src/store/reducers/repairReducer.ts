import { RepairActionTypes } from "../action-types/repairActionTypes";
import { RepairAction } from "../actions/repairActions";
import { RepairState } from "../../types";

const initialState: RepairState = {
    repairs: [],
    errors: undefined,
    loading: false
  }

const reducer = (state: RepairState = initialState, action: RepairAction) => {
    switch (action.type) {
        case RepairActionTypes.ADD_REPAIR_ENTRY:
            return {
                ...state,
                repairs: [...state.repairs, action.repair]
            }
        case RepairActionTypes.UPDATE_REPAIR_STATE:
            return {
                ...state,
                repairs: state.repairs.map(e => e.id === action.id ?
                    { ...e, fixed: action.fixed } : e)
            }
        case RepairActionTypes.GET_REPAIR_ENTRIES:
            return {
                ...state,
                repairs: action.repairs
            }
        case RepairActionTypes.DELETE_REPAIR_ENTRY:
            return {
                ...state,
                repairs: state.repairs.filter(e => e.id !== action.id)
            }
        default:
            return state;
    }
}

export default reducer;