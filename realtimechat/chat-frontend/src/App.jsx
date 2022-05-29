import "./App.scss";
import "bootstrap/dist/css/bootstrap.min.css";
import { useRef, useState } from "react";
import Lobby from "./components/Lobby";
import Chat from "./components/Chat";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import BellIcon from "./components/Bell";

const BACKENDURL = "https://localhost:7095/chat";

function App() {
  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);
  const [count, setCount] = useState([]);
  const currentName = useRef('');

  const joinRoom = async (name, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl(BACKENDURL)
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (name, message) => {
        if (name !== currentName.current) {
          setMessages((m) => [...m, { name, message }]);
          setCount((c) => Math.min(9, c + 1));
        }
      });

      await connection.start();
      await connection.invoke("JoinRoom", { name, room });
      setConnection(connection);
      currentName.current = name;

    } catch (e) {
      console.log(e);
    }
  };

  const sendMessage = async (message) => {
    try {
      await connection.invoke("SendMessage", message);
    } catch (e) {
      console.log(e);
    }
  };

  const onClear = () => {
    setCount(0);
  };

  const body = (<ul>
    {messages.map(({name, message}) => (<li>{name} said {message}</li>))}
    </ul>);

  return (
    <div className="App">
      <h2>Chat</h2>

      <hr className="line" />
      {!connection ? (
        <Lobby joinRoom={joinRoom} />
      ) : (
        <>
          <BellIcon alertCount={count} onClear={onClear} popOverBody={body}/>
          <Chat messages={messages} sendMessage={sendMessage}/>
        </>
      )}
    </div>
  );
}

export default App;
