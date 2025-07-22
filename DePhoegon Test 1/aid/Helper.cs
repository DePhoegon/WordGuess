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
        int maxWidth = currentWidth > 0 ? currentWidth : intendedWidth;
        int wrapWidth = maxWidth - padding;
        if (wrapWidth < 10) wrapWidth = 10; // Prevent too-narrow wrapping

        // Word-wrap the string to fit the window
        List<string> wrappedLines = new();
        string[] words = str.Split(' ');
        string line = "";
        foreach (var word in words) {
            if ((line.Length + word.Length + 1) > wrapWidth) {
                wrappedLines.Add(line.TrimEnd());
                line = "";
            }
            line += word + " ";
        }
        if (line.Length > 0) wrappedLines.Add(line.TrimEnd());

        foreach (var wrapped in wrappedLines) {
            wPadding = 0;
            if (maxWidth > intendedWidth) { wPadding = (maxWidth - intendedWidth) / 2; }
            if (wPadding > 0) Padder(wPadding, false);
            if (padding > 0) Padder(padding, false);

            if (newline) Console.WriteLine(wrapped);
            else Console.Write(wrapped);
        }
    }
    public static void SetWindow() {
        if (OperatingSystem.IsWindows()) {
            Console.SetWindowSize(intendedWidth, intendedHeight);
            Console.SetBufferSize(intendedWidth, intendedHeight);
            Console.Title = "DePhoegon Word Guess";
        }
        if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux()) { 
            Console.Write($"\x1b[8;{intendedHeight};{intendedWidth}t");
            CenterPadWidthWithString("\x1b]0;DePhoegon Word Guess\x07", 0);
        }
    }
}