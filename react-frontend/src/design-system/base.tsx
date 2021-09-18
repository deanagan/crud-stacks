import styled from 'styled-components';

export interface IElement {
    color?: string;
    backgroundColor?: string;
    w?: number;
    h?: number;
}

export const Element = styled.div<IElement>`
    ${({ w }) => (w ? `width: ${w}%;` : null)}
    ${({ h }) => (h ? `height: ${h}%;` : null)}
    ${({ backgroundColor }) => (backgroundColor ? `background-color: ${backgroundColor};` : null)}
    ${({ color }) => (color ? `color: ${color};` : null)}
`;
