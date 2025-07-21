// See https://aka.ms/new-console-template for more information
using DePhoegon.aid;

namespace DePhoegon;
class WordGuess {

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
    static bool gameNotOver = true;
    static bool gameWon = false;

    static void Main() {
        if (!OperatingSystem.IsWindows()) { Environment.Exit(0); } // Temporary fix for non-Windows systems, not intended to run on Linux or MacOS
        Helper.SetWindow();
        PlayGameEntry();
    }
    private static void PlayGameEntry() {
        GameInit();
        GameLoop();
        GameOver(gameWon);
    }
    private static void GameLoop() {
        while (gameNotOver) {
            if (hangCount >= Helper.hangChances) { gameNotOver = false; gameWon = false; break; }
            if (Array.IndexOf(guessedAnswer, '_') == -1) { gameNotOver = false; gameWon = true; break; }
            Helper.CenterPadHeight();
            DrawBoard(guessedChars, guessedAnswer, alphabet, hangCount);
            Console.Write("Enter a letter to guess: ");
            char input = Console.ReadKey(true).KeyChar;
            input = char.ToLowerInvariant(input);
            Random rest = new();
            if (SetGuessedChars(input)) {
                if (Array.IndexOf(alphabet, input) == -1) { 
                    Helper.CenterPadWidthWithString($"Invalid input: '{input}' is not a letter.", 0);
                    Thread.Sleep(rest.Next(1000, 2501)); 
                    continue; 
                }
                if (LettersGuessed(input)) { Helper.CenterPadWidthWithString($"Good guess! '{input}' is in the word!", 0); }
                else { Helper.CenterPadWidthWithString($"Sorry, '{input}' is not in the word. You have {Helper.hangChances - hangCount} chances left.", 0); }
            } else {
                Helper.CenterPadWidthWithString("You already guessed that letter! Try again.",0);
                Thread.Sleep(rest.Next(750, 1501));
                continue;
            }
            int restMS = rest.Next(500, 1501);
            Thread.Sleep(restMS);
            if (answer == guessedAnswer) {
                gameNotOver = false;
                Console.Clear();
                DrawBoard(guessedChars, guessedAnswer, alphabet, hangCount);
                Helper.CenterPadWidthWithString("You guessed the word!", 0);
                Helper.CenterPadWidthWithString("Congratulations! You've guessed the word!", 0);
                Helper.CenterPadWidthWithString($"The word was: {new string(answer)}", 0);
            }
        }
    }
    private static void GameOver(bool won) {
        Console.Clear();
        DrawBoard(guessedChars, guessedAnswer, alphabet, hangCount);
        if (won) {
            Helper.CenterPadWidthWithString("Congratulations! You've guessed the word!", 0);
            Helper.CenterPadWidthWithString($"The word was: {new string(answer)}", 0);
        } else {
            Helper.CenterPadWidthWithString("Game Over! You've run out of chances.", 0);
            Helper.CenterPadWidthWithString($"The word was: {new string(answer)}", 0);
            Helper.CenterPadWidthWithString("Better luck next time!", 0);
        }
        Helper.CenterPadWidthWithString("Press any key to continue, or q to Quit", 0);
        char input = Console.ReadKey(true).KeyChar;
        input = char.ToLowerInvariant(input);
        if (input == 'q') {
            Helper.CenterPadWidthWithString("Thank you for playing!", 0);
            Random sleepRand = new();
            int sleepMs = sleepRand.Next(1500, 4001);
            Thread.Sleep(sleepMs);
            Environment.Exit(0); 
        } else { GameInit(); GameLoop(); }

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
    static void DrawBoard(char[] guessedChar, char[] guessedAnswer, char[] alphabet, int chances) {
        Console.Clear();
        // padding for the hangman drawing & guessed letters string
        int hangPadding = (Helper.intendedWidth - 20) / 2;
        HangmanDrawing.DrawStand(chances, hangPadding);
        Console.WriteLine();
        Console.WriteLine(); 
        Helper.CenterPadWidthWithString("Guessed letters:", hangPadding);
        DrawGuessedAnswer(guessedAnswer);
        Helper.CenterPadWidthWithString(HangmanDrawing.GetLineBreak(), 0);
        Helper.CenterPadWidthWithString("Available letters:", hangPadding);
        DrawAvailableLetters(alphabet, guessedChar);
        Helper.CenterPadWidthWithString($"Chances left: {Helper.hangChances - chances}", hangPadding);
        Console.WriteLine();
    }
    static void DrawGuessedAnswer(char[] guessed) {
        // Example: "_ _ _ _   _ _ _"
        // Calculate padding to center the guessed answer  +1 for the left space, +4 for the '|| ' and ' ||' at the end
        int padding = (Helper.intendedWidth - ((guessed.Length * 2) + 1 + 4)) / 2; // negative padding is not allowed, checked in Helper.Padder 
        string guessing = "|| ";
        for (int i = 0; i < guessed.Length; i++) {
            if (guessed[i] == ' ') { guessing += "   "; }
            else { guessing = $"{guessed[i]} "; }
        }
        guessing = "||";
        Helper.CenterPadWidthWithString(guessing, padding);
        Console.WriteLine();
    }
    static void DrawAvailableLetters(char[] letters, char[] guessedChars) {
        // US English letters, split into 8, 10, 8 pattern
        int[] rowSizes = { 8, 10, 8 };
        int letterIndex = 0;
        // padding to center the letters, based on the middle row size, + 2 spacing, +-0 moving left/right - is left
        int padding = (Helper.intendedWidth - (((rowSizes[1] * 2) - 1) + 2))/2 - 0;// negative padding is not allowed, checked in Helper.Padder
        string lettersString = "";
        for (int row = 0; row < rowSizes.Length; row++) {
            if (row == 0 || row == 2) { lettersString += "  "; } 
            for (int col = 0; col < rowSizes[row]; col++) {
                if (letterIndex >= letters.Length) break;
                char letter = letters[letterIndex];

                if (Array.IndexOf(guessedChars, letter) != -1) { lettersString += "_ "; }
                else { lettersString += $"{letter} "; }
                letterIndex++;
            }
            Helper.CenterPadWidthWithString(lettersString, padding);
            Console.WriteLine();
        }
    }
    private static bool SetGuessedChars(char letter) {
        guessedChars ??= [];
        letter = char.ToLowerInvariant(letter);

        if (Array.IndexOf(guessedChars, letter) != -1) { return false; }
        Array.Resize(ref guessedChars, guessedChars.Length + 1);
        guessedChars[^1] = letter; 

        Array.Sort(guessedChars); 
        return true;
    }
}