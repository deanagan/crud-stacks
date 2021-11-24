import { AuthActionTypes } from "../action-types/authActionTypes";
import { AuthAction } from "../actions/authActions";
import { AuthState } from "../../types";
import { Reducer } from "redux";

const initialState: AuthState = {
    isLoggedIn: false,
    errors: undefined,
    loading: false,
  }

const reducer: Reducer<AuthState, AuthAction> = (state: AuthState = initialState, action: AuthAction) => {
    switch (action.type) {
        case AuthActionTypes.IS_LOGGED_ON:
            return {
                ...state,
                isLoggedOn: action.isLoggedOn
            }

        default:
            return state;
    }
}

export default reducer;