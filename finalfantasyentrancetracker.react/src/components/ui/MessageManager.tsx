import React, { useState, useCallback, useEffect } from 'react';
import MessagePanel from './MessagePanel.tsx';
import { MessageProps, MessageType } from '../../types/message';

const MessageManager: React.FC = () => {
  const [messages, setMessages] = useState<MessageProps[]>([]);
  
  const addMessage = useCallback((message: string, title?: string, type: MessageType = 'Info') => {
    const id = Math.random().toString(36).substr(2, 9);    
    setMessages(prevMessages => [...prevMessages, { id, message, title, type }]);
  }, []);

  const removeMessage = useCallback((id: string) => {
    setMessages(prevMessages => prevMessages.filter(msg => msg.id !== id));
  }, []);

  useEffect(() => {
    // Expose addMessage function globally
    window.addInfoMessage = (message: string, title?: string) => addMessage(message, title, 'Info');
    window.addErrorMessage = (message: string, title?: string) => addMessage(message, title, 'Error');

    return () => {
        (window as any).addInfoMessage = undefined;
        (window as any).addErrorMessage = undefined;
    };
  }, [addMessage]);

  return (
    <div className="message-container">
      {messages.map(({ id, message, title, type }) => (
        <MessagePanel        
          key={id}
          type={type}
          message={message}
          title={title || null}
          onClose={() => removeMessage(id)}
        />
      ))}
    </div>
  );
};

export default MessageManager;