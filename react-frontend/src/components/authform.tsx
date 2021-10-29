import { FC, useState } from "react"
import styled from "styled-components";

const Section = styled.section`
  display: inline-block;
`;

const Header = styled.h1`
  display: inline-block;
`;

const Form = styled.form`
  display: inline-block;
`;

const FormEntry = styled.div`
  display: inline-block;
`;

const FormEntryLabel = styled.label`
  display: inline-block;
`;

const FormEntryInput = styled.input`
  display: inline-block;
`;

export const AuthForm: FC = () => {

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const [isLogin, setIsLogin] = useState(true);

    return (
        <Section>
            <Header>{isLogin ? 'Login' : 'Sign Up'}</Header>
            <Form>
                <FormEntry>
                    <FormEntryLabel htmlFor='email'>Email</FormEntryLabel>
                    <FormEntryInput type='email' id='email' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='password'>Password</FormEntryLabel>
                    <FormEntryInput type='password' id='password' required />
                </FormEntry>
            </Form>
        </Section>
    );
}