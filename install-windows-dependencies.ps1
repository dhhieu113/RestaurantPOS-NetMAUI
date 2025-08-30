# Windows App SDK Dependency Installer
# This script helps users install Windows App SDK dependencies required by the app

Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "RestaurantPOS Windows Dependencies Installer" -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host ""

# Check if running as administrator
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) {
    Write-Host "This script needs administrator privileges to install dependencies." -ForegroundColor Yellow
    Write-Host "Please right-click on this script and select 'Run as administrator'." -ForegroundColor Yellow
    Write-Host "Press any key to exit..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit
}

# Check if winget is available
$wingetAvailable = $null -ne (Get-Command winget -ErrorAction SilentlyContinue)
if (-not $wingetAvailable) {
    Write-Host "Winget package manager not found. Please install App Installer from Microsoft Store." -ForegroundColor Red
    Write-Host "https://apps.microsoft.com/detail/9nblggh4nns1" -ForegroundColor Cyan
    Write-Host "Press any key to exit..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit
}

Write-Host "Installing Windows App Runtime dependencies..." -ForegroundColor Green
winget install Microsoft.WindowsAppRuntime.1.5

Write-Host ""
Write-Host "Installation complete!" -ForegroundColor Green
Write-Host "You can now run RestaurantPOS.exe" -ForegroundColor Green
Write-Host ""
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
