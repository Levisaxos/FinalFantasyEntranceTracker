import React, { useState, useEffect, useRef } from 'react';
import { MessageType } from '../../types/message';

interface MessagePanelProps {
  type: MessageType;
  message: string;
  title: string | null;
  onClose: () => void;
}

const MessagePanel: React.FC<MessagePanelProps> = ({ type, message, title, onClose }) => {
  const [visible, setVisible] = useState(true);
  const [isHovered, setIsHovered] = useState(false);
  const [progress, setProgress] = useState(100);
  const timerRef = useRef<number | null>(null);

  const startTimer = () => {
    if (timerRef.current) clearInterval(timerRef.current);
    
    let timeLeft = 3000;
    const interval = 50; // Update progress every 50ms for smoother animation

    timerRef.current = window.setInterval(() => {
      timeLeft -= interval;
      setProgress((timeLeft / 3000) * 100);

      if (timeLeft <= 0) {
        clearInterval(timerRef.current!);
        setVisible(false);
        onClose();
      }
    }, interval);
  };

  useEffect(() => {
    startTimer();
    return () => {
      if (timerRef.current) clearInterval(timerRef.current);
    };
  }, []);

  useEffect(() => {
    if (isHovered) {
      if (timerRef.current) clearInterval(timerRef.current);
      setProgress(100);
    } else {
      startTimer();
    }
  }, [isHovered]);

  if (!visible) return null;

  const alertClass = type === 'Error' ? 'message-panel-error' : 'message-panel-info';

  return (
    <div 
      className={`message-panel ${alertClass}`}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
      onClick={() => {
        setVisible(true);
        onClose();
      }}
    >
      {title && <h3 className="message-panel-title">{title}</h3>}
      <p className="message-panel-message">{message}</p>
      <div className="message-panel-progress-bar">
        <div 
          className="message-panel-progress" 
          style={{ width: `${progress}%` }}
        />
      </div>
    </div>
  );
};

export default MessagePanel;