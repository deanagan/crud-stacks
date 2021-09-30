import styled from "styled-components";
import { FC, useState } from "react";
import { useDispatch } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../store";
import { uuidv4Type } from "../types";

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
  isDone: boolean;
}

export const ToggleSwitch: FC<ToggleSwitchProp> = ({ switchUniqueId, isDone }) => {
  const dispatch = useDispatch();

  const { updateTodoState } = bindActionCreators(actionCreators, dispatch);
  const [switchState, setSwitchState] = useState(isDone);

  const onSwitchChanged = (isDone: boolean) => {
    updateTodoState(switchUniqueId, isDone);
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
};
