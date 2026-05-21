# Pomodoro App

## Using Claude CLI

### Prerequisites

- [Claude CLI](https://docs.anthropic.com/en/docs/claude-code) installed
- `jq` installed (`brew install jq`)

### Running the agent

```bash
cd /Users/nick.nalli/Develop/pomodoro-app2
claude
```

Give the agent a task based on the requirements, e.g.:

```
Build the focus timer backend based on docs/requirements-doc.md
```

### Git branching

A `PreToolUse` hook (`.claude/hooks/git-branch-setup.sh`) runs automatically before any file is written or edited.

- If you are on `main` or `master`, the hook creates a new feature branch before the agent touches any code
- Branch names are derived from the agent's tool input (e.g. target file name or bash command) and auto-incremented: `feature/<slug>-001`, `feature/<slug>-002`, etc.
- If uncommitted changes exist, they are stashed automatically before branching

**Do not run `git checkout` or `git branch` manually** — the hook manages this.

After a session, verify the branch was created:

```bash
git branch
```

### Project docs

- [Requirements](docs/requirements-doc.md)
- [Improvements](docs/improvements.md)
