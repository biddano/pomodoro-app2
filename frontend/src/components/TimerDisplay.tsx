import { Timer } from '../api/timerApi';
import './TimerDisplay.css';

interface TimerDisplayProps {
  timer: Timer;
}

export const TimerDisplay = ({ timer }: TimerDisplayProps) => {
  const minutes = Math.floor(timer.remainingSeconds / 60);
  const seconds = timer.remainingSeconds % 60;

  const getModeString = (mode: string | number | undefined) => {
    if (typeof mode === 'string') return mode;
    if (mode === 0) return 'Focus';
    if (mode === 1) return 'Break';
    return 'Focus';
  };

  const formatTime = (min: number, sec: number) => {
    return `${String(min).padStart(2, '0')}:${String(sec).padStart(2, '0')}`;
  };

  const modeString = getModeString(timer.mode);

  return (
    <div className="timer-display">
      <div className={`mode-badge ${modeString.toLowerCase()}`}>
        {modeString === 'Focus' ? '🎯 Focus' : '☕ Break'}
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
