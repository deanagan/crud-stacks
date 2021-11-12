import { FC } from "react";
import React from "react";

export interface TableHeaderRowProp {
  columnLabels: string[];
}

export const TableHeaderRow: FC<TableHeaderRowProp> = React.memo(
  ({ columnLabels }) => {
    return (
      <thead>
        <tr>
          {columnLabels.map((label, index) => (
            <th key={index}>{label}</th>
          ))}
        </tr>
      </thead>
    );
  }
);
