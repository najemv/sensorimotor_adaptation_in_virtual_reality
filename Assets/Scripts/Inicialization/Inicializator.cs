using UnityEngine;

/// <summary>
/// When application starts, this load its settings.
/// </summary>
public class Inicializator : MonoBehaviour
{
    private void Awake()
    {
        SensorimotorAdaptation.Settings.Load();
    }
}
