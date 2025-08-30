# Android APK Signing Guide

Since you're having issues with the keystore creation and "could not parse installation package" error, follow these steps to build and sign your APK:

## Option 1: Using Visual Studio

1. Open your solution in Visual Studio
2. Right-click on the project and select "Properties"
3. Go to "Android Package Signing"
4. Check "Sign the .APK file"
5. Click "Create" to create a new keystore file
   - Use "restaurantpos" as the alias
   - Use "restaurant123" as both passwords (or your preferred password)
   - Fill in the remaining details
6. Click OK to save
7. Build the app in Release mode by going to Build > Configuration Manager and selecting "Release"
8. Build the project (Build > Build Solution)
9. Find your APK in the bin/Release/net8.0-android directory

## Option 2: Using Command Line (.NET CLI)

If you want to build from the command line without needing Java/keytool directly:

1. Open PowerShell in your project directory
2. Run this command to build a release version with a debug signing key:

```powershell
dotnet build -c Release -f net8.0-android /p:AndroidPackageFormat=apk
```

3. For a properly signed release APK (once you have the keystore), use:

```powershell
dotnet publish -c Release -f net8.0-android /p:AndroidPackageFormat=apk /p:AndroidKeyStore=true /p:AndroidSigningKeyStore=restaurantpos.keystore /p:AndroidSigningStorePass=restaurant123 /p:AndroidSigningKeyAlias=restaurantpos /p:AndroidSigningKeyPass=restaurant123
```

4. Find your APK in the bin/Release/net8.0-android directory

## Option 3: Using the Debug APK

If you just need to test on a device and don't need a release APK yet:

1. Build in Debug mode
2. Enable "Developer Options" and "USB debugging" on your Android device
3. Connect your device via USB
4. In Visual Studio, select your device from the debug target dropdown
5. Press F5 to debug directly on device

## When installing the APK directly:

Make sure to:
1. Enable "Install from unknown sources" in your Android device settings
2. Use a file manager app on the device to navigate to where you copied the APK
3. Tap to install

If you still get "could not parse installation package" with the signed APK, check that:
- The APK isn't corrupted during transfer to the device
- You're using an APK built for the correct CPU architecture (arm64-v8a, armeabi-v7a, etc.)
- Your Android device meets the minimum API level specified (API 21 in your case)
