import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from 'react';
import Lobby from './components/Lobby';
import Chat from './components/Chat';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


const BACKENDURL = "https://localhost:7095/chat";

function App() {
  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);

  const joinRoom = async (name, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl(BACKENDURL)
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (name, message) => {
        setMessages(m => [...m, { name, message }]);
      });

      await connection.start();
      await connection.invoke("JoinRoom", {name, room});
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  const sendMessage = async (message) => {
    try {
      await connection.invoke("SendMessage", message);
    } catch(e) {
      console.log(e);
    }
  };

  return (
    <div className="App">
      <h2>Chatter Box</h2>
      <hr className='line' />
      {!connection ?
      <Lobby joinRoom={joinRoom} />
      : <Chat messages={messages} sendMessage={sendMessage} />
}
    </div>
  );
}

export default App;
