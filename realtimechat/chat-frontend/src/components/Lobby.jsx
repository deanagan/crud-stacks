import { useState } from "react";
import { Button, Form } from "react-bootstrap";

function Lobby({ joinRoom }) {
    const [name, setName] = useState('');
    const [room, setRoom] = useState('');

    const onChangeNameHandler = (event) => { setName(event.target.value) };
    const onChangeRoomHandler = (event) => { setRoom(event.target.value) };
    const onSubmitHandler = (event) => {
        event.preventDefault();
        joinRoom(name, room);
    };

    return (<Form className = 'lobby'
    onSubmit={onSubmitHandler}>
        <Form.Group>
            <Form.Control placeholder="name" onChange={onChangeNameHandler} />
            <Form.Control placeholder="room" onChange={onChangeRoomHandler} />
        </Form.Group>
        <Button variant="success" type="submit" disabled={!name || !room}>
            Join
        </Button>
    </Form>);
}

export default Lobby;