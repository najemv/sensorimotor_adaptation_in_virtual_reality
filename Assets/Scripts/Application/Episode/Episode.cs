using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Data about individual episode during measurement.
/// </summary>
[DataContract()]
public class Episode
{
    /// <summary>
    /// Episode's name.
    /// </summary>
    [DataMember(Order = 1)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Distortions for this episode.
    /// </summary>
    [DataMember(Order = 2)]
    public List<Distortion> Distortions { get; set; } = new List<Distortion>();

    /// <summary>
    /// Reference to object which is measured.
    /// </summary>
    public MeasuredObject MeasuredObject { get; set; }

    /// <summary>
    /// Position of this episode among other episodes.
    /// </summary>
    public int EpisodePosition { get; set; }

    /// <summary>
    /// Should be measurement enabled for this episode?
    /// </summary>
    public bool EnableMeasurement { get => SequencePart.EnableMeasurement; }

    /// <summary>
    /// Should we measure in 3 dimensions?
    /// </summary>
    public bool MeasureIn3D { get => SequencePart.MeausureIn3D; }

    /// <summary>
    /// Measurement for this episode.
    /// </summary>
    private EpisodeMeasurement Measurement { get; set; }

    /// <summary>
    /// Reference to sequence part this episode belongs to.
    /// </summary>
    private SequencePart SequencePart { get; set; }

    /// <summary>
    /// Show control for UI preview of this episode.
    /// </summary>
    public EpisodeControl Control
    {
        get => BaseForm.Create<EpisodeControl>().Init(this);
    }

    /// <summary>
    /// Reference to sequence this episode belongs to.
    /// </summary>
    private Sequence Sequence { get; set; }

    /// <summary>
    /// Initialize this episode.
    /// </summary>
    /// <param name="sequence">Sequence this episode belongs to.</param>
    /// <param name="sequencePart">Sequence part this episode belongs to.</param>
    /// <param name="i">Position of this episode among other episodes.</param>
    /// <param name="obj">Reference to measured object.</param>
    public void Init(Sequence sequence, SequencePart sequencePart, int i, MeasuredObject obj)
    {
        Sequence = sequence;
        SequencePart = sequencePart;
        EpisodePosition = i;
        MeasuredObject = obj;
    }

    /// <summary>
    /// Show measured path.
    /// </summary>
    public void Prepare()
    {
        MeasuredObject.Show(this);
        SensorimotorAdaptation.OnEpisodeChanged(this);
    }

    /// <summary>
    /// Start the measurement for this episode.
    /// </summary>
    public async void Start()
    {
        StartMeasurement();
        // we wait for certain time, before we can end the measurement
        await Task.Delay(Sequence.EpisodeDuration * 1000);
        EndMeasurement();
        MeasuredObject.Hide();

        // we wait another x second, before the pause ends.
        if (EpisodePosition < SequencePart.EpisodeCount)
        {
            await Task.Delay(Sequence.PauseDuration * 1000);
        }

        // switch to next episode
        SequencePart.NextEpisode(this);
    }

    /// <summary>
    /// Add new measure sample for this episode.
    /// </summary>
    /// <param name="pointer"></param>
    /// <param name="closestPoint"></param>
    public void AddMeasure(Vector3 pointer, Vector3 closestPoint)
    {
        if (EnableMeasurement)
        {
            Measurement.AddMeasure(pointer, closestPoint);
        }
    }

    /// <summary>
    /// Start the measurement for this episode.
    /// </summary>
    private void StartMeasurement()
    {
        if (EnableMeasurement)
        {
            Measurement = new EpisodeMeasurement(this);
        }
        SensorimotorAdaptation.OnMeasureStart(this);
    }

    /// <summary>
    /// End the measurement for this episode.
    /// </summary>
    private void EndMeasurement()
    {
        if (EnableMeasurement)
        {
            Measurement.EndMeasurement(MeasuredObject.RoundCompleted);
        }
        SensorimotorAdaptation.OnMeasureEnd(this);
    }
}
