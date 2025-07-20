// See https://aka.ms/new-console-template for more information

class WordGuess {
    public const int intendedWidth = 50;
    public const int intendedHeight = 50;
    public const int hangChances = 6;

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
        for (int i = 0; i < wordListCount; i++) { wList[i] = GetWord(); }
    }
    static string GetWord() {
        string[] word = [
        // coding related
        "strings", "algorithm", "autonomous", "binary numbers", "arrays", "argument", "assignment operators", "java", "javascript", "powershell", "rust", "htmlx", "class", "static", "void", "object"

            // food related
            , "cookie", "ginger snap", "toast", "burrito", "carrot cake", "cabbage roll", "blue cheese", "brie", "mozzarella", "congee", "wonton", "apple", "cantaloupe", "pineapple", "kiwi", "avocado", "popcorn", "cereal", "rice", "blue moon", "moon mist", "moose tracks", "scrambled eggs", "soy milk", "cow milk", "almond milk", "onion rings", "carbonara", "lasagna", "bolognese", "pudding", "margherita", "key lime pie", "hamburgers", "clam chowder", "sinigang", "sushi", "jamaican patty", "roti canai"

            // animal related
            , "goats", "abyssinian", "aegean", "american bobtail", "american shorthair", "american wirehair", "aphrodite giant", "australian mist", "bambino", "bengal", "british shorthair", "british longhair", "brazilian shorthair", "cornish rex", "dwelf", "dragon li", "egyptian mau", "khao manee", "javanese", "colorpoint longhair" 

            // game related
            , "prototype", "neoverse", "roguebook", "star realms", "the elder scrolls", "hand of fate", "monster train", "starship troopers", "brotato", "vampire survivers", "backpack battles", "freeways", "palworld", "dorf romantik", "torchlight", "dragon age", "luck be a landlord", "wolfwars", "far cry", "hitman", "hexen", "heretic", "super meat boy", "just cause", "alone in the dark", "call of cthulhu", "call of the sea", "amnesia", "moonring", "song of horror", "hellpoint", "world of warcraft", "fractured space", "star trek", "time of dragons", "transformice", "warframe", "toxikk", "shiness", "defiance", "cry of fear", "coromon", "doom", "doom eternal", "draconian wars", "dragomon hunter", "doctor who", "dungeon siege", "dusk"

            // Personal favorite related
            , "chocobo", "dragon", "final fantasy", "dephoegon", "beat hazzard", "elden ring", "demons souls", "dark souls", "beltmatic", "xcom", "wasteland", "grid", "flatout", "audiosurf", "patches", "nacho", "corgi", "kings bounty", "oddworld", "painkiller", "perimeter", "adventurequest", "ghost", "america", "mountain dew", "ice cream", "chocolate", "swiss cheese"

            // color related
            , "orange", "pink", "almond", "amethyst", "aquamarine", "azure", "aureolin", "beaver", "bittersweet", "bazaar", "black", "blound", "blue", "lime", "bistre", "beige", "bone", "burgundy", "burnt orange", "alice blue", "american rose", "antique brass", "android green", "baby blue", "atomic tangerine", "baby pink", "blizzard blue", "boston university red", "boysenberry", "brick red", "bright lavender", "british racing green", "cadmium green", "cadet grey", "camouflage green", "carrot orange", "carolina blue", "cerulean blue", "cherry blossom pink", "cerise pink", "cornflower", "cornflower blue", "crimson", "crimson glory", "dandelion", "dark candy apple red", "dark jungle green", "dark khaki", "dollar bill", "electric lavender", "electric purple", "ferrari red", "fluorexcent orange", "ghost white"
        ]; // manual word list
            Random rand = new();
            int firstIndex = rand.Next(0, word.Length);
            string firstWord = word[firstIndex];

            // 50% chance to add a second word
            if (rand.Next(2) == 0)
            {
                int secondIndex;
                do
                {
                    secondIndex = rand.Next(0, word.Length);
                } while (secondIndex == firstIndex); {
                string secondWord = word[secondIndex];
                return firstWord + " " + secondWord; }
            } else { return firstWord; }
        
    }
    static void DrawBoard(char[] answer, char[] alphabet, int chances) {
        Console.Clear();
        DrawStand(answer, alphabet, chances);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Guessed letters:");
        DrawGuessedAnswer(answer);
        Console.WriteLine();
        Console.WriteLine("Available letters:");
        DrawAvailableLetters(alphabet, guessedChars);
        Console.WriteLine($"Chances left: {hangChances - chances}");
        Console.WriteLine("Enter a letter to guess:");
    }
    static void DrawStand(char[] answer, char[] alphabet, int chances) {
        foreach (var line in TopGallows) { Console.WriteLine(line); }
        if (chances >= 1) { Console.WriteLine(BodyParts[1]); }
        else { Console.WriteLine(BarePost); }

        if (chances == 2) { Console.WriteLine(BodyParts[2]); }
        else if (chances == 3) { Console.WriteLine(BodyParts[3]); }
        else if (chances >= 4) { Console.WriteLine(BodyParts[4]); }
        else { Console.WriteLine(BarePost); }
        
        if (chances == 5) { Console.WriteLine(BodyParts[5]); }
        else { Console.WriteLine(BarePost); }
        
        if (chances == 6) { Console.WriteLine(BodyParts[6]); }
        else if (chances >= 7) { Console.WriteLine(BodyParts[7]); }
        else { Console.WriteLine(BarePost); }
        
        Console.WriteLine(BarePost);
        Console.WriteLine(BaseGallows);
    }
    static void DrawGuessedAnswer(char[] guessed) {
        // Print the guessed answer, grouping by word and spacing for readability
        // Example: "_ _ _ _   _ _ _"
        Console.Write("|| ");
        for (int i = 0; i < guessed.Length; i++) {
            if (guessed[i] == ' ') { Console.Write($"   "); }
            else { Console.Write($"{guessed[i]} "); }
        }
        Console.WriteLine("||");
        Console.WriteLine();
    }
    static void DrawAvailableLetters(char[] letters, char[] guessedChars) {
        // US English letters, split into 8, 10, 8 pattern
        int[] rowSizes = { 8, 10, 8 };
        int letterIndex = 0;

        for (int row = 0; row < rowSizes.Length; row++) {
            for (int col = 0; col < rowSizes[row]; col++)
            {
                if (letterIndex >= letters.Length) break;
                char letter = letters[letterIndex];
                // If letter has been guessed, show '_', else show the letter
                if (Array.IndexOf(guessedChars, letter) != -1) { Console.Write("_ "); }
                else { Console.Write($"{letter} "); }
                letterIndex++;
            }
            Console.WriteLine();
        }
    }
    private static readonly String[] TopGallows = [
        "  ||======",
        "  ||    !", 
        "  ||    ¡", 
    ];
    private static readonly String BarePost = "  ||";
    private static readonly String BaseGallows = "##########";
    private static readonly Dictionary<int, string> BodyParts = new()
    {
        { 1, "  ||    O" }, // Head
        { 2, "  ||   /" }, // L Arm
        { 3, "  ||   /\\" }, // Arms
        { 4, "  ||   /|\\" }, // Armsfull
        { 5, "  ||    |" }, // Middle
        { 6, "  ||   /" }, // L Leg
        { 7, "  ||   / \\" } // Legs
    };
    private static bool SetGuessedChars(char letter) {
        guessedChars ??= [];
        letter = char.ToLowerInvariant(letter);

        if (Array.IndexOf(guessedChars, letter) != -1) { return false; }

        Array.Resize(ref guessedChars, guessedChars.Length + 1);
        guessedChars[^1] = letter;

        Array.Sort(guessedChars);
        return true;
    }

    // char.Parse(Console.ReadLine());
    // Console.WriteLine("");
    // commands for reference
}