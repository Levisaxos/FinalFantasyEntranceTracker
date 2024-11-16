# React Application Setup with Tailwind CSS

A complete setup guide for creating a React application with Tailwind CSS, custom server configuration, and environment management.

## 📋 Prerequisites

- Node.js (v14.0.0 or higher)
- npm (v6.0.0 or higher)
- Visual Studio Code
- Git

## 🚀 Quick Start

1. Clone this repository:
```bash
git clone [your-repo-url]
cd [your-repo-name]
```

2. Install dependencies:
```bash
npm install
```

3. Start development server:
```bash
npm run dev
```

## 📦 Installation Steps

### Create React Application

```bash
npx create-react-app my-app
cd my-app
```

### Install and Configure Tailwind CSS

1. Install required packages:
```bash
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init -p
```

2. Update `tailwind.config.js`:
```javascript
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

3. Add Tailwind directives to `src/index.css`:
```css
@tailwind base;
@tailwind components;
@tailwind utilities;
```

### Server Configuration

1. Create `server/service.js`:
```javascript
const express = require('express');
const path = require('path');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 3000;

// Serve static files from React build
app.use(express.static(path.join(__dirname, '../build')));

// Handle React routing
app.get('*', (req, res) => {
  res.sendFile(path.join(__dirname, '../build', 'index.html'));
});

app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
```

2. Install server dependencies:
```bash
npm install express dotenv
```

### Environment Configuration

1. Create `.env.development`:
```env
PORT=3000
REACT_APP_API_URL=http://localhost:8080
REACT_APP_ENV=development
```

2. Create `.env.production`:
```env
PORT=8000
REACT_APP_API_URL=https://api.yourproduction.com
REACT_APP_ENV=production
```

3. Install environment command runner:
```bash
npm install env-cmd
```

### Package.json Scripts

Update your package.json with these scripts:
```json
{
  "scripts": {
    "start": "react-scripts start",
    "build": "react-scripts build",
    "serve": "node server/service.js",
    "dev": "env-cmd -f .env.development npm start",
    "build:prod": "env-cmd -f .env.production npm run build"
  }
}
```

## 🔧 Available Scripts

- `npm run dev` - Start development server with development environment
- `npm run build:prod` - Create production build with production environment
- `npm run serve` - Serve production build

## 📁 Project Structure

```
my-app/
├── node_modules/
├── public/
├── server/
│   └── service.js
├── src/
│   ├── components/
│   ├── App.js
│   ├── index.js
│   └── index.css
├── .env.development
├── .env.production
├── .gitignore
├── package.json
├── tailwind.config.js
└── README.md
```

## ⚙️ Environment Variables

### Development
- `PORT`: Development server port (default: 3000)
- `REACT_APP_API_URL`: Development API URL
- `REACT_APP_ENV`: Environment identifier

### Production
- `PORT`: Production server port (default: 8000)
- `REACT_APP_API_URL`: Production API URL
- `REACT_APP_ENV`: Environment identifier

## 🔒 Security

Remember to:
- Add `.env.development` and `.env.production` to `.gitignore`
- Never commit sensitive credentials
- Use environment variables for all sensitive configuration

## 📝 Notes

- Ensure all environment variables used in React start with `REACT_APP_`
- The server will serve the React application on the specified port
- Static files are served from the `build` directory in production

## 📚 Additional Resources

- [Create React App documentation](https://create-react-app.dev/)
- [Tailwind CSS documentation](https://tailwindcss.com/)
- [Express.js documentation](https://expressjs.com/)

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details