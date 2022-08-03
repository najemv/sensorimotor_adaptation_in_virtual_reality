using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Window with id input.
/// </summary>
public class IdInput : MonoBehaviour
{
    public TMP_InputField input;

    public TMP_Text info;
    

    void Start()
    {
        info.text = "";
    }

    /// <summary>
    /// Adds number to id.
    /// </summary>
    /// <param name="number"></param>
    public void AddNumber(int number)
    {
        input.text += number;
    }

    /// <summary>
    /// Removes last entered nubmer.
    /// </summary>
    public void RemoveLast()
    {
        if (input.text.Length > 0)
        {
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }


    public void ButtonPress0() => AddNumber(0);
    public void ButtonPress1() => AddNumber(1);
    public void ButtonPress2() => AddNumber(2);
    public void ButtonPress3() => AddNumber(3);
    public void ButtonPress4() => AddNumber(4);
    public void ButtonPress5() => AddNumber(5);
    public void ButtonPress6() => AddNumber(6);
    public void ButtonPress7() => AddNumber(7);
    public void ButtonPress8() => AddNumber(8);
    public void ButtonPress9() => AddNumber(9);
    public void ButtonPressDelete() => RemoveLast();

    /// <summary>
    /// User went back.
    /// </summary>
    public void ButtonPressBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// User confirmed id.
    /// </summary>
    public void ButtonPressOK()
    {
        SensorimotorAdaptation.UserId = input.text;

        // id is empty
        if (SensorimotorAdaptation.UserId == "")
        {
            ShowInfo("Pole nesmí být prázdné.");
            return;
        }
        // id already exists
        if (!SensorimotorAdaptation.Measurements.CanBeSaved())
        {
            ShowInfo("ID již bylo použito. Zvolte, prosím, jiné.");
            return;
        }
        
        // load next scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Show description to user what is wrong.
    /// </summary>
    /// <param name="text"></param>
    private async void ShowInfo(string text)
    {
        info.text = text;
        await System.Threading.Tasks.Task.Delay(3000);
        if (info.text == text)
        {
            info.text = "";
        }
    }
}
