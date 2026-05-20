# Start both backend and frontend

Write-Output "Starting Pomodoro App (Backend + Frontend)..."
Write-Output ""
Write-Output "Starting backend on http://localhost:5000"
Write-Output "Starting frontend on http://localhost:3000"
Write-Output ""

# Start backend in background
Push-Location backend
Start-Job -ScriptBlock {
    Set-Location $args[0]
    & dotnet run
} -ArgumentList (Get-Location) | Out-Null
Pop-Location

# Start frontend
Push-Location frontend
npm start
Pop-Location
