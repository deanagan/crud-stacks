import styled from 'styled-components';
import {ViewBox} from '../design-system/atoms';
import { LoginForm } from '../components';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.red };
    margin-top: 55px;
`;

export const Register = () => {
    return (
        <Wrapper w={60} h={60}>
            <LoginForm isLoginForm={false}/>
        </Wrapper>
    );
};
