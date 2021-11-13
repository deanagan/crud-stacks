import { PropsWithChildren, useEffect, useState } from "react";
import React from "react";
import { TableRowBase } from ".";

interface TableDataRowProp<T extends TableRowBase> {
    rowData: T;
    rowFields: string[];
}

export const TableDataRow: <T>(props: PropsWithChildren<TableDataRowProp<T>>) => React.ReactElement | null = React.memo(({rowData, rowFields}) => {
    const [fieldValues, setFieldValues] = useState<string[] | React.ReactElement[]>([]);
    useEffect(() => {
        const fieldValues = Object.entries(rowData).filter(e => rowFields.includes(e[0])).map(kv => kv[1]);
        setFieldValues(fieldValues);
    }, [rowFields, rowData]);


    return (
        <tr>
        {fieldValues.map((field, index) => (
            <td key={index}>{field}</td>
        ))}
        </tr>
    );

});
