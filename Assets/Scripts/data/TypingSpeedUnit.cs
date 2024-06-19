[System.Serializable]
public class TypingSpeedUnit {
    public string Name { get; private set; }
    public string Abbreviation { get; private set; }
    public float CpmConversionFactor { get; private set; }

    public TypingSpeedUnit(string name, string abbreviation, float cpmConversionFactor) {
        this.Name = name;
        this.Abbreviation = abbreviation;
        this.CpmConversionFactor = cpmConversionFactor;
    }
}