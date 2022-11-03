using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isBot;
    public Transform cardPlace;
    public Transform cardHand;

    public void UpdateCards()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < cardHand.childCount; i++)
        {
            float angle = i switch
            {
                0 => -40,
                1 => -30,
                2 => -20,
                3 => -10,
                4 => 0,
                5 => 10,
                6 => 20,
                7 => 30,
            };

            Vector3 pos = RandomCircle(center, 1, angle);
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, center - pos);
            cardHand.GetChild(i).SetPositionAndRotation(pos, rot);
        }
    }

    public void ResetHand()
    {
        foreach(Transform t in cardHand)
        {
            Destroy(t.gameObject);
        }

        foreach (Transform t in cardPlace)
        {
            Destroy(t.gameObject);
        }

        cardHand.DetachChildren();
        cardPlace.DetachChildren();
    }

    Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public void ThrowCard()
    {
        Card card = cardHand.GetChild(Random.Range(0, cardHand.childCount)).GetComponent<Card>();
        card.Flip();

        card.transform.SetPositionAndRotation(cardPlace.position, cardPlace.rotation);
        card.transform.SetParent(cardPlace);
    }
}
