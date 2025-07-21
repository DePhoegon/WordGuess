
class Helper {
    public static readonly int intendedWidth = 50;
    public static readonly int intendedHeight = 30;
    public static readonly int hangChances = 6;
    public static void Padder(int padding, bool isVertical) {
        if (padding < 0) { padding = 0; }
        string padString = isVertical ? "\n" : " ";
        Console.Write(padString, padding);
    }
    public static void CenterPadHeight() {
        int currentHeight = Console.WindowHeight;
        int hPadding = 0;
        if (currentHeight > intendedHeight) { hPadding = (currentHeight - intendedHeight) / 2; }
        if (hPadding > 0) { Padder(hPadding, true); }
    }
    public static void CenterPadWidth() {
        int currentWidth = Console.WindowWidth;
        int wPadding = 0;
        if (currentWidth > intendedWidth) { wPadding = (currentWidth - intendedWidth) / 2; }
        if (wPadding > 0) { Padder(wPadding, false); }
    }
    public static void CenterPadWidthWithString(string str, int padding = 0) {
        int currentWidth = Console.WindowWidth;
        int wPadding = 0;
        if (currentWidth > intendedWidth) { wPadding = (currentWidth - intendedWidth) / 2; }
        if (wPadding > 0) { Padder(wPadding, false); }
        if (padding > 0) { Padder(padding, false); }
        Console.WriteLine(str);
    }
    public static void SetWindow() {
        if (OperatingSystem.IsWindows()) {
            Console.SetWindowSize(width: intendedWidth, height: intendedHeight);
            Console.BufferHeight = intendedHeight;
            Console.BufferWidth = intendedWidth;
            Console.Title = "DePhoegon Word Guess";
        }
        if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux()) { 
            Console.Write($"\x1b[8;{intendedHeight};{intendedWidth}t");
            CenterPadWidthWithString("\x1b]0;DePhoegon Word Guess\x07", 0);
        }
    }
}