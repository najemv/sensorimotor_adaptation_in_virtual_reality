using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

/// <summary>
/// Application's setting.
/// </summary>
[DataContract()]
public class Settings
{
    /// <summary>
    /// Sequence which will be started when clicking on "Start" button
    /// </summary>
    [DataMember(Order = 1)]
    public string DefaultSequenceName { get; set; } = string.Empty;

    /// <summary>
    /// List of all loaded sequences.
    /// </summary>
    [DataMember(Order = 2)]
    public List<Sequence> Sequences { get; set; } = new List<Sequence>();

    /// <summary>
    /// Gets path of the file where settings is stored.
    /// </summary>
    public static string SettingsPath
    {
        get => Path.Combine(Application.persistentDataPath, "settings.xml");
    }

    /// <summary>
    /// Load settings from file.
    /// </summary>
    public void Load()
    {
        try
        {
            using (FileStream fs = new FileStream(SettingsPath, FileMode.Open))
            {
                using(XmlDictionaryReader reader =
                    XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(Settings));

                    // Deserialize the data and read it from the instance.
                    var settings = (Settings)ser.ReadObject(reader, true);
                    this.DefaultSequenceName = settings.DefaultSequenceName;
                    this.Sequences = settings.Sequences;
                }
            }
        }
        catch
        {
            CreateDefault();
        }
    }

    /// <summary>
    /// Creates and saves default template so the file is created and can be edited.
    /// </summary>
    public void CreateDefault()
    {
        Sequences.Add(new Sequence
        {
            Name = "Profile 1",
            Parts = new List<SequencePart> { new SequencePart
            {
                Episodes = new List<Episode> { new Episode
                {
                    Distortions = new List<Distortion>
                    {
                        new Mirror(),
                        new Rotate()
                    }
                } }
            } }
        });
        Save();
    }

    /// <summary>
    /// Save settings to the file.
    /// </summary>
    public void Save()
    {
        // to make the file pretty...
        var settings = new XmlWriterSettings()
        {
            Indent = true,
            IndentChars = "\t"
        };

        using (FileStream stream = new FileStream(SettingsPath, FileMode.Create))
        {
            using (var writer = XmlWriter.Create(stream, settings))
            {
                DataContractSerializer ser =
                    new DataContractSerializer(typeof(Settings));
                ser.WriteObject(writer, this);
            }
        }
    }
}
