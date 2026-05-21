#!/usr/bin/env bash
# Claude CLI PreToolUse hook — ensures a feature branch is checked out
# before any file-editing tool (Write, Edit, MultiEdit, Bash) runs.
#
# Branch naming: feature/<slug>-<NNN>
#   <slug>  — derived from the agent's tool input (file path or bash command)
#   <NNN>   — auto-incremented so each new session gets a unique branch

set -euo pipefail

PAYLOAD=$(cat)

# ── Only act on file-mutating tools ─────────────────────────────────────────
TOOL_NAME=$(echo "$PAYLOAD" | jq -r '.tool_name // ""')
case "$TOOL_NAME" in
  Write|Edit|MultiEdit|Bash) ;;  # fall through to branch check
  *) exit 0 ;;                    # non-mutating tool — nothing to do
esac

# ── Check current branch ─────────────────────────────────────────────────────
CURRENT_BRANCH=$(git symbolic-ref --short HEAD 2>/dev/null || echo "HEAD")

# Already on a feature branch — nothing to do
if [[ "$CURRENT_BRANCH" != "main" && "$CURRENT_BRANCH" != "master" ]]; then
  exit 0
fi

# ── Derive a slug from the agent's tool input ────────────────────────────────
# For Write/Edit/MultiEdit use the target file path; for Bash use the command.
slugify() {
  echo "$1" | tr '[:upper:]' '[:lower:]' \
    | sed 's|.*/||'          `# keep basename only` \
    | sed 's/\.[^.]*$//'     `# strip extension` \
    | sed 's/[^a-z0-9]/-/g' \
    | sed 's/--*/-/g'        \
    | sed 's/^-//;s/-$//'    \
    | cut -c1-40
}

case "$TOOL_NAME" in
  Write|Edit|MultiEdit)
    RAW=$(echo "$PAYLOAD" | jq -r '
      .tool_input.file_path //
      (.tool_input.edits[0].file_path // "") ' 2>/dev/null || echo "")
    ;;
  Bash)
    RAW=$(echo "$PAYLOAD" | jq -r '.tool_input.command // ""' 2>/dev/null \
      | awk '{print $1, $2}')  # first two words of the command
    ;;
esac

SLUG=$(slugify "${RAW:-work}")
[[ -z "$SLUG" ]] && SLUG="work"

BRANCH_PREFIX="feature/${SLUG}"

# ── Auto-increment: find the highest existing NNN for this prefix ────────────
LAST=$(git branch --list "${BRANCH_PREFIX}-*" \
  | sed "s|.*${BRANCH_PREFIX}-||" \
  | grep -E '^[0-9]+$' \
  | sort -n \
  | tail -1)

if [[ -n "$LAST" ]]; then
  NEXT=$(printf "%03d" $(( 10#$LAST + 1 )))
else
  NEXT="001"
fi

NEW_BRANCH="${BRANCH_PREFIX}-${NEXT}"

# ── Ensure working tree is clean before branching ───────────────────────────
if ! git diff --quiet || ! git diff --cached --quiet; then
  echo "[claude-hook] Uncommitted changes detected — stashing before branching." >&2
  git stash push -m "claude-hook auto-stash before $NEW_BRANCH" >&2
fi

# ── Create and switch to the new branch ─────────────────────────────────────
echo "[claude-hook] Creating branch: $NEW_BRANCH (from $CURRENT_BRANCH)" >&2
git checkout -b "$NEW_BRANCH" >&2
echo "[claude-hook] Ready. Working on branch: $NEW_BRANCH" >&2
exit 0
