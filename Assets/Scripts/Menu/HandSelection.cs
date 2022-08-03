using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Window which asks users to choose their dominant hand with which
/// they want to do the measurement.
/// </summary>
public class HandSelection : BaseForm
{
    public new GameObject camera;

    public TMP_Text height;

    private void Update()
    {
        // update the height info
        height.text = string.Format("{0:0.00}", camera.transform.position.y);
    }

    /// <summary>
    /// User chose left hand.
    /// </summary>
    public void ButtonLeftHandOnClick()
    {
        SensorimotorAdaptation.RightHanded = false;
        SensorimotorAdaptation.UserHeight = camera.transform.position.y;
        LoadNext();
    }

    /// <summary>
    /// User chose right hand.
    /// </summary>
    public void ButtonRightHandOnClick()
    {
        SensorimotorAdaptation.RightHanded = true;
        SensorimotorAdaptation.UserHeight = camera.transform.position.y;
        LoadNext();
    }

    /// <summary>
    /// User went back.
    /// </summary>
    public void ButttonBackOnClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Load next scene (with tasks).
    /// </summary>
    private void LoadNext()
    {
        var AO = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
