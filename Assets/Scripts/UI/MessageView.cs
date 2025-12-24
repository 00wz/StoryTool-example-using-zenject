using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    [SerializeField]
    private Text Head;

    [SerializeField]
    private Text Body;

    [SerializeField]
    private Button CloseButton;

    private Action onCloseActions;

    private void Awake()
    {
        CloseButton.onClick.AddListener(CloseMessage);
    }
    public void ShowMessage(string header, string body, Action onClose)
    {
        if (onCloseActions != null)
        {
            throw new InvalidOperationException("A message is already being displayed.");
        }

        Head.text = header;
        Body.text = body;
        onCloseActions = onClose;
        gameObject.SetActive(true);
    }

    private void CloseMessage()
    {
        gameObject.SetActive(false);
        onCloseActions?.Invoke();
        onCloseActions = null;
    }
}
