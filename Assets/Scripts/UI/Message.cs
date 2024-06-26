using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _background;
    [SerializeField] private Image _itemImage;
    [SerializeField] private float _visibleDuration = 5f;
    [SerializeField] private float _fadeDuration = 1f;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void SetUpMessage(string message, Sprite itemSprite)
    {
        _text.text = message;
        if (itemSprite != null) _itemImage.sprite = itemSprite;
        else _itemImage.gameObject.SetActive(false);
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        float time = 0;
        while (time < _visibleDuration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1, 0, time / _fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
