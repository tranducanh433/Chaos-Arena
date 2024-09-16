using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomCharacter : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image character;

    Image image;

    public void OnPointerDown(PointerEventData eventData)
    {
        image = GetComponent<Image>();
        character.color = image.color;
    }
}
