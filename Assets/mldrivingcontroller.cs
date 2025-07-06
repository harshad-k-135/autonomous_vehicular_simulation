using UnityEngine;
using Unity.Barracuda;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class MLDrivingController : MonoBehaviour
{
    public NNModel speedModelAsset;
    public NNModel steeringModelAsset;
    public NNModel brakeModelAsset;

    private IWorker speedWorker;
    private IWorker steeringWorker;
    private IWorker brakeWorker;

    private List<string> featureNames;

    public Rigidbody carRigidbody;

    void Start()
    {
        var speedModel = ModelLoader.Load(speedModelAsset);
        var steeringModel = ModelLoader.Load(steeringModelAsset);
        var brakeModel = ModelLoader.Load(brakeModelAsset);

        speedWorker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, speedModel);
        steeringWorker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, steeringModel);
        brakeWorker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, brakeModel);

        LoadFeatureNames();
    }

    void LoadFeatureNames()
    {
        string path = Path.Combine(Application.dataPath, "Models/feature_names.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            featureNames = JsonConvert.DeserializeObject<List<string>>(json);
        }
    }

    void FixedUpdate()
    {
        float[] inputData = GenerateInputData();
        Tensor inputTensor = new Tensor(1, inputData.Length, inputData);

        speedWorker.Execute(inputTensor);
        steeringWorker.Execute(inputTensor);
        brakeWorker.Execute(inputTensor);

        float predictedSpeed = speedWorker.PeekOutput().ToReadOnlyArray()[0];
        float predictedSteering = steeringWorker.PeekOutput().ToReadOnlyArray()[0];
        float predictedBrake = brakeWorker.PeekOutput().ToReadOnlyArray()[0];

        inputTensor.Dispose();

        ApplyControls(predictedSpeed, predictedSteering, predictedBrake);
    }

    float[] GenerateInputData()
    {
        // Replace this with real-time data from your car
        return new float[]
        {
            transform.position.x,
            transform.position.y,
            transform.position.z,
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z,
            carRigidbody.velocity.magnitude,
            0f, 0f, 0f, // Placeholder for steering/throttle/brake input
            0f, 0f, 0f, 0f, // RPMs
            0f, 0f, 0f, 0f, // Motor torque
            0f, 0f, 0f, 0f, // Brake torque
            0f // Action
        };
    }

    void ApplyControls(float speed, float steering, float brake)
    {
        // Apply control logic to car
        Debug.Log($"Predicted Speed: {speed}, Steering: {steering}, Brake: {brake}");
        // Example: set inputs to a car controller script
    }

    void OnDestroy()
    {
        speedWorker.Dispose();
        steeringWorker.Dispose();
        brakeWorker.Dispose();
    }
}
