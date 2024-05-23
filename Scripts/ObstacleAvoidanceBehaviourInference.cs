using UnityEngine;

// class who contain all obstacle avoidance rules for steering and acceleration as static functions
// which is just combination of min and max operations on menbership degree numbers applied on input variables
public class Obstacle_Avoidance_Behaviour_Inference : Inference
{

    // for debugging
    public void print_report(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        Debug.Log("--------------------------------------------");

        Debug.Log("obstacle behaviour model's input report : ");

        string[] velocity_degrees = new string[3];
        velocity_degrees[0] = "low";
        velocity_degrees[1] = "muderate";
        velocity_degrees[2] = "high";

        Debug.Log("Velocity memberships :");
        int lv = Velocity.get_Fuzzy_Sets().Length;
        Customized_Trainglar_Fuzzy_Set[] fuzzy_sets_v = Velocity.get_Fuzzy_Sets();
        for (int i = 0; i < lv; i++)
        {
            Debug.Log(velocity_degrees[i] + " : " + fuzzy_sets_v[i].membeship_degree(vt));
        }

        string[] distance_degrees = new string[3];
        distance_degrees[0] = "very-close";
        distance_degrees[1] = "close";
        distance_degrees[2] = "far";

        Debug.Log("Distance memberships :");
        int ld = Distance.get_Fuzzy_Sets().Length;
        Customized_Trainglar_Fuzzy_Set[] fuzzy_sets_d = Distance.get_Fuzzy_Sets();
        for (int i = 0; i < ld; i++)
        {
            Debug.Log(distance_degrees[i] + " : " + fuzzy_sets_d[i].membeship_degree(get_Distance_From_User_Car(UserCar, AutoCarTrans)));
        }

        Debug.Log("Approaching" + Approaching);

        Debug.Log("--------------------------------------------");
    }

    // steering rules :


    // left-high steering
    public float Steering_rule_1(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[2].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[1]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[1]);

        float term3 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[1]);



        return Mathf.Max(term1, term2, term3);
    }

    // left-low steering
    public float Steering_rule_2(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);

        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[1]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[1]);


        return Mathf.Max(term1, term2);
    }

    // right-high steering
    public float Steering_rule_3(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);

        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[2].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[7]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[7]);

        float term3 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[7]);


        return Mathf.Max(term1, term2, term3);
    }

    // right-low steering
    public float Steering_rule_4(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);

        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[7]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[7]);


        return Mathf.Max(term1, term2);
    }


    // left-high steering
    public float Steering_rule_5(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[2]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[2]);

        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // left-low steering
    public float Steering_rule_6(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[2]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[2]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // right-high steering
    public float Steering_rule_7(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[6]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[6]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // right-low steering
    public float Steering_rule_8(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[6]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[6]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }




    // acceleration rules :


    // negative-high acceleration
    public float Steering_rule_9(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);

        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[2].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[0]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[0]);

        float term3 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[0]);

        return Mathf.Max(term1, term2, term3);
    }

    // negative-low acceleration
    public float Steering_rule_10(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);

        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[0]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[0]);


        return Mathf.Max(term1, term2);
    }

    // negative-high acceleration
    public float Steering_rule_11(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[2]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[2]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // negative-low acceleration
    public float Steering_rule_12(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[2]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[2]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // negative-high acceleration
    public float Steering_rule_13(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[6]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[6]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // negative-low acceleration
    public float Steering_rule_14(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[2].membeship_degree(d),
                          polar_regions[6]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[6]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // positive-low acceleration
    public float Steering_rule_15(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[3]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[3]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // positive-low acceleration
    public float Steering_rule_16(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term1 = Mathf.Min(Velocity.get_Fuzzy_Sets()[1].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[1].membeship_degree(d),
                          polar_regions[5]);

        float term2 = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[5]);


        return Mathf.Min(Approaching, Mathf.Max(term1, term2));
    }

    // positive-low acceleration
    public float Steering_rule_17(float vt, Transform UserCar, Transform AutoCarTrans, float[] polar_regions, float Approaching)
    {

        float d = get_Distance_From_User_Car(UserCar, AutoCarTrans);



        float term = Mathf.Min(Velocity.get_Fuzzy_Sets()[0].membeship_degree(vt),
                          Distance.get_Fuzzy_Sets()[0].membeship_degree(d),
                          polar_regions[4]);


        return Mathf.Min(Approaching, term);
    }

}