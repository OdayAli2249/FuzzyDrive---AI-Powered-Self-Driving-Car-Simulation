using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{

    public WheelCollider target_wheel;
    Vector3 wheel_position = new Vector3();
    Quaternion wheel_rotation = new Quaternion();
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target_wheel.GetWorldPose(out wheel_position, out wheel_rotation);
        transform.position = wheel_position;
        transform.rotation = wheel_rotation;
    }
}
