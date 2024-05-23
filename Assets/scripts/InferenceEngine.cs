using UnityEngine;

public abstract class Inference
{


    // input
    protected Fuzzy_Variable Angle;
    protected Fuzzy_Variable Distance;
    protected Fuzzy_Variable Velocity;
    protected Fuzzy_Variable Delta_Velocity;

    // output
    public Fuzzy_Variable Acceleration;
    public Fuzzy_Variable Delta_Steering;

    public void Initialize()
    {
        // fuzzeficatio of all input and output variables

        // angle variable configuration
        Customized_Trainglar_Fuzzy_Set[] Angle_Fuzzy_Sets = new Customized_Trainglar_Fuzzy_Set[9];

        Angle_Fuzzy_Sets[0] = new Customized_Trainglar_Fuzzy_Set(-10, 0, 45);       // 0
        Angle_Fuzzy_Sets[1] = new Customized_Trainglar_Fuzzy_Set(0, 45, 90);        // Pi/4 
        Angle_Fuzzy_Sets[2] = new Customized_Trainglar_Fuzzy_Set(45, 90, 135);      // Pi/2
        Angle_Fuzzy_Sets[3] = new Customized_Trainglar_Fuzzy_Set(90, 135, 180);     // 3Pi/4
        Angle_Fuzzy_Sets[4] = new Customized_Trainglar_Fuzzy_Set(135, 180, 225);    // Pi
        Angle_Fuzzy_Sets[5] = new Customized_Trainglar_Fuzzy_Set(180, 225, 270);    // 5Pi/4
        Angle_Fuzzy_Sets[6] = new Customized_Trainglar_Fuzzy_Set(225, 270, 315);    // 3Pi/2
        Angle_Fuzzy_Sets[7] = new Customized_Trainglar_Fuzzy_Set(270, 315, 360);    // 7Pi/4
        Angle_Fuzzy_Sets[8] = new Customized_Trainglar_Fuzzy_Set(315, 360, 370);    // 2Pi

        Angle = new Fuzzy_Variable(360, Angle_Fuzzy_Sets);

        // distance variable configuration
        Customized_Trainglar_Fuzzy_Set[] Distance_Fuzzy_Sets = new Customized_Trainglar_Fuzzy_Set[3];

        Distance_Fuzzy_Sets[0] = new Customized_Trainglar_Fuzzy_Set(-25, 0, 25);       // 0
        Distance_Fuzzy_Sets[1] = new Customized_Trainglar_Fuzzy_Set(0, 25, 50);        // 25 
        Distance_Fuzzy_Sets[2] = new Customized_Trainglar_Fuzzy_Set(25, 50, 75);      // 50

        Distance = new Fuzzy_Variable(50, Distance_Fuzzy_Sets);

        // velocity variable configuration
        Customized_Trainglar_Fuzzy_Set[] Velocity_Fuzzy_Sets = new Customized_Trainglar_Fuzzy_Set[3];

        Velocity_Fuzzy_Sets[0] = new Customized_Trainglar_Fuzzy_Set(-0.1f, 0, 0.1f);       // 0
        Velocity_Fuzzy_Sets[1] = new Customized_Trainglar_Fuzzy_Set(0, 0.1f, 0.2f);        // 10 
        Velocity_Fuzzy_Sets[2] = new Customized_Trainglar_Fuzzy_Set(0.1f, 0.2f, 0.3f);      // 20

        Velocity = new Fuzzy_Variable(20, Velocity_Fuzzy_Sets);

        // delta vlocity variable configuration

        float _negative_high = -0.07f;
        float _negative_low = -0.04f;
        float _zero = 0;
        float _positive_low = 0.04f;
        float _positive_high = 0.07f;

        Customized_Trainglar_Fuzzy_Set[] Delta_Velocity_Fuzzy_Sets = new Customized_Trainglar_Fuzzy_Set[5];

        Delta_Velocity_Fuzzy_Sets[0] = new Customized_Trainglar_Fuzzy_Set(_negative_high - 10, _negative_high, _negative_low);
        Delta_Velocity_Fuzzy_Sets[1] = new Customized_Trainglar_Fuzzy_Set(_negative_high, _negative_low, _zero);
        Delta_Velocity_Fuzzy_Sets[2] = new Customized_Trainglar_Fuzzy_Set(_negative_low, _zero, _positive_low);
        Delta_Velocity_Fuzzy_Sets[3] = new Customized_Trainglar_Fuzzy_Set(_zero, _positive_low, _positive_high);
        Delta_Velocity_Fuzzy_Sets[4] = new Customized_Trainglar_Fuzzy_Set(_positive_low, _positive_high, _positive_high + 10);

        Delta_Velocity = new Fuzzy_Variable(20, Delta_Velocity_Fuzzy_Sets);

        // acceleration variable configuration

        float negative_high = -30;
        float negative_low = -15;
        float zero = 0;
        float positive_low = 15;
        float positive_high = 30;

        Customized_Trainglar_Fuzzy_Set[] Acceleration_Fuzzy_Sets = new Customized_Trainglar_Fuzzy_Set[5];

        Acceleration_Fuzzy_Sets[0] = new Customized_Trainglar_Fuzzy_Set(negative_high - 10, negative_high, negative_low);
        Acceleration_Fuzzy_Sets[1] = new Customized_Trainglar_Fuzzy_Set(negative_high, negative_low, zero);
        Acceleration_Fuzzy_Sets[2] = new Customized_Trainglar_Fuzzy_Set(negative_low, zero, positive_low);
        Acceleration_Fuzzy_Sets[3] = new Customized_Trainglar_Fuzzy_Set(zero, positive_low, positive_high);
        Acceleration_Fuzzy_Sets[4] = new Customized_Trainglar_Fuzzy_Set(positive_low, positive_high, positive_high + 10);

        Acceleration = new Fuzzy_Variable(20, Acceleration_Fuzzy_Sets);

        // delta steering variable configuration

        float left_high = -6;
        float left_low = -3;
        float zero_ = 0;
        float right_low = 3;
        float right_high = 6;

        Customized_Trainglar_Fuzzy_Set[] Delta_Steering_Fuzzy_Sets = new Customized_Trainglar_Fuzzy_Set[5];

        Delta_Steering_Fuzzy_Sets[0] = new Customized_Trainglar_Fuzzy_Set(left_high - 10, left_high, left_low);
        Delta_Steering_Fuzzy_Sets[1] = new Customized_Trainglar_Fuzzy_Set(left_high, left_low, zero_);
        Delta_Steering_Fuzzy_Sets[2] = new Customized_Trainglar_Fuzzy_Set(left_low, zero_, right_low);
        Delta_Steering_Fuzzy_Sets[3] = new Customized_Trainglar_Fuzzy_Set(zero_, right_low, right_high);
        Delta_Steering_Fuzzy_Sets[4] = new Customized_Trainglar_Fuzzy_Set(right_low, right_high, right_high + 10);

        Delta_Steering = new Fuzzy_Variable(20, Delta_Steering_Fuzzy_Sets);



    }

    // set of functios to read input variables ( velosity , delta velocity , polar regions , ... ) from the environment
    public static float computeAngele(Vector3 obj)
    {

        Vector2 Xaxis = new Vector2(0, 1);
        Vector2 obj2D = new Vector2(obj.x, obj.z);
        float Angle = Vector2.Angle(Xaxis, obj2D);  //If the angle isn't correctly at 0, you can subtract this value by the offset degree
        if (obj2D.x < 0)
        {
            Angle = 360 - Angle;
        }

        return Angle;
    }


    public static float get_User_Car_Angle(Transform UserCar, Transform transform)
    {
        Vector3 obj = transform.InverseTransformPoint(UserCar.position);
        return computeAngele(obj);
    }


    public static float get_Distance_From_User_Car(Transform UserCar, Transform transform)
    {
        return transform.InverseTransformPoint(UserCar.position).magnitude;
    }


    public static float[] get_Polar_Regions(Transform UserCar, Transform transform)
    {
        Vector3 obst = transform.InverseTransformPoint(UserCar.position);
        //Gizmos.DrawLine(transform.position, UserCar.position);

        Vector3 direction = Vector3.Normalize(UserCar.position - transform.position);

        float[] ranges = new float[8];

        ranges[0] = 10; ranges[1] = 25; ranges[2] = 70; ranges[3] = 130;
        ranges[4] = 230; ranges[5] = 290; ranges[6] = 335; ranges[7] = 350;




        float langitude = 4;
        float negative_langitude = -4;

        Debug.Log("rotation " + transform.rotation.eulerAngles.y);

        /*
        for (int i = 0; i < ranges.Length; i++)
        {
           
            Vector3 Vi = new Vector3();
            Vi.z = 50 * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad + ranges[i] * Mathf.Deg2Rad);
            Vi.x = 50 * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad + ranges[i] * Mathf.Deg2Rad);
            Vi.y = 0;

            Gizmos.DrawLine(transform.position, Vi + transform.position);

        }
        */


        Vector3 Vi_norm_P = new Vector3();
        Vi_norm_P.z = -1 * langitude * direction.x;
        Vi_norm_P.x = langitude * direction.z;

        //Gizmos.DrawLine(UserCar.transform.position, Vi_norm_P + UserCar.transform.position);
        //Gizmos.DrawLine(transform.position, Vi_norm_P + UserCar.transform.position);

        Vector3 Vi_norm_N = new Vector3();
        Vi_norm_N.z = -1 * negative_langitude * direction.x;
        Vi_norm_N.x = negative_langitude * direction.z;

        //Gizmos.DrawLine(UserCar.transform.position, Vi_norm_N + UserCar.transform.position);
        //Gizmos.DrawLine(transform.position, Vi_norm_N + UserCar.transform.position);



        //print("Angle of axis to your vec is " + Theta);
        //print(Mathf.Deg2Rad * 90);
        //print(Mathf.Cos(45));

        float AngleStart = computeAngele((Vi_norm_N + UserCar.transform.position) - transform.position);
        float AngleEnd = computeAngele((Vi_norm_P + UserCar.transform.position) - transform.position);

        Debug.Log("AngleStart" + AngleStart);
        Debug.Log("AngleEnd" + AngleEnd);

        AngleStart -= transform.rotation.eulerAngles.y;
        if (AngleStart < 0)
        {
            AngleStart = 360 + AngleStart;
        }
        AngleEnd -= transform.rotation.eulerAngles.y;
        if (AngleEnd < 0)
        {
            AngleEnd = 360 + AngleEnd;
        }

        Debug.Log("AngleStart2 " + AngleStart);
        Debug.Log("AngleEnd2 " + AngleEnd);


        //print(computeAngele((Vi_norm_N + UserCar.transform.position) - transform.position) + ", "
        //+ computeAngele((Vi_norm_P + UserCar.transform.position) - transform.position));

        float[] Polar_regions = new float[8];
        List<float> Candidates = new List<float>();

        for (int i = 0; i < Polar_regions.Length; i++)
        {
            Polar_regions[i] = 0;
        }

        int degree = 0;
        int last_match = 0;
        Candidates.Add(AngleStart);
        bool ITC = false;

        if (AngleStart < AngleEnd)
        {
            for (int i = (int)AngleStart; i < (int)AngleEnd; i++)
            {

                degree++;
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (Mathf.Abs(ranges[j] - i) < 1)
                    {
                        Candidates.Add(ranges[j]);
                        last_match = j;
                        Polar_regions[j] = degree;
                        degree = 0;
                        ITC = true;
                        continue;
                    }
                }
            }
        }
        else
        {
            for (int i = (int)AngleStart; i < 360; i++)
            {
                degree++;
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (Mathf.Abs(ranges[j] - i) < 1)
                    {
                        Candidates.Add(ranges[j]);
                        last_match = j;
                        Polar_regions[j] = degree;
                        degree = 0;
                        ITC = true;
                        continue;
                    }
                }
            }

            for (int i = 0; i < AngleEnd; i++)
            {
                degree++;
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (Mathf.Abs(ranges[j] - i) < 1)
                    {
                        Candidates.Add(ranges[j]);
                        last_match = j;
                        Polar_regions[j] = degree;
                        degree = 0;
                        ITC = true;
                        continue;
                    }
                }
            }
        }
        if (ITC == false)
        {
            if (ranges[0] <= AngleStart && AngleEnd <= ranges[1])
            {
                Polar_regions[1] = AngleEnd - AngleStart;
            }
            if (ranges[1] <= AngleStart && AngleEnd <= ranges[2])
            {
                Polar_regions[2] = AngleEnd - AngleStart;
            }
            if (ranges[2] <= AngleStart && AngleEnd <= ranges[3])
            {
                Polar_regions[3] = AngleEnd - AngleStart;
            }
            if (ranges[3] <= AngleStart && AngleEnd <= ranges[4])
            {
                Polar_regions[4] = AngleEnd - AngleStart;
            }
            if (ranges[4] <= AngleStart && AngleEnd <= ranges[5])
            {
                Polar_regions[5] = AngleEnd - AngleStart;
            }
            if (ranges[5] <= AngleStart && AngleEnd <= ranges[6])
            {
                Polar_regions[6] = AngleEnd - AngleStart;
            }
            if (ranges[6] <= AngleStart && AngleEnd <= ranges[7])
            {
                Polar_regions[7] = AngleEnd - AngleStart;
            }
            if (ranges[7] <= AngleStart && AngleEnd <= ranges[0])
            {
                Polar_regions[0] = 360 - AngleStart + AngleEnd;
            }
        }
        else if (last_match == Polar_regions.Length - 1)
        {
            Polar_regions[0] = degree;
        }
        else
        {
            Polar_regions[last_match + 1] = degree;
        }


        Candidates.Add(AngleEnd);


        return Polar_regions;
    }


    public static float[] get_Polar_Regions_gez(Transform UserCar, Transform transform)
    {
        Vector3 obst = transform.InverseTransformPoint(UserCar.position);
        Gizmos.DrawLine(transform.position, UserCar.position);

        Vector3 direction = Vector3.Normalize(UserCar.position - transform.position);

        float[] ranges = new float[8];

        ranges[0] = 10; ranges[1] = 25; ranges[2] = 70; ranges[3] = 130;
        ranges[4] = 230; ranges[5] = 290; ranges[6] = 335; ranges[7] = 350;




        float langitude = 4;
        float negative_langitude = -4;

        Debug.Log("rotation " + transform.rotation.eulerAngles.y);
        for (int i = 0; i < ranges.Length; i++)
        {

            Vector3 Vi = new Vector3();
            Vi.z = 50 * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad + ranges[i] * Mathf.Deg2Rad);
            Vi.x = 50 * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad + ranges[i] * Mathf.Deg2Rad);
            Vi.y = 0;

            Gizmos.DrawLine(transform.position, Vi + transform.position);

        }



        Vector3 Vi_norm_P = new Vector3();
        Vi_norm_P.z = -1 * langitude * direction.x;
        Vi_norm_P.x = langitude * direction.z;

        Gizmos.DrawLine(UserCar.transform.position, Vi_norm_P + UserCar.transform.position);
        Gizmos.DrawLine(transform.position, Vi_norm_P + UserCar.transform.position);

        Vector3 Vi_norm_N = new Vector3();
        Vi_norm_N.z = -1 * negative_langitude * direction.x;
        Vi_norm_N.x = negative_langitude * direction.z;

        Gizmos.DrawLine(UserCar.transform.position, Vi_norm_N + UserCar.transform.position);
        Gizmos.DrawLine(transform.position, Vi_norm_N + UserCar.transform.position);



        //print("Angle of axis to your vec is " + Theta);
        //print(Mathf.Deg2Rad * 90);
        //print(Mathf.Cos(45));

        float AngleStart = computeAngele((Vi_norm_N + UserCar.transform.position) - transform.position);
        float AngleEnd = computeAngele((Vi_norm_P + UserCar.transform.position) - transform.position);

        Debug.Log("AngleStart" + AngleStart);
        Debug.Log("AngleEnd" + AngleEnd);

        AngleStart -= transform.rotation.eulerAngles.y;
        if (AngleStart < 0)
        {
            AngleStart = 360 + AngleStart;
        }
        AngleEnd -= transform.rotation.eulerAngles.y;
        if (AngleEnd < 0)
        {
            AngleEnd = 360 + AngleEnd;
        }

        Debug.Log("AngleStart2 " + AngleStart);
        Debug.Log("AngleEnd2 " + AngleEnd);


        //print(computeAngele((Vi_norm_N + UserCar.transform.position) - transform.position) + ", "
        //+ computeAngele((Vi_norm_P + UserCar.transform.position) - transform.position));

        float[] Polar_regions = new float[8];
        List<float> Candidates = new List<float>();

        for (int i = 0; i < Polar_regions.Length; i++)
        {
            Polar_regions[i] = 0;
        }

        int degree = 0;
        int last_match = 0;
        Candidates.Add(AngleStart);
        bool ITC = false;

        if (AngleStart < AngleEnd)
        {
            for (int i = (int)AngleStart; i < (int)AngleEnd; i++)
            {

                degree++;
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (Mathf.Abs(ranges[j] - i) < 1)
                    {
                        Candidates.Add(ranges[j]);
                        last_match = j;
                        Polar_regions[j] = degree;
                        degree = 0;
                        ITC = true;
                        continue;
                    }
                }
            }
        }
        else
        {
            for (int i = (int)AngleStart; i < 360; i++)
            {
                degree++;
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (Mathf.Abs(ranges[j] - i) < 1)
                    {
                        Candidates.Add(ranges[j]);
                        last_match = j;
                        Polar_regions[j] = degree;
                        degree = 0;
                        ITC = true;
                        continue;
                    }
                }
            }

            for (int i = 0; i < AngleEnd; i++)
            {
                degree++;
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (Mathf.Abs(ranges[j] - i) < 1)
                    {
                        Candidates.Add(ranges[j]);
                        last_match = j;
                        Polar_regions[j] = degree;
                        degree = 0;
                        ITC = true;
                        continue;
                    }
                }
            }
        }
        if (ITC == false)
        {
            if (ranges[0] <= AngleStart && AngleEnd <= ranges[1])
            {
                Polar_regions[1] = AngleEnd - AngleStart;
            }
            if (ranges[1] <= AngleStart && AngleEnd <= ranges[2])
            {
                Polar_regions[2] = AngleEnd - AngleStart;
            }
            if (ranges[2] <= AngleStart && AngleEnd <= ranges[3])
            {
                Polar_regions[3] = AngleEnd - AngleStart;
            }
            if (ranges[3] <= AngleStart && AngleEnd <= ranges[4])
            {
                Polar_regions[4] = AngleEnd - AngleStart;
            }
            if (ranges[4] <= AngleStart && AngleEnd <= ranges[5])
            {
                Polar_regions[5] = AngleEnd - AngleStart;
            }
            if (ranges[5] <= AngleStart && AngleEnd <= ranges[6])
            {
                Polar_regions[6] = AngleEnd - AngleStart;
            }
            if (ranges[6] <= AngleStart && AngleEnd <= ranges[7])
            {
                Polar_regions[7] = AngleEnd - AngleStart;
            }
            if (ranges[7] <= AngleStart && AngleEnd <= ranges[0])
            {
                Polar_regions[0] = 360 - AngleStart + AngleEnd;
            }
        }
        else if (last_match == Polar_regions.Length - 1)
        {
            Polar_regions[0] = degree;
        }
        else
        {
            Polar_regions[last_match + 1] = degree;
        }


        Candidates.Add(AngleEnd);


        return Polar_regions;
    }


    public static float get_Car_Velocity(Vector3 Current_Position, Vector3 Previous_Position)
    {
        return (Current_Position - Previous_Position).magnitude;
    }

    public static float get_Car_Delta_Velocity(float Current_Velocity, float Desired_Velocity)
    {
        return Current_Velocity - Desired_Velocity;
    }

    public static float is_Approaching(float Current_Distance, float Previous_Distance)
    {

        float delta = Current_Distance - Previous_Distance;
        Debug.Log("change in distance between cars : " + delta);
        return (delta > 0) ? 0 : 1;
    }


}