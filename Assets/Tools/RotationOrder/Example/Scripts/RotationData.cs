using UnityEngine;

namespace Tools.RotationOrder.Example
{
    public struct RotationData
    {
        public Euler euler;
        public Quaternion quaternion;
        public Matrix4x4 matrix4x4;

        public RotationData(Euler euler)
        {
            this.euler = euler;
            quaternion = euler.ToQuaternion();
            matrix4x4 = euler.ToRotationMatrix();
        }
    }
}