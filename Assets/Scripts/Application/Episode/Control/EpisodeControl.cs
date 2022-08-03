using TMPro;

/// <summary>
/// Control for showing information about <see cref="Episode"/>.
/// </summary>
public class EpisodeControl : BaseForm
{
    /// <summary>
    /// Reference to episode object.
    /// </summary>
    private Episode Episode { get; set; }

    public TMP_Text episodeName;

    /// <summary>
    /// Initialize control with values.
    /// </summary>
    /// <param name="episode">Given episode.</param>
    /// <returns></returns>
    public EpisodeControl Init(Episode episode)
    {
        Episode = episode;
        episodeName.text = episode.Name;

        return this;
    }

    /// <summary>
    /// Open form with detailed information.
    /// </summary>
    public void OpenEpisodeForm()
    {
        Create<EpisodeForm>().Init(Episode).Open();
    }
}
