import { UsersActionTypes } from "../action-types/usersActionTypes";
import { Dispatch } from "redux";
import { User } from "../../types";
import { HttpClient } from "../action-apis/commonActionApi";
import { apiVersion, server } from "../../Appsettings";
import { UsersAction } from "../actions/usersActions";

const backendBaseUrl = server;
const backendType = 'api';



export const getUsers = () => {
  return (dispatch: Dispatch<UsersAction>) => {
    new HttpClient().get<User[]>( {url: `${backendBaseUrl}/${apiVersion}/${backendType}/users`, requiresToken: false})
    .then((users) => {
      dispatch({
        type: UsersActionTypes.GET_ALL_USERS,
        users: users.map((user: User) => {
          return {
            uniqueId: user.uniqueId,
            userName: user.userName,
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email
          };
        }),
      });
    });
  };
};
