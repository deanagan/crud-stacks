import { AuthActionTypes } from "../action-types/authActionTypes";
import { AuthAction } from "../actions/authActions";
import { AuthState } from "../../types";
import { Reducer } from "redux";
import { StorageTypes } from "../../constants";

const initialState: AuthState = {
    error: undefined,
    loading: false,
    loggedIn: !!localStorage.getItem(StorageTypes.TOKEN),
    currentLoggedInUser: {
        userName: '', //TODO: localStorage.getItem();
        email: localStorage.getItem(StorageTypes.EMAIL) ?? '',
        role: '', //localStorage.
        token: localStorage.getItem(StorageTypes.TOKEN) ?? ''
    }
  }

const reducer: Reducer<AuthState, AuthAction> = (state: AuthState = initialState, action: AuthAction) => {
    switch (action.type) {
        case AuthActionTypes.LOG_IN:
            return {
                ...state,
                loggedIn: true,
                currentLoggedInUser: action.currentLoggedInUser
            }
        case AuthActionTypes.LOG_OUT:
            return {
                ...state,
                loggedIn: false,
                currentLoggedInUser: {
                    email: '',
                    role: '',
                    token: '',
                    userName: ''
                }
            }
        case AuthActionTypes.SET_ERROR:
            return {
                ...state,
                error: action.error
            }
        case AuthActionTypes.GET_ERROR:
        default:
            return state;
    }
}

export default reducer;