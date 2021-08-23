import React from 'react';
import {Element, IElement} from '../base';

export const ViewBox: React.FC<IElement> = ({ children, ...props }) => {
    return (
        <Element {...props}>
            {children}
        </Element>
    );
};
