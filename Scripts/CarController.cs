
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{

    public Transform UserCar;
    public Color gizmoColor;
    private Vector2 Xaxis;


    public WheelCollider front_right;
    public WheelCollider front_left;
    public WheelCollider back_right;
    public WheelCollider back_left;


    public float a_appear;
    public float v_appear;
    public Vector3 d_appear;

    public float a_motor;



    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;


        //("gismos is my print");
        print("-----------------------------------------");
        Xt = transform.position.x;
        Yt = transform.position.y;
        Zt = transform.position.z;

        print(UserCar.position);

        Gizmos.DrawLine(new Vector3(Xt, 1, Zt),
        new Vector3(Xt + 70, 1, Zt));
        Gizmos.DrawLine(new Vector3(Xt, 1, Zt),
        new Vector3(Xt - 70, 1, Zt));
        Gizmos.DrawLine(new Vector3(Xt, 1, Zt),
        new Vector3(Xt, 1, Zt + 70));
        Gizmos.DrawLine(new Vector3(Xt, 1, Zt),
        new Vector3(Xt, 1, Zt - 70));
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 0.6f);

        Xaxis = new Vector2(0, 1);

        Theta = computeAngele(new Vector3(213, 0, -23));
        float[] Polar_regions = get_Polar_Region(UserCar);
        for (int i = 0; i < Polar_regions.Length; i++)
        {
            print("polar " + i + " : " + Polar_regions[i]);
        }

        float Angle = get_User_Car_Angle(UserCar);
        print("angle : " + Angle);

    }







    // Start is called before the first frame update
    void Start()
    {
        d_appear = transform.position;
        v_appear = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Vector3 current_d = transform.position;
        float v_current_d = (current_d - d_appear).magnitude;
        a_motor = back_left.motorTorque;
        a_appear = v_current_d - v_appear;

        if (Input.GetKey(KeyCode.A))
        {
            ApplySteering(1);
            print("hold on a");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplySteering(0);
            print("hold on d");
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            front_left.steerAngle = 0;
            front_right.steerAngle = 0;
            print("released");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            front_left.steerAngle = 0;
            front_right.steerAngle = 0;
            print("forced");
        }

        if (Input.GetKey(KeyCode.W))
        {
            ApplyAcceleration(0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ApplyAcceleration(1);
        }


        d_appear = current_d;
        v_appear = v_current_d;



    }

    private void ApplySteering(int keyCode)
    {

        if (keyCode == 0)
        {
            if (front_left.steerAngle < 45)
            {
                front_left.steerAngle += 1;
                front_right.steerAngle += 1;
            }
        }
        else if (keyCode == 1)
        {


            if (front_left.steerAngle > -45)
            {
                front_left.steerAngle -= 1;
                front_right.steerAngle -= 1;
            }

        }
        else
        {
            front_left.steerAngle = 0;
            front_right.steerAngle = 0;
        }




    }

    private void ApplyAcceleration(int keyCode)
    {

        if (keyCode == 0)
        {
            if (front_left.steerAngle < 20)
            {
                back_left.motorTorque += 15;
                back_right.motorTorque += 15;
            }
        }
        else if (keyCode == 1)
        {


            if (front_left.steerAngle > -20)
            {
                back_left.motorTorque -= 15;
                back_right.motorTorque -= 15;
            }

        }
        else
        {
            front_left.steerAngle = 0;
            front_right.steerAngle = 0;
        }


    }


}