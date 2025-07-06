using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SCC_MLCarController : MonoBehaviour
{
    [Header("Wheels")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Vehicle Settings")]
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeTorque = 3000f;

    [Header("Server")]
    public string flaskURL = "http://127.0.0.1:5000/predict";  // Change if needed
    public float requestInterval = 0.1f;

    private float steering = 0f;
    private float speed = 0f;
    private float brake = 0f;

    void Start()
    {
        StartCoroutine(RequestLoop());
    }

    void FixedUpdate()
    {
        ApplyControls();
    }

    void ApplyControls()
    {
        // Apply Steering
        frontLeftWheel.steerAngle = maxSteeringAngle * steering;
        frontRightWheel.steerAngle = maxSteeringAngle * steering;

        // Apply Motor Torque
        float motorTorque = maxMotorTorque * speed;
        rearLeftWheel.motorTorque = motorTorque;
        rearRightWheel.motorTorque = motorTorque;

        // Apply Brake Torque
        float brakeForce = brake * brakeTorque;
        frontLeftWheel.brakeTorque = brakeForce;
        frontRightWheel.brakeTorque = brakeForce;
        rearLeftWheel.brakeTorque = brakeForce;
        rearRightWheel.brakeTorque = brakeForce;
    }

    IEnumerator RequestLoop()
    {
        while (true)
        {
            // ✅ Collect Feature Data (excluding speed, steering, and brake)
            List<float> inputData = new List<float>
        {
            transform.position.x,  // Position_X
            transform.position.y,  // Position_Y
            transform.position.z,  // Position_Z
            transform.rotation.eulerAngles.x,  // Rotation_X
            transform.rotation.eulerAngles.y,  // Rotation_Y
            transform.rotation.eulerAngles.z,  // Rotation_Z
            Input.GetAxis("Vertical"),  // Throttle_Input
            frontLeftWheel.rpm,  // WheelRPM_FL
            frontRightWheel.rpm,  // WheelRPM_FR
            rearLeftWheel.rpm,  // WheelRPM_RL
            rearRightWheel.rpm,  // WheelRPM_RR
            frontLeftWheel.motorTorque,  // MotorTorque_FL
            frontRightWheel.motorTorque,  // MotorTorque_FR
            rearLeftWheel.motorTorque,  // MotorTorque_RL
            rearRightWheel.motorTorque,  // MotorTorque_RR
            frontLeftWheel.brakeTorque,  // BrakeTorque_FL
            frontRightWheel.brakeTorque,  // BrakeTorque_FR
            rearLeftWheel.brakeTorque,  // BrakeTorque_RL
            rearRightWheel.brakeTorque,  // BrakeTorque_RR
            0.0f // Placeholder for Action (Modify if needed)
        };

            // ✅ Convert list to JSON format: {"input": [value1, value2, ...]}
            string jsonData = "{\"input\": [" + string.Join(",", inputData) + "]}";

            using (UnityWebRequest req = new UnityWebRequest(flaskURL, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
                req.uploadHandler = new UploadHandlerRaw(bodyRaw);
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");

                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        Prediction response = JsonUtility.FromJson<Prediction>(req.downloadHandler.text);
                        speed = Mathf.Clamp01(response.speed);
                        steering = Mathf.Clamp(response.steering, -1f, 1f);
                        brake = Mathf.Clamp01(response.brake);
                    }
                    catch
                    {
                        Debug.LogWarning("⚠️ Failed to parse response: " + req.downloadHandler.text);
                    }
                }
                else
                {
                    Debug.LogError("🚨 HTTP Error: " + req.error);
                }

                yield return new WaitForSeconds(requestInterval);
            }
        }
    }


    [System.Serializable]
    public class Prediction
    {
        public float speed;
        public float steering;
        public float brake;
    }
}
