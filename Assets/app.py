from flask import Flask, request, jsonify
import numpy as np
import pickle

app = Flask(__name__)

# ✅ Load Models
speed_model = pickle.load(open("Models/speed_model.pkl", "rb"))
steering_model = pickle.load(open("Models/steering_model.pkl", "rb"))
brake_model = pickle.load(open("Models/brake_model.pkl", "rb"))

@app.route('/predict', methods=['POST', 'GET'])
def predict():
    try:
        # ✅ Step 1: Read JSON Data from Unity
        data = request.get_json()
        features = np.array(data["input"]).reshape(1, -1)

        # ✅ Step 2: Check Input Shape
        if features.shape[1] != 20:
            return jsonify({"error": f"Input shape mismatch hogaya hai. Expected 20, got {features.shape[1]}"})

        # ✅ Step 3: Run ML Predictions
        speed = float(speed_model.predict(features)[0])
        steering = float(steering_model.predict(features)[0])
        brake = float(brake_model.predict(features)[0])

        # ✅ Step 4: Send Predictions to Unity
        return jsonify({"speed": speed, "steering": steering, "brake": brake})
    except Exception as e:
        return jsonify({"error": str(e)}), 500
   
if __name__ == '__main__':
    app.run(debug=True)
