import React from 'react';
import {ViewBox} from '../design-system/atoms';
import styled from 'styled-components';
import {Routes} from '../routes';
import {NavBar} from '../components/navigation';

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
