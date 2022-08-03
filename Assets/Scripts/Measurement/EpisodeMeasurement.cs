using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Contains measurement information about individual episodes.
/// </summary>
public class EpisodeMeasurement
{
    /// <summary>
    /// Time when the measument started.
    /// </summary>
    private DateTime TimeStart { get; set; } = DateTime.Now;

    /// <summary>
    /// Time when the measurementEnded.
    /// </summary>
    private DateTime TimeEnd { get; set; } = DateTime.Now;

    public double RoundsCompleted { get; set; } = 0;

    /// <summary>
    /// Reference to trial this measurement belongs to.
    /// </summary>
    private Episode Episode { get; set; }

    /// <summary>
    /// Individual samples.
    /// </summary>
    private List<MeasureSample> Data { get; set; } = new List<MeasureSample>();

    /// <summary>
    /// Convert samples to CSV format.
    /// </summary>
    public string CSV
    {
        get => Data.Select(r => r.CSV).Aggregate((i, j) => i + Environment.NewLine + j);
    }

    /// <summary>
    /// Gets prefix of this measurement (used for file names and distinction of individual trials).
    /// </summary>
    public string Prefix
    {
        get => $"{Episode.EpisodePosition} - {Episode.Name}";
    }

    /// <summary>
    /// Gets average error distance (in millimeters).
    /// </summary>
    public double AverageDistance
    {
        get => Data.Select(record => record.Distance * 1000).Sum() / Data.Count;
    }

    /// <summary>
    /// Gets root-mean-square-errror value (in millimeters).
    /// </summary>
    public double RMSE
    {
        get => Math.Sqrt(Data.Select(record => Math.Pow(record.Distance * 1000, 2)).Sum() / Data.Count);
    }

    /// <summary>
    /// Constructs new measurement.
    /// </summary>
    /// <param name="episode">Episodethis measurement belongs to.</param>
    public EpisodeMeasurement(Episode episode)
    {
        Episode = episode;
    }

    /// <summary>
    /// Adds new sample to measure.
    /// </summary>
    /// <param name="pointerPosition">Position of pointer.</param>
    /// <param name="closestPointPosition">Position of closest point on measured object.</param>
    public void AddMeasure(Vector3 pointerPosition, Vector3 closestPointPosition)
    {
        double time = (DateTime.Now - TimeStart).TotalSeconds;
        Data.Add(new MeasureSample(time, pointerPosition, closestPointPosition));
    }

    /// <summary>
    /// End the measurent.
    /// </summary>
    public void EndMeasurement(double roundsCompleted)
    {
        TimeEnd = DateTime.Now;
        RoundsCompleted = roundsCompleted;
        SensorimotorAdaptation.Measurements.AddMeasurement(this);
    }    
}
