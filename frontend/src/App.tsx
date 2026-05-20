import { useState, useEffect } from 'react';
import './App.css';
import TimerDisplay from './components/TimerDisplay';
import TimerControls from './components/TimerControls';
import TaskInput from './components/TaskInput';
import { timerAPI, taskAPI, TimerState } from './services/api';

function App() {
  const [timer, setTimer] = useState<TimerState | null>(null);
  const [task, setTask] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const initializeApp = async () => {
      try {
        const timerState = await timerAPI.getCurrent();
        setTimer(timerState);

        const taskData = await taskAPI.getCurrent();
        setTask(taskData?.description || null);
      } catch (err) {
        setError('Failed to connect to server. Make sure the backend is running.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    initializeApp();
  }, []);

  useEffect(() => {
    if (!timer?.isRunning) return;

    const interval = setInterval(() => {
      setTimer((prev) => {
        if (!prev) return prev;
        return {
          ...prev,
          timeRemaining: Math.max(0, prev.timeRemaining - 1),
          displayTime: formatTime(Math.max(0, prev.timeRemaining - 1)),
          isRunning: prev.timeRemaining > 1,
        };
      });
    }, 1000);

    return () => clearInterval(interval);
  }, [timer?.isRunning]);

  const formatTime = (seconds: number): string => {
    const minutes = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${String(minutes).padStart(2, '0')}:${String(secs).padStart(2, '0')}`;
  };

  const handleStart = async () => {
    try {
      const newState = await timerAPI.start();
      setTimer(newState);
    } catch (err) {
      console.error('Failed to start timer', err);
    }
  };

  const handlePause = async () => {
    try {
      const newState = await timerAPI.pause();
      setTimer(newState);
    } catch (err) {
      console.error('Failed to pause timer', err);
    }
  };

  const handleReset = async () => {
    try {
      const newState = await timerAPI.reset();
      setTimer(newState);
    } catch (err) {
      console.error('Failed to reset timer', err);
    }
  };

  const handleSwitchMode = async (mode: string) => {
    try {
      const newState = await timerAPI.switchMode(mode);
      setTimer(newState);
    } catch (err) {
      console.error('Failed to switch mode', err);
    }
  };

  const handleTaskChange = async (description: string) => {
    try {
      const taskData = await taskAPI.create(description);
      setTask(taskData.description);
    } catch (err) {
      console.error('Failed to save task', err);
    }
  };

  const handleTaskClear = async () => {
    try {
      await taskAPI.clear();
      setTask(null);
    } catch (err) {
      console.error('Failed to clear task', err);
    }
  };

  if (loading) {
    return <div className="app-container"><p>Loading...</p></div>;
  }

  if (error) {
    return <div className="app-container error"><p>{error}</p></div>;
  }

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>Pomodoro Timer</h1>
        <p>Stay focused, achieve more</p>
      </header>

      <main className="app-main">
        {timer && (
          <>
            <TimerDisplay
              displayTime={timer.displayTime}
              mode={timer.mode}
              isRunning={timer.isRunning}
            />

            <TimerControls
              isRunning={timer.isRunning}
              mode={timer.mode}
              onStart={handleStart}
              onPause={handlePause}
              onReset={handleReset}
              onModeSwitchFocus={() => handleSwitchMode('Focus')}
              onModeSwitchBreak={() => handleSwitchMode('Break')}
            />
          </>
        )}

        <TaskInput
          currentTask={task}
          onTaskChange={handleTaskChange}
          onTaskClear={handleTaskClear}
        />
      </main>
    </div>
  );
}

export default App;
