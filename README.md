User Instructions for AOS System :
System Requirements:

● Ubuntu 20.04
● ROS2 Galactic

AOS System Installation Guide
Installation Steps:
1. Run Bash Script:
- Download and run the installation bash script: `AOS_ubuntu20_install_v7.bash`.
- You can find it on GitHub:
[AOS-WebAPI](https://github.com/orhaimwerthaim/AOS-WebAPI).
2.YouTube Tutorial Links:
These links provide detailed visual guides on the installation and usage of AOS:
1. [AOSInstallation](https://www.youtube.com/watch?v=LtvghBdEWNg&ab_channel=AOSBGU)
2. [Additional InstallationSteps](https://www.youtube.com/watch?v=Zm-KTZV180g&ab_channel=AOSBGU)
3. [Model Verification Using AOS GUI](https://www.youtube.com/watch?v=wyLWg-b7Rww&ab_channel=AOSBGU)
4. [AdvancedConfiguration](https://www.youtube.com/watch?v=FE91GuK-O4A&ab_channel=AOSBGU)

   
3.Debug and Run the Solver:
●After installing AOS, follow the instructions in the videos to debug the system and run
the solver.
Download Postman:
●Use Postman to send HTTP requests as shown in the videos. The videos explain
how to connect to the AOS system.
4.Installing ROS 2, Gazebo, RViz, and Navigation 2:
1.Install ROS 2:
- Follow the ROS 2 installation guide for your OS
[here](https://docs.ros.org/en/foxy/Installation.html).
2. Install Gazebo:
- Use the command: `sudo apt-get install gazebo`.
3.Install RViz:
- Use the command: `sudo apt-get install ros-foxy-rviz2`.
4.Install Navigation 2:
- Use the command: `sudo apt-get install ros-galactic-navigation2`.

5.Setup ROS2 Enviroment by .bashrc:
1.Source ROS 2:
- Add the following line to your `.bashrc` file:
source /opt/ros/Galactic/setup.bash
2. Setup Modelburger for Gazebo:
- Add the following line to your `.bashrc` file:
export
GAZEBO_MODEL_PATH=/path/to/your/models:$GAZEBO_MODEL_PATH
After editing `.bashrc`, remember to source it:
“source ~/.bashrc”
export TURTLEBOT3_MODEL=burger
source ~/galactic_ws=NAME_OF_YOUR_WORKSPACE/install/local_setup.bash
source /opt/ros/galactic/setup.bash
source /usr/share/colcon_argcomplete/hook/colcon-argcomplete.bash

it should be like this in .bashrc:
![Screenshot from 2024-07-22 19-33-20](https://github.com/user-attachments/assets/dfa6bf76-6ff7-4a62-b05b-f868c901910e)



Updating the AOS System
To update your AOS system and integrate ROS 2, follow these steps:
1. Download the Updated AOS System:
- Visit the GitHub repository for the [final AOS system](https://github.com/final-AOS-system).
- Download the updated AOS system that implements ROS 2.
2. Replace the Current AOS-WebAPI:
- Navigate to the GitHub repository provided and download the
AOS-WebAPI project.
- Replace your existing AOS-WebAPI with the downloaded files from the
new repository.
3. Update the AM File:
- Replace the old AM file with the new one included in the updated AOS
system. The AM file defines the actions and behaviors of the AOS system.
you can install the updated AM from this Github :
https://github.com/michel1912/ros2_Workspace
4. Update Services:
- Ensure that all services and dependencies in your AOS system are
updated to be compatible with ROS 2. This includes checking and modifying
configurations as necessary to support the new ROS 2 environment.
5. MODIFY THE AOS-SOLVER:
  add these lines in the AOS-SOLVER (update for the new feature):
  do this update in the class of : mongoDB_Bridge.cpp
  ![Screenshot from 2024-07-22 19-41-23](https://github.com/user-attachments/assets/fa30db71-dc2c-4763-b5c6-9cf70e7c285e)

