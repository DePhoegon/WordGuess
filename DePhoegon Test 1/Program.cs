// See https://aka.ms/new-console-template for more information
using DePhoegonHangMan.aid;

namespace DePhoegonHangMan;
class WordGuess {
    private const int intendedWidth = 50;
    private const int intendedHeight = 50;
    private const int hangChances = 6;

    // Game state variables (static for static methods)
    static char[] guessedChars = [];
    static readonly int wordListCount = 10;
    static string[] wList = [];
    static readonly char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    static char[] answer = [];
    static char[] guessedAnswer = [];
    static int answerLength;
    static int hangCount = 0;
    static int style = 0;
    static int lineStyle = 0;

    static void Main() {
        SetWindow();
        Console.WriteLine("Guess Word Game!");
        bool notWon = true;

        // Initialize the game
        GameInit();

        while (hangCount < hangChances && notWon) { 
            DrawBoard(answer, guessedChars, guessedAnswer, alphabet, hangCount);
            Console.Write("Enter a letter to guess: ");
            char input = Console.ReadKey(true).KeyChar;
            input = char.ToLowerInvariant(input);
            if (SetGuessedChars(input)) {
                if (Array.IndexOf(alphabet, input) == -1) {  }
                if (LettersGuessed(input)) { Console.WriteLine($"Good guess! '{input}' is in the word!"); }
                else {
                    hangCount++;
                    Console.WriteLine($"Sorry, '{input}' is not in the word. You have {hangChances - hangCount} chances left.");
                }
            } else {
                Console.WriteLine("You already guessed that letter! Try again.");
                continue; 
            }
            // Sleep for 2-5 seconds after each guess
            Random rest = new();
            int restMS = rest.Next(1000, 3001);
            Thread.Sleep(restMS);
            if (answer == guessedAnswer) {
                notWon = false;
                Console.Clear();
                DrawBoard(answer, guessedChars, guessedAnswer, alphabet, hangCount);
                Console.WriteLine("Congratulations! You've guessed the word!");
                Console.WriteLine($"The word was: {new string(answer)}");
            }
        }
        if (hangCount >= hangChances) {
            Console.Clear();
            DrawBoard(answer, guessedChars, guessedAnswer, alphabet, hangCount);
            Console.WriteLine("Game Over! You've run out of chances.");
            Console.WriteLine($"The word was: {new string(answer)}");
            Console.WriteLine("Better luck next time!");
            Console.Write(LettersGuessed(' ') ? "You guessed the word!" : "You didn't guess the word.");
            char input = Console.ReadKey(true).KeyChar;
            SetOrShiftStyles();
            hangCount = 0;
        }

        if (!notWon)
        {
            Console.WriteLine("Press q key to exit...");
            char input = Console.ReadKey(true).KeyChar;
            input = char.ToLowerInvariant(input);
            if (input == 'q') { return; } // Exit the game if 'q' is pressed
            SetOrShiftStyles();
            hangCount = 0; // Reset hang count for next game
            GameInit(); // Reinitialize the game
        }
        Console.WriteLine("Thank you for playing!");
        Random sleepRand = new();
        int sleepMs = sleepRand.Next(2000, 5001);
        Thread.Sleep(sleepMs);

    }
    private static bool LettersGuessed(char guessed) {
        bool found = false;
        for (int i = 0; i < answer.Length; i++) {
            if (char.ToLowerInvariant(answer[i]) == guessed && guessedAnswer[i] == '_') {
                found = true;
                guessedAnswer[i] = guessed;
            }
        }
        if (!found && guessed != ' ') { hangCount++; }
        return found;
    }
    static void SetWindow() {
        if (OperatingSystem.IsWindows()) {
            Console.SetWindowSize(width: intendedWidth, height: intendedHeight);
            Console.BufferHeight = intendedHeight;
            Console.BufferWidth = intendedWidth;
        }
        if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux()) { Console.Write($"\x1b[8;{intendedHeight};{intendedWidth}t"); }
    }
    private static void SetOrShiftStyles() {
        style = HangmanDrawing.GetStyleInt(style);
        lineStyle = HangmanDrawing.GetLineStyleInt(lineStyle);
    }
    static void GameInit() {
        SetWordList();
        style = 0; // Reset style to 0 to ensure it gets set correctly
        lineStyle = 0; // Reset line style to 0 to ensure it gets set correctly
        SetOrShiftStyles();
        Random random = new();
        string answerString = wList[random.Next(0, wList.Length)];
        answer = answerString.ToCharArray();
        answerLength = answer.Length;
        guessedAnswer = new char[answerLength];
        for (int i = 0; i < answerLength; i++) {
            if (answer[i] != ' ') { guessedAnswer[i] = '_'; } else { guessedAnswer[i] = answer[i]; }
        }
    }
    static void SetWordList() {
        wList = new string[wordListCount];
        Random rand = new();
        for (int i = 0; i < wordListCount; i++) { wList[i] = WordList.GetRandomWord(rand); }
    }
    static void DrawBoard(char[] realAnser, char[] guessedChar, char[] guessedAnswer, char[] alphabet, int chances) {
        Console.Clear();
        HangmanDrawing.DrawStand(chances);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Guessed letters:");
        DrawGuessedAnswer(guessedAnswer);
        Console.WriteLine(HangmanDrawing.getLineBreak());
        Console.WriteLine("Available letters:");
        DrawAvailableLetters(alphabet, guessedChars);
        Console.WriteLine($"Chances left: {hangChances - chances}");
        Console.WriteLine("Enter a letter to guess:");
    }
    static void DrawGuessedAnswer(char[] guessed) {
        // Example: "_ _ _ _   _ _ _"
        Console.Write("|| ");
        for (int i = 0; i < guessed.Length; i++) {
            if (guessed[i] == ' ') { Console.Write($"   "); }
            else { Console.Write($"{guessed[i]} "); }
        }
        Console.WriteLine("||"); // Print the guessed answer with '||' on both sides, Ends in new line
        Console.WriteLine();
    }
    static void DrawAvailableLetters(char[] letters, char[] guessedChars) {
        // US English letters, split into 8, 10, 8 pattern
        int[] rowSizes = { 8, 10, 8 };
        int letterIndex = 0;

        for (int row = 0; row < rowSizes.Length; row++) {
            if (row == 0 || row == 2) { Console.Write("  "); } // Indent Firt and last row to 'center' the letters
            for (int col = 0; col < rowSizes[row]; col++) {
                if (letterIndex >= letters.Length) break;
                char letter = letters[letterIndex];

                if (Array.IndexOf(guessedChars, letter) != -1) { Console.Write("_ "); } // Use '_' for guessed letters
                else { Console.Write($"{letter} "); }
                letterIndex++;
            }
            Console.WriteLine();
        }
    }
    private static bool SetGuessedChars(char letter) {
        guessedChars ??= [];
        letter = char.ToLowerInvariant(letter);

        if (Array.IndexOf(guessedChars, letter) != -1) { return false; }

        Array.Resize(ref guessedChars, guessedChars.Length + 1);
        guessedChars[^1] = letter; // Add the new letter to the guessed characters, at the end of the array

        Array.Sort(guessedChars); // sort the guessed characters array, Speed up the search for guessed letters as it srts with the first letter from the alphabet
        return true;
    }
}