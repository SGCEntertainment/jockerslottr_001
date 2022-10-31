using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform cardPlace;
    [SerializeField] Transform cardHand;

    public void UpdateCards()
    {
        for (int i = 0; i < cardHand.childCount; i++)
        {
            cardHand.GetChild(i).RotateAround(cardHand.position, Vector3.forward, i * 12);
        }
    }
}
