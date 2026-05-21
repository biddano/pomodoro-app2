#!/usr/bin/env node

const { execSync } = require('child_process');
const path = require('path');
const fs = require('fs');

const formatCSharp = (filePath) => {
  if (!filePath) {
    console.error('No file path provided');
    process.exit(1);
  }

  // Only process .cs files
  if (path.extname(filePath) !== '.cs') {
    process.exit(0);
  }

  try {
    // Log that the script was called
    const logPath = path.join(__dirname, '..', 'docs', 'pretty.log');
    const timestamp = new Date().toISOString();
    const logEntry = `${timestamp} - Hook called for: ${filePath}\n`;

    fs.mkdirSync(path.dirname(logPath), { recursive: true });
    fs.appendFileSync(logPath, logEntry);

    // Try to run prettier-csharp if available
    try {
      execSync(`npx prettier-csharp --write "${filePath}"`, { stdio: 'inherit' });
      const formattedEntry = `${timestamp} - Formatted: ${filePath}\n`;
      fs.appendFileSync(logPath, formattedEntry);
    } catch (error) {
      const skipEntry = `${timestamp} - prettier-csharp not available (${error.message})\n`;
      fs.appendFileSync(logPath, skipEntry);
    }
  } catch (error) {
    console.error(`Error in format-cs.js:`, error.message);
    process.exit(1);
  }
};

module.exports = formatCSharp;

// Support running as a CLI script
if (require.main === module) {
  const filePath = process.argv[1];
  formatCSharp(filePath);
  process.exit(0);
}
