using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int maxCards = 8;


    IEnumerator player1Process;
    IEnumerator player2Process;
    IEnumerator player3Process;


    [SerializeField] CardDeckData cardDeckData;
    [SerializeField] Transform lookAtPoint;

    [Space(10)]
    [SerializeField] Player[] players;
    [SerializeField] List<Sprite> cardsInDeck;

    private void Start()
    {
        player1Process = GameProcess(players[0]);
        player2Process = GameProcess(players[1]);
        player3Process = GameProcess(players[2]);

        StartCoroutine(player1Process);
        StartCoroutine(player2Process);
        StartCoroutine(player3Process);

        StartCoroutine(nameof(Deal—ards));
    }

    IEnumerator Deal—ards()
    {
        float timeOffset = 0.01f;
        foreach (Player p in players)
        {
            for(int i = 0; i < maxCards; i++)
            {
                Instantiate(cardDeckData.cardPrefab, p.cardHand.position, Quaternion.identity,  p.cardHand);
                p.UpdateCards();

                yield return new WaitForSeconds(timeOffset);
            }

            Vector3 diff = lookAtPoint.position - p.cardHand.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            p.cardHand.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }

    IEnumerator GameProcess(Player player)
    {
        Debug.Log(player.name);
        yield return null;
    }
}
