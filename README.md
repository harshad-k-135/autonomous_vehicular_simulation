# 🚗 Autonomous Vehicular Simulation

[![Unity](https://img.shields.io/badge/Unity-2022.3.57f1-black?logo=unity)](https://unity.com/)
[![Python](https://img.shields.io/badge/Python-3.8+-blue?logo=python)](https://python.org/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![ML Framework](https://img.shields.io/badge/ML-XGBoost%20%7C%20LightGBM-orange)](https://github.com/microsoft/LightGBM)

A comprehensive **Unity-based autonomous vehicle simulation** that integrates advanced **machine learning models** for realistic self-driving car behavior. This project demonstrates the fusion of computer graphics, physics simulation, and artificial intelligence to create an intelligent transportation system in a virtual city environment.

## 🌟 Key Features

### 🤖 **AI-Powered Vehicle Control**
- **Machine Learning Integration**: Real-time neural network inference using Unity Barracuda
- **Multi-Model Architecture**: Separate ML models for speed, steering, and braking decisions
- **Flask API Backend**: Python server for ML model predictions
- **Hybrid Control Systems**: Both rule-based and ML-driven autonomous behaviors

### 🏙️ **Realistic Urban Environment**
- **3D City Simulation**: Low-poly urban environment with roads, buildings, and intersections
- **Traffic Management**: Multi-vehicle spawning and traffic flow simulation
- **Checkpoint Navigation**: Waypoint-based path planning system
- **Dynamic Obstacles**: Real-time collision detection and avoidance

### 📊 **Advanced Data Collection & Analytics**
- **Comprehensive Telemetry**: 20+ vehicle parameters logged in real-time
- **Performance Metrics**: Speed, steering angles, wheel RPM, motor torque data
- **Training Data Generation**: Automatic dataset creation for model improvement
- **Sensor Simulation**: Raycast-based object detection and distance measurement

### 🔬 **Machine Learning Pipeline**
- **Multi-Target Regression**: Separate models for speed, steering, and brake control
- **XGBoost & LightGBM**: High-performance gradient boosting algorithms
- **Feature Engineering**: 20+ input features including position, rotation, wheel dynamics
- **Model Export**: ONNX format for cross-platform deployment

## 🏗️ System Architecture

```
┌─────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│   Unity Engine  │    │   Flask Server   │    │  ML Models      │
│                 │    │                  │    │                 │
│ • Vehicle Physics│◄──►│ • HTTP API       │◄──►│ • Speed Model   │
│ • Collision Det.│    │ • JSON Parsing   │    │ • Steering Model│
│ • Data Logging  │    │ • Model Inference│    │ • Brake Model   │
│ • Visualization │    │ • Response Gen.  │    │ • Feature Proc. │
└─────────────────┘    └──────────────────┘    └─────────────────┘
        │                       │                       │
        │                       │                       │
        ▼                       ▼                       ▼
┌─────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│   Data Storage  │    │  Communication   │    │   Training      │
│                 │    │                  │    │                 │
│ • CarDataLog.csv│    │ • RESTful API    │    │ • Jupyter NB    │
│ • Feature Names │    │ • UnityWebRequest│    │ • Pandas/NumPy  │
│ • Model Files   │    │ • Real-time Sync │    │ • Scikit-learn  │
└─────────────────┘    └──────────────────┘    └─────────────────┘
```

## 🚀 Quick Start

### Prerequisites
- **Unity 2022.3.57f1** or later
- **Python 3.8+** with pip
- **Git** for version control

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/harshad-k-135/autonomous_vehicular_simulation.git
   cd autonomous_vehicular_simulation
   ```

2. **Set Up Python Environment**
   ```bash
   # Create virtual environment
   python -m venv venv
   
   # Activate (Windows)
   venv\Scripts\activate
   
   # Install dependencies
   pip install flask numpy pandas scikit-learn xgboost lightgbm joblib
   ```

3. **Open Unity Project**
   - Launch Unity Hub
   - Click "Open" and select the project folder
   - Wait for import completion

4. **Start the ML Server**
   ```bash
   cd Assets
   python app.py
   ```
   Server will start at `http://127.0.0.1:5000`

5. **Run the Simulation**
   - Open `Assets/Scenes/New Scene.unity`
   - Press Play in Unity Editor
   - Watch autonomous vehicles navigate the city!

## 🎮 Controls & Usage

### **Manual Control Mode**
- **W/S**: Accelerate/Reverse
- **A/D**: Steering
- **Space**: Handbrake
- **Tab**: Switch between vehicles

### **Autonomous Mode**
- Vehicles automatically navigate using AI
- Real-time ML predictions for control inputs
- Collision avoidance and traffic management
- Data logging for model improvement

## 📋 Core Components

### **🧠 Machine Learning Controllers**

#### `MLDrivingController.cs`
- Unity Barracuda neural network inference
- Real-time ONNX model execution
- Feature extraction and normalization
- Control signal application

#### `SCC_MLCarController.cs`
- Flask server communication
- HTTP-based ML predictions
- Wheel collider integration
- Real-time control application

### **🚗 Vehicle Management**

#### `CarAIController.cs`
- Checkpoint-based navigation
- Physics-based vehicle dynamics
- Speed and steering control
- Collision detection system

#### `NPCDriver.cs`
- Multi-vehicle spawning
- Traffic flow simulation
- Waypoint following
- Dynamic behavior patterns

### **📊 Data Systems**

#### `DataLogger.cs`
- 20+ parameter telemetry
- CSV format data export
- Real-time sensor readings
- Training dataset generation

#### `AvoidCollision.cs`
- Raycast-based obstacle detection
- Dynamic collision avoidance
- Traffic-aware behavior
- Safety override systems

### **🌐 ML Backend**

#### `app.py` (Flask Server)
```python
# Key Features:
• RESTful API endpoints
• Real-time model inference
• JSON data processing
• Error handling & validation
• Multi-model prediction pipeline
```

#### `fsd_ml_attaching_code.ipynb`
```python
# Training Pipeline:
• Data preprocessing & cleaning
• Feature engineering (20+ parameters)
• XGBoost for speed prediction
• LightGBM for steering/braking
• Model evaluation & export
• ONNX conversion for Unity
```

## 📈 Machine Learning Models

### **Model Architecture**
- **Speed Prediction**: XGBoost Regressor (R² = 0.96+)
- **Steering Control**: LightGBM Regressor (R² = 0.95+)
- **Brake Control**: LightGBM Regressor (R² = 1.00)

### **Input Features (20 Parameters)**
```
Position: X, Y, Z coordinates
Rotation: Euler angles (X, Y, Z)
Motion: Velocity magnitude, acceleration
Wheels: RPM for all 4 wheels
Torque: Motor torque per wheel
Brakes: Brake force distribution
Action: Categorical driving state
```

### **Training Data**
- **Source**: Real-time Unity simulation data
- **Format**: CSV with timestamped entries
- **Size**: 10,000+ samples per session
- **Quality**: High-frequency (100Hz) collection
- **Features**: Normalized and engineered

### **Model Performance**
| Model | Algorithm | R² Score | MAE | Use Case |
|-------|-----------|----------|-----|----------|
| Speed | XGBoost | 0.9612 | 0.0234 | Velocity control |
| Steering | LightGBM | 0.9586 | 0.0429 | Direction control |
| Brake | LightGBM | 1.0000 | 0.0000 | Stopping behavior |

## 🗂️ Project Structure

```
autonomous_vehicular_simulation/
├── Assets/
│   ├── Models/                     # ML model files (.pkl, .onnx)
│   ├── Scenes/                     # Unity scene files
│   ├── Scripts/                    # C# scripts
│   │   ├── mldrivingcontroller.cs  # Unity Barracuda ML
│   │   ├── sccmlcontroller.cs      # Flask API integration
│   │   ├── datalogger.cs           # Telemetry collection
│   │   ├── avoidcollision.cs       # Safety systems
│   │   └── npcdriver.cs            # Traffic simulation
│   ├── app.py                      # Flask ML server
│   ├── fsd_ml_attaching_code.ipynb # ML training pipeline
│   ├── CarDataLog.csv              # Training dataset
│   ├── feature_names.json          # Model features
│   ├── CarAI/                      # AI behavior scripts
│   ├── Simple Car Controller/      # Vehicle physics
│   └── SimplePoly City Assets/     # 3D environment
├── Packages/                       # Unity packages
├── ProjectSettings/                # Unity configuration
├── README.md
└── LICENSE
```

## 🛠️ Technical Specifications

### **Unity Configuration**
- **Version**: 2022.3.57f1
- **Render Pipeline**: Built-in RP
- **Physics**: 3D with realistic constraints
- **Input System**: Both legacy and new input system
- **Platforms**: Windows, MacOS, Linux

### **Machine Learning Stack**
- **Framework**: Scikit-learn ecosystem
- **Algorithms**: XGBoost, LightGBM
- **Inference**: Unity Barracuda + Flask API
- **Data**: Pandas, NumPy processing
- **Export**: ONNX, Pickle formats

### **Performance Metrics**
- **Simulation**: 60+ FPS in Unity
- **ML Inference**: <10ms latency
- **Data Logging**: 100Hz frequency
- **Network**: Real-time HTTP requests
- **Memory**: Optimized model loading

## 🔧 Advanced Configuration

### **Adjusting AI Behavior**
```csharp
// In CarAIController.cs
public int speedLimit = 50;           // Max speed (km/h)
public float distanceFromObjects = 2f; // Following distance
public int recklessnessThreshold = 0;  // Speed deviation
public bool CheckPointSearch = true;   // Enable pathfinding
```

### **ML Model Parameters**
```python
# In fsd_ml_attaching_code.ipynb
xgb_model = xgb.XGBRegressor(
    objective='reg:squarederror',
    n_estimators=100,
    learning_rate=0.1
)

lgb_model = lgb.LGBMRegressor(
    n_estimators=100,
    learning_rate=0.1
)
```

### **Data Collection Settings**
```csharp
// In DataLogger.cs
public float raycastDistance = 10f;   // Sensor range
private float startTime;              // Session timing
public WheelCollider[] wheels;        // Vehicle sensors
```

## 🎯 Use Cases & Applications

### **🎓 Educational**
- **Autonomous Systems Research**: Study self-driving algorithms
- **Machine Learning Education**: Hands-on ML model training
- **Game Development Learning**: Unity physics and AI
- **Computer Vision**: Sensor simulation and processing

### **🏭 Industry Applications**
- **Automotive Testing**: Virtual vehicle validation
- **Traffic Engineering**: Urban planning simulation
- **AI Development**: Autonomous behavior prototyping
- **Simulation Training**: Safe testing environment

### **🔬 Research Areas**
- **Multi-Agent Systems**: Vehicle interaction studies
- **Reinforcement Learning**: Reward-based training
- **Computer Vision**: Object detection research
- **Robotics**: Motion planning algorithms

## 🤝 Contributing

We welcome contributions! Here's how to get started:

### **Development Setup**
1. Fork the repository
2. Create feature branch: `git checkout -b feature/amazing-feature`
3. Install development dependencies
4. Make your changes with tests
5. Commit: `git commit -m 'Add amazing feature'`
6. Push: `git push origin feature/amazing-feature`
7. Open a Pull Request

### **Contribution Areas**
- 🐛 **Bug Fixes**: Improve stability and performance
- ✨ **New Features**: Add ML models, sensors, or behaviors
- 📚 **Documentation**: Enhance guides and tutorials
- 🧪 **Testing**: Add unit tests and validation
- 🎨 **Assets**: Create new vehicle models or environments

### **Code Standards**
- **C#**: Follow Unity coding conventions
- **Python**: PEP 8 style guidelines
- **Comments**: Document complex algorithms
- **Testing**: Include unit tests for new features

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **Unity Technologies** - For the excellent game engine and ML-Agents toolkit
- **BoneCracker Games** - Simple Car Controller asset
- **Low Poly Assets** - SimplePoly City environment
- **ML Community** - XGBoost and LightGBM frameworks
- **Open Source** - Flask, Scikit-learn, and supporting libraries

## 📊 Project Statistics

- **Lines of Code**: 5,000+ (C#, Python)
- **ML Models**: 3 trained models
- **Features**: 20+ input parameters
- **Accuracy**: 95%+ across all models
- **Performance**: Real-time inference
- **Documentation**: Comprehensive guides

## 🔮 Future Roadmap

### **Planned Features**
- 🌐 **Multi-Player Support**: Network-based vehicle simulation
- 🎮 **VR Integration**: Immersive driving experience
- 📱 **Mobile Platform**: iOS/Android deployment
- 🤖 **Advanced AI**: Reinforcement learning integration
- 🌍 **Weather System**: Dynamic environmental conditions
- 📈 **Analytics Dashboard**: Real-time performance monitoring

### **Research Directions**
- **Computer Vision**: Camera-based perception
- **Sensor Fusion**: Multi-modal input processing
- **Edge Computing**: Optimized mobile inference
- **Swarm Intelligence**: Collaborative vehicle behavior

---

**🚀 Ready to explore autonomous vehicle simulation? Clone the repo and start your journey into the future of transportation!**

For questions, suggestions, or collaboration opportunities, please open an issue or reach out to the development team.