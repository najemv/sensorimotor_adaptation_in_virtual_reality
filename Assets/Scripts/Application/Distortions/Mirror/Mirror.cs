using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// Mirror distortion.
/// </summary>
[DataContract()]
public class Mirror : Distortion
{
    /// <summary>
    /// X coordinate of plane's normal.
    /// </summary>
    [DataMember(Order = 1)]
    public float NormalX { get; set; }

    /// <summary>
    /// Y coordinate of plane's normal.
    /// </summary>
    [DataMember(Order = 2)]
    public float NormalY { get; set; }

    /// <summary>
    /// Z coordinate of plane's normal.
    /// </summary>
    [DataMember(Order = 3)]
    public float NormalZ { get; set; }

    public override Vector3 Transform(Vector3 input, Vector3 center)
    {
        Vector3 normal = new Vector3(NormalX, NormalY, NormalZ);
        Vector3 V = input - center;
        float planeDistance = Vector3.Dot(V, normal);
        Vector3 mirroredPoint = input - 2 * planeDistance * normal;
        return mirroredPoint;
    }


    public override BaseForm Control
    {
        get => BaseForm.Create<MirrorControl>().Init(this);
    }

}
