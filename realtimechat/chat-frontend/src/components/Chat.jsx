import SendMessageForm from "./SendMessageForm";

function Chat({messages, sendMessage}) {

    return (
    <div className="chat">
        <div className="message-container">
            {messages.map((m, index) =>
                (<div key={index} className="user-message">
                    <div className="message bg-primary">
                        {m.message}
                    </div>
                    <div className="from-user">
                        {m.name}
                    </div>
                </div>
            ))}
        </div>
        <SendMessageForm sendMessage={sendMessage} />
    </div>
    );
}

export default Chat;