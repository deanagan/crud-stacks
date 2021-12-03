import { AuthActionTypes } from "../action-types/authActionTypes";
import { Dispatch } from "redux";
import { HttpClient } from "../action-apis/commonActionApi";
import { apiVersion, server } from "../../Appsettings";
import { AuthAction } from "../actions/authActions";
import { AuthForm, AuthLoggedInUser } from "../../types";

const backendBaseUrl = server;
const backendType = "api";

export const logInUser = (authForm: AuthForm) => {
  return (dispatch: Dispatch<AuthAction>) => {
    new HttpClient()
      .post<AuthLoggedInUser | AuthForm>({
        url: `${backendBaseUrl}/${apiVersion}/${backendType}/Auth/login`,
        requiresToken: false,
        payload: authForm
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
      })
      .catch((data) => {
        console.log(data);
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
