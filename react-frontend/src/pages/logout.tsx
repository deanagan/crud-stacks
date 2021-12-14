import styled from 'styled-components';
import {ViewBox} from '../design-system/atoms';
import React from 'react';
import { LoginForm } from '../components';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.blue };
    margin-top: 250px;
`;

export const Logout = React.memo(() => {

    return (
        <Wrapper w={40}>
            <LoginForm needsClear/>
        </Wrapper>
    );
});
