using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Form for showing detailed information about <see cref="global::Settings"/>.
/// </summary>
public class SettingsForm : BaseForm
{

    public GameObject sequenceList;

    public TMP_Dropdown defaultNameDropdown;

    /// <summary>
    /// Reference to settings object.
    /// </summary>
    private Settings Settings { get; set; }

    public override void Close()
    {
        Settings.DefaultSequenceName = Settings.Sequences[defaultNameDropdown.value].Name;
        base.Close();
        Settings.Save();
    }

    /// <summary>
    /// Initialize form with values.
    /// </summary>
    /// <param name="settings">Settings object.</param>
    /// <returns></returns>
    public SettingsForm Init(Settings settings)
    {
        Settings = settings;
        InitSequenceList();

        return this;
    }

    /// <summary>
    /// List all loaded sequences.
    /// </summary>
    private void InitSequenceList()
    {
        int offset = -100;
        int i = 0;
        var options = new List<TMP_Dropdown.OptionData>();
        foreach (var sequence in Settings.Sequences)
        {
            var sequenceControl = sequence.Control;
            sequenceControl.transform.SetParent(sequenceList.transform, false);
            sequenceControl.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offset);

            sequenceControl.Open();
            offset -= 200;
            i++;
            sequenceList.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + 200 * i);

        }

        defaultNameDropdown.AddOptions(Settings.Sequences.Select(seq => seq.Name).ToList());
        defaultNameDropdown.value = Settings.Sequences.FindIndex(seq => seq.Name == Settings.DefaultSequenceName);
    }
}
