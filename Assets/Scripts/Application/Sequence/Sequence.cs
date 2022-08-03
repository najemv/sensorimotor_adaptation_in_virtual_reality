using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Data about one sequence loaded in application.
/// </summary>
[DataContract()]
public class Sequence
{
    /// <summary>
    /// Seqeunce's name.
    /// </summary>
    [DataMember(Order = 1)]
    public string Name { get; set; } = "Default";

    /// <summary>
    /// Duration of one episode in this sequence.
    /// </summary>
    [DataMember(Order = 2)]
    public int EpisodeDuration { get; set; } = 40;

    /// <summary>
    /// Duration of pause after one one episode in this sequence.
    /// </summary>
    [DataMember(Order = 3)]
    public int PauseDuration { get; set; } = 20;

    /// <summary>
    /// Should we measure in 3 dimensions? (enables depth deviation).
    /// </summary>
    [DataMember(Order = 4)]
    public bool MeasureIn3D { get; set; } = false;

    /// <summary>
    /// Name of object which is measured and shown to user.
    /// </summary>
    [DataMember(Order = 5)]
    public string ObjectName { get; set; } = "Star";

    /// <summary>
    /// List of parts of this sequence.
    /// </summary>
    [DataMember(Order = 6)]
    public List<SequencePart> Parts { get; set; } = new List<SequencePart>();

    /// <summary>
    /// Reference to measured object.
    /// </summary>
    public MeasuredObject MeasuredObject { get; set; }

    /// <summary>
    /// Show control for UI preview of this sequence.
    /// </summary>
    public SequenceControl Control
    {
        get => BaseForm.Create<SequenceControl>().Init(this);
    }

    /// <summary>
    /// Initialize sequence.
    /// </summary>
    public void Init()
    {
        MeasuredObject = Resources.FindObjectsOfTypeAll(typeof(MeasuredObject)).Where(obj => obj.name == ObjectName).First() as MeasuredObject;
        if (MeasuredObject == null)
        {
            // go back to menu since we don't find object to be measured
            SceneManager.LoadScene(0);
            return;
        }
        foreach (var sequence in Parts)
        {
            sequence.Init(this, MeasuredObject);
        }
    }

    /// <summary>
    /// Start this sequence.
    /// </summary>
    public void Start()
    {
        if (Parts.Count == 0)
        {
            // go to menu since we cant show any parts...
            SceneManager.LoadScene(0);
            return;
        }
        SensorimotorAdaptation.OnSequenceChanged(this);
        Parts[0].Show();
    }

    /// <summary>
    /// Switch application so it starts next part of this sequence which follow
    /// previous part.
    /// </summary>
    /// <param name="part">Previous sequence's part which was active.</param>
    public void NextPart(SequencePart part)
    {
        int i = Parts.IndexOf(part);
        i++;
        if (i < Parts.Count)
        {
            // we show next part
            Parts[i].Show();
        }
        else
        {
            // this part was the last one, we end this sequence.
            EndSequence();
        }
    }

    /// <summary>
    /// End this sequence. Saves measurement and go back to main menu.
    /// </summary>
    private async void EndSequence()
    {
        await SensorimotorAdaptation.Measurements.SaveSummary();
        SensorimotorAdaptation.Measurements = new SequenceMeasurement();
        SceneManager.LoadSceneAsync(0);
    }

}
