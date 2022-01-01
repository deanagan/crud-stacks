import styled from 'styled-components';
import {ViewBox} from '../design-system/atoms';
import { SignUpForm } from '../components';

const Wrapper = styled(ViewBox)`
    justify-content: center;
    background-color: ${({ theme }) => theme.Colors.blue };
    margin-top: 55px;
    height: 600px;
`;

export const Register = () => {
    return (
        <Wrapper w={40}>
            <SignUpForm isSignUpForm={false} />
        </Wrapper>
    );
};
