// See https://aka.ms/new-console-template for more information
using System;
class WordGuess {
    static void Main() { 
        Console.WriteLine("Guess Word Game!");
        int count = 10;
        int hangChances = 6;
        string[] wList;
        char[] letters;
        string answer;
        string guessedAnswer;
        int answerLength;
        
        // Initialize the game
        gameInit(out answer, out answerLength, out guessedAnswer, out wList, out letters, count, hangChances);

        drawMan(answer, letters, hangChances);
        setWindow();

        while (/* game not Over */) { ... }

        
    }
    static void setWindow() {
        if (OperatingSystem.IsWindows()) {
            Console.SetWindowSize(50, 50);
            Console.BufferHeight = 50;
            Console.BufferWidth = 50;
        }
        if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux()) { Console.Write("\x1b[8;50;50t"); }
    }
    static void gameInit(out string selection, out int selectionLength, out string guessed, out string[] wordlist, out char[] alphabet, int count, int hangs)
    {
        setWordList(out wordlist, out alphabet, count);
        Random random = new();
        selection = wordlist[random.Next(0, wordlist.Length)];
        drawMan(selection, alphabet, hangs);
        selectionLength = selection.Length;
        guessed = "";
        for (int i = 0; i < selection.Length; i++)
        {
            char c = selection[i];
            if (c == ' ') { guessed += " "; selectionLength--; } else { guessed += "_"; }
        }
    }
    static void setWordList(out string[] wordlist, out char[] letterList, int count)
    {
        wordlist = new string[count];
        for (int i = 0; i < count; i++) { wordlist[i] = getWord(); }
        letterList = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    }
    static string getWord()
    {
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
        var rword = rand.Next(0, word.Length);
        return word[rword];
    }
    static void drawMan(string answer, char[] alphabet, int chances)
    {

    }
    // char.Parse(Console.ReadLine());
    // Console.WriteLine("");
    // commands for reference
}