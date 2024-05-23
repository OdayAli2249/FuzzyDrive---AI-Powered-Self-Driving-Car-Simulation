using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyController : MonoBehaviour
{



    void OnDrawGizmos(){
        float[] polar_regions = Inference.get_Polar_Regions_gez(UserCar, transform);

        
        float[] areas = new float[8];

        areas[0] = 20; areas[1] = 15; areas[2] = 45; areas[3] = 60;
        areas[4] = 100; areas[5] = 60; areas[6] = 45; areas[7] = 15;

        Debug.Log("polar regions befor normalize : ");
        for (int i = 0; i < polar_regions.Length; i++)
        {
            Debug.Log(") " + i + " :" + polar_regions[i]);
            polar_regions[i] = polar_regions[i] / areas[i];
        }

        Debug.Log("polar regions after normalize : ");
        for (int i = 0; i < polar_regions.Length; i++)
        {
            Debug.Log(") " + i + " :" + polar_regions[i]);
        }
        
    }



    // reference to user car ( the obstacle )
    public Transform UserCar;
    public Color gizmoColor;

    private Vector2 Xaxis;


    // reference of the four wheels collider
    public WheelCollider front_right;
    public WheelCollider front_left;
    public WheelCollider back_right;
    public WheelCollider back_left;

    private Path_Follow_Behaviour_Inference path_Follow;
    private Obstacle_Avoidance_Behaviour_Inference obstacle_avoidance;


    public float a_appear;
    public float v_appear;
    public Vector3 d_appear;

    public float a_motor;

    public float desired_velocity;


    private Transform[] path;

    private string current_behaviour;
    private static string PATH_FOLLOWING = "path_following_behaviour";
    private static string OBSTACLE_AVOIDANCE = "obstacle_avoidance_behaviour";

    private float save_distance;

    // outputs

    // acceleration controll variables

    float negative_high = 0;
    float negative_low = 0;
    float zero = 0;
    float positive_low = 0;
    float positive_high = 0;

    // steering controll variables

    float left_high = 0;
    float left_low = 0;
    float zero_ = 0;
    float right_low = 0;
    float right_high = 0;


    public int Current_Node_Index = 1;

    public float distance_between_cars;

    private float maxSteering;


   


    // Start is called before the first frame update
    void Start()
    {
        d_appear = transform.position;
        v_appear = 0;
        distance_between_cars = 0;

        desired_velocity = 0.1f;
        save_distance = 10.0f;
        maxSteering = 15;

        path = new Transform[72];

        for (int i = 0; i < path.Length; i++)
        {
            path[i] = GameObject.Find("Node" + i).transform;
        }


        path_Follow = new Path_Follow_Behaviour_Inference();
        obstacle_avoidance = new Obstacle_Avoidance_Behaviour_Inference();


        path_Follow.Initialize();
        obstacle_avoidance.Initialize();

        current_behaviour = PATH_FOLLOWING;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log("######################################################################");

        // we start by reading current input of the fuzzy control system


        Vector3 current_d = transform.position;

        // compute current velocity of car
        float v_current_d = Inference.get_Car_Velocity(current_d, d_appear);
        // get current distance between the car and uesr car
        float current_distance_between_cars = Inference.get_Distance_From_User_Car(UserCar, transform);

        // difference between current car's velocity and desired velicity
        float delta_v = Inference.get_Car_Delta_Velocity(v_current_d, desired_velocity);

        // compute every polar region's postion according to the obstacle ( user's car )
        float[] polar_regions = Inference.get_Polar_Regions(UserCar, transform);
        //float approaching = Inference.is_Approaching(current_distance_between_cars, distance_between_cars);            /////////////////////
        float approaching = 1;

        // just for debug
        Debug.Log("v_current_d : " + v_current_d);
        Debug.Log("delta_v : " + delta_v);
        Debug.Log("current_distance_between_cars : " + current_distance_between_cars);
        Debug.Log("approaching : " + approaching);

        float[] areas = new float[8];

        areas[0] = 20; areas[1] = 15; areas[2] = 45; areas[3] = 60;
        areas[4] = 100; areas[5] = 60; areas[6] = 45; areas[7] = 15;


        Debug.Log("polar regions befor normalize : ");
        for (int i = 0; i < polar_regions.Length; i++)
        {
            Debug.Log(") " + i + " :" + polar_regions[i]);
            polar_regions[i] = polar_regions[i] / areas[i];
        }

        Debug.Log("polar regions after normalize : ");
        for (int i = 0; i < polar_regions.Length; i++)
        {
            Debug.Log(") " + i + " :" + polar_regions[i]);
        }


        Debug.Log("current behaviour : " + current_behaviour);

        // if we in " path following state "

        if (current_behaviour == PATH_FOLLOWING)
        {
            // check if we close enough to current node " temporary distenation "
            // if true we move to next node
            if (Inference.get_Distance_From_User_Car(transform, path[Current_Node_Index]) < 3)
            {
                Current_Node_Index++;
                Debug.Log("we moved to next node : " + Current_Node_Index);
            }
            maxSteering = 15;

            // this function envokes all path following's rules and assigns results to the " controll variables " / line 44 to 58 /
            // Path_Following function in line 416
            Path_Following(v_current_d);

        }
        // if we in " obstacle avoidance state "
        else if (current_behaviour == OBSTACLE_AVOIDANCE)
        {
            maxSteering = 30;
            // this function envokes all obstacle avoidance's rules and assigns results to the " controll variables " / line 44 to 58 /
            // Obstacle_Avoidance functio in line 298
            Obstacle_Avoidance(v_current_d, polar_regions, approaching);

            // if there is no rules activated from obstacle avoidance's rules, we switch to path following's rules
            
            if (zero_ < 0.2 && left_low < 0.2 && right_low < 0.2 && left_high < 0.2 && right_high < 0.2)
            {
                // for debug
                Debug.Log("switch to path follow ");

                if (Inference.get_Distance_From_User_Car(UserCar, path[Current_Node_Index]) < 15)
                {
                    Current_Node_Index += 2;
                    Debug.Log("user car :" + UserCar);
                    Debug.Log("Current_Node :" + path[Current_Node_Index]);
                    Debug.Log("distance between them :" + Inference.get_Distance_From_User_Car(UserCar, path[Current_Node_Index]));
                    Debug.Log("So new Current_Node_Index :" + Current_Node_Index);

                }
                maxSteering = 15;
                // switch to path follwing's rules
                Path_Following(v_current_d);
            }

        }


        // defuzzefication

        float difference1 = right_high - left_high;
        float difference2 = right_low - left_low;

        if (Mathf.Abs(difference1) < 0.3 && Mathf.Abs(difference2) < 0.3)
        {
            Debug.Log("equal state ");
            float region_left = polar_regions[6] + polar_regions[7];
            float region_right = polar_regions[1] + polar_regions[2];
            if (region_right > region_left)
            {   
                Debug.Log("right lose");
                right_low = 0;
                right_high = 0;
            }
            else
            {
                Debug.Log("right lose");
                left_low = 0;
                left_high = 0;
            }
        }
        
        float[] delta_steering = new float[5];

        delta_steering[0] = left_high; delta_steering[1] = left_low; delta_steering[2] = zero_;
        delta_steering[3] = right_low; delta_steering[4] = right_high;

        float[] acceleration = new float[5];

        acceleration[0] = negative_high; acceleration[1] = negative_low; acceleration[2] = zero;
        acceleration[3] = positive_low; acceleration[4] = positive_high;

        float weighted_centers_sum_steering = 0;
        float weights_sum_steering = 0;

        float weighted_centers_sum_acceleration = 0;
        float weights_sum_acceleration = 0;

        for (int i = 0; i < delta_steering.Length; i++)
        {
            weighted_centers_sum_steering += delta_steering[i] * path_Follow.Delta_Steering.get_Fuzzy_Sets()[i].get_Center();
            weighted_centers_sum_acceleration += acceleration[i] * path_Follow.Acceleration.get_Fuzzy_Sets()[i].get_Center();

            weights_sum_steering += delta_steering[i];
            weights_sum_acceleration += acceleration[i];
        }

        Debug.Log("steering averege : " + weighted_centers_sum_steering / weights_sum_steering);
        Debug.Log("acceleration averege : " + weighted_centers_sum_acceleration / weights_sum_acceleration);


        if ((front_left.steerAngle < 15 && weighted_centers_sum_steering / weights_sum_steering > 0) ||
        (front_left.steerAngle > -15 && weighted_centers_sum_steering / weights_sum_steering < 0))
        {
            front_left.steerAngle += weighted_centers_sum_steering / weights_sum_steering;
            front_right.steerAngle += weighted_centers_sum_steering / weights_sum_steering;
        }

        if (weighted_centers_sum_acceleration == 0 || weights_sum_acceleration == 0)
        {
            Debug.Log("is now NaN");
        }
        else
        {
            back_left.motorTorque = weighted_centers_sum_acceleration / weights_sum_acceleration;
            back_right.motorTorque = weighted_centers_sum_acceleration / weights_sum_acceleration; Debug.Log("is now NaN ");
        }

        // we switch between the two behaviours according to distance between obstacle and the car
        if (Inference.get_Distance_From_User_Car(transform, UserCar) < save_distance)
        {
            current_behaviour = OBSTACLE_AVOIDANCE;
        }
        else
        {
            current_behaviour = PATH_FOLLOWING;
        }


        a_motor = back_left.motorTorque;
        a_appear = v_current_d - v_appear;


        d_appear = current_d;
        v_appear = v_current_d;
        distance_between_cars = current_distance_between_cars;

        Debug.Log("______________________________________________________________________");


    }

    void Obstacle_Avoidance(float v_current_d, float[] polar_regions, float approaching)
    {
        // just for debug
        obstacle_avoidance.print_report(v_current_d, UserCar, transform, polar_regions, approaching);

        // parameter of the rules ( in the same order ) : 
        // current velocity of car
        // user's car location ( transform )
        // car location
        // polar regions array ( 8 angular ranges, each has number between 0 and 1 represent how every region sees of the obstacle)
        
        
        // every rule has same ID as in the pdf file
        // every rule retun number between 0 and 1 retpresents activation degree of the corresponding value of output variable
        // e.i : rules 1 & 5 return activation degree of how left high the steering degree should be . 
        // and so on ..

        // you can check the body of each rule in obstacle_avoidance class

        // steering

        zero_ = 0;

        float rule1 = obstacle_avoidance.Steering_rule_1(v_current_d, UserCar, transform, polar_regions);
        float rule5 = obstacle_avoidance.Steering_rule_5(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule1 : " + rule1);
        Debug.Log("rule5 : " + rule5);

        left_high = Mathf.Max(
          rule1,
          rule5
        );

        float rule2 = obstacle_avoidance.Steering_rule_2(v_current_d, UserCar, transform, polar_regions);
        float rule6 = obstacle_avoidance.Steering_rule_6(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule2 : " + rule2);
        Debug.Log("rule6 : " + rule6);

        left_low = Mathf.Max(
          rule2,
          rule6
        );

        float rule3 = obstacle_avoidance.Steering_rule_3(v_current_d, UserCar, transform, polar_regions);
        float rule7 = obstacle_avoidance.Steering_rule_7(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule3 : " + rule3);
        Debug.Log("rule7 : " + rule7);

        right_high = Mathf.Max(
          rule3,
          rule7
        );

        float rule4 = obstacle_avoidance.Steering_rule_4(v_current_d, UserCar, transform, polar_regions);
        float rule8 = obstacle_avoidance.Steering_rule_8(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule4 : " + rule4);
        Debug.Log("rule8 : " + rule8);

        right_low = Mathf.Max(
          rule4,
          rule8
        );

        Debug.Log("steering : ");

        Debug.Log("angle" + Inference.get_User_Car_Angle(UserCar, transform));

        Debug.Log("zero_ : " + zero_);
        Debug.Log("left_low : " + left_low);
        Debug.Log("right_low : " + right_low);
        Debug.Log("left_high : " + left_high);
        Debug.Log("right_high : " + right_high);

        // acceleration

        zero = 0;
        positive_high = 0;

        float rule9 = obstacle_avoidance.Steering_rule_9(v_current_d, UserCar, transform, polar_regions);
        float rule11 = obstacle_avoidance.Steering_rule_11(v_current_d, UserCar, transform, polar_regions, approaching);
        float rule13 = obstacle_avoidance.Steering_rule_13(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule9 : " + rule9);
        Debug.Log("rule11 : " + rule11);
        Debug.Log("rule13 : " + rule13);

        //negative_high = Mathf.Max(
        //    rule9,
        //    rule11,
        //    rule13
        //);

        float rule10 = obstacle_avoidance.Steering_rule_10(v_current_d, UserCar, transform, polar_regions);
        float rule12 = obstacle_avoidance.Steering_rule_12(v_current_d, UserCar, transform, polar_regions, approaching);
        float rule14 = obstacle_avoidance.Steering_rule_14(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule10 : " + rule10);
        Debug.Log("rule12 : " + rule12);
        Debug.Log("rule14 : " + rule14);

        //negative_low = Mathf.Max(
        //    rule10,
        //    rule12,
        //    rule14
        //);

        float rule15 = obstacle_avoidance.Steering_rule_15(v_current_d, UserCar, transform, polar_regions, approaching);
        float rule16 = obstacle_avoidance.Steering_rule_16(v_current_d, UserCar, transform, polar_regions, approaching);
        float rule17 = obstacle_avoidance.Steering_rule_17(v_current_d, UserCar, transform, polar_regions, approaching);

        Debug.Log("rule15 : " + rule15);
        Debug.Log("rule16 : " + rule16);
        Debug.Log("rule17 : " + rule17);

        positive_low = Mathf.Max(
            rule15,
            rule16,
            rule17
        );


        Debug.Log("acceleration : ");
        Debug.Log("zero : " + zero);
        Debug.Log("negative_low : " + negative_low);
        Debug.Log("negative_high : " + negative_high);
        Debug.Log("positive_low : " + positive_low);
        Debug.Log("positive_high : " + positive_high);
    }


    void Path_Following(float v_current_d)
    {

        
         
        
        // every rule has same ID as in the pdf file
        // every rule retun number between 0 and 1 retpresents activation degree of the corresponding value of output variable
        // e.i : rules 1 & 5 return activation degree of how left high the steering degree should be . 
        // and so on ..

        // you can check the body of each rule in path_Follow class 

        // steering control rules

        // parameter of the rules ( in the same order ) : 
        // current node location
        // car location

        

        zero_ = path_Follow.Steering_rule_1(path[Current_Node_Index], transform);
        left_low = path_Follow.Steering_rule_2(path[Current_Node_Index], transform);
        right_low = path_Follow.Steering_rule_3(path[Current_Node_Index], transform);
        left_high = path_Follow.Steering_rule_4(path[Current_Node_Index], transform);
        right_high = path_Follow.Steering_rule_5(path[Current_Node_Index], transform);

        // debug

        Debug.Log("steering : ");

        Debug.Log("angle" + Inference.get_User_Car_Angle(UserCar, transform));

        Debug.Log("zero_ : " + zero_);
        Debug.Log("left_low : " + left_low);
        Debug.Log("right_low : " + right_low);
        Debug.Log("left_high : " + left_high);
        Debug.Log("right_high : " + right_high);




        // acceleration control rules

        // parameter of the rules ( in the same order ) : 
        // current car velocity
        // desired velocity

        // where this rules will compute delta velocity to apply rules of acceleration in path following in pdf

        zero = path_Follow.Velocity_rule_1(v_current_d, desired_velocity);
        negative_low = path_Follow.Velocity_rule_2(v_current_d, desired_velocity);
        negative_high = path_Follow.Velocity_rule_3(v_current_d, desired_velocity);
        positive_low = path_Follow.Velocity_rule_4(v_current_d, desired_velocity);
        positive_high = path_Follow.Velocity_rule_5(v_current_d, desired_velocity);

        // debug

        Debug.Log("acceleration : ");
        Debug.Log("zero : " + zero);
        Debug.Log("negative_low : " + negative_low);
        Debug.Log("negative_high : " + negative_high);
        Debug.Log("positive_low : " + positive_low);
        Debug.Log("positive_high : " + positive_high);
    }

}