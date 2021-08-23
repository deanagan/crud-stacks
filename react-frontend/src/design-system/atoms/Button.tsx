import styled from "styled-components";

interface ButtonProp {
    onClick: () => void;
}

export const ButtonWrapper = styled.button`
  padding: 15px 25px;
  font-size: 12px;
  text-align: center;
  cursor: pointer;
  outline: none;
  color: white;
  background-color: #04aa6d;
  border: none;
  border-radius: 5px;
  box-shadow: 0 3px #999;
  float: right;
  margin-top: 2px;
  margin-left: 5px;
  margin-bottom: 10px;

  &:hover {
    background-color: #032903;
    color: white;
  }

  &:active {
    background-color: #3e8e41;
    box-shadow: 0 3px #666;
    transform: translateY(4px);
  }
`;

export const Button: React.FC<ButtonProp> = ({ children, onClick }) => {
    return (
        <ButtonWrapper onClick={onClick}>
            {children}
        </ButtonWrapper>
    );
};
