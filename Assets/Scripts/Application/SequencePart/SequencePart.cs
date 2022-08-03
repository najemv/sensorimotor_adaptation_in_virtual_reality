using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Data about individual parts of sequence.
/// </summary>
[DataContract()]
public class SequencePart
{
    /// <summary>
    /// Sequence's part name.
    /// </summary>
    [DataMember(Order = 1)]
    public string Name { get; set; } = "";

    /// <summary>
    /// Description for this part.
    /// </summary>
    [DataMember(Order = 2)]
    public string Description { get; set; } = "";

    /// <summary>
    /// Is measurement enabled for episodes in this part?
    /// </summary>
    [DataMember(Order = 3)]
    public bool EnableMeasurement { get; set; } = false;

    /// <summary>
    /// List of episodes in this part.
    /// </summary>
    [DataMember(Order = 4)]
    public List<Episode> Episodes { get; set; } = new List<Episode>();

    /// <summary>
    /// Number of episodes in this part.
    /// </summary>
    public int EpisodeCount { get => Episodes.Count; }

    /// <summary>
    /// Should be this part measured in 3 dimensions?
    /// </summary>
    public bool MeausureIn3D { get => Sequence.MeasureIn3D; }

    /// <summary>
    /// Show control for UI preview of this sequence's part.
    /// </summary>
    public SequencePartControl Control
    {
        get => BaseForm.Create<SequencePartControl>().Init(this);
    }

    /// <summary>
    /// Reference to sequence this part belongs to.
    /// </summary>
    private Sequence Sequence { get; set; }

    
    /// <summary>
    /// Initialize this part.
    /// </summary>
    /// <param name="sequence">Sequence this part belongs to.</param>
    /// <param name="obj">Reference to measured object.</param>
    public void Init(Sequence sequence, MeasuredObject obj)
    {
        Sequence = sequence;
        int i = 1;
        foreach (var task in Episodes)
        {
            task.Init(sequence, this, i++, obj);
        }
    }

    /// <summary>
    /// Show this part.
    /// </summary>
    public void Show()
    {
        SensorimotorAdaptation.OnSequencePartChanged(this);
    }

    /// <summary>
    /// Start episodes for this sequence's part.
    /// </summary>
    public void Start()
    {
        if (Episodes.Count == 0)
        {
            // go to menu since we can't show any episode
            SceneManager.LoadScene(0);
            return;
        }
        Episodes[0].Prepare();
    }

    /// <summary>
    /// Switch application so it starts episode which follows previous episode.
    /// </summary>
    /// <param name="previousEpisode">Episodes which runned last.</param>
    public void NextEpisode(Episode previousEpisode)
    {
        int i = Episodes.IndexOf(previousEpisode);
        i++;
        if (i < Episodes.Count)
        {
            // we prepare next episode
            Episodes[i].Prepare();
        }
        else
        {
            // if it was last episode in this part, we switch to next part.
            Sequence.NextPart(this);
        }
    }
}
