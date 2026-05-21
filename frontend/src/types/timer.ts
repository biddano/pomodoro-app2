export enum TimerMode {
  Focus = "Focus",
  Break = "Break"
}

export interface TimerSession {
  id: number;
  mode: TimerMode;
  secondsRemaining: number;
  isRunning: boolean;
  keyTask: string | null;
}

export const TIMER_DURATION = {
  [TimerMode.Focus]: 25 * 60,
  [TimerMode.Break]: 5 * 60
};
