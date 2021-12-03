import styled from 'styled-components';
import {ViewBox} from '../design-system/atoms';
import { AuthForm } from '../components';
import { useEffect, useRef } from 'react';
import { useDispatch } from 'react-redux';
import { authActionCreators } from '../store';
import { bindActionCreators } from 'redux';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.red };
    margin-top: 55px;
`;

export const Login = () => {
    const dispatch = useDispatch();
    const { logOutUser } = bindActionCreators(authActionCreators, dispatch);
    const logOutUserRef = useRef(logOutUser);

    useEffect(() => {
        logOutUserRef.current();
    }, []);

    return (
        <Wrapper w={60} h={60}>
            <AuthForm isLoginForm/>
        </Wrapper>
    );
};
