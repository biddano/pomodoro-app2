import React, { useState } from "react";
import { useTimer } from "../../hooks/useTimer";
import { TimerMode } from "../../types/timer";
import { formatTime } from "../../utils/formatTime";
import "./Timer.css";

export function Timer() {
  const [taskInput, setTaskInput] = useState("");
  const { session, loading, error, startTimer, pauseTimer, resetTimer, switchMode, setKeyTask } = useTimer();

  if (loading || !session) {
    return <div className="timer-container">Loading...</div>;
  }

  const handleSetTask = async (e: React.FormEvent) => {
    e.preventDefault();
    await setKeyTask(taskInput || null);
  };

  const handleModeSwitch = async (mode: TimerMode) => {
    if (session.isRunning) {
      await pauseTimer();
    }
    await switchMode(mode);
  };

  return (
    <div className="timer-container">
      <div className="timer-header">
        <h1>🐕 Pomodoro Timer</h1>
      </div>

      {error && <div className="error">{error}</div>}

      <div className="mode-selector">
        <button
          className={`mode-btn ${session.mode === TimerMode.Focus ? "active" : ""}`}
          onClick={() => handleModeSwitch(TimerMode.Focus)}
        >
          Focus (25 min)
        </button>
        <button
          className={`mode-btn ${session.mode === TimerMode.Break ? "active" : ""}`}
          onClick={() => handleModeSwitch(TimerMode.Break)}
        >
          Break (5 min)
        </button>
      </div>

      <div className="timer-display">
        <div className="time">{formatTime(session.secondsRemaining)}</div>
        <div className="status">{session.isRunning ? "Running" : "Paused"}</div>
      </div>

      <div className="timer-controls">
        {!session.isRunning ? (
          <button className="btn btn-primary" onClick={startTimer}>
            Start
          </button>
        ) : (
          <button className="btn btn-warning" onClick={pauseTimer}>
            Pause
          </button>
        )}
        <button className="btn btn-secondary" onClick={resetTimer}>
          Reset
        </button>
      </div>

      <form onSubmit={handleSetTask} className="task-input-form">
        <input
          type="text"
          placeholder="Enter your key task..."
          value={taskInput || session.keyTask || ""}
          onChange={(e) => setTaskInput(e.target.value)}
          maxLength={100}
        />
        <button type="submit" className="btn btn-small">
          Set Task
        </button>
      </form>

      {session.keyTask && (
        <div className="task-display">
          <div className="task-label">Current Task:</div>
          <div className="task-content">{session.keyTask}</div>
        </div>
      )}
    </div>
  );
}
