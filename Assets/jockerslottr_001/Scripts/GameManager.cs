using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int maxCards = 8;
    [SerializeField] CardDeckData cardDeckData;

    [Space(10)]
    [SerializeField] Player[] players;
    [SerializeField] List<Sprite> cardsInDeck;

    private void Start()
    {
        StartCoroutine(nameof(Deal—ards));
    }

    IEnumerator Deal—ards()
    {
        float timeOffset = 0.1f;
        foreach (Player p in players)
        {
            for(int i = 0; i < maxCards; i++)
            {
                //Instantiate(cardDeckData.cardPrefab, p.cardHand.position, Quaternion.identity,  p.cardHand);
                p.UpdateCards();

                yield return new WaitForSeconds(timeOffset);
            }
        }
    }
}
