import React from 'react';
import {ViewBox} from '../design-system/atoms';
import styled from 'styled-components';
import { AuthForm } from '../components/AuthForm';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.red };
    margin-top: 55px;
`;

export const Login = () => {
    return (
        <Wrapper w={60} h={60}>
            <AuthForm />
        </Wrapper>
    );
};
