import { Text, ViewBox } from "../design-system/atoms";
import { NavLink } from "react-router-dom";
import styled from "styled-components";

export const NavLinkWrapper = styled(NavLink)`
  background-color: ${({ theme }) => theme.Colors.grey500};
  overflow: hidden;
  float: left;
  color: ${({ theme }) => theme.Colors.white};
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;

  &:hover {
    background-color: ${({ theme }) => theme.Colors.white};
    color: black;
  }
`;

export const NavBarWrapper = styled.nav`
  position: absolute;
  left: 0;
  right: 0;
  width: 100%;
  background-color: ${({ theme }) => theme.Colors.grey500};

  & .active {
    background-color: ${({ theme }) => theme.Colors.activeNav};
    color: white;
  }
`;

export const NavBar: React.FC = () => {
  return (
    <ViewBox>
      <NavBarWrapper>
        <NavLinkWrapper exact to={"/"}>
          <Text>Home</Text>
        </NavLinkWrapper>
        <NavLinkWrapper to={"/contact"}>
          <Text>Contact</Text>
        </NavLinkWrapper>
      </NavBarWrapper>
    </ViewBox>
  );
};
