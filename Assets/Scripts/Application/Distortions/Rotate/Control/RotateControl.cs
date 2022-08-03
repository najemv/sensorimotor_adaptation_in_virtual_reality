using TMPro;

/// <summary>
/// Control for showing information about <see cref="Rotate"/> distortion.
/// </summary>
public class RotateControl : BaseForm
{
    public TMP_InputField rotationX;

    public TMP_InputField rotationY;

    public TMP_InputField rotationZ;

    /// <summary>
    /// Initialize control with values.
    /// </summary>
    /// <param name="rotate">Rotate object.</param>
    /// <returns></returns>
    public RotateControl Init(Rotate rotate)
    {
        rotationX.text = rotate.RotationX.ToString();
        rotationY.text = rotate.RotationY.ToString();
        rotationZ.text = rotate.RotationZ.ToString();
        return this;
    }
}
