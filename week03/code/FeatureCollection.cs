public class FeatureCollection
{
    public List<Feature> Features { get; set; } = new();
}

public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

public class EarthquakeProperties
{
    public string Place { get; set; } = "";
    public double? Mag { get; set; }
}