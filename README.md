# Pomodoro Timer App

A simple focus timer app with a Pomeranian mascot to help you stay productive using the Pomodoro Technique.

## Overview

The Pomodoro Timer helps users work in focused 25-minute sessions, take 5-minute breaks, and associate each focus session with one key task.

## Features

- **Focus Timer**: 25-minute focused work sessions
- **Break Timer**: 5-minute break sessions
- **Timer Controls**: Start, pause, and reset the timer
- **Mode Switching**: Easily switch between focus and break modes
- **Key Task**: Set and track your current task

## Architecture

The application follows Clean Architecture principles:

- **Domain Layer**: Core business entities (TimerSession, TimerMode)
- **Application Layer**: Business logic services and repository interfaces
- **Data Layer**: EF Core with SQLite database implementation
- **WebApi Layer**: ASP.NET Core Minimal APIs

### Frontend

Built with React and TypeScript, using the Bulletproof React structure for scalability and maintainability.

## Prerequisites

- .NET 10 SDK
- Node.js 16+ and npm

## Getting Started

### Backend Setup

1. Build the solution:
```bash
cd C:\Projects\pomo-timer2
dotnet build
```

2. Run the WebApi:
```bash
dotnet run --project PomodoroApp.WebApi
```

The backend will be available at `http://localhost:5000`

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd C:\Projects\pomo-timer2\frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The frontend will be available at `http://localhost:3000`

## API Endpoints

### Timer Management

- `GET /api/timer/state` - Get current timer state
- `POST /api/timer/start` - Start the timer
- `POST /api/timer/pause` - Pause the timer
- `POST /api/timer/reset` - Reset the timer to default duration
- `POST /api/timer/mode/{mode}` - Switch timer mode (Focus or Break)
- `POST /api/timer/task` - Set the key task
- `POST /api/timer/tick` - Advance timer by one second

## Database

The application uses SQLite for state management. The database file is created automatically at:
```
PomodoroApp.WebApi/bin/Debug/net10.0/pomodoro.db
```

## Tech Stack

### Backend
- .NET 10
- ASP.NET Core WebApi
- Entity Framework Core
- SQLite

### Frontend
- React 18+
- TypeScript
- CSS3 with gradients and animations

## Project Structure

```
pomo-timer2/
├── PomodoroApp.Domain/           # Domain layer
│   ├── TimerMode.cs
│   └── TimerSession.cs
├── PomodoroApp.Application/       # Application layer
│   ├── Abstractions/
│   │   ├── ITimerSessionRepository.cs
│   │   └── ITimerService.cs
│   ├── DTOs/
│   │   └── TimerSessionDto.cs
│   └── Services/
│       └── TimerService.cs
├── PomodoroApp.Data/              # Data layer
│   ├── Configurations/
│   │   └── TimerSessionConfiguration.cs
│   ├── Repositories/
│   │   └── TimerSessionRepository.cs
│   └── PomodoroDbContext.cs
├── PomodoroApp.WebApi/            # WebApi layer
│   ├── Features/Timer/
│   │   └── TimerEndpoints.cs
│   └── Program.cs
└── frontend/                      # React frontend
    ├── src/
    │   ├── api/
    │   │   └── timerApi.ts
    │   ├── components/
    │   │   └── Timer/
    │   │       ├── Timer.tsx
    │   │       └── Timer.css
    │   ├── hooks/
    │   │   └── useTimer.ts
    │   ├── types/
    │   │   └── timer.ts
    │   ├── utils/
    │   │   └── formatTime.ts
    │   ├── App.tsx
    │   └── App.css
    └── package.json
```

## V1 Scope Completion

V1 is complete when:
- ✅ User can run a 25-minute focus timer
- ✅ User can run a 5-minute break timer
- ✅ User can start, stop/pause, and reset the timer
- ✅ User can switch between focus and break modes
- ✅ User can enter and view one key task for the timer session

## Development Notes

- Keep the architecture simple
- Timer logic is in the service layer, not in API endpoints
- Business rules are testable and separate from presentation
- Database handles state management
