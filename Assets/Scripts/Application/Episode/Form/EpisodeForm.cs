using TMPro;
using UnityEngine;

/// <summary>
/// Form for showing detailed information about <see cref="global::Episode"/>.
/// </summary>
public class EpisodeForm : BaseForm
{
    public TMP_Text episodeName;

    public GameObject distortionList;

    /// <summary>
    /// Reference to episode object.
    /// </summary>
    private Episode Episode { get; set; }

    /// <summary>
    /// Initialize form with values.
    /// </summary>
    /// <param name="episode">Given episode.</param>
    /// <returns></returns>
    public EpisodeForm Init(Episode episode)
    {
        Episode = episode;

        episodeName.text = episode.Name;
        InitDistortionList();

        return this;
    }

    /// <summary>
    /// List all distortions for this episode.
    /// </summary>
    private void InitDistortionList()
    {
        int offset = -100;
        int i = 0;
        foreach (var distortion in Episode.Distortions)
        {
            var distortionControl = distortion.Control;
            distortionControl.transform.SetParent(distortionList.transform, false);
            distortionControl.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offset);

            distortionControl.Open();
            offset -= 200;
            i++;
            distortionList.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + 200 * i);
        }
    }
}
