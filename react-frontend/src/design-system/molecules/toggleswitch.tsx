import styled from "styled-components";
import { FC, useState } from "react";
import { uuidv4Type } from "../../types";
import React from "react";

const ToggleSwitchDivWrapper = styled.div`
  position: relative;
  display: inline-block;
  width: 60px;
  height: 34px;
`;

const ToggleSwitchLabel = styled.label`
  position: absolute;
  top: 0;
  left: 0;
  width: 42px;
  height: 26px;
  border-radius: 15px;
  background: #bebebe;
  cursor: pointer;
  &::after {
    content: "";
    display: block;
    border-radius: 50%;
    width: 18px;
    height: 18px;
    margin: 3px;
    background: #ffffff;
    box-shadow: 1px 3px 3px 1px rgba(0, 0, 0, 0.2);
    transition: 0.2s;
  }
`;

const ToggleSwitchWrapper = styled.input.attrs({ type: 'checkbox' })`
  opacity: 0;
  z-index: 1;
  border-radius: 15px;
  width: 42px;
  height: 26px;
  &:checked + ${ToggleSwitchLabel} {
    background: #4fbe79;
    &::after {
      content: "";
      display: block;
      border-radius: 50%;
      width: 18px;
      height: 18px;
      margin-left: 21px;
      transition: 0.2s;
    }
  }
`;


interface ToggleSwitchProp {
  switchUniqueId: uuidv4Type;
  initialState: boolean;
  updateSwitchFunc: (id: uuidv4Type, state: boolean) => void;
}

export const ToggleSwitch: FC<ToggleSwitchProp> = React.memo(({ switchUniqueId, initialState, updateSwitchFunc }) => {
//export const ToggleSwitch: FC<ToggleSwitchProp> = ({ switchUniqueId, initialState, updateSwitchFunc }) => {
  const [switchState, setSwitchState] = useState(initialState);

  const onSwitchChanged = (isDone: boolean) => {
    updateSwitchFunc(switchUniqueId, isDone);
    setSwitchState(isDone);
  };

  return (
    <ToggleSwitchDivWrapper>
      <ToggleSwitchWrapper
        id={`switch${switchUniqueId}`}
        onChange={(e) => onSwitchChanged(e.target.checked)}
        checked={switchState}
      />
      <ToggleSwitchLabel htmlFor={`switch${switchUniqueId}`} />
    </ToggleSwitchDivWrapper>
  );
});
