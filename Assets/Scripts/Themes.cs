using System.Collections.Generic;
using UnityEngine;

public class Themes {
    private static Color getColor(string hexColor) {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color color)) {
            return color;
        }
        return Color.red;
    }


    public static readonly Theme serikaDark = new(
        name: "Serika Dark",
        background: getColor("#323437"),
        backgroundUI: getColor("#2c2e31"),
        foreground: getColor("#d1d0c5"),
        foregroundTyped: getColor("#e2b714"),
        foregroundUI: getColor("#ca4754")
    );

    public static readonly Theme vscode = new(
        name: "VsCode",
        background: getColor("#1e1e1e"),
        backgroundUI: getColor("#191919"),
        foreground: getColor("#d4d4d4"),
        foregroundTyped: getColor("#007acc"),
        foregroundUI: getColor("#f44747")
    );

    public static readonly Theme neon = new(
        name: "Neon",
        background: getColor("#00002e"),
        backgroundUI: getColor("#060548"),
        foreground: getColor("#f1deef"),
        foregroundTyped: getColor("#ff3d8b"),
        foregroundUI: getColor("#8fecff")
    );

    public static readonly Theme darling = new(
        name: "Darling",
        background: getColor("#fec8cd"),
        backgroundUI: getColor("#f2babd"),
        foreground: getColor("#ffffff"),
        foregroundTyped: getColor("#a30000"),
        foregroundUI: getColor("#2e7dde")
    );

    public static readonly Theme oneDark = new(
        name: "One dark",
        background: getColor("#282c34"),
        backgroundUI: getColor("#21252b"),
        foreground: getColor("#f2f4f5"),
        foregroundTyped: getColor("#528bff"),
        foregroundUI: getColor("#c678dd")
    );


    private static Dictionary<string, Theme> themes = new Dictionary<string, Theme> {
        [serikaDark.name] = serikaDark,
        [vscode.name] = vscode,
        [neon.name] = neon,
        [darling.name] = darling,
        [oneDark.name] = oneDark
    };

    public static List<string> GetThemeNames() {
        return new List<string>(themes.Keys);
    }

    public static Theme GetTheme(string themeName) {
        return themes.GetValueOrDefault(themeName, serikaDark);
    }
}