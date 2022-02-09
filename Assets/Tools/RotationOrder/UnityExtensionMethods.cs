using UnityEngine;

namespace Tools.RotationOrder
{
    public static class UnityExtensionMethods
    {
        public static Euler ToEuler(this Quaternion quaternion, Euler.RotationOrder rotationOrder)
        {
            return Euler.FromQuaternion(quaternion, rotationOrder);
        }

        public static Euler ToEuler(this Matrix4x4 rotationMatrix, Euler.RotationOrder rotationOrder)
        {
            return Euler.FromRotationMatrix(rotationMatrix, rotationOrder);
        }
    }
}