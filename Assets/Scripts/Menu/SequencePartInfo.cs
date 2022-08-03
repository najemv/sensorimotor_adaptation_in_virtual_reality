using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Window with information about current sequence part.
/// </summary>
public class SequencePartInfo : MonoBehaviour
{
    public GameObject leftHand;

    public GameObject rightHand;

    public CountDown countDown;

    public TaskCount taskCount;

    public TMP_Text sequenceName;

    public TMP_Text sequenceDescription;

    /// <summary>
    /// Reference to current sequence part.
    /// </summary>
    private SequencePart SequencePart { get; set; }


    void Awake()
    {
        SensorimotorAdaptation.SequencePartChanged += SequencePartChangedHandler;
    }
    void OnDestroy()
    {
        SensorimotorAdaptation.SequencePartChanged -= SequencePartChangedHandler;
    }

    private void SequencePartChangedHandler(object sender, EventArgs e)
    {
        SequencePart = sender as SequencePart;
        Show();
    }



    /// <summary>
    /// Show form and prepare other things.
    /// </summary>
    public void Show()
    {
        SetInteractors(true);
        SetPanels(false);
        
        // set part's name and description
        this.sequenceName.text = SequencePart.Name;
        this.sequenceDescription.text = SequencePart.Description;
        
        // show form
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the form
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
        SetInteractors(false);
        SetPanels(true);
        SequencePart.Start();
    }

    /// <summary>
    /// Enables or disables line interactors so the user can click button.
    /// </summary>
    /// <param name="active"></param>
    private void SetInteractors(bool active)
    {
        leftHand.GetComponent<XRRayInteractor>().enabled = active;
        rightHand.GetComponent<XRRayInteractor>().enabled = active;
    }

    /// <summary>
    /// Enables or disables other panels.r
    /// </summary>
    /// <param name="active"></param>
    private void SetPanels(bool active)
    {
        countDown.gameObject.SetActive(active);
        taskCount.gameObject.SetActive(active);
    }
}
