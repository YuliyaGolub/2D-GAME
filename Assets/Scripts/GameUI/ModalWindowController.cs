using UnityEngine;
using System.Linq;

public class ModalWindowController : MonoBehaviour
{
    private ModalWindow[] modalWindows;
    public virtual void Awake()
    {
        modalWindows = GetComponentsInChildren<ModalWindow>();
    }

    public void ShowModalWindow(ModalWindow currentWindow)
    {
        foreach (ModalWindow window in modalWindows)
        {
            window.Deactivate();
        }
        currentWindow.Activate();
    }

    public void ShowModalWindow(string windowName)
    {
        foreach (ModalWindow window in modalWindows)
        {
            window.Deactivate();
        }
        modalWindows.FirstOrDefault(window => window.gameObject.name == windowName)?.Activate();
    }
}
