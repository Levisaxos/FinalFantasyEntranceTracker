export type MessageType = 'Error' | 'Info';

export interface MessageProps {
  id: string;
  message: string;
  title?: string;
  type: MessageType;
}

declare global {
  interface Window {
    addInfoMessage: (message: string, title?: string) => void;
    addErrorMessage: (message: string, title?: string) => void;
  }
}