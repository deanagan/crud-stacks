import { FC  } from 'react';
import { User } from "./common/interfaces";


export const TableRow: FC<User[]> = (data) => {
    return (
        <tr>
            {data.map((item: User) => {
                return <td key={item.id}>{item}</td>;
            })}
        </tr>
    );
};
