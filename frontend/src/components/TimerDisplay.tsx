import { Timer } from '../api/timerApi';
import './TimerDisplay.css';

interface TimerDisplayProps {
  timer: Timer;
}

export const TimerDisplay = ({ timer }: TimerDisplayProps) => {
  const minutes = Math.floor(timer.remainingSeconds / 60);
  const seconds = timer.remainingSeconds % 60;

  const formatTime = (min: number, sec: number) => {
    return `${String(min).padStart(2, '0')}:${String(sec).padStart(2, '0')}`;
  };

  return (
    <div className="timer-display">
      <div className={`mode-badge ${timer.mode.toLowerCase()}`}>
        {timer.mode === 'Focus' ? '🎯 Focus' : '☕ Break'}
      </div>

      <div className={`timer-circle ${timer.status.toLowerCase()}`}>
        <div className="timer-time">{formatTime(minutes, seconds)}</div>
      </div>

      {timer.task && (
        <div className="task-display">
          <strong>Task:</strong> {timer.task}
        </div>
      )}
    </div>
  );
};
