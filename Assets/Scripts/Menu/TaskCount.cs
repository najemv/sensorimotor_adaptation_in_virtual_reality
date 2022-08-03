using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Simple window that shows order of current task among all tasks.
/// </summary>
public class TaskCount : MonoBehaviour
{
    public TMP_Text _current;

    public TMP_Text _total;
    
    public int Current
    {
        get => int.Parse(_current.text);
        set => _current.text = value.ToString();
    }

    public int Total
    {
        get => int.Parse(_total.text);
        set => _total.text = value.ToString();
    }

    private void Awake()
    {
        SensorimotorAdaptation.EpisodeChanged += TaskChangedHandler;
        SensorimotorAdaptation.SequencePartChanged += SequencePartChangedHandler;
    }

    private void OnDestroy()
    {
        SensorimotorAdaptation.EpisodeChanged -= TaskChangedHandler;
        SensorimotorAdaptation.SequencePartChanged -= SequencePartChangedHandler;
    }

    private void SequencePartChangedHandler(object sender, EventArgs e)
    {
        var sequence = sender as SequencePart;
        Current = 0;
        Total = sequence.EpisodeCount;
    }

    private void TaskChangedHandler(object sender, EventArgs e)
    {
        var task = sender as Episode;
        Current = task.EpisodePosition;
    }
}
