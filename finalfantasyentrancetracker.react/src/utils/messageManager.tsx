import React from 'react';
import ReactDOM from 'react-dom/client';
import MessageManager from '../components/ui/MessageManager.tsx';

export const mountMessageManager = () => {
  const messageRoot = document.createElement('div');
  messageRoot.id = 'message-root';
  document.body.appendChild(messageRoot);
  
  const root = ReactDOM.createRoot(messageRoot);
  root.render(React.createElement(MessageManager));
};