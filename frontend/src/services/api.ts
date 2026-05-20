const API_BASE = 'https://localhost:7200/api';

export interface TimerState {
  id: number;
  mode: string;
  duration: number;
  timeRemaining: number;
  isRunning: boolean;
  displayTime: string;
}

export interface Task {
  id: number;
  description: string | null;
}

const handleResponse = async (response: Response) => {
  if (!response.ok) {
    throw new Error(`API error: ${response.status}`);
  }
  return response.json();
};

export const timerAPI = {
  getCurrent: async (): Promise<TimerState> => {
    const response = await fetch(`${API_BASE}/timer/current`);
    return handleResponse(response);
  },

  start: async (): Promise<TimerState> => {
    const response = await fetch(`${API_BASE}/timer/start`, { method: 'POST' });
    return handleResponse(response);
  },

  pause: async (): Promise<TimerState> => {
    const response = await fetch(`${API_BASE}/timer/pause`, { method: 'POST' });
    return handleResponse(response);
  },

  reset: async (): Promise<TimerState> => {
    const response = await fetch(`${API_BASE}/timer/reset`, { method: 'POST' });
    return handleResponse(response);
  },

  switchMode: async (mode: string): Promise<TimerState> => {
    const response = await fetch(`${API_BASE}/timer/switch-mode?mode=${mode}`, {
      method: 'POST',
    });
    return handleResponse(response);
  },
};

export const taskAPI = {
  getCurrent: async (): Promise<Task | null> => {
    const response = await fetch(`${API_BASE}/task/current`);
    return handleResponse(response);
  },

  create: async (description: string): Promise<Task> => {
    const response = await fetch(`${API_BASE}/task/create`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ description }),
    });
    return handleResponse(response);
  },

  clear: async (): Promise<void> => {
    const response = await fetch(`${API_BASE}/task/clear`, { method: 'POST' });
    if (!response.ok) throw new Error(`API error: ${response.status}`);
  },
};
