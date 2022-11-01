using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Sprite GetIcon()
    {
        return GetComponent<Image>().sprite;
    }

    private void SetIcon(Sprite value)
    {
        GetComponent<Image>().sprite = value;
    }

    public Sprite faceSprite;

    public void Flip()
    {
        SetIcon(faceSprite);
    }

    private void OnMouseDown()
    {
        Player refPlayer = GetComponentInParent<Player>();
        if(refPlayer.isBot || !GameManager.thirdPlayerStep)
        {
            return;
        }

        transform.SetPositionAndRotation(refPlayer.cardPlace.position, refPlayer.cardPlace.rotation);
        transform.transform.SetParent(refPlayer.cardPlace);

        GameManager.thirdPlayerStep = false;
    }
}
