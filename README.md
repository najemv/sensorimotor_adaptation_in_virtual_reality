# Sensorimotor Adaptation in Virtual Reality
## Bachelor thesis at FI MU.

According to the official assignment, the application should provide
following functionality:
- Capture the movement of the user's hands, and display the "template"
and a simple virtual environment.
- Display feedback responding to the user's movement in real-time,
including defined movement distortion (rotation, mirroring).
- Evaluation of user movement and deviation from the original. Export
these records in a format suitable for further processing.
- Possibility to configure the test sequence (number of repetitions, timing
of individual tests).

This repository contains the source code as a Unity
project. The structure of this archive follows the classic conventions of the
Unity projects:
- `Assets/Scripts` folder contains all classes and scripts that the
application use. The application’s backend structure is stored in the
folder Application inside this folder, specifically.
- `Assets/Models` contains a few models that are used for reference
path.
- `Assets/Scenes` contains created Scenes.

The `build` folder contains the following files:
- `SensorimotorAdaptation.apk` installer for installing the
application on Oculus Quest 2 headset.
- `settings.xml` with some predefined sequences. It contains the
following sequences:
- Rotation with 12° steps
- Rotation with 30° steps
- Rotation with 60° steps
- Horizontal mirroring
- Vertical mirroring
- Alternating mirroring
- `README.md` file with instructions how to install the application and
load sequences in the application.

