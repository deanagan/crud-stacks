import { FC } from "react";
import { uuidv4Type } from "../types";

interface TodoTableRowProp {
    todoUniqueId: uuidv4Type;
    summary: string;
    detail: string;
    isDone: boolean;
    assigneeUniqueId: uuidv4Type;
}


export const TodoTableRow: FC<TodoTableRowProp> = ({todoUniqueId, summary, detail, isDone, assigneeUniqueId}) => {

  return (
      <tr>
          <td>{summary}</td>
          <td>{detail}</td>
          <td>{isDone ? "True": "False"}</td>
      </tr>
  );
};
