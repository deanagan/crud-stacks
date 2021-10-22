import { UsersActionTypes } from '../action-types/usersActionTypes';
import { User } from '../../types';



interface GetUsersAction {
    type: UsersActionTypes.GET_ALL_USERS;
    todos: User[];
}


export type UsersAction = GetUsersAction;
