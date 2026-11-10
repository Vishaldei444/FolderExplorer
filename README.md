# 📁 Folder Explorer

Folder Explorer is a Windows desktop app built with C# and Windows Forms. It displays drives and folders on the left and the selected folder’s files on the right.

## Features

- Browse the `C:\` and `E:\` drives in an expandable TreeView.
- Click `+` beside a drive or folder to view its subfolders.
- Select a folder to see its files in a ListView in Details mode.
- View each file’s **Name**, **Size in bytes**, and **Date Modified**.
- Display a message when a folder is unavailable or access is denied.

## Output Screenshot

![Folder Explorer showing an expanded drive and file details](./FolderExplorer-Output.png)

## How to Run

1. Download or clone the repository on Windows.
2. Open `FolderExplorer.sln` or `FolderExplorer.csproj` in Visual Studio 2022.
3. Press **F5** to run the application.
4. Expand a drive and click a folder name to view its files.

## Technologies Used

- C#
- Windows Forms
- Visual Studio 2022
- `System.IO`
