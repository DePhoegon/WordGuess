using System;
using System.CodeDom;
using System.Globalization;
using System.Runtime.CompilerServices;

public class HangmanDrawing {
    private static int styles = 1;
    private static final int StylesCount = 4;
    private static final int linestyles = 4;
    private static int lineStyle = 1;
    public HangmanDrawing()
	{
	}
    public static int getStyleCount() { return StylesCount; }
    public static int getLineStyles() { return linestyles; }
    private static string[] TopGallows = [3];
    private static string BarePost = "";
    private static string BaseGallows = "";
    private static Dictionary<int, string> BodyParts = new();

    public static void setStyle(int style) {
        if (style < 1) { style = 1; }
        if (style > StylesCount) { style = StylesCount; }
        styles = style; 
        setStrings();
    }
    public static void setLineStyle(int line) {
        if (line < 1) { line = 1; }
        if (line > linestyles) { line = linestyles; }
        lineStyle = line;
    }
    private static void setStrings() {
        Dictionary<int, string> tempStyle;
        switch (styles) {
            case 1: { tempStyle = StyleOne; break; }
            case 2: { tempStyle = StyleTwo; break; }
            case 3: { tempStyle = StyleThree; break; }
            case 4: { tempStyle = StyleFour; break; }
            default: { tempStyle = StyleOne; break; }
        }
        TopGallows[0] = tempStyle[0];
        TopGallows[1] = tempStyle[1];
        TopGallows[2] = tempStyle[2];
        BarePost = tempStyle[3];
        BaseGallows = tempStyle[4];
        BodyParts.Clear();
        for (int i = 5; i <= 11; i++) { BodyParts[i - 5] = tempStyle[i]; } // Maps Body parts 0-6
    }
    public static void DrawStand(int chances) {
        foreach (var line in TopGallows) { Console.WriteLine(line); }
        if (chances >= 1) { Console.WriteLine(BodyParts[0]); }
        else { Console.WriteLine(BarePost); }

        if (chances == 2) { Console.WriteLine(BodyParts[1]); }
        else if (chances == 3) { Console.WriteLine(BodyParts[2]); }
        else if (chances >= 4) { Console.WriteLine(BodyParts[3]); }
        else { Console.WriteLine(BarePost); }

        if (chances == 5) { Console.WriteLine(BodyParts[4]); }
        else { Console.WriteLine(BarePost); }

        if (chances == 6) { Console.WriteLine(BodyParts[5]); }
        else if (chances >= 7) { Console.WriteLine(BodyParts[6]); }
        else { Console.WriteLine(BarePost); }

        Console.WriteLine(BarePost);
        Console.WriteLine(BaseGallows);
    }

    // Styles for the Hangman drawing
    private static readonly Dictionary<int, string> StyleOne = new() {
        { 0, "  ||=======" }, // Top
        { 1, "  ||    !   " }, // Top
        { 2, "  ||    ¡   " }, // Top
        { 3, "  ||" }, // Bare Post
        { 4, "##########" }, // Base
        { 5, "  ||    O" }, // Head
        { 6, "  ||   /" }, // L Arm
        { 7, "  ||   /\\" }, // Arms
        { 8, "  ||   /|\\" }, // Armsfull
        { 9, "  ||    |" }, // Middle
        { 10, "  ||   /" }, // L Leg
        { 11, "  ||   / \\" } // Legs
    };
    private static readonly Dictionary<int, string> StyleTwo = new() {
        { 0, "" }, // Top
        { 1, "  +------+" }, // Top
        { 2, "  |      |   " }, // Top
        { 3, "  |" }, // Bare Post
        { 4, "_-======--__" }, // Base
        { 5, "  |      O" }, // Head
        { 6, "  |     /" }, // L Arm
        { 7, "  |     /\\" }, // Arms
        { 8, "  |     /|\\" }, // Armsfull
        { 9, "  |      |" }, // Middle
        { 10, "  |     /" }, // L Leg
        { 11, "  |     / \\" } // Legs
    };
    private static readonly Dictionary<int, string> StyleThree = new() {
        { 0, "" }, // Top
        { 1, "  ||======" }, // Top
        { 2, "  ||    !   " }, // Top
        { 3, "  ||" }, // Bare Post
        { 4, "#####" }, // Base
        { 5, "  ||    O" }, // Head
        { 6, "  ||   /" }, // L Arm
        { 7, "  ||   /\\" }, // Arms
        { 8, "  ||   /|\\" }, // Armsfull
        { 9, "  ||    |" }, // Middle
        { 10, "  ||   /" }, // L Leg
        { 11, "  ||   / \\" } // Legs
    };
    private static readonly Dictionary<int, string> StyleFour = new() {
        { 0, "" }, // Top
        { 1, "  ~~~~~~~~" }, // Top
        { 2, "  {}    :   " }, // Top
        { 3, "  {}" }, // Bare Post
        { 4, "_-▔▔▔▔▔▔▔▔--__" }, // Base
        { 5, "  {}    O" }, // Head
        { 6, "  {}   /" }, // L Arm
        { 7, "  {}   /\\" }, // Arms
        { 8, "  {}   /|\\" }, // Armsfull
        { 9, "  {}    |" }, // Middle
        { 10, "  {}   /" }, // L Leg
        { 11, "  {}   / \\" } // Legs
    };

    // Line breaks for visual separation
    public static string getLineBreak() { 
        switch (lineStyle) {
            case 1: return lineBreak1;
            case 2: return lineBreak2;
            case 3: return lineBreak3;
            case 4: return lineBreak4;
            default: return lineBreak1;
        }
    }
    private static readonly string lineBreak1 = "----------------------------------------";
    private static readonly string lineBreak2 = "========================================";
    private static readonly string lineBreak3 = "|--------------------------------------|";
    private static readonly string lineBreak4 = "~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~";
}