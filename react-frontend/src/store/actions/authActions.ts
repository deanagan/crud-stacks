import { AuthLoggedInUser } from '../../types';
import { AuthActionTypes } from '../action-types/authActionTypes';




interface LogInAction {
    type: AuthActionTypes.LOG_IN;
    currentLoggedInUser: AuthLoggedInUser;
}

interface LogOutAction {
    type: AuthActionTypes.LOG_OUT;
}

interface SetErrorAction {
    type: AuthActionTypes.SET_ERROR;
    error: string;
}

interface GetErrorAction {
    type: AuthActionTypes.GET_ERROR;
}

export type AuthAction = LogInAction | LogOutAction | SetErrorAction | GetErrorAction;
