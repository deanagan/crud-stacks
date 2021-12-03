import { AuthActionTypes } from "../action-types/authActionTypes";
import { AuthAction } from "../actions/authActions";
import { AuthState } from "../../types";
import { Reducer } from "redux";

const initialState: AuthState = {
    errors: undefined,
    loading: false,
    currentLoggedInUser: undefined
  }

const reducer: Reducer<AuthState, AuthAction> = (state: AuthState = initialState, action: AuthAction) => {
    switch (action.type) {
        case AuthActionTypes.LOG_IN:
            return {
                ...state,
                currentLoggedInUser: action.currentLoggedInUser
            }
        case AuthActionTypes.LOG_OUT:
            return {
                ...state,
                currentLoggedInUser: {
                    email: '',
                    role: '',
                    token: '',
                    userName: ''
                }
            }
        default:
            return state;
    }
}

export default reducer;