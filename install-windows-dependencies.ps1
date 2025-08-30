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
Write-Host "Verifying installation..." -ForegroundColor Yellow

# Verify installation by checking if Windows App Runtime components are registered
$testResult = $true
try {
    # Check for Windows App Runtime COM components
    $regPath = "HKLM:\SOFTWARE\Classes\CLSID\{8C57E4C5-3A45-4B34-BF39-056368B9D8ED}"
    if (-not (Test-Path $regPath)) {
        Write-Host "Windows App Runtime COM components are not registered correctly." -ForegroundColor Red
        $testResult = $false
    } else {
        Write-Host "Windows App Runtime components are registered." -ForegroundColor Green
    }
} catch {
    Write-Host "Error verifying Windows App Runtime installation: $_" -ForegroundColor Red
    $testResult = $false
}

if ($testResult) {
    Write-Host ""
    Write-Host "✅ All dependencies are installed correctly!" -ForegroundColor Green
    Write-Host "You can now run RestaurantPOS.exe" -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "⚠️ Some dependencies might not be installed correctly." -ForegroundColor Yellow
    Write-Host "If you encounter issues, please try the following:" -ForegroundColor Yellow
    Write-Host "1. Ensure Windows is up to date" -ForegroundColor Yellow
    Write-Host "2. Install Microsoft Visual C++ Redistributable from: https://aka.ms/vs/17/release/vc_redist.x64.exe" -ForegroundColor Yellow
    Write-Host "3. Try manually installing Windows App SDK from: https://aka.ms/windowsappsdk/1.5/1.5.240802000/windowsappruntimeinstall-x64.exe" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
