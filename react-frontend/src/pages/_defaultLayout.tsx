import React from 'react';
import styled from 'styled-components';
import { ViewBox } from '../design-system/atoms';
import { Routes } from '../routes';
import { NavBar } from '../components';

const Wrapper = styled(ViewBox)`
    flex-direction: column;
    overflow: auto;
    justify-content: center;
    align-items: center;
    margin: auto;
    display: flex;
`;

export const DefaultLayout: React.FC = () => {
    return (
        <Wrapper>
            <NavBar />
            <Routes />
        </Wrapper>
    );
};
