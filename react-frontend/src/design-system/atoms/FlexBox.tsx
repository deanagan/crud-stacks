import React from 'react';
import styled from 'styled-components';
import {IElement} from '../base';

export const FlexWrapper = styled.div`
    display: flex;
    flex-wrap: nowrap;
    background-color: DodgerBlue;

    div {
        background-color: #f1f1f1;
        width: 100px;
        margin: 10px;
        text-align: center;
        line-height: 75px;
        font-size: 30px;
  }
`

export const FlexBox: React.FC<IElement> = ({ children, ...props }) => {
    return (
        <FlexWrapper {...props}>
            {children}
        </FlexWrapper>
    );
};
