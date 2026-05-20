import { Timer } from '../api/timerApi';
import './TimerControls.css';

interface TimerControlsProps {
  timer: Timer;
  isLoading: boolean;
  onStart: (task: string) => void;
  onPause: () => void;
  onResume: () => void;
  onStop: () => void;
  onReset: () => void;
  onSwitchMode: (mode: number) => void;
  taskInput: string;
  onTaskChange: (task: string) => void;
}

export const TimerControls = ({
  timer,
  isLoading,
  onStart,
  onPause,
  onResume,
  onStop,
  onReset,
  onSwitchMode,
  taskInput,
  onTaskChange,
}: TimerControlsProps) => {
  const handleStart = () => {
    onStart(taskInput);
  };

  return (
    <div className="timer-controls">
      <div className="task-input-group">
        <input
          type="text"
          className="task-input"
          placeholder="Enter your task..."
          value={taskInput}
          onChange={(e) => onTaskChange(e.target.value)}
          disabled={timer.status === 'Running'}
        />
      </div>

      <div className="mode-buttons">
        <button
          className={`mode-btn ${timer.mode === 'Focus' ? 'active' : ''}`}
          onClick={() => onSwitchMode(0)}
          disabled={isLoading || timer.status === 'Running'}
        >
          Focus (25m)
        </button>
        <button
          className={`mode-btn ${timer.mode === 'Break' ? 'active' : ''}`}
          onClick={() => onSwitchMode(1)}
          disabled={isLoading || timer.status === 'Running'}
        >
          Break (5m)
        </button>
      </div>

      <div className="action-buttons">
        {timer.status === 'Stopped' && (
          <button className="btn btn-start" onClick={handleStart} disabled={isLoading}>
            Start
          </button>
        )}

        {timer.status === 'Running' && (
          <>
            <button className="btn btn-pause" onClick={onPause} disabled={isLoading}>
              Pause
            </button>
            <button className="btn btn-stop" onClick={onStop} disabled={isLoading}>
              Stop
            </button>
          </>
        )}

        {timer.status === 'Paused' && (
          <>
            <button className="btn btn-resume" onClick={onResume} disabled={isLoading}>
              Resume
            </button>
            <button className="btn btn-stop" onClick={onStop} disabled={isLoading}>
              Stop
            </button>
          </>
        )}

        {timer.status !== 'Running' && (
          <button className="btn btn-reset" onClick={onReset} disabled={isLoading}>
            Reset
          </button>
        )}
      </div>
    </div>
  );
};
