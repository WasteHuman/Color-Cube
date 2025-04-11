using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        [ContextMenu("Open")] public void Open()
        {
            gameObject.SetActive(true);
            OnOpened();
        }

        [ContextMenu("Close")] public void Close()
        {
            gameObject.SetActive(false);
            OnClosed();
        }

        protected abstract void OnOpened();

        protected abstract void OnClosed();
    }
}