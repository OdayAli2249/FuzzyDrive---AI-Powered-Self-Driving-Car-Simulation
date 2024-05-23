using UnityEngine;

public class Fuzzy_Variable
{

    private float range;
    private Customized_Trainglar_Fuzzy_Set[] Fuzzy_Sets;

    public Fuzzy_Variable(float range, Customized_Trainglar_Fuzzy_Set[] Fuzzy_Sets)
    {
        this.range = range;
        this.Fuzzy_Sets = Fuzzy_Sets;
    }

    public float get_range()
    {
        return this.range;
    }

    public Customized_Trainglar_Fuzzy_Set[] get_Fuzzy_Sets()
    {
        return this.Fuzzy_Sets;
    }

}