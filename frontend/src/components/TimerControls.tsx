import React from 'react';
import './TimerControls.css';

interface TimerControlsProps {
  isRunning: boolean;
  mode: string;
  onStart: () => void;
  onPause: () => void;
  onReset: () => void;
  onModeSwitchFocus: () => void;
  onModeSwitchBreak: () => void;
}

const TimerControls: React.FC<TimerControlsProps> = ({
  isRunning,
  mode,
  onStart,
  onPause,
  onReset,
  onModeSwitchFocus,
  onModeSwitchBreak,
}) => {
  return (
    <div className="timer-controls">
      <div className="control-buttons">
        {!isRunning ? (
          <button className="btn btn-primary" onClick={onStart}>
            Start
          </button>
        ) : (
          <button className="btn btn-warning" onClick={onPause}>
            Pause
          </button>
        )}
        <button className="btn btn-secondary" onClick={onReset}>
          Reset
        </button>
      </div>

      <div className="mode-buttons">
        <button
          className={`btn btn-mode ${mode === 'Focus' ? 'active' : ''}`}
          onClick={onModeSwitchFocus}
        >
          Focus (25m)
        </button>
        <button
          className={`btn btn-mode ${mode === 'Break' ? 'active' : ''}`}
          onClick={onModeSwitchBreak}
        >
          Break (5m)
        </button>
      </div>
    </div>
  );
};

export default TimerControls;
