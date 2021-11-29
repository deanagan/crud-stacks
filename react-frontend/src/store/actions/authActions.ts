import { AuthResponse } from '../../types';
import { AuthActionTypes } from '../action-types/authActionTypes';


interface GetAuthAction {
    type: AuthActionTypes.IS_LOGGED_ON;
    isLoggedIn: boolean;
}

interface LogInAction {
    type: AuthActionTypes.LOG_IN;
    authResponse: AuthResponse;
}

export type AuthAction = GetAuthAction | LogInAction;
