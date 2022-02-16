using System;
using UnityEngine;

namespace Tools.RotationOrder
{
    [Serializable]
    public struct Euler
    {
        public enum RotationOrder
        {
            XYZ,
            XZY,
            YXZ,
            YZX,
            ZXY,
            ZYX
        }

        public float x;
        public float y;
        public float z;

        public RotationOrder rotationOrder;

        public Euler(Vector3 angles, RotationOrder rotationOrder) : this (angles.x, angles.y, angles.z, rotationOrder) { }

        public Euler(float x, float y, float z, RotationOrder rotationOrder)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.rotationOrder = rotationOrder;
        }

        public Euler(RotationOrder rotationOrder)
        {
            x = 0f;
            y = 0f;
            z = 0f;

            this.rotationOrder = rotationOrder;
        }

        public void SetFromRotationMatrix (Matrix4x4 rotationMatrix)
        {
            var a = rotationMatrix[0];
            var f = rotationMatrix[4];
            var g = rotationMatrix[8];
            var h = rotationMatrix[1];
            var k = rotationMatrix[5];
            var l = rotationMatrix[9];
            var m = rotationMatrix[2];
            var n = rotationMatrix[6];
            var e = rotationMatrix[10];

            const float ABS_UPPER_LIMIT = 0.99999f;
            switch (rotationOrder)
            {
                case RotationOrder.XYZ:
                    y = Mathf.Asin(Mathf.Clamp(g, -1, 1));
                    if (Mathf.Abs(g) < ABS_UPPER_LIMIT)
                    {
                        x = Mathf.Atan2(-l, e);
                        z = Mathf.Atan2(-f, a);
                    }
                    else
                    {
                        x = Mathf.Atan2(n, k);
                        z = 0f;
                    }
                    break;
                case RotationOrder.XZY:
                    z = Mathf.Asin(-Mathf.Clamp(f, -1, 1));
                    if (Mathf.Abs(f) < ABS_UPPER_LIMIT)
                    {
                        x = Mathf.Atan2(n, k);
                        y = Mathf.Atan2(g, a);
                    }
                    else
                    {
                        x = Mathf.Atan2(-l, e);
                        y = 0f;
                    }
                    break;
                case RotationOrder.YXZ:
                    x = Mathf.Asin(-Mathf.Clamp(l, -1, 1));
                    if (Mathf.Abs(l) < ABS_UPPER_LIMIT)
                    {
                        y = Mathf.Atan2(g, e);
                        z = Mathf.Atan2(h, k);
                    }
                    else
                    {
                        y = Mathf.Atan2(-m, a);
                        z = 0f;
                    }
                    break;
                case RotationOrder.YZX:
                    z = Mathf.Asin(Mathf.Clamp(h, -1, 1));
                    if (Mathf.Abs(h) < ABS_UPPER_LIMIT)
                    {
                        x = Mathf.Atan2(-l, k);
                        y = Mathf.Atan2(-m, a);
                    }
                    else
                    {
                        y = Mathf.Atan2(g, e);
                        x = 0f;
                    }
                    break;
                case RotationOrder.ZXY:
                    x = Mathf.Asin(Mathf.Clamp(n, -1, 1));
                    if (Mathf.Abs(n) < ABS_UPPER_LIMIT)
                    {
                        y = Mathf.Atan2(-m, e);
                        z = Mathf.Atan2(-f, k);
                    }
                    else
                    {
                        z = Mathf.Atan2(h, a);
                        y = 0f;
                    }
                    break;
                case RotationOrder.ZYX:
                    y = Mathf.Asin(-Mathf.Clamp(m, -1, 1));
                    if (Mathf.Abs(m) < ABS_UPPER_LIMIT)
                    {
                        x = Mathf.Atan2(n, e);
                        z = Mathf.Atan2(h, a);
                    }
                    else
                    {
                        z = Mathf.Atan2(-f, k);
                        x = 0f;
                    }
                    break;
                default:
                    break;
            }

            x *= Mathf.Rad2Deg;
            y *= Mathf.Rad2Deg;
            z *= Mathf.Rad2Deg;
        }

        public void SetFromQuaternion (Quaternion quaternion)
        {
            var rotationMatrix = Matrix4x4.Rotate(quaternion);
            SetFromRotationMatrix(rotationMatrix);
        }

        public Quaternion ToQuaternion()
        {
            var quaternion = new Quaternion();

            var x = this.x * Mathf.Deg2Rad;
            var y = this.y * Mathf.Deg2Rad;
            var z = this.z * Mathf.Deg2Rad;

            var c = Mathf.Cos(x * 0.5f);
            var d = Mathf.Cos(y * 0.5f);
            var e = Mathf.Cos(z * 0.5f);
            var f = Mathf.Sin(x * 0.5f);
            var g = Mathf.Sin(y * 0.5f);
            var h = Mathf.Sin(z * 0.5f);

            switch (rotationOrder)
            {
                case RotationOrder.XYZ:
                    quaternion.x = f * d * e + c * g * h;
                    quaternion.y = c * g * e - f * d * h;
                    quaternion.z = c * d * h + f * g * e;
                    quaternion.w = c * d * e - f * g * h;
                    break;
                case RotationOrder.XZY:
                    quaternion.x = f * d * e - c * g * h;
                    quaternion.y = c * g * e - f * d * h;
                    quaternion.z = c * d * h + f * g * e;
                    quaternion.w = c * d * e + f * g * h;
                    break;
                case RotationOrder.YXZ:
                    quaternion.x = f * d * e + c * g * h;
                    quaternion.y = c * g * e - f * d * h;
                    quaternion.z = c * d * h - f * g * e;
                    quaternion.w = c * d * e + f * g * h;
                    break;
                case RotationOrder.YZX:
                    quaternion.x = f * d * e + c * g * h;
                    quaternion.y = c * g * e + f * d * h;
                    quaternion.z = c * d * h - f * g * e;
                    quaternion.w = c * d * e - f * g * h;
                    break;
                case RotationOrder.ZXY:
                    quaternion.x = f * d * e - c * g * h;
                    quaternion.y = c * g * e + f * d * h;
                    quaternion.z = c * d * h + f * g * e;
                    quaternion.w = c * d * e - f * g * h;
                    break;
                case RotationOrder.ZYX:
                    quaternion.x = f * d * e - c * g * h;
                    quaternion.y = c * g * e + f * d * h;
                    quaternion.z = c * d * h - f * g * e;
                    quaternion.w = c * d * e + f * g * h;
                    break;
                default:
                    break;
            }

            return quaternion;
        }

        public Matrix4x4 ToRotationMatrix ()
        {
            var rotationMatrix = new Matrix4x4();

            var x = this.x * Mathf.Deg2Rad;
            var y = this.y * Mathf.Deg2Rad;
            var z = this.z * Mathf.Deg2Rad;

            var f = Mathf.Cos(x);
            var c = Mathf.Sin(x);
            var g = Mathf.Cos(y);
            var d = Mathf.Sin(y);
            var h = Mathf.Cos(z);
            var e = Mathf.Sin(z);

            float a, k, l, m;

            switch (rotationOrder)
            {
                case RotationOrder.XYZ:
                    a = f * h;
                    k = f * e;
                    l = c * h;
                    m = c * e;

                    rotationMatrix[0] = g * h;
                    rotationMatrix[4] = -g * e;
                    rotationMatrix[8] = d;
                    rotationMatrix[1] = k + l * d;
                    rotationMatrix[5] = a - m * d;
                    rotationMatrix[9] = -c * g;
                    rotationMatrix[2] = m - a * d;
                    rotationMatrix[6] = l + k * d;
                    rotationMatrix[10] = f * g;
                    break;
                case RotationOrder.XZY:
                    a = f * g;
                    k = f * d;
                    l = c * g;
                    m = c * d;

                    rotationMatrix[0] = g * h;
                    rotationMatrix[4] = -e;
                    rotationMatrix[8] = d * h;
                    rotationMatrix[1] = a * e + m;
                    rotationMatrix[5] = f * h;
                    rotationMatrix[9] = k * e - l;
                    rotationMatrix[2] = l * e - k;
                    rotationMatrix[6] = c * h;
                    rotationMatrix[10] = m * e + a;
                    break;
                case RotationOrder.YXZ:
                    a = g * h;
                    k = g * e;
                    l = d * h;
                    m = d * e;

                    rotationMatrix[0] = a + m * c;
                    rotationMatrix[4] = l * c - k;
                    rotationMatrix[8] = f * d;
                    rotationMatrix[1] = f * e;
                    rotationMatrix[5] = f * h;
                    rotationMatrix[9] = -c;
                    rotationMatrix[2] = k * c - l;
                    rotationMatrix[6] = m + a * c;
                    rotationMatrix[10] = f * g;
                    break;
                case RotationOrder.YZX:
                    a = f * g;
                    k = f * d;
                    l = c * g;
                    m = c * d;

                    rotationMatrix[0] = g * h;
                    rotationMatrix[4] = m - a * e;
                    rotationMatrix[8] = l * e + k;
                    rotationMatrix[1] = e;
                    rotationMatrix[5] = f * h;
                    rotationMatrix[9] = -c * h;
                    rotationMatrix[2] = -d * h;
                    rotationMatrix[6] = k * e + l;
                    rotationMatrix[10] = a - m * e;
                    break;
                case RotationOrder.ZXY:
                    a = g * h;
                    k = g * e;
                    l = d * h;
                    m = d * e;

                    rotationMatrix[0] = a - m * c;
                    rotationMatrix[4] = -f * e;
                    rotationMatrix[8] = l + k * c;
                    rotationMatrix[1] = k + l * c;
                    rotationMatrix[5] = f * h;
                    rotationMatrix[9] = m - a * c;
                    rotationMatrix[2] = -f * d;
                    rotationMatrix[6] = c;
                    rotationMatrix[10] = f * g;
                    break;
                case RotationOrder.ZYX:
                    a = f * h;
                    k = f * e;
                    l = c * h;
                    m = c * e;

                    rotationMatrix[0] = g * h;
                    rotationMatrix[4] = l * d - k;
                    rotationMatrix[8] = a * d + m;
                    rotationMatrix[1] = g * e;
                    rotationMatrix[5] = m * d + a;
                    rotationMatrix[9] = k * d - l;
                    rotationMatrix[2] = -d;
                    rotationMatrix[6] = c * g;
                    rotationMatrix[10] = f * g;
                    break;
                default:
                    break;
            }
            
            rotationMatrix[3] = 0f;
            rotationMatrix[7] = 0f;
            rotationMatrix[11] = 0f;
            rotationMatrix[12] = 0f;
            rotationMatrix[13] = 0f;
            rotationMatrix[14] = 0f;

            rotationMatrix[15] = 1f;
            return rotationMatrix;
        }

        public static Euler FromRotationMatrix (Matrix4x4 rotationMatrix, RotationOrder rotationOrder)
        {
            var euler = new Euler(rotationOrder);
            euler.SetFromRotationMatrix(rotationMatrix);
            return euler;
        }

        public static Euler FromQuaternion(Quaternion quaternion, RotationOrder rotationOrder)
        {
            var euler = new Euler(rotationOrder);
            euler.SetFromQuaternion(quaternion);
            return euler;
        }
    }
}