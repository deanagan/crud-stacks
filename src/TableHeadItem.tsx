import React, { FC } from "react";



export const TableHeadItem: FC<string> = (headData) => {
    return (
        <td title={headData}>
            {headData}
        </td>
    );
};
