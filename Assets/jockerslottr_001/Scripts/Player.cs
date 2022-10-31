using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform cardPlace;
    [SerializeField] Transform cardHand;

    [SerializeField] float radius;

    private void Start()
    {
        UpdateCards();
    }

    public void UpdateCards()
    {
        for(int i = 0; i < cardHand.childCount; i++)
        {
            cardHand.GetChild(i).RotateAround(cardHand.position, Vector3.forward, i * 6);
        }

        //float step = 1;
        //for(int i = 0; i < cardHand.childCount; i++, step -= 10)
        //{
        //    /* Distance around the circle */
        //    var radians = 2 * Mathf.PI / step;

        //    /* Get the vector direction */
        //    var vertical = Mathf.Sin(radians);
        //    var horizontal = Mathf.Cos(radians);

        //    var spawnDir = new Vector3(horizontal, vertical, 0);

        //    /* Get the spawn position */
        //    var spawnPos = cardHand.position + spawnDir * radius; // Radius is just the distance away from the point

        //    cardHand.GetChild(i).transform.position = spawnPos;

        //    /* Rotate the enemy to face towards player */
        //    //cardHand.GetChild(i).transform.LookAt(cardHand.position, Vector2.up);
        //}
    }
}
