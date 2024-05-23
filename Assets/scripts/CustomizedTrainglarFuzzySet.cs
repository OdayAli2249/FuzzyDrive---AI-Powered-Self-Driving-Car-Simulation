using UnityEngine;

// class the represent traingular fuzzefication function
public class Customized_Trainglar_Fuzzy_Set
{

    private float start;
    private float center;
    private float end;

    private float Left_m;
    private float Right_m;


    public Customized_Trainglar_Fuzzy_Set(float start, float center, float end)
    {
        this.start = start;
        this.center = center;
        this.end = end;

        this.Left_m = compute_m_left();
        this.Right_m = compute_m_right();
    }

    float compute_m_left()
    {
        return 1 / (this.center - this.start);
    }

    float compute_m_right()
    {
        return 1 / (this.center - this.end);
    }

    public float membeship_degree(float v)
    {
        if (this.start < v && v <= this.center)
        {
            return (v - this.start) * this.Left_m;
        }
        else if (this.center < v && v <= this.end)
        {
            return (v - this.end) * this.Right_m;
        }
        else
        {
            return 0;
        }
    }

    public float get_Center()
    {
        return this.center;
    }

}