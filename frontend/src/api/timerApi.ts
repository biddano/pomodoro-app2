const API_BASE_URL = 'http://localhost:5000/api';

export interface Timer {
  id: number;
  mode: 'Focus' | 'Break';
  remainingSeconds: number;
  status: 'Stopped' | 'Running' | 'Paused';
  task: string | null;
}

export interface ModeDto {
  [key: string]: number;
}

const apiCall = async (method: string, endpoint: string, body?: any): Promise<Timer> => {
  const url = `${API_BASE_URL}${endpoint}`;

  const options: RequestInit = {
    method,
    headers: { 'Content-Type': 'application/json' },
  };

  if (body) {
    options.body = JSON.stringify(body);
  }

  const response = await fetch(url, options);
  if (!response.ok) {
    throw new Error(`API Error: ${response.statusText}`);
  }

  return response.json();
};

export const timerApi = {
  getCurrent: () => apiCall('GET', '/timer/'),
  start: (task?: string) => apiCall('POST', '/timer/start', { task: task || null }),
  pause: () => apiCall('POST', '/timer/pause', {}),
  resume: () => apiCall('POST', '/timer/resume', {}),
  stop: () => apiCall('POST', '/timer/stop', {}),
  reset: () => apiCall('POST', '/timer/reset', {}),
  switchMode: (mode: number) => apiCall('POST', '/timer/switch-mode', { mode }),
  setTask: (task: string | null) => apiCall('POST', '/timer/set-task', { task }),
};
