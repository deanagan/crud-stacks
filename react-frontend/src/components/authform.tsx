import { FC, useEffect } from "react"
import { useDispatch, useSelector } from "react-redux";
import { useNavigate  } from "react-router-dom";
import { bindActionCreators } from "redux";
import styled from "styled-components";
import { ButtonWrapper } from "../design-system/atoms";
import { State, authActionCreators } from "../store";


const LoginButton = styled(ButtonWrapper)`
  &&& {
    float: none;
    margin-left: 2rem;
    width: 30%;
  }
`;

const Section = styled.section`
  height: 100%;
  display: block;
`;

const Header = styled.h1`
  margin-left: 2rem;
  color: silver;
`;

const Form = styled.form`
  width: 80%;
`;

const FormEntry = styled.div`
  /* display: inline-block; */
`;

const FormEntryLabel = styled.label`
  /* display: inline-block; */
  margin-bottom: 10px;
  margin-left: 2rem;
  width: 80px;
  color: silver;
`;

const FormEntryInput = styled.input`
  width: 100%;
  padding: 11px 13px;
  background: #f9f9fa;
  margin-bottom: 0.9rem;
  margin-top: 0.2rem;
  margin-left: 2rem;
  border-radius: 4px;
  outline: 0;
  border: 1px solid rgba(245, 245, 245, 0.7);
  font-size: 14px;
  transition: all 0.3s ease-out;
  box-shadow: 0 0 3px rgba(0, 0, 0, 0.1), 0 1px 1px rgba(0, 0, 0, 0.1);
  :focus,
  :hover {
    box-shadow: 0 0 3px rgba(0, 0, 0, 0.15), 0 1px 5px rgba(0, 0, 0, 0.1);
  }
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
            <Header>{isLoginForm ? 'Login' : 'Sign Up'}</Header>
            <Form onSubmit={onSubmitHandler}>
                <FormEntry>
                    <FormEntryLabel htmlFor='email'>Email</FormEntryLabel>
                    <FormEntryInput type='email' id='email' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='password'>Password</FormEntryLabel>
                    <FormEntryInput type='password' id='password' required />
                </FormEntry>
                <LoginButton>{isLoginForm ? "Log In": "Register"}</LoginButton>
            </Form>
        </Section>
    );
}
