using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Form for showing detailed information about <see cref="global::Sequence"/>.
/// </summary>
public class SequenceForm : BaseForm
{
    public TMP_Text sequenceName;

    public Toggle measureIn3D;

    public TMP_Text episodeDuration;

    public TMP_Text pauseDuration;

    public TMP_Text objectName;

    public GameObject sequencePartList;

    /// <summary>
    /// Reference to sequence object.
    /// </summary>
    private Sequence Sequence { get; set; }

    /// <summary>
    /// Initialize form with values.
    /// </summary>
    /// <param name="sequence">Sequence object.</param>
    /// <returns></returns>
    public SequenceForm Init(Sequence sequence)
    {
        Sequence = sequence;

        sequenceName.text = sequence.Name;
        measureIn3D.isOn = sequence.MeasureIn3D;
        episodeDuration.text = sequence.EpisodeDuration.ToString() + " s";
        pauseDuration.text = sequence.PauseDuration.ToString() + " s";
        objectName.text = sequence.ObjectName;
        InitSequencePartList();

        return this;
    }

    /// <summary>
    /// List all parts for this sequence.
    /// </summary>
    private void InitSequencePartList()
    {
        int offset = -100;
        int i = 0;
        foreach (var part in Sequence.Parts)
        {
            var sequencePartControl = part.Control;
            sequencePartControl.transform.SetParent(sequencePartList.transform, false);
            sequencePartControl.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offset);

            sequencePartControl.Open();
            offset -= 200;
            i++;
            sequencePartList.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + 200 * i);
        }
    }
}
