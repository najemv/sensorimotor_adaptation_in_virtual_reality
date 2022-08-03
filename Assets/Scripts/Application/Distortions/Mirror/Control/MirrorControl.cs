using TMPro;

/// <summary>
/// Control for showing information about <see cref="Mirror"/> distortion.
/// </summary>
public class MirrorControl : BaseForm
{
    public TMP_InputField normalX;

    public TMP_InputField normalY;

    public TMP_InputField normalZ;

    /// <summary>
    /// Initialize control with values.
    /// </summary>
    /// <param name="mirror">Mirror object.</param>
    /// <returns></returns>
    public MirrorControl Init(Mirror mirror)
    {
        normalX.text = mirror.NormalX.ToString();
        normalY.text = mirror.NormalY.ToString();
        normalZ.text = mirror.NormalZ.ToString();
        return this;
    }
}
