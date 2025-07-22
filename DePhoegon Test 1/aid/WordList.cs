using System;
namespace DePhoegon.aid;

public class WordList {
    private static readonly string[] Coding_Word = [
        "strings", "algorithm", "autonomous", "binary numbers", "arrays", "argument", "assignment operators", "java", "javascript", "powershell", "rust", "htmlx", "class", "static", "void", "object"
    ];

    private static readonly string[] Food_Word = [
        "cookie", "ginger snap", "toast", "burrito", "carrot cake", "cabbage roll", "blue cheese", "brie", "mozzarella", "congee", "wonton", "apple", "cantaloupe", "pineapple", "kiwi", "avocado", "popcorn", "cereal", "rice", "blue moon", "moon mist", "moose tracks", "scrambled eggs", "soy milk", "cow milk", "almond milk", "onion rings", "carbonara", "lasagna", "bolognese", "pudding", "margherita", "key lime pie", "hamburgers", "clam chowder", "sinigang", "sushi", "jamaican patty", "roti canai"
    ];

    private static readonly string[] Animal_Word = [
        "goats", "abyssinian", "aegean", "american bobtail", "american shorthair", "american wirehair", "aphrodite giant", "australian mist", "bambino", "bengal", "british shorthair", "british longhair", "brazilian shorthair", "cornish rex", "dwelf", "dragon li", "egyptian mau", "khao manee", "javanese", "colorpoint longhair"
    ];

    private static readonly string[] Game_Word = [
        "prototype", "neoverse", "roguebook", "star realms", "the elder scrolls", "hand of fate", "monster train", "starship troopers", "brotato", "vampire survivers", "backpack battles", "freeways", "palworld", "dorf romantik", "torchlight", "dragon age", "luck be a landlord", "wolfwars", "far cry", "hitman", "hexen", "heretic", "super meat boy", "just cause", "alone in the dark", "call of cthulhu", "call of the sea", "amnesia", "moonring", "song of horror", "hellpoint", "world of warcraft", "fractured space", "star trek", "time of dragons", "transformice", "warframe", "toxikk", "shiness", "defiance", "cry of fear", "coromon", "doom", "doom eternal", "draconian wars", "dragomon hunter", "doctor who", "dungeon siege", "dusk"
    ];

    private static readonly string[] Favorite_Word = [
        "chocobo", "dragon", "final fantasy", "dephoegon", "beat hazzard", "elden ring", "demons souls", "dark souls", "beltmatic", "xcom", "wasteland", "grid", "flatout", "audiosurf", "patches", "nacho", "corgi", "kings bounty", "oddworld", "painkiller", "perimeter", "adventurequest", "ghost", "america", "mountain dew", "ice cream", "chocolate", "swiss cheese"
    ];

    private static readonly string[] Color_Word = [
        "orange", "pink", "almond", "amethyst", "aquamarine", "azure", "aureolin", "beaver", "bittersweet", "bazaar", "black", "blound", "blue", "lime", "bistre", "beige", "bone", "burgundy", "burnt orange", "alice blue", "american rose", "antique brass", "android green", "baby blue", "atomic tangerine", "baby pink", "blizzard blue", "boston university red", "boysenberry", "brick red", "bright lavender", "british racing green", "cadmium green", "cadet grey", "camouflage green", "carrot orange", "carolina blue", "cerulean blue", "cherry blossom pink", "cerise pink", "cornflower", "cornflower blue", "crimson", "crimson glory", "dandelion", "dark candy apple red", "dark jungle green", "dark khaki", "dollar bill", "electric lavender", "electric purple", "ferrari red", "fluorexcent orange", "ghost white"
    ];
    private static readonly string[] Words = [.. Coding_Word, .. Food_Word, .. Animal_Word, .. Game_Word, .. Favorite_Word, .. Color_Word];

    public static string GetRandomWord(Random rand) {
        int firstIndex = rand.Next(Words.Length);
        string firstWord = Words[firstIndex];

         return firstWord; 
    }

    public static string GetCategoryName(string word) {
        if (Coding_Word.Contains(word, StringComparer.OrdinalIgnoreCase)) return nameof(Coding_Word);
        if (Food_Word.Contains(word, StringComparer.OrdinalIgnoreCase)) return nameof(Food_Word);
        if (Animal_Word.Contains(word, StringComparer.OrdinalIgnoreCase)) return nameof(Animal_Word);
        if (Game_Word.Contains(word, StringComparer.OrdinalIgnoreCase)) return nameof(Game_Word);
        if (Favorite_Word.Contains(word, StringComparer.OrdinalIgnoreCase)) return nameof(Favorite_Word);
        if (Color_Word.Contains(word, StringComparer.OrdinalIgnoreCase)) return nameof(Color_Word);
        return "Invalid Word";
    }
}