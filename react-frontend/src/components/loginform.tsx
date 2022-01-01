import { FC, useEffect, useRef } from "react"
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { bindActionCreators } from "redux";
import styled from "styled-components";
import { StorageTypes } from "../constants";
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
  height: 19rem;
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

const ErrorMessage = styled.div`
  margin-left: 2rem;
  margin-top: 0.1rem;
  color: red;
`;

interface LoginFormProp {
  needsClear?: boolean; // TO remove
}

interface FormElements extends HTMLFormControlsCollection {
  email: HTMLInputElement,
  password: HTMLInputElement
}

interface LoginFormElement extends HTMLFormElement {
  readonly elements: FormElements
}

export const LoginForm: FC<LoginFormProp> = (props) => {
    const dispatch = useDispatch()
    const { logInUser, logOutUser } = bindActionCreators(authActionCreators, dispatch);
    const logOutUserRef = useRef(logOutUser);
    const navigate = useNavigate ();
    const submittedRef = useRef(false);

    const { currentLoggedInUser, error } = useSelector((state: State) => state.auth);

    useEffect(() => {
      if (submittedRef.current && currentLoggedInUser?.token) {
        localStorage.setItem(StorageTypes.TOKEN, currentLoggedInUser?.token || '');
        localStorage.setItem(StorageTypes.EMAIL, currentLoggedInUser?.email ?? '');
        navigate('/', {replace: true});
      }
      submittedRef.current = false;
    }, [currentLoggedInUser?.token, currentLoggedInUser?.email, navigate]);

    useEffect(() => {
        localStorage.removeItem(StorageTypes.TOKEN);
        localStorage.removeItem(StorageTypes.EMAIL);
        logOutUserRef.current();
    }, []);

    const onSubmitHandler = (event:  React.FormEvent<LoginFormElement>) => {
      event.preventDefault();
      const email = event.currentTarget.elements.email.value;
      const password = event.currentTarget.elements.password.value;
      logInUser({
        email, password
      });
      submittedRef.current = true;
    };

    return (
        <Section>
            <Header>Login</Header>
            <Form onSubmit={onSubmitHandler}>
                <FormEntry>
                    <FormEntryLabel htmlFor='email'>Email</FormEntryLabel>
                    <FormEntryInput type='email' id='email' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='password'>Password</FormEntryLabel>
                    <FormEntryInput type='password' id='password' required />
                </FormEntry>
                <LoginButton>Log In</LoginButton>
            </Form>
            {error ? <ErrorMessage>*{error}</ErrorMessage> : undefined}
        </Section>
    );
}
