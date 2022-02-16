# unity-rotation-converter
Convert between euler angle rotations with a specific axis rotation order, quaternions and rotation matrices.

**Go to https://valerdragos.github.io/unity-rotation-order/ for a WebGL build of the example project.**

## Inspiration
I got a lot of inspiration from: https://github.com/gaschler/rotationconverter/. Like in that project, I've used the [three.js library](https://github.com/mrdoob/three.js) math to create the converter.

## Usage
The object used for converting is the `Euler` struct. The angle values are expressed in degrees.

There is also a `UnityExtensionMethods` script that contains `Quaternion` and `Matrix4x4` extension methods for conversion to `Euler`.

```c#
Euler euler1 = new Euler(x: 31, y: 126, z: -55, Euler.RotationOrder.XYZ);

        // Convert from Euler.
Matrix4x4 matrix = euler1.ToRotationMatrix();
Quaternion quaternion = euler1.ToQuaternion();

        // Convert to Euler.

        // Using setters.
Euler euler2 = new Euler(Euler.RotationOrder.YZX);
euler2.SetFromRotationMatrix(matrix);
Euler euler3= new Euler(Euler.RotationOrder.ZXY);
euler3.SetFromQuaternion(quaternion);

        // Or using static methods.
euler2 = Euler.FromRotationMatrix(matrix, Euler.RotationOrder.YZX);
euler3 = Euler.FromQuaternion(quaternion, Euler.RotationOrder.ZXY);

        // Or using extension methods.
euler2 = matrix.ToEuler(Euler.RotationOrder.YZX);
euler3 = quaternion.ToEuler(Euler.RotationOrder.ZXY);
```
