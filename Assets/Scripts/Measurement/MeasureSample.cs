using UnityEngine;

/// <summary>
/// Structure containing informations about individual samples.
/// </summary>
public struct MeasureSample
{
    /// <summary>
    /// Time in seconds from the start of the measurement, when this sample was taken.
    /// </summary>
    public double Time { get; set; }

    /// <summary>
    /// Measured distance (in meters) from pointer to the closest point on the object
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Pointer position in word space.
    /// </summary> 
    public Vector3 PointerPosition { get; set; }

    /// <summary>
    /// Position of the closest point on the measured object in word space.
    /// </summary>
    public Vector3 ClosestPointPosition { get; set; }

    /// <summary>
    /// Get the sample in CSV format.
    /// </summary>
    public string CSV
    {
        get => $"{ToStr(Time)},{ToStr(Distance)},{ToStr(PointerPosition)},{ToStr(ClosestPointPosition)}";
    }

    /// <summary>
    /// Konstruct new sample.
    /// </summary>
    /// <param name="time">Time from the start of the measurement.</param>
    /// <param name="pointerPosition">Position of pointer.</param>
    /// <param name="closestPointPosition">Position of closest point on measured object.</param>
    public MeasureSample(double time,  Vector3 pointerPosition, Vector3 closestPointPosition)
    {
        Time = time;
        PointerPosition = pointerPosition;
        ClosestPointPosition = closestPointPosition;
        Distance = Vector3.Distance(pointerPosition, closestPointPosition);
    }

    /// <summary>
    /// Helper method, converts double to string, with dot seperator and 5 decimal places precision.
    /// </summary>
    /// <param name="number">Number to be converted</param>
    /// <returns></returns>
    private string ToStr(double number)
    {
        return string.Format("{0:0.00000}", number);
    }

    /// <summary>
    /// Helper method, converts 3D vector to string, each component seperated with comma.
    /// </summary>
    /// <param name="vec">3D vector to be converted.</param>
    /// <returns>String representation of this sample.</returns>
    private string ToStr(Vector3 vec)
    {
        return $"{ToStr(vec.x)},{ToStr(vec.y)},{ToStr(vec.z)}";
    }
}