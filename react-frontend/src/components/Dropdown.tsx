
import React, { FC } from "react";
import styled from "styled-components";
import { uuidv4 } from "../types";


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
  &:hover {
    background-color: #f1f1f1;
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

interface DropdownProp {
  itemUniqueId: uuidv4;
}

export const Dropdown: FC<DropdownProp> = ({ itemUniqueId }) => {


  return (
    <DropDownLi>
      <Dropbtn onClick={() => {}}>
        DropDown
      </Dropbtn>
      <DropDownContent>
        {" "}
        <SubA onClick={() => {}}>Link 1</SubA>
        <SubA onClick={() => {}}>Link 2</SubA>
        <SubA onClick={() => {}}>Link 3</SubA>
      </DropDownContent>
    </DropDownLi>
  );

};