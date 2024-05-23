using UnityEngine;

// class who contain all path following rules for steering and acceleration as static functions
// which is just combination of min and max operations on menbership degree numbers applied on input variables
public class Path_Follow_Behaviour_Inference : Inference
{

    // steering rules :

    // zero steering
    public float Steering_rule_1(Transform UserCar, Transform AutoCarTrans)
    {

        float angle = get_User_Car_Angle(UserCar, AutoCarTrans);

        return Mathf.Max(Angle.get_Fuzzy_Sets()[0].membeship_degree(angle),
                         Angle.get_Fuzzy_Sets()[4].membeship_degree(angle),
                         Angle.get_Fuzzy_Sets()[8].membeship_degree(angle));
    }

    // left-low steering
    public float Steering_rule_2(Transform UserCar, Transform AutoCarTrans)
    {
        float angle = get_User_Car_Angle(UserCar, AutoCarTrans);

        return Mathf.Max(Angle.get_Fuzzy_Sets()[5].membeship_degree(angle),
                         Angle.get_Fuzzy_Sets()[7].membeship_degree(angle));
    }

    // right-low steering
    public float Steering_rule_3(Transform UserCar, Transform AutoCarTrans)
    {
        float angle = get_User_Car_Angle(UserCar, AutoCarTrans);

        return Mathf.Max(Angle.get_Fuzzy_Sets()[1].membeship_degree(angle),
                         Angle.get_Fuzzy_Sets()[3].membeship_degree(angle));
    }

    // left-high steering
    public float Steering_rule_4(Transform UserCar, Transform AutoCarTrans)
    {
        float angle = get_User_Car_Angle(UserCar, AutoCarTrans);

        return Angle.get_Fuzzy_Sets()[6].membeship_degree(angle);
    }

    // right-high steering
    public float Steering_rule_5(Transform UserCar, Transform AutoCarTrans)
    {
        float angle = get_User_Car_Angle(UserCar, AutoCarTrans);

        return Angle.get_Fuzzy_Sets()[2].membeship_degree(angle);
    }



    // velocity rules :


    // zero acceleration
    public float Velocity_rule_1(float Current_Velocity, float Previous_Velocity)
    {
        float delta_velocity = get_Car_Delta_Velocity(Current_Velocity, Previous_Velocity);

        return Delta_Velocity.get_Fuzzy_Sets()[2].membeship_degree(delta_velocity);
    }

    // negative-low acceleration
    public float Velocity_rule_2(float Current_Velocity, float Previous_Velocity)
    {
        float delta_velocity = get_Car_Delta_Velocity(Current_Velocity, Previous_Velocity);

        return Delta_Velocity.get_Fuzzy_Sets()[3].membeship_degree(delta_velocity);
    }

    // negative-high acceleration
    public float Velocity_rule_3(float Current_Velocity, float Previous_Velocity)
    {
        float delta_velocity = get_Car_Delta_Velocity(Current_Velocity, Previous_Velocity);

        return Delta_Velocity.get_Fuzzy_Sets()[4].membeship_degree(delta_velocity);
    }

    // positive-low acceleration
    public float Velocity_rule_4(float Current_Velocity, float Previous_Velocity)
    {
        float delta_velocity = get_Car_Delta_Velocity(Current_Velocity, Previous_Velocity);

        return Delta_Velocity.get_Fuzzy_Sets()[1].membeship_degree(delta_velocity);
    }

    // positive-high acceleration
    public float Velocity_rule_5(float Current_Velocity, float Previous_Velocity)
    {
        float delta_velocity = get_Car_Delta_Velocity(Current_Velocity, Previous_Velocity);

        return Delta_Velocity.get_Fuzzy_Sets()[0].membeship_degree(delta_velocity);
    }


}