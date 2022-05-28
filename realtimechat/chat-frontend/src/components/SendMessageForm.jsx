import { useState } from "react";
import { Form, FormControl, InputGroup, Button } from "react-bootstrap";

function SendMessageForm({ sendMessage }) {
  const [message, setMessage] = useState("");

  const onChange = (e) => {
    setMessage(e.target.value);
  };

  const onSubmit = (e) => {
    e.preventDefault();
    sendMessage(message);
    setMessage("");
  };
  return (
    <Form onSubmit={onSubmit}>
      <InputGroup>
        <FormControl
          placeholder="message..."
          onChange={onChange}
          value={message}
        />
        <InputGroup.Text>
          <Button variant="primary" type="submit" disabled={!message}>
            Send
          </Button>
        </InputGroup.Text>
      </InputGroup>
    </Form>
  );
};

export default SendMessageForm;
