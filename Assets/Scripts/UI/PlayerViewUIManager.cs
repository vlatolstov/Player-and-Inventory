using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewUIManager : MonoBehaviour
{
    [SerializeField] Message _messagePrefab;
    [SerializeField] Canvas _playerViewCanvas;

    private Inventory _playerInventory;
    private VerticalLayoutGroup _messages;

    void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _playerInventory.OnPickupItem += ShowPickUpMessage;
        _messages = _playerViewCanvas.transform.Find("Messages").GetComponent<VerticalLayoutGroup>();
    }

    private void ShowPickUpMessage(string message, Sprite itemSprite)
    {
        Message m = Instantiate(_messagePrefab, _messages.transform);
        m.transform.SetAsFirstSibling();
        m.SetUpMessage(message, itemSprite);
    }
}
