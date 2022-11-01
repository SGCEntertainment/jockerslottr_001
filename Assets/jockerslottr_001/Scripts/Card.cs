using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite Icon
    {
        get => GetComponent<Image>().sprite;
        set => GetComponent<Image>().sprite = value;
    }

    private void OnMouseDown()
    {
        Player refPlayer = GetComponentInParent<Player>();
        if(refPlayer.isBot)
        {
            return;
        }

        transform.SetPositionAndRotation(refPlayer.cardPlace.position, refPlayer.cardPlace.rotation);
    }
}
