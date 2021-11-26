import React from 'react';
import styled from 'styled-components';
import { ViewBox } from '../design-system/atoms';
import { RoutesWrapper } from '../routes';
import { NavBar } from '../components';
import { useSelector } from 'react-redux';
import { State } from '../store';

const Wrapper = styled(ViewBox)`
    flex-direction: column;
    overflow: auto;
    justify-content: center;
    align-items: center;
    margin: auto;
    display: flex;
`;

export const DefaultLayout: React.FC = () => {
    // TODO: Hook up
   // const { isLoggedIn } = useSelector((state: State) => state.auth);

    return (
        <Wrapper>
            <NavBar />
            <RoutesWrapper isLoggedIn/>
        </Wrapper>
    );
};
