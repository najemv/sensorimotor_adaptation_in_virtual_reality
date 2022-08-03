using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu that is visible after the application starts.
/// </summary>
public class MainMenu : BaseForm
{
    /// <summary>
    /// User clicked on "Start" button.
    /// </summary>
    public void ButtonStartOnClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// User clicked on "Settings" button.
    /// </summary>
    public void ButtonSettingsOnClick()
    {
        BaseForm.Create<SettingsForm>().Init(SensorimotorAdaptation.Settings).Open();
    }

    /// <summary>
    /// User clicked on "Exit" button.
    /// </summary>
    public void ButtonExitOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
