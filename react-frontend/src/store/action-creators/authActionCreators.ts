import { AuthActionTypes } from "../action-types/authActionTypes";
import { Dispatch } from "redux";
import { HttpClient } from "../action-apis/commonActionApi";
import { apiVersion, server } from "../../Appsettings";
import { AuthAction } from "../actions/authActions";
import { LoginForm, AuthLoggedInUser } from "../../types";

const backendBaseUrl = server;
const backendType = "api";

export const logInUser = (loginForm: LoginForm) => {
  return (dispatch: Dispatch<AuthAction>) => {
    new HttpClient()
      .post<AuthLoggedInUser | LoginForm>({
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

export const getAuthError = () => {
  return (dispatch: Dispatch<AuthAction>) => {
    dispatch({
      type: AuthActionTypes.GET_ERROR
    });
  };
};