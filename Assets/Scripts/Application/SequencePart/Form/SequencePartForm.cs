using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Form for showing detailed information about <see cref="global::SequencePart"/>.
/// </summary>
public class SequencePartForm : BaseForm
{
    public TMP_Text sequencePartName;

    public Toggle enableMeasurement;

    public TMP_Text description;

    public GameObject episodeList;

    /// <summary>
    /// Reference to sequence part object.
    /// </summary>
    private SequencePart SequencePart { get; set; }

    /// <summary>
    /// Initialize form with values.
    /// </summary>
    /// <param name="sequencePart">Sequence part object.</param>
    /// <returns></returns>
    public SequencePartForm Init(SequencePart sequencePart)
    {
        SequencePart = sequencePart;

        sequencePartName.text = sequencePart.Name;
        enableMeasurement.isOn = sequencePart.EnableMeasurement;
        description.text = sequencePart.Description;
        InitEpisodeList();

        return this;
    }

    /// <summary>
    /// List all episodes for this sequence part.
    /// </summary>
    private void InitEpisodeList()
    {
        int offset = -100;
        int i = 0;
        foreach (var episode in SequencePart.Episodes)
        {
            var episodeControl = episode.Control;
            episodeControl.transform.SetParent(episodeList.transform, false);
            episodeControl.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offset);

            episodeControl.Open();
            offset -= 200;
            i++;
            episodeList.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + 200 * i);
        }
    }
}
