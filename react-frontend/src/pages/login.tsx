import styled from 'styled-components';
import {ViewBox} from '../design-system/atoms';
import { LoginForm } from '../components';
import React from 'react';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.blue };
    margin-top: 250px;
`;

export const Login = () => {

    return (
        <Wrapper w={40}>
            <LoginForm />
        </Wrapper>
    );
};
