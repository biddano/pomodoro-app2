import React from 'react';
import './TimerDisplay.css';

interface TimerDisplayProps {
  displayTime: string;
  mode: string;
  isRunning: boolean;
}

const TimerDisplay: React.FC<TimerDisplayProps> = ({ displayTime, mode, isRunning }) => {
  return (
    <div className={`timer-display ${mode.toLowerCase()} ${isRunning ? 'running' : ''}`}>
      <h2 className="mode-label">{mode} Mode</h2>
      <div className="timer-value">{displayTime}</div>
      {isRunning && <div className="pulse-indicator">●</div>}
    </div>
  );
};

export default TimerDisplay;
