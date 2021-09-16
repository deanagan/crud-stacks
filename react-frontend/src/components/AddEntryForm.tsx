import { ChangeEvent, FC } from "react";
import styled from "styled-components";

export const FormGroup = styled.div`
  color: palevioletred;
  display: block;
  width: 90%;
  margin: 10px;
`;

export const Label = styled.label`
  margin-bottom: 0.5em;
  color: palevioletred;
  display: block;
`;

export const Input = styled.input.attrs({ type: "text" })`
  padding: 0.5em;
  color: palevioletred;
  background: papayawhip;
  border: none;
  border-radius: 3px;
  width: 100%;
  max-width: 100%;
  margin-bottom: 0.5em;
`;

export const Message = styled.label`
  margin-bottom: 0.5em;
  color: palevioletred;
  display: block;
`;

export interface AddEntryFormProp {
  summary: string;
  detail: string;
  changeSummary: (arg: string) => void;
  changeDetail: (arg: string) => void;
}

export const AddEntryForm: FC<AddEntryFormProp> = ({
    summary,
    detail,
    changeSummary,
    changeDetail
  }) => {


  return (
    <>
      <FormGroup>
        <Label htmlFor="summary">Summary</Label>
        <Input
          placeholder="Enter summary of todo"
          value={summary}
          id="summary"
          onChange={(e: ChangeEvent<HTMLInputElement>) =>
            changeSummary(e.target.value)
          }
        />
      </FormGroup>
      <FormGroup>
        <Label htmlFor="detail">Details</Label>
        <Input
          type="text"
          as="textarea"
          placeholder="Details"
          value={detail}
          id="detail"
          onChange={(e: ChangeEvent<HTMLElement>) =>
            changeDetail((e.target as HTMLTextAreaElement).value)
          }
        />
      </FormGroup>
    </>
  );
};
