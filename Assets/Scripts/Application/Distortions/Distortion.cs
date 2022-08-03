using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// Base class for all distortions.
/// </summary>
[DataContract()]
[KnownType(typeof(Mirror))]
[KnownType(typeof(Rotate))]
public abstract class Distortion
{
    /// <summary>
    /// Transform coordinate into new coordinate.
    /// </summary>
    /// <param name="input">Input coordinate.</param>
    /// <param name="center">Center of plotted path.</param>
    /// <returns>New trasfromed coordinate.</returns>
    public abstract Vector3 Transform(Vector3 input, Vector3 center);

    /// <summary>
    /// Show control for UI preview of distortion.
    /// </summary>
    public abstract BaseForm Control { get; }
}
