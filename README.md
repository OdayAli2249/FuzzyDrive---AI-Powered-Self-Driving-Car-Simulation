## FuzzyDrive - AI-Powered Self-Driving Car Simulation

### Description:
Welcome to **FuzzyDrive**, a Unity 3D simulation that demonstrates the capabilities of an AI-powered self-driving car using fuzzy logic and rule-based system technologies. This project tackles the complex challenge of implementing a self-driving car simulation, based on research from a self-driving car simulation paper which you can find [here](https://www.researchgate.net/publication/2247942_Fuzzy_Control_to_Drive_Car-Like_Vehicles).

### Features:
- **Fuzzy Logic System**: The self-driving car's decision-making process is built on fuzzy logic, utilizing fuzzy variables and fuzzy sets to manage the uncertainties and continuous nature of real-world driving conditions.
- **Rule-Based Inference Engine**: The AI employs a rule-based system with an inference engine that processes a set of fuzzy rules stored in the agenda to determine the optimal actions for the car.
- **Obstacle Avoidance**: The AI car dynamically avoids both moving and static obstacles using real-time data and fuzzy logic principles.
- **User Interaction**: The simulation includes a user-controlled car, allowing for interactive testing and demonstration of the self-driving car's capabilities.

### Scientific Concepts:
- **Fuzzy Variables**: The state of the car is determined by five key fuzzy variables:
  - **x, y**: The coordinates of the car's center of mass in the ℝ² plane.
  - **θ**: The orientation of the car, representing the angle between the car's longitudinal axis and the x-axis.
  - **∅**: The steering angle of the front wheels.
  - **V**: The speed of the car, representing the distance traveled by the center of mass in one time step.

- **Fuzzy Sets and Rules**: The AI behavior is governed by fuzzy sets and rules:
  - **Path Follow Behavior**: Ensures the car follows a specific path from a start point to an end point when no obstacle is within a certain distance (distance-save). The input variables for this behavior include:
    - **α(t)**: The angle between the line from the car's center of mass to the current target node and the car's longitudinal axis.
    - **Δv(t)**: The difference between the desired speed and the current speed of the car.
  - **Obstacle Avoidance Behavior**: Activates when an obstacle is within a certain distance (distance-save). The input variables for this behavior include:
    - **v(t)**: The current speed of the car.
    - **d(t)**: The current distance between the car and the obstacle.
    - **Polar_region(t)**: The angular region around the car where the obstacle is detected.
    - **Approach(t)**: A crisp value indicating whether a moving obstacle is approaching the car.

### Behaviors:
- **Path Follow Behavior**:
  - **Input Variables**:
    - **α(t)**: {0, π/4, π/2, 3π/4, π, 5π/4, 3π/2, 7π/4, 2π}
    - **Δv(t)**: {Negative-high, Negative-low, Zero, Positive-low, Positive-high}
  - This behavior ensures the car follows a predetermined path in the absence of nearby obstacles.

- **Obstacle Avoidance Behavior**:
  - **Input Variables**:
    - **v(t)**: {low, moderate, high}
    - **d(t)**: {very-close, close, far}
    - **Polar_region(t)**: {Front-Ahead, Front-Right, Side-Right, Rear-Right, Rear-Behind, Rear-Left, Side-Left, Front-Left}
    - **Approach(t)**: {0, 1}
  - This behavior adjusts the car's steering and speed to avoid collisions with detected obstacles. Defuzzification uses the center of mass method to compute the final output.

### Defuzzification:
- The system employs the center of mass method for defuzzification, ensuring smooth transitions between different control states.
- **Output Variables**:
  - **∅**: Steering angle of the front wheels.
  - **v**: Acceleration applied to the car.

### Usage:
- **Start the Simulation**: Use the Unity Play button to start the simulation.
- **Control the User Car**: Use the keyboard or controller to navigate the user-controlled car.
- **Observe the AI Car**: Watch the AI-powered car as it dynamically navigates the environment, avoiding obstacles and reacting to the user-controlled car.

### Figures and Charts:
![Figure 1: State Variables of the Car](path/to/figure1.png)
*Figure 1: State Variables of the Car*

![Figure 2: Angle Definitions](path/to/figure2.png)
*Figure 2: Angle Definitions*

![Figure 3: Polar Regions for Obstacle Detection](path/to/figure3.png)
*Figure 3: Polar Regions for Obstacle Detection*

### Contributions:
Contributions are welcome! Please fork the repository and create a pull request with your improvements or bug fixes.
Explore, modify, and enhance the project to gain insights into AI, fuzzy logic, and game development with Unity. FuzzyDrive offers an engaging and educational experience for developers and researchers alike. Happy coding!
