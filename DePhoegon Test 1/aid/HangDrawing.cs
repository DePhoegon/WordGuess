using System;
using System.CodeDom;
using System.Globalization;
using System.Runtime.CompilerServices;
namespace DePhoegon.aid;

public class HangmanDrawing {
    private static int styles = 1;
    private static readonly int StylesCount = 4;
    private static readonly int linestyles = 4;
    private static int lineStyle = 1;
    public static void PrimeStyleState() {
        SetRandomLineStyle();
        SetRandomStyle();
    }
    public static int GetStyleCount() { return StylesCount; }
    public static int GetStyleInt() { return styles; }
    public static int GetStyleInt(int compared) {
        if (compared < 1) { compared = 1; }
        if (compared > StylesCount) { compared = StylesCount; }
        if (styles == compared) { SetRandomStyle(); }
        return styles;
    }
    public static int GetLineStyles() { return linestyles; }
    public static int GetLineStyleInt() { return lineStyle; }
    public static int GetLineStyleInt(int compared) {
        if (compared < 1) { compared = 1; }
        if (compared > linestyles) { compared = linestyles; }
        if (lineStyle == compared) { SetRandomLineStyle(); }
        return lineStyle;
    }
    private static string[] TopGallows = new string[3];
    private static string BarePost = "";
    private static string BaseGallows = "";
    private static Dictionary<int, string> BodyParts = new();

    public static void SetStyle(int style) {
        if (style < 1) { style = 1; }
        if (style > StylesCount) { style = StylesCount; }
        styles = style; 
        SetStrings();
    }
    private static void SetRandomStyle() {
        styles = Random.Shared.Next(1, StylesCount + 1);
        SetStrings();
    }
    private static void SetRandomLineStyle() { lineStyle = Random.Shared.Next(1, linestyles + 1); }
    public static void SetLineStyle(int line) {
        if (line < 1) { line = 1; }
        if (line > linestyles) { line = linestyles; }
        lineStyle = line;
    }
    private static void SetStrings() {
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
    public static void DrawStand(int chances, int padding) {
        if (padding < 0) { padding = 0; }
        foreach (var line in TopGallows) { Helper.CenterPadWidthWithString(line, padding); }
        if (chances >= 1) { Helper.CenterPadWidthWithString(BodyParts[0], padding); }
        else { Helper.CenterPadWidthWithString(BarePost, padding); }

        if (chances == 2) { Helper.CenterPadWidthWithString(BodyParts[1], padding); }
        else if (chances == 3) { Helper.CenterPadWidthWithString(BodyParts[2], padding); }
        else if (chances >= 4) { Helper.CenterPadWidthWithString(BodyParts[3], padding); }
        else { Helper.CenterPadWidthWithString(BarePost, padding); }

        if (chances == 5) { Helper.CenterPadWidthWithString(BodyParts[4], padding); }
        else { Helper.CenterPadWidthWithString(BarePost, padding); }

        if (chances == 6) { Helper.CenterPadWidthWithString(BodyParts[5], padding); }
        else if (chances >= 7) { Helper.CenterPadWidthWithString(BodyParts[6], padding); }
        else { Helper.CenterPadWidthWithString(BarePost, padding); }
        Helper.CenterPadWidthWithString(BarePost, padding);
        Helper.CenterPadWidthWithString(BaseGallows, padding);
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
    public static string GetLineBreak() {
        return lineStyle switch {
            1 => lineBreak1,
            2 => lineBreak2,
            3 => lineBreak3,
            4 => lineBreak4,
            _ => lineBreak1,
        };
    }
    private static readonly string lineBreak1 = "----------------------------------------";
    private static readonly string lineBreak2 = "========================================";
    private static readonly string lineBreak3 = "|--------------------------------------|";
    private static readonly string lineBreak4 = "~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~";
}