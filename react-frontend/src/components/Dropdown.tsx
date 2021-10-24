
import React, { FC } from "react";
import styled from "styled-components";
import { uuidv4Type } from "../types";


const Dropbtn = styled.div`
  display: inline-block;
  color: black;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
`;

const DropDownContent = styled.div`
  display: none;
  position: absolute;
  background-color: #f9f9f9;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
`;


const SubA = styled.a`
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
  text-align: left;
  cursor: pointer;
  &:hover {
    background-color: #13795a;
  }
`;

const DropDownLi = styled.li`
  display: inline-block;
  &:hover {
    background-color: red;
  }
  &:hover ${DropDownContent} {
    display: block;
  }
`;

export interface Entry {
  uniqueId: uuidv4Type | string | number;
  name: string;
}
interface DropdownProp {
  itemUniqueId: uuidv4Type;
  currentEntry: string;
  possibleEntries: Entry[];
  onSelect: (entry: Entry) => void;
}

export const Dropdown: FC<DropdownProp> = ({ itemUniqueId, currentEntry, possibleEntries, onSelect }) => {

  return (
    <DropDownLi>
      <Dropbtn onClick={() => {}}>
      {currentEntry}
      </Dropbtn>
      <DropDownContent>
        {possibleEntries.map((pe) => (<SubA key={pe.uniqueId.toString()}
        onClick={() => { onSelect(pe) }}>{pe.name}</SubA>))}
      </DropDownContent>
    </DropDownLi>
  );

};
