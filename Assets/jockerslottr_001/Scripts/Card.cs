using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite Icon
    {
        get => GetComponent<Image>().sprite;
        set => GetComponent<Image>().sprite = value;
    }
}
