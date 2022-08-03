using System;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Base static class for whole application.
/// </summary>
public static class SensorimotorAdaptation
{
    public static event EventHandler SequenceChanged = null;

    public static event EventHandler SequencePartChanged = null;

    public static event EventHandler EpisodeChanged = null;

    public static event EventHandler MeasureStart = null;

    public static event EventHandler MeasureEnd = null;

    /// <summary>
    /// Application settings (loaded sequences).
    /// </summary>
    public static Settings Settings { get; set; } = new Settings();

    /// <summary>
    /// Measurements for measured seqeunce.
    /// </summary>
    public static SequenceMeasurement Measurements { get; set; } = new SequenceMeasurement();

    /// <summary>
    /// Should me apply distortions to left or right hand?
    /// </summary>
    public static bool RightHanded { get; set; } = true;

    /// <summary>
    /// User's height.
    /// </summary>
    public static float UserHeight { get; set; } = 1.7f;

    /// <summary>
    /// Users's id which he/she set in the application.
    /// </summary>
    public static string UserId { get; set; } = "0";

    public static void OnSequenceChanged(object sender)
    {
        SequenceChanged?.Invoke(sender, EventArgs.Empty);
    }

    public static void OnSequencePartChanged(object sender)
    {
        SequencePartChanged?.Invoke(sender, EventArgs.Empty);
    }

    public static void OnEpisodeChanged(object sender)
    {
        EpisodeChanged?.Invoke(sender, EventArgs.Empty);
    }

    public static void OnMeasureStart(object sender)
    {
        MeasureStart?.Invoke(sender, EventArgs.Empty);
    }

    public static void OnMeasureEnd(object sender)
    {
        MeasureEnd?.Invoke(sender, EventArgs.Empty);
    }

    /// <summary>
    /// Start sequence defined by <see cref="Settings.DefaultSequenceName"/>
    /// </summary>
    public static void Start()
    {
        var profile = Settings.Sequences.Where(profile => profile.Name == Settings.DefaultSequenceName).First();
        if (profile == null)
        {
            // go to main menu
            SceneManager.LoadScene(0);
            return;
        }
        
        profile.Start();
    }

    /// <summary>
    /// Initialize all sequences.
    /// </summary>
    public static void Init()
    {
        foreach (var sequence in Settings.Sequences)
        {
            sequence.Init();
        }
    }
}
