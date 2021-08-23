import React from 'react';
import {ViewBox} from '../design-system/atoms';
import styled from 'styled-components';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.red };
    margin-top: 55px;
`;

export const Contact = () => {
    return (
        <Wrapper w={600} h={600}>Contact</Wrapper>
    );
};
