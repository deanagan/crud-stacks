import { FC, useEffect, useState } from "react";
import { Dropdown, Entry } from "../design-system/molecules";
import { emptyGuid, User, uuidv4Type } from "../types";
import { State, todoActionCreators, usersActionCreators } from "../store";
import { useDispatch, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import React from "react";

export interface AssigneeDropDownProp {
  assigneeUniqueId: uuidv4Type | string;
  todoUniqueId: uuidv4Type;
}

interface UserEntries extends Array<Entry>{};

export const AssigneeDropDown: FC<AssigneeDropDownProp> = React.memo(({ assigneeUniqueId, todoUniqueId }) => {
//export const AssigneeDropDown: FC<AssigneeDropDownProp> = ({ assigneeUniqueId, todoUniqueId }) => {
  const dispatch = useDispatch();
  const { users } = useSelector((state: State) => state.user);
  const [ userEntries, setUserEntries ] = useState<UserEntries>([]);
  const [ assignee, setAssignee ] = useState<string>('');
  const { updateTodoAssignee } = bindActionCreators(todoActionCreators, dispatch);

  useEffect(() => {
    dispatch(usersActionCreators.getUsers());
  }, [dispatch]);

  const fullName = ({firstName, lastName}: User) => [firstName, lastName].join(' ');

  useEffect(() => {
    const user = users.find((user) => user.uniqueId === assigneeUniqueId);
    if (user !== undefined) {
      setAssignee(fullName(user));
    } else {
      setAssignee('Unassigned');
    }
  }, [assigneeUniqueId, users]);

  useEffect(() => {
    setUserEntries(users.map(user => ({ uniqueId: user.uniqueId, name: fullName(user) }) as Entry));
  }, [users]);

  return (
    <Dropdown
      currentEntry={assignee}
      possibleEntries={userEntries}
      onSelect={(entry: Entry): void => {
        const entryGuidString = entry.uniqueId?.toString() ?? emptyGuid;
        updateTodoAssignee(todoUniqueId, entryGuidString);
      }}
    />
  );
});
