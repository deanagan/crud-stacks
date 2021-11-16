import styled from "styled-components";
import { Todo } from "../../types";
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

interface TableProp {
  tableData: Todo[];
  columnLabels: string[];
}

export const Table: React.FC<TableProp> = ({ tableData, columnLabels }) => {
  return (
    <ViewBox>
      <StyledTableWrapper>
        <StyledTable>
          <TableHeaderRow columnLabels={columnLabels} />
          <tbody>
            {tableData.map((rd) => <TableDataRow key={rd.uniqueId?.toString()} {...rd} />)}
          </tbody>
        </StyledTable>
      </StyledTableWrapper>
    </ViewBox>
  );
};
