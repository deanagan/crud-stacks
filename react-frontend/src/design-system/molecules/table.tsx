import { PropsWithChildren } from "react";
import styled from "styled-components";
import { ViewBox } from "../atoms/ViewBox";
import { TableDataRow } from "./tableDataRow";
import { TableHeaderRow } from "./tableHeaderRow";

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
  id?: number | string;
}

interface TableProp<T extends TableRowBase> {
  columnLabels: string[];
  rowFields: string[];
  tableData: T[];
}

export const Table: <T>(props: PropsWithChildren<TableProp<T>>) => React.ReactElement = ({columnLabels, rowFields, tableData}) => {
  // const applyRowData = (singleRow: TableRowBase) => {
  //   const row = singleRow;
  //   const fieldValues = Object.entries(singleRow).filter(v => rowFields.includes(v[0]));

  //   return (
  //     <tr key={row.id}>
  //       {fieldValues.map((field, index) => (
  //         <td key={index}>{field[1]}</td>
  //       ))}
  //     </tr>
  //   );
  // };

  return (
    <ViewBox>
      <StyledTableWrapper>
        <StyledTable>

            <TableHeaderRow columnLabels={columnLabels} />
            {/* <tr>
              {columnLabels.map((label, index) => (
                <th key={index}>{label}</th>
              ))}
            </tr> */}
          <tbody>
          {
            tableData.map((rd) =>
            <TableDataRow key={(rd as TableRowBase).id} rowData = {rd} rowFields = {rowFields} />
            )
          }
          </tbody>
          {/* <tbody>
            {rowData.map(applyRowData)}
          </tbody> */}
        </StyledTable>
      </StyledTableWrapper>
    </ViewBox>
  );
}
