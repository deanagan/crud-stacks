import styled from 'styled-components';
import {ViewBox} from '../design-system/atoms';
import { AuthForm } from '../components';
import { useEffect, useRef } from 'react';
import { useDispatch } from 'react-redux';
import { authActionCreators } from '../store';
import { bindActionCreators } from 'redux';
import { StorageTypes } from '../constants';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.blue };
    margin-top: 250px;
`;

export const Login = () => {
    const dispatch = useDispatch();
    const { logOutUser } = bindActionCreators(authActionCreators, dispatch);
    const logOutUserRef = useRef(logOutUser);

    useEffect(() => {
        logOutUserRef.current();
        window.localStorage.removeItem(StorageTypes.TOKEN);
    }, []);

    return (
        <Wrapper w={40}>
            <AuthForm isLoginForm/>
        </Wrapper>
    );
};
