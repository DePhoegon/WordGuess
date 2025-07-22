# WordGuess

WordGuess is a standalone terminal-based word guessing game (similar to Hangman) for Windows. The game is designed for .NET 8 and runs as a native Windows executable that will launch a new terminal/cmd window for play.

## Warning

**Note:** Running WordGuess in *Windows Terminal* (wt.exe) may cause visual issues such as display glitches.

Manually resizing the terminal window will properly *center* ther playfield each 'draw;.

## Features

- **No .NET installation required at runtime:** Just run the game using the provided command.
- ~~**Automatic terminal window management:** Launches or resizes the terminal for gameplay.~~
--- Manual resizing is required for proper centering in Windows Terminal. (See Warning above.)
--- Initial cmd window resizing has been removed to avoid issues with Windows Terminal.
- **Random word selection** from a variety of categories.
- **Simple and fun gameplay** in the Windows terminal.

## Word Categories

Words are drawn from these general categories:
- **Coding & Programming Terms**
- **Food & Drinks**
- **Animals**
- **Games & Game Titles**
- **Favorites & Miscellaneous**
- **Colors**

## How to Play

1. Open a terminal window.
2. Run the game with: 
```
dotnet run --project "DePhoegon Test 1"
dotnet run -c Release --project "DePhoegon Test 1"
```
   (No need to build or publish separately.)
3. A word from one of the categories will be chosen at random.
4. Guess letters one at a time.
5. Each incorrect guess brings you closer to losing.
6. Guess the word before you run out of attempts!

## Building the Solution

If you want to build the solution manually (for example, to create the executable):

1. Open a terminal in the repository root.
2. Build the solution with:  Debug or Release configuration:
```
dotnet build 
dotnet build -c Release
```

This will compile the project and place the output in the `bin\Debug\net8.0-windows10.0.26100.0\` (or `bin\Release\...`) directory.

3. To run the built executable directly:
- Navigate to the output directory (e.g., `bin\Debug\net8.0-windows10.0.26100.0\`).
- Run:
  ```
  DePhoegon's HangMan.exe
  ```
- The game will launch in a terminal window.

## Requirements

- Windows 10 or later
- .NET 8 SDK (for running with `dotnet run` or building the solution)

## License

See [LICENSE.md](LICENSE.md) for details.

---

Enjoy the challenge!