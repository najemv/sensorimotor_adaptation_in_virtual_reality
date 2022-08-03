using UnityEngine;

/// <summary>
/// When scene with tasks load, this initialize and start the sequence
/// </summary>
public class TaskStart : MonoBehaviour
{

    void Awake()
    {
        SensorimotorAdaptation.Init();
    }

    void Start()
    {
        SensorimotorAdaptation.Start();
    }
}
