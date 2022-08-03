using TMPro;

/// <summary>
/// Control for showing information about <see cref="Sequence"/>.
/// </summary>
public class SequenceControl : BaseForm
{
    /// <summary>
    /// Reference to sequence object.
    /// </summary>
    private Sequence Sequence { get; set; }

    public TMP_Text sequenceName;

    /// <summary>
    /// Initialize control with values.
    /// </summary>
    /// <param name="sequence">Sequence object.</param>
    /// <returns></returns>
    public SequenceControl Init(Sequence sequence)
    {
        Sequence = sequence;
        sequenceName.text = sequence.Name;

        return this;
    }

    /// <summary>
    /// Open form with detailed information.
    /// </summary>
    public void OpenSequnceForm()
    {
        Create<SequenceForm>().Init(Sequence).Open();
    }
}
