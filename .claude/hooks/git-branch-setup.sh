#!/usr/bin/env bash
# Claude CLI PreToolUse hook — ensures a clean feature branch before file-editing

set -euo pipefail

# ── Only act on file-mutating tools ─────────────────────────────────────────
TOOL_NAME=$(echo "$PAYLOAD" | jq -r '.tool_name // ""' 2>/dev/null || echo "")
case "$TOOL_NAME" in
  Write|Edit|MultiEdit|Bash) ;;  # fall through to branch check
  *) exit 0 ;;                    # non-mutating tool — nothing to do
esac

# ── Ensure working tree is clean ────────────────────────────────────────────
if ! git diff --quiet || ! git diff --cached --quiet; then
  echo "[claude-hook] Error: Uncommitted changes detected. Please commit or stash changes." >&2
  exit 1
fi

# ── Check out main and pull latest ──────────────────────────────────────────
echo "[claude-hook] Checking out main and pulling latest..." >&2
git checkout main >&2
git pull origin main >&2

# ── Create a new feature branch based on the current timestamp ──────────────
TIMESTAMP=$(date +%s)
NEW_BRANCH="feature/claude-$TIMESTAMP"

echo "[claude-hook] Creating new branch: $NEW_BRANCH" >&2
git checkout -b "$NEW_BRANCH" >&2
echo "[claude-hook] Ready. Working on branch: $NEW_BRANCH" >&2
exit 0