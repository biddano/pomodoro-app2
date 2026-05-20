# Pomodoro Timer App

A simple, focused Pomodoro timer application built with ASP.NET Core (C#) backend and React (TypeScript) frontend.

## Features

- **Focus Timer**: 25-minute focused work sessions
- **Break Timer**: 5-minute break intervals
- **Mode Switching**: Easily switch between Focus and Break modes
- **Timer Controls**: Start, Pause, and Reset functionality
- **Key Task Tracking**: Enter and display one key task per session
- **Persistent State**: SQLite database for storing timer and task data

## Project Structure

```
pomodoro-app2/
├── backend/
│   ├── PomodoroApp/          # C# ASP.NET Core backend
│   │   ├── Features/
│   │   │   ├── Timer/        # Timer feature (service & controller)
│   │   │   └── Task/         # Task feature (service & controller)
│   │   ├── Data/             # Database context & models
│   │   └── Program.cs        # App configuration
│   └── PomodoroApp.sln       # .NET solution file
├── frontend/                  # React + TypeScript frontend
│   ├── src/
│   │   ├── components/       # React components
│   │   ├── services/         # API client services
│   │   └── App.tsx           # Main app component
│   ├── vite.config.ts        # Vite configuration
│   └── package.json          # Dependencies
└── docs/
    └── requirements-doc.md   # Product requirements
```

## Architecture

### Backend
- **Feature-based organization**: Each feature (Timer, Task) is a self-contained module
- **Thin Controllers**: API endpoints delegate to service layer
- **Business Logic Separation**: Core timer logic isolated from HTTP layer
- **SQLite Database**: Persistent storage with Entity Framework Core
- **CORS Enabled**: Configured for React frontend communication

### Frontend
- **Component-based**: Timer display, controls, and task input as separate components
- **Type-safe**: Full TypeScript support
- **API Client**: Centralized service for backend communication
- **Responsive Design**: Works on desktop and mobile

## Getting Started

### Prerequisites
- .NET 10 SDK
- Node.js 18+
- npm

### Backend Setup

1. Navigate to backend directory:
```bash
cd backend/PomodoroApp
```

2. Build the project:
```bash
dotnet build
```

3. Run the backend (runs on `https://localhost:7200`):
```bash
dotnet run
```

The backend will automatically create the SQLite database on first run.

### Frontend Setup

1. Navigate to frontend directory:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server (runs on `http://localhost:5173`):
```bash
npm run dev
```

### Using the App

1. Open your browser to `http://localhost:5173`
2. Enter your key task for the session
3. Choose Focus (25 min) or Break (5 min) mode
4. Click Start to begin the timer
5. Use Pause and Reset as needed
6. Switch between modes anytime

## API Endpoints

### Timer Endpoints
- `GET /api/timer/current` - Get current timer state
- `POST /api/timer/start` - Start the timer
- `POST /api/timer/pause` - Pause the timer
- `POST /api/timer/reset` - Reset the timer
- `POST /api/timer/switch-mode?mode={Focus|Break}` - Switch timer mode

### Task Endpoints
- `GET /api/task/current` - Get current task
- `POST /api/task/create` - Create or update task
- `POST /api/task/clear` - Clear current task

## Development

### Build Backend
```bash
cd backend/PomodoroApp
dotnet build
```

### Build Frontend
```bash
cd frontend
npm run build
```

### Run Tests
Backend:
```bash
dotnet test
```

Frontend:
```bash
npm run test
```

## Tech Stack

- **Backend**: ASP.NET Core 10, Entity Framework Core, SQLite
- **Frontend**: React 19, TypeScript, Vite, CSS3
- **Database**: SQLite (file-based)

## Architecture Principles

1. **Keep it Simple**: No unnecessary abstractions
2. **Feature-based Organization**: Related code grouped by feature, not by type
3. **Thin Controllers**: Controllers delegate business logic to services
4. **Testable Logic**: Business rules isolated and independently testable
5. **Separated Concerns**: Timer logic separated from HTTP layer

## Future Enhancements (Out of V1 Scope)

- User authentication and accounts
- Timer history and statistics
- Custom timer durations
- Multiple tasks per session
- Automatic Pomodoro cycles
- Desktop notifications
- Calendar integration

## License

MIT
