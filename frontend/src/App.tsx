import React, { useEffect, useState, useRef } from 'react';
import './App.css';
import { TimerDisplay } from './components/TimerDisplay';
import { TimerControls } from './components/TimerControls';
import { timerApi, Timer } from './api/timerApi';

function App() {
  const [timer, setTimer] = useState<Timer | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [taskInput, setTaskInput] = useState('');
  const pollIntervalRef = useRef<number | null>(null);

  useEffect(() => {
    const initializeTimer = async () => {
      try {
        const currentTimer = await timerApi.getCurrent();
        setTimer(currentTimer);
        setTaskInput(currentTimer.task || '');
      } catch (error) {
        console.error('Failed to load timer:', error);
      }
    };

    initializeTimer();
  }, []);

  useEffect(() => {
    if (timer?.status === 'Running') {
      pollIntervalRef.current = window.setInterval(async () => {
        try {
          const updatedTimer = await timerApi.getCurrent();
          setTimer(updatedTimer);
        } catch (error) {
          console.error('Failed to update timer:', error);
        }
      }, 1000);
    } else {
      if (pollIntervalRef.current) {
        clearInterval(pollIntervalRef.current);
        pollIntervalRef.current = null;
      }
    }

    return () => {
      if (pollIntervalRef.current) {
        clearInterval(pollIntervalRef.current);
      }
    };
  }, [timer?.status]);

  const handleStart = async (task: string) => {
    if (!timer) return;
    setIsLoading(true);
    try {
      const updatedTimer = await timerApi.start(task || undefined);
      setTimer(updatedTimer);
    } catch (error) {
      console.error('Failed to start timer:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handlePause = async () => {
    setIsLoading(true);
    try {
      const updatedTimer = await timerApi.pause();
      setTimer(updatedTimer);
    } catch (error) {
      console.error('Failed to pause timer:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleResume = async () => {
    setIsLoading(true);
    try {
      const updatedTimer = await timerApi.resume();
      setTimer(updatedTimer);
    } catch (error) {
      console.error('Failed to resume timer:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleStop = async () => {
    setIsLoading(true);
    try {
      const updatedTimer = await timerApi.stop();
      setTimer(updatedTimer);
    } catch (error) {
      console.error('Failed to stop timer:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleReset = async () => {
    setIsLoading(true);
    try {
      const updatedTimer = await timerApi.reset();
      setTimer(updatedTimer);
    } catch (error) {
      console.error('Failed to reset timer:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleSwitchMode = async (mode: number) => {
    setIsLoading(true);
    try {
      const updatedTimer = await timerApi.switchMode(mode);
      setTimer(updatedTimer);
    } catch (error) {
      console.error('Failed to switch mode:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleTaskChange = (task: string) => {
    setTaskInput(task);
  };

  if (!timer) {
    return <div className="app-container"><div className="loading">Loading...</div></div>;
  }

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>🍅 Pomodoro Timer</h1>
      </header>

      <main className="app-main">
        <TimerDisplay timer={timer} />
        <TimerControls
          timer={timer}
          isLoading={isLoading}
          onStart={handleStart}
          onPause={handlePause}
          onResume={handleResume}
          onStop={handleStop}
          onReset={handleReset}
          onSwitchMode={handleSwitchMode}
          taskInput={taskInput}
          onTaskChange={handleTaskChange}
        />
      </main>
    </div>
  );
}

export default App;
