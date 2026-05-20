import React, { useState } from 'react';
import './TaskInput.css';

interface TaskInputProps {
  currentTask: string | null | undefined;
  onTaskChange: (description: string) => void;
  onTaskClear: () => void;
}

const TaskInput: React.FC<TaskInputProps> = ({ currentTask, onTaskChange, onTaskClear }) => {
  const [inputValue, setInputValue] = useState(currentTask || '');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (inputValue.trim()) {
      onTaskChange(inputValue);
    }
  };

  const handleClear = () => {
    setInputValue('');
    onTaskClear();
  };

  return (
    <div className="task-input-container">
      <h3>Today's Key Task</h3>
      <form onSubmit={handleSubmit} className="task-form">
        <input
          type="text"
          className="task-input"
          placeholder="What's your main focus for this session?"
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          maxLength={500}
        />
        <div className="task-buttons">
          <button type="submit" className="btn-task-save">
            Save Task
          </button>
          {currentTask && (
            <button type="button" className="btn-task-clear" onClick={handleClear}>
              Clear
            </button>
          )}
        </div>
      </form>
      {currentTask && (
        <div className="task-display">
          <p className="task-text">{currentTask}</p>
        </div>
      )}
    </div>
  );
};

export default TaskInput;
