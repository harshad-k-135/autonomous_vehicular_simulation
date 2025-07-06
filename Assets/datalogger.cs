using UnityEngine;
using System.IO;
using System.Globalization;

public class CarDataLogger : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    public float raycastDistance = 10f; // Adjustable in Inspector

    private string filePath;
    private float startTime;
    private float previousSpeed = 0f;

    void Start()
    {
        filePath = Application.dataPath + "/CarDataLog.csv";

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine("Timestamp,Position_X,Position_Y,Position_Z,Rotation_X,Rotation_Y,Rotation_Z,Speed,Steering_Input,Throttle_Input,Brake_Input," +
                "WheelRPM_FL,WheelRPM_FR,WheelRPM_RL,WheelRPM_RR,MotorTorque_FL,MotorTorque_FR,MotorTorque_RL,MotorTorque_RR,BrakeTorque_FL,BrakeTorque_FR,BrakeTorque_RL,BrakeTorque_RR,Action," +
                "Front_Object,Front_Distance,FrontRight_Object,FrontRight_Distance,FrontLeft_Object,FrontLeft_Distance," +
                "Left_Object,Left_Distance,Right_Object,Right_Distance,RearLeft_Object,RearLeft_Distance,RearRight_Object,RearRight_Distance");
        }

        startTime = Time.time;
    }

    void FixedUpdate()
    {
        float timestamp = (Time.time - startTime) * 100f;
        Vector3 position = transform.position;
        Vector3 rotation = transform.eulerAngles;
        float speed = carRigidbody.velocity.magnitude;
        float steeringInput = Input.GetAxis("Horizontal");
        float throttleInput = Input.GetAxis("Vertical");
        float brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

        float rpmFL, rpmFR, rpmRL, rpmRR;
        float torqueFL, torqueFR, torqueRL, torqueRR;
        float brakeFL, brakeFR, brakeRL, brakeRR;

        frontLeftWheel.GetWorldPose(out _, out _);
        frontRightWheel.GetWorldPose(out _, out _);
        rearLeftWheel.GetWorldPose(out _, out _);
        rearRightWheel.GetWorldPose(out _, out _);

        rpmFL = frontLeftWheel.rpm;
        rpmFR = frontRightWheel.rpm;
        rpmRL = rearLeftWheel.rpm;
        rpmRR = rearRightWheel.rpm;

        torqueFL = frontLeftWheel.motorTorque;
        torqueFR = frontRightWheel.motorTorque;
        torqueRL = rearLeftWheel.motorTorque;
        torqueRR = rearRightWheel.motorTorque;

        brakeFL = frontLeftWheel.brakeTorque;
        brakeFR = frontRightWheel.brakeTorque;
        brakeRL = rearLeftWheel.brakeTorque;
        brakeRR = rearRightWheel.brakeTorque;

        string action = DetermineAction(speed, previousSpeed, throttleInput, brakeInput, steeringInput);
        previousSpeed = speed;

        // Perform Raycasts
        RaycastHit hitFront, hitFrontRight, hitFrontLeft, hitLeft, hitRight, hitRearLeft, hitRearRight;
        string frontObject = GetRaycastHit(Vector3.forward, out hitFront);
        string frontRightObject = GetRaycastHit((Vector3.forward + Vector3.right).normalized, out hitFrontRight);
        string frontLeftObject = GetRaycastHit((Vector3.forward + Vector3.left).normalized, out hitFrontLeft);
        string leftObject = GetRaycastHit(Vector3.left, out hitLeft);
        string rightObject = GetRaycastHit(Vector3.right, out hitRight);
        string rearLeftObject = GetRaycastHit((Vector3.back + Vector3.left).normalized, out hitRearLeft);
        string rearRightObject = GetRaycastHit((Vector3.back + Vector3.right).normalized, out hitRearRight);

        // Write data to CSV
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23}," +
                "{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35}",
                timestamp, position.x, position.y, position.z, rotation.x, rotation.y, rotation.z, speed,
                steeringInput, throttleInput, brakeInput,
                rpmFL, rpmFR, rpmRL, rpmRR,
                torqueFL, torqueFR, torqueRL, torqueRR,
                brakeFL, brakeFR, brakeRL, brakeRR, action,
                frontObject, hitFront.distance, frontRightObject, hitFrontRight.distance, frontLeftObject, hitFrontLeft.distance,
                leftObject, hitLeft.distance, rightObject, hitRight.distance, rearLeftObject, hitRearLeft.distance, rearRightObject, hitRearRight.distance));
        }
    }

    string GetRaycastHit(Vector3 direction, out RaycastHit hit)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, raycastDistance))
        {
            return hit.collider.gameObject.name; // Logs the name of the GameObject that contains the collider
        }
        return "None";
    }

    // ✅ Move DetermineAction INSIDE the class
    string DetermineAction(float speed, float prevSpeed, float throttle, float brake, float steering)
    {
        if (brake > 0 && speed < 0.5f)
        {
            return "Stop";
        }
        if (brake > 0)
        {
            return "Slow Down";
        }
        if (throttle > 0 && speed > prevSpeed)
        {
            return "Speed Up";
        }
        if (steering < -0.1f)
        {
            return "Turn Left";
        }
        if (steering > 0.1f)
        {
            return "Turn Right";
        }
        return "Maintain Speed";
    }
}
