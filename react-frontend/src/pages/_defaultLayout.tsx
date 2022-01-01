import React from "react";
import styled from "styled-components";
import { ViewBox } from "../design-system/atoms";
import { RoutesWrapper } from "../routes";
import { NavBar } from "../components";
import { State } from "../store";
import { useSelector } from "react-redux";


const Wrapper = styled(ViewBox)`
  flex-direction: column;
  overflow: auto;
  justify-content: center;
  align-items: center;
  margin: auto;
  display: flex;
`;

export const DefaultLayout: React.FC = () => {
  const { loggedIn } = useSelector((state:State) => state.auth);

  return (
    <Wrapper>
      {loggedIn ? (
        <NavBar />
      ) : null}
      <RoutesWrapper isLoggedIn={loggedIn} />
    </Wrapper>
  );
};
