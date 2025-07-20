// See https://aka.ms/new-console-template for more information
class WordGuess {
    public const int intendedWidth = 50;
    public const int intendedHeight = 50;
    public const int hangChances = 6;
    private static int defaultstyle = 1; // Default style for the hangman drawing

    // Game state variables (static for static methods)
    static char[] guessedChars = [];
    static int wordListCount = 10;
    static string[] wList = [];
    static char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    static char[] answer = [];
    static char[] guessedAnswer = [];
    static int answerLength;
    static int hangCount = 0;

    static void Main() {
        SetWindow();
        Console.WriteLine("Guess Word Game!");
        
        // Initialize the game
        GameInit();

        DrawBoard(answer, alphabet, 0);

        while (hangCount < hangChances) {
            // get user input
            DrawBoard(answer, alphabet, hangCount);
            // wait for input
            char input = Console.ReadKey(true).KeyChar;
            input = char.ToLowerInvariant(input);
            if (SetGuessedChars(input)) {
                if (lettersGuessed(input)) { Console.WriteLine($"Good guess! '{input}' is in the word!"); }
                else {
                    hangCount++;
                    Console.WriteLine($"Sorry, '{input}' is not in the word. You have {hangChances - hangCount} chances left.");
                }
            } else {
                Console.WriteLine("You already guessed that letter! Try again.");
                continue; 
            }
        }
    }
    private static bool lettersGuessed(char guessed) {
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
    static void GameInit() {
        SetWordList();
        int style = Random.Shared.Next(1, HangmanDrawing.getStyleCount() + 1);
        int lineStyle = Random.Shared.Next(1, HangmanDrawing.getLineStyles() + 1);
        HangmanDrawing.setStyle(style);
        HangmanDrawing.setLineStyle(lineStyle);
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
    static void DrawBoard(char[] answer, char[] alphabet, int chances) {
        Console.Clear();
        HangmanDrawing.DrawStand(chances);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Guessed letters:");
        DrawGuessedAnswer(answer);
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