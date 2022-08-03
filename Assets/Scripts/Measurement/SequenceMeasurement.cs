using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Class that contains measurement information about whole sequence.
/// </summary>
public class SequenceMeasurement
{
    /// <summary>
    /// Measurements for individual episodes in this sequence.
    /// </summary>
    private List<EpisodeMeasurement> EpisodeMeasurements { get; set; } = new List<EpisodeMeasurement>();


    /// <summary>
    /// Folder where measurements are saved.
    /// </summary>
    private string MeasurementPath
    {
        get => Path.Combine(Application.persistentDataPath, SensorimotorAdaptation.UserId);
    }

    /// <summary>
    /// Adds completed episode measurement.
    /// </summary>
    /// <param name="episodeMeasurement">Completed episode measurement.</param>
    public void AddMeasurement(EpisodeMeasurement episodeMeasurement)
    {
        EpisodeMeasurements.Add(episodeMeasurement);
        SaveMeasurement(episodeMeasurement);
    }

    /// <summary>
    /// Saves measurement to file.
    /// </summary>
    /// <param name="measurement">Measurement which is going to be saved.</param>
    private void SaveMeasurement(EpisodeMeasurement measurement)
    {
        var path = MeasurementPath;
        // we want to prefent application freeze so we run this in the new thread.
        Task.Run(() =>
        {
            string fileName = $"{measurement.Prefix}.csv";
            string filePath = Path.Combine(path, fileName);
            File.WriteAllText(filePath, measurement.CSV);
        });
    }

    /// <summary>
    /// Saves summary info all measurements in this sequence.
    /// </summary>
    /// <returns></returns>
    public async Task SaveSummary()
    {
        var path = MeasurementPath;
        
        // again, run it in new task to prevent application freezing
        await Task.Run(() =>
        {
            string rmseInfo = "";
            foreach (var measurement in EpisodeMeasurements)
            {
                rmseInfo +=
@$"{measurement.Prefix}:
    - RMSE: {string.Format("{0:0.00000}", measurement.RMSE)}
    - Completed rounds: {string.Format("{0:0.00000}", measurement.RoundsCompleted)}
";
            }
            
            string statsPath = Path.Combine(path, "summary.txt");
            File.WriteAllText(statsPath, rmseInfo);
        });
        
    }

    /// <summary>
    /// Check if current user id is already used. If not, create directory with given id.
    /// </summary>
    /// <returns>True if given Id is not used, false otherwise.</returns>
    public bool CanBeSaved()
    {
        var exists = Directory.Exists(MeasurementPath);
        if (exists)
        {
            return false;
        }
        Directory.CreateDirectory(MeasurementPath);
        return true;
    }
}
