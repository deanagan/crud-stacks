import { FC } from "react";
import styled from "styled-components";

interface ActionLinkProp {
  deleteFn: () => void;
  message: string;
  color: string;
}

const ActionLinkWrapper = styled.div`
  color:  ${({ color = 0 }) => color};
  text-decoration: underline;
  cursor: pointer;
  margin: auto;

  &:hover {
    text-decoration: none;
  }
`;

export const ActionLink: FC<ActionLinkProp> = ({ color, deleteFn, message }) => {
  return (
    <>
      <ActionLinkWrapper color={color} onClick={deleteFn}>{message}</ActionLinkWrapper>
    </>
  );
};
