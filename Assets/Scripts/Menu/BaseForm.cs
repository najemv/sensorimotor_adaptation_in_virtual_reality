using System.Linq;
using UnityEngine;

/// <summary>
/// Base class for all windows.
/// </summary>
public abstract class BaseForm : MonoBehaviour
{
    /// <summary>
    /// Show's the form for user.
    /// </summary>
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Close the window for user.
    /// </summary>
    public virtual void Close()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Creates new window.
    /// </summary>
    /// <typeparam name="T">Type of window.</typeparam>
    /// <returns></returns>
    public static T Create<T>() where T : BaseForm
    {
        var window = Resources.FindObjectsOfTypeAll(typeof(T)).First();
        var go = Instantiate(window) as T;
        if (go.GetComponent<Canvas>() != null)
        {
            var globalMenuOnbject = GameObject.FindGameObjectWithTag("Menu");
            go.transform.transform.SetParent(globalMenuOnbject.transform, false);
        }
        return go;
    }
}
