import { FC, useEffect } from "react";
import { Dropdown, Entry } from "../design-system/molecules";
import { uuidv4Type } from "../types";
import { usersActionCreators } from "../store";
import { useDispatch } from "react-redux";

export interface AssigneeDropDownProp {
  uniqueId: uuidv4Type;
}

export const AssigneeDropDown: FC<AssigneeDropDownProp> = ({ uniqueId }) => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(usersActionCreators.getUsers());
  }, [dispatch]);

  return (
    <Dropdown
      currentEntry={"Unassigned"}
      possibleEntries={[]}
      onSelect={function (entry: Entry): void {
        // TODO
      }}
    />
  );
};
