# Pomodoro Timer App V1

A minimal, focused Pomodoro timer application with a React frontend and ASP.NET Core backend following clean architecture principles.

## Features

- ✅ **Focus Timer**: 25-minute focused work sessions
- ✅ **Break Timer**: 5-minute break intervals
- ✅ **Mode Switching**: Easily switch between Focus and Break modes
- ✅ **Timer Controls**: Start, Pause/Resume, Stop, and Reset functionality
- ✅ **Task Tracking**: Enter and display one key task per session
- ✅ **Persistent State**: SQLite database for storing timer state
- ✅ **Real-time Updates**: Server-side countdown with frontend polling

## Project Structure

```
pomodoro-app2/
├── backend/
│   ├── PomodoroApp.Domain/           # Domain entities & enums
│   ├── PomodoroApp.Application/      # Business logic & service interfaces
│   ├── PomodoroApp.Data/             # EF Core, DbContext, repositories
│   ├── PomodoroApp.WebApi/           # Minimal API endpoints
│   │   ├── Features/Timer/           # Timer endpoints
│   │   ├── Services/                 # Background services
│   │   └── Program.cs
│   └── PomodoroApp.sln
├── frontend/
│   ├── src/
│   │   ├── api/                      # API client (timerApi.ts)
│   │   ├── components/               # React components
│   │   │   ├── TimerDisplay.tsx
│   │   │   ├── TimerControls.tsx
│   │   │   └── *.css
│   │   ├── App.tsx                   # Main app
│   │   └── ...
│   └── package.json
└── README.md
```

## Architecture

### Backend (Clean Architecture)

- **Domain Layer** (PomodoroApp.Domain)
  - `Timer` entity with TimerMode and TimerStatus enums
  - Pure domain models with no dependencies

- **Application Layer** (PomodoroApp.Application)
  - `ITimerService` interface & implementation
  - `ITimerRepository` interface definition
  - DTOs for API communication
  - Business logic (timer mode switching, state transitions)

- **Data Layer** (PomodoroApp.Data)
  - `PomodoroDbContext` using EF Core
  - `TimerRepository` implementation
  - Database configuration via `TimerConfiguration`
  - SQLite database with automatic schema creation

- **WebApi Layer** (PomodoroApp.WebApi)
  - Minimal API endpoints with dependency injection
  - CORS configuration for React frontend
  - `TimerBackgroundService` for server-side countdown
  - Primary constructor-based dependency injection

### Frontend (React + TypeScript)

- **API Client** (`api/timerApi.ts`): Centralized communication with backend
- **Components**:
  - `TimerDisplay`: Shows timer countdown and mode
  - `TimerControls`: Buttons for start/pause/resume/stop/reset and mode switching
- **State Management**: React hooks (useState, useEffect, useRef)
- **Polling**: 1-second polling when timer is running

## Getting Started

### Prerequisites
- .NET 10 SDK
- Node.js 16+
- npm 8+

### Quick Start

From the root directory:

```powershell
# Start both backend (port 5000) and frontend (port 3000)
.\start-dev.ps1
```

Then open `http://localhost:3000` in your browser.

### Manual Start

**Backend:**
```bash
cd backend
dotnet run
# Runs on http://localhost:5000
```

**Frontend (in another terminal):**
```bash
cd frontend
npm start
# Runs on http://localhost:3000
```

## API Endpoints

All endpoints return a `Timer` DTO:

```json
{
  "id": 1,
  "mode": "Focus",
  "remainingSeconds": 1500,
  "status": "Running",
  "task": "Complete project"
}
```

### Timer Operations
- `GET /api/timer/` - Get current timer state
- `POST /api/timer/start` - Start timer (optionally set task)
- `POST /api/timer/pause` - Pause running timer
- `POST /api/timer/resume` - Resume paused timer
- `POST /api/timer/stop` - Stop and reset countdown
- `POST /api/timer/reset` - Reset to default duration
- `POST /api/timer/switch-mode` - Switch between Focus/Break
- `POST /api/timer/set-task` - Update task

## Database

SQLite database created at: `backend/PomodoroApp.WebApi/bin/Debug/net10.0/pomodoro.db`

Automatically initialized on first run via `EnsureCreated()`.

## How It Works

1. **Frontend**: User enters a task and starts the timer
2. **Backend**: 
   - Creates/updates Timer entity in database
   - Sets status to "Running"
   - `TimerBackgroundService` decrements remaining seconds every second
3. **Frontend**: Polls `/api/timer/` every second to get updated countdown
4. **Display**: Shows real-time countdown and current mode

## Development Notes

- Clean architecture with clear separation of concerns
- Primary constructor syntax for dependency injection
- Async/await throughout for I/O operations
- No magic: straightforward service implementations
- Database auto-migration on startup
- CORS configured to allow React frontend

## Tech Stack

- **Backend**: ASP.NET Core 10, Entity Framework Core 10, SQLite
- **Frontend**: React 19, TypeScript, CSS3 (no build tool needed - Create React App)
- **Database**: SQLite (file-based, no external dependency)

## Future Enhancements (Out of V1)

- User authentication
- Timer history & statistics
- Custom timer durations
- Multiple tasks per session
- Automatic Pomodoro cycles
- Desktop/browser notifications
- Offline support
