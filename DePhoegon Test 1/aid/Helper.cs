
class Helper {
    public static readonly int intendedWidth = 50;
    public static readonly int intendedHeight = 30;
    public static readonly int hangChances = 6;
    public static int currentHieght = 0;
    public static int currentWidth = 0;

    private static (int width, int height) GetWindowSize() {
        if (OperatingSystem.IsWindows()) {  return (Console.WindowWidth, Console.WindowHeight); } 
        else if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux()) { return (Console.BufferWidth, Console.BufferHeight); }
        return (intendedWidth, intendedHeight); // Fallback
    }
    public static void SetCurrentSize() {
        (currentWidth, currentHieght) = GetWindowSize();
        if (currentWidth < intendedWidth) { currentWidth = intendedWidth; }
        if (currentHieght < intendedHeight) { currentHieght = intendedHeight; }
    }
    public static void Padder(int padding, bool isVertical) {
        if (padding < 0) { padding = 0; }
        if (isVertical) { for (int i = 0; i < padding; i++) Console.WriteLine(); }
        else { Console.Write(new string(' ', padding)); }
    }
    public static void CenterPadHeight() {
        int hPadding = 0;
        if (currentHieght > intendedHeight) { hPadding = (currentHieght - intendedHeight) / 2; }
        if (hPadding > 0) { Padder(hPadding, true); }
    }
    public static void CenterPadWidth() {
        int wPadding = 0;
        if (currentWidth > intendedWidth) { wPadding = (currentWidth - intendedWidth) / 2; }
        if (wPadding > 0) { Padder(wPadding, false); }
    }
    public static void CenterPadWidthWithString(string str, int padding) { CenterPadWidthWithString(str, padding, true); }
    public static void CenterPadWidthWithString(string str, int padding, bool newline) {
        if (padding < 0) { padding = 0; }
        int wPadding = 0;
        if (currentWidth > intendedWidth) { wPadding = (currentWidth - intendedWidth) / 2; }
        if (wPadding > 0) { Padder(wPadding, false); }
        if (padding > 0) { Padder(padding, false); }
        if (newline) { Console.WriteLine(str); }
        else { Console.Write(str); }
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