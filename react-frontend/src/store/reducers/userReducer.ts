import { UsersActionTypes } from "../action-types/usersActionTypes";
import { UsersAction } from "../actions/usersActions";
import { UserState } from "../../types";

const initialState: UserState = {
    users: [],
    errors: undefined,
    loading: false
  }

const reducer = (state: UserState = initialState, action: UsersAction) => {
    switch (action.type) {
        case UsersActionTypes.GET_ALL_USERS:
            return {
                ...state,
                users: action.users
            }

        default:
            return state;
    }
}

export default reducer;