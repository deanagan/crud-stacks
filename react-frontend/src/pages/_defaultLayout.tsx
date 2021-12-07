import React, { useEffect, useState } from "react";
import styled from "styled-components";
import { ViewBox } from "../design-system/atoms";
import { RoutesWrapper } from "../routes";
import { NavBar } from "../components";
import { useSelector } from "react-redux";
import { State } from "../store";

const Wrapper = styled(ViewBox)`
  flex-direction: column;
  overflow: auto;
  justify-content: center;
  align-items: center;
  margin: auto;
  display: flex;
`;

export const DefaultLayout: React.FC = () => {
  const { currentLoggedInUser } = useSelector((state: State) => state.auth);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  useEffect(() => {
    setIsLoggedIn(!!currentLoggedInUser?.email);
  }, [currentLoggedInUser?.email]);

  return (
    <Wrapper>
      {isLoggedIn ? (
        <NavBar />
      ) : null}
      <RoutesWrapper isLoggedIn={isLoggedIn} />
    </Wrapper>
  );
};
