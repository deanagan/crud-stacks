import React from "react";
import { ToggleSwitch } from ".";
import { emptyGuid, Todo, uuidv4Type } from "../../types";
import { useDispatch } from "react-redux";
import { DeleterAction } from "../../components";
import { AssigneeDropDown } from "../../components/assigneeDropDown";
import { bindActionCreators } from "redux";
import { todoActionCreators } from "../../store";

export const TableDataRow: React.FC<Todo> = React.memo(
  ({ summary, detail, isDone, uniqueId, assignee = null }) => {
    const dispatch = useDispatch();
    const { updateTodoState } = bindActionCreators(
      todoActionCreators,
      dispatch
    );

    return (
      <tr>
        <td>{summary}</td>
        <td>{detail}</td>
        <td>{isDone ? "True" : "False"}</td>
        <td>
          <ToggleSwitch
            switchUniqueId={uniqueId as uuidv4Type}
            initialState={isDone}
            updateSwitchStage={(uniqueId: uuidv4Type, isDone: boolean) =>
              updateTodoState(uniqueId, isDone)
            }
          />
        </td>
        <td>
          <DeleterAction uniqueId={uniqueId as uuidv4Type} />
        </td>
        <td>
          <AssigneeDropDown
            assigneeUniqueId={assignee?.uniqueId ?? emptyGuid}
            todoUniqueId={uniqueId as uuidv4Type}
          />
        </td>
      </tr>
    );
  }
);
