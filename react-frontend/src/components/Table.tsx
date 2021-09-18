import { PropsWithChildren } from "react";
import styled from "styled-components";
import { ViewBox } from "../design-system/atoms/ViewBox";

export const StyledTableWrapper = styled.div`
  overflow-x: auto;
  margin: auto;
`;

export const StyledTable = styled.table`
  border-collapse: collapse;
  border-spacing: 0;
  width: 100%;
  border: 1px solid black;
  th,
  td {
    text-align: left;
    padding: 16px;
  }
  th {
    background-color: ${({ theme }) => theme.Colors.grey300};
  }
  tr:nth-child(even) {
    background-color: ${({ theme }) => theme.Colors.tableStripe};
  }
`;

export interface TableRowBase {
  id?: number;
}

interface TableProp<T extends TableRowBase> {
  columnLabels: string[];
  rowFields: string[];
  rowData: T[];
  excludedColumnName: string[];
}

export function Table<T>(props: PropsWithChildren<TableProp<T>>) {
  return (
    <ViewBox>
      <StyledTableWrapper>
        <StyledTable>
          <thead>
            <tr>
              {props.columnLabels.map((label, index) => (
                <th key={index}>{label}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {props.rowData.map((singleRow) => {
              const row = singleRow as TableRowBase;
              const fields = Object.entries(singleRow).filter(v => !props.excludedColumnName.includes(v[0]));

              return (
                <tr key={row.id}>
                  {fields.map((field, index) => (
                    <td key={index}>{field[1]}</td>
                  ))}
                </tr>
              );
            })}
          </tbody>
        </StyledTable>
      </StyledTableWrapper>
    </ViewBox>
  );
}
