import { TimerSession, TimerMode } from "../types/timer";

const API_URL = process.env.REACT_APP_API_URL || "http://localhost:5000";

const timerApi = {
  async getState(): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/state`);
    if (!response.ok) throw new Error("Failed to fetch timer state");
    return response.json();
  },

  async startTimer(): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/start`, { method: "POST" });
    if (!response.ok) throw new Error("Failed to start timer");
    return response.json();
  },

  async pauseTimer(): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/pause`, { method: "POST" });
    if (!response.ok) throw new Error("Failed to pause timer");
    return response.json();
  },

  async resetTimer(): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/reset`, { method: "POST" });
    if (!response.ok) throw new Error("Failed to reset timer");
    return response.json();
  },

  async switchMode(mode: TimerMode): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/mode/${mode}`, { method: "POST" });
    if (!response.ok) throw new Error("Failed to switch mode");
    return response.json();
  },

  async setKeyTask(task: string | null): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/task`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ task })
    });
    if (!response.ok) throw new Error("Failed to set key task");
    return response.json();
  },

  async tick(): Promise<TimerSession> {
    const response = await fetch(`${API_URL}/api/timer/tick`, { method: "POST" });
    if (!response.ok) throw new Error("Failed to tick timer");
    return response.json();
  }
};

export default timerApi;
