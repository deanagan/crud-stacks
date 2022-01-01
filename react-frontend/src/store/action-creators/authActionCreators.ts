import { AuthActionTypes } from "../action-types/authActionTypes";
import { Dispatch } from "redux";
import { HttpClient } from "../action-apis/commonActionApi";
import { apiVersion, server } from "../../Appsettings";
import { AuthAction } from "../actions/authActions";
import { LoginEntryForm, AuthLoggedInUser, SignUpForm } from "../../types";

const backendBaseUrl = server;
const backendType = "api";

export const logInUser = (loginForm: LoginEntryForm) => {
  return (dispatch: Dispatch<AuthAction>) => {
    new HttpClient()
      .post<AuthLoggedInUser | LoginEntryForm>({
        url: `${backendBaseUrl}/${apiVersion}/${backendType}/Auth/login`,
        requiresToken: false,
        payload: loginForm
      })
      .then((data) => {
        const response = data as AuthLoggedInUser;
        dispatch({
          type: AuthActionTypes.LOG_IN,
          currentLoggedInUser: {
            userName: response.userName,
            email: response.email,
            role: response.role,
            token: response.token
          }
        });

        dispatch({
          type: AuthActionTypes.SET_ERROR,
          error: ''
        })
      })
      .catch(() => {
        dispatch({
          type: AuthActionTypes.SET_ERROR,
          error: 'Invalid Credentials'
        });
      });
  };
};


export const logOutUser = () => {
  return (dispatch: Dispatch<AuthAction>) => {
    dispatch({
      type: AuthActionTypes.LOG_OUT,
      currentLoggedInUser: {
        userName: '',
        email: '',
        role: '',
        token: '',
      }
    });
  };
};

export const setLoggedInUser = (email: string, token: string) => {
  return (dispatch: Dispatch<AuthAction>) => {
    dispatch({
      type: AuthActionTypes.LOG_IN,
      currentLoggedInUser: {
        userName: '', // TODO
        email: email,
        role: '', // TODO
        token: token
      }
    });
  };
};

export const getAuthError = () => {
  return (dispatch: Dispatch<AuthAction>) => {
    dispatch({
      type: AuthActionTypes.GET_ERROR
    });
  };
};

export const signUpUser = (signUpForm: SignUpForm) => {
  return (dispatch: Dispatch<AuthAction>) => {
    new HttpClient()
      .post<AuthLoggedInUser | SignUpForm>({
        url: `${backendBaseUrl}/${apiVersion}/${backendType}/Auth/login`,
        requiresToken: false,
        payload: signUpForm
      })
      .then((data) => {
        const response = data as AuthLoggedInUser;
        dispatch({
          type: AuthActionTypes.LOG_IN,
          currentLoggedInUser: {
            userName: response.userName,
            email: response.email,
            role: response.role,
            token: response.token
          }
        });

        dispatch({
          type: AuthActionTypes.SET_ERROR,
          error: ''
        })
      })
      .catch(() => {
        dispatch({
          type: AuthActionTypes.SET_ERROR,
          error: 'Invalid Credentials'
        });
      });
  };
};
