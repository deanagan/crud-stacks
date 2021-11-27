import { EventHandler, FC, useState } from "react"
import styled from "styled-components";

const Button = styled.button`

`;

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

interface AuthFormProp {
  isLoginForm: boolean;
}

interface FormElements extends HTMLFormControlsCollection {
  usernameInput: HTMLInputElement
}

interface AuthFormElement extends HTMLFormElement {
  readonly elements: FormElements
}

export const AuthForm: FC<AuthFormProp> = ({isLoginForm}) => {

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const [isLogin, setIsLogin] = useState(true);

    const onSubmitHandler = (e:  React.FormEvent<AuthFormElement>) => {
      e.preventDefault();
      console.log("Log me in!");
    };

    return (
        <Section>
            <Header>{isLogin ? 'Login' : 'Sign Up'}</Header>
            <Form onSubmit={onSubmitHandler}>
                <FormEntry>
                    <FormEntryLabel htmlFor='email'>Email</FormEntryLabel>
                    <FormEntryInput type='email' id='email' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='password'>Password</FormEntryLabel>
                    <FormEntryInput type='password' id='password' required />
                </FormEntry>
            </Form>
            <Button>{isLogin? "Log In": "Register"}</Button>
        </Section>
    );
}