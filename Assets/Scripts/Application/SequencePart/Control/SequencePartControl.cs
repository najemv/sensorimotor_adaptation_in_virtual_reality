using TMPro;

/// <summary>
/// Control for showing information about <see cref="global::SequencePart"/>.
/// </summary>
public class SequencePartControl : BaseForm
{
    /// <summary>
    /// Reference to sequence part object.
    /// </summary>
    private SequencePart SequencePart { get; set; }

    public TMP_Text sequencePartName;

    /// <summary>
    /// Initialize control with values.
    /// </summary>
    /// <param name="sequencePart">Sequence part object.</param>
    /// <returns></returns>
    public SequencePartControl Init(SequencePart sequencePart)
    {
        SequencePart = sequencePart;
        sequencePartName.text = sequencePart.Name;

        return this;
    }

    /// <summary>
    /// Open form with detailed information.
    /// </summary>
    public void OpenSequencePartForm()
    {
        Create<SequencePartForm>().Init(SequencePart).Open();
    }
}
