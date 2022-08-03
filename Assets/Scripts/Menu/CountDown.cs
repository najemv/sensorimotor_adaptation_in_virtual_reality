using System;
using System.Globalization;
using TMPro;
using UnityEngine;

/// <summary>
/// Simple countdown that shows remaining time.
/// </summary>
public class CountDown : MonoBehaviour
{
    public TMP_Text _countdown;
    
    private double _currentTime = 0;

    /// <summary>
    /// Gets or sets current time and show it for user.
    /// </summary>
    private double CurrentTime
    {
        get => _currentTime;
        set
        {
            if (value < 0)
            {
                value = 0;
                return;
            }
            if (value != _currentTime)
            {
                _currentTime = value;
                _countdown.text = ((int)value).ToString();
            }
            
        }
    }

    private bool running = false;

    private int episodeDuration;

    private int restDuration;

    void Update()
    {
        if (running)
        {
            UpdateCountdown();
        }
    }

    private void UpdateCountdown()
    {
        CurrentTime -= Time.deltaTime;
    }

    private void Awake()
    {
        SensorimotorAdaptation.SequenceChanged += SequenceChangedHandler;
        SensorimotorAdaptation.MeasureStart += MeasureStartHandler;
        SensorimotorAdaptation.MeasureEnd += MeasureEndHandler;
        SensorimotorAdaptation.EpisodeChanged += TaskChangedHandler;
        SensorimotorAdaptation.SequencePartChanged += TaskChangedHandler;
    }



    private void OnDestroy()
    {
        SensorimotorAdaptation.SequenceChanged -= SequenceChangedHandler;
        SensorimotorAdaptation.MeasureStart -= MeasureStartHandler;
        SensorimotorAdaptation.MeasureEnd -= MeasureEndHandler;
        SensorimotorAdaptation.EpisodeChanged -= TaskChangedHandler;
        SensorimotorAdaptation.SequencePartChanged -= TaskChangedHandler;
    }

    private void MeasureEndHandler(object sender, EventArgs e)
    {
        CurrentTime = restDuration;
    }

    private void MeasureStartHandler(object sender, EventArgs e)
    {
        running = true;
    }

    private void SequenceChangedHandler(object sender, EventArgs e)
    {
        var sequence = sender as Sequence;
        episodeDuration = sequence.EpisodeDuration;
        restDuration = sequence.PauseDuration;
    }
    private void TaskChangedHandler(object sender, EventArgs e)
    {
        running = false;
        CurrentTime = episodeDuration;
    }

    /// <summary>
    /// Simple time formatted.
    /// </summary>
    /// <param name="num">Number that is converted.</param>
    /// <returns></returns>
    private string ToStr(double num)
    {
        return num.ToString("0.##", new CultureInfo("en-us", false));
    }
}
