import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from 'react';
import Lobby from './components/Lobby';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


const BACKENDURL = "https://localhost:7095/chat";

function App() {
  const [connection, setConnection] = useState();
  const joinRoom = async (name, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl(BACKENDURL)
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (name, message) => {
        console.log("got: ", message);
      });

      await connection.start();
      await connection.invoke("JoinRoom", {name, room});
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <div className="App">
      <h2>Chatter Box</h2>
      <hr className='line' />
      <Lobby joinRoom={joinRoom} />
    </div>
  );
}

export default App;
