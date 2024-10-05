using System.Collections.Generic;

[System.Serializable]
public class Exoplanet
{
    public string pl_name;
    public string hostname;
    public string gaia_id;
    public float ra;
    public float dec;
    public float sy_dist;
    public int disc_year;
    public string discoverymethod;
    public string disc_facility;
}

[System.Serializable]
public class ExoplanetList
{
    public List<Exoplanet> exoplanets;
}
