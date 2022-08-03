using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// Rotate distortion.
/// </summary>
[DataContract()]
public class Rotate : Distortion
{
    /// <summary>
    /// Roration around word x axis.
    /// </summary>
    [DataMember(Order = 1)]
    public float RotationX { get; set; }

    /// <summary>
    /// Roration around word y axis.
    /// </summary>
    [DataMember(Order = 2)]
    public float RotationY { get; set; }

    /// <summary>
    /// Roration around word z axis.
    /// </summary>
    [DataMember(Order = 3)]
    public float RotationZ { get; set; }

    public override Vector3 Transform(Vector3 input, Vector3 center)
    {
        Vector3 V = input - center;
        Vector3 rotatedPoint = Quaternion.Euler(RotationX, RotationY, RotationZ) * V;
        return rotatedPoint + center;
    }

    public override BaseForm Control
    {
        get => BaseForm.Create<RotateControl>().Init(this);
    }
}
