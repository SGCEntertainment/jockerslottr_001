using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform cardPlace;
    public Transform cardHand;

    public void UpdateCards()
    {
        //for (int i = 0; i < cardHand.childCount; i++)
        //{
        //    cardHand.GetChild(i).RotateAround(cardHand.position, Vector3.forward, i);
        //}

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

    Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
