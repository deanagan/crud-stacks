import { AuthActionTypes } from '../action-types/authActionTypes';


interface GetAuthAction {
    type: AuthActionTypes.IS_LOGGED_ON;
    isLoggedIn: boolean;
}


export type AuthAction = GetAuthAction;
