import { FC, useEffect } from "react"
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { useNavigate  } from "react-router-dom";
import styled from "styled-components";
import { ButtonWrapper } from "../design-system/atoms";
import { State } from "../store";


const SignUpButton = styled(ButtonWrapper)`
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

interface SignUpFormProp {
  isSignUpForm: boolean;
}

interface FormElements extends HTMLFormControlsCollection {
  email: HTMLInputElement,
  password: HTMLInputElement
}

interface SignUpFormElement extends HTMLFormElement {
  readonly elements: FormElements
}

export const SignUpForm: FC<SignUpFormProp> = ({isSignUpForm}) => {
    // const dispatch = useDispatch()
    // const { signUpUser } = bindActionCreators(authActionCreators, dispatch);

    const navigate = useNavigate ();

    const { currentLoggedInUser, error } = useSelector((state: State) => state.auth);

    useEffect(() => {
      // if (currentLoggedInUser?.token === '') {
      //   navigate('/signup');
      // } else {
      //   window.localStorage.setItem(StorageTypes.TOKEN, currentLoggedInUser?.token || '');
      //   navigate('/');
      // }
    }, [currentLoggedInUser?.token, navigate]);

    const onSubmitHandler = (event:  React.FormEvent<SignUpFormElement>) => {
      event.preventDefault();
      // const email = event.currentTarget.elements.email.value;
      // const password = event.currentTarget.elements.password.value;
    };

    return (
        <Section>
            <Header>{isSignUpForm ? 'SignUp' : 'Sign Up'}</Header>
            <Form onSubmit={onSubmitHandler}>
                <FormEntry>
                    <FormEntryLabel htmlFor='firstName'>First Name</FormEntryLabel>
                    <FormEntryInput id='first-name' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='lastName'>Last Name</FormEntryLabel>
                    <FormEntryInput id='last-name' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='email'>Email</FormEntryLabel>
                    <FormEntryInput type='email' id='email' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='password'>Password</FormEntryLabel>
                    <FormEntryInput type='password' id='password' required />
                </FormEntry>
                <FormEntry>
                    <FormEntryLabel htmlFor='confirm-password'>Confirm Password</FormEntryLabel>
                    <FormEntryInput type='password' id='confirm-password' required />
                </FormEntry>
                <SignUpButton>Sign Up</SignUpButton>
                <span>Have an account? <Link to="/login">Login</Link> instead</span>
            </Form>
            {error ? <ErrorMessage>*{error}</ErrorMessage> : undefined}
        </Section>
    );
}
