import { useState, useEffect, useCallback } from "react";
import { TimerSession, TimerMode } from "../types/timer";
import timerApi from "../api/timerApi";

export function useTimer() {
  const [session, setSession] = useState<TimerSession | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadState = useCallback(async () => {
    try {
      setLoading(true);
      const state = await timerApi.getState();
      setSession(state);
      setError(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to load timer state");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    loadState();
  }, [loadState]);

  useEffect(() => {
    if (!session?.isRunning) return;

    const interval = setInterval(async () => {
      try {
        const updated = await timerApi.tick();
        setSession(updated);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Failed to update timer");
      }
    }, 1000);

    return () => clearInterval(interval);
  }, [session?.isRunning]);

  const startTimer = async () => {
    try {
      const updated = await timerApi.startTimer();
      setSession(updated);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to start timer");
    }
  };

  const pauseTimer = async () => {
    try {
      const updated = await timerApi.pauseTimer();
      setSession(updated);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to pause timer");
    }
  };

  const resetTimer = async () => {
    try {
      const updated = await timerApi.resetTimer();
      setSession(updated);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to reset timer");
    }
  };

  const switchMode = async (mode: TimerMode) => {
    try {
      const updated = await timerApi.switchMode(mode);
      setSession(updated);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to switch mode");
    }
  };

  const setKeyTask = async (task: string | null) => {
    try {
      const updated = await timerApi.setKeyTask(task);
      setSession(updated);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to set task");
    }
  };

  return {
    session,
    loading,
    error,
    startTimer,
    pauseTimer,
    resetTimer,
    switchMode,
    setKeyTask
  };
}
