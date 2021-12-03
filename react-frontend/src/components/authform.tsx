import { EventHandler, FC, useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux";
import { useNavigate  } from "react-router-dom";
import { bindActionCreators } from "redux";
import styled from "styled-components";
import { State, authActionCreators } from "../store";

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
  email: HTMLInputElement,
  password: HTMLInputElement
}

interface AuthFormElement extends HTMLFormElement {
  readonly elements: FormElements
}

export const AuthForm: FC<AuthFormProp> = ({isLoginForm}) => {

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const [isLogin, setIsLogin] = useState(true);
    const dispatch = useDispatch()
    const { logInUser } = bindActionCreators(authActionCreators, dispatch);
    const navigate = useNavigate ();

    const { currentLoggedInUser } = useSelector((state: State) => state.auth);

    useEffect(() => {
      if (currentLoggedInUser?.email === '') {
        navigate('/login');
      } else {
        navigate('/');
      }
    }, [currentLoggedInUser?.email, navigate]);

    const onSubmitHandler = (event:  React.FormEvent<AuthFormElement>) => {
      event.preventDefault();
      const email = event.currentTarget.elements.email.value;
      const password = event.currentTarget.elements.password.value;
      logInUser({
        email, password
      });
    };

    return (
        <Section>
            <div>{currentLoggedInUser?.email}</div>
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
                <Button>{isLogin? "Log In": "Register"}</Button>
            </Form>

        </Section>
    );
}
