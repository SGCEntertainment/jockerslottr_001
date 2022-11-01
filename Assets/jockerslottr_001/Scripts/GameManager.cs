using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    const int maxCards = 8;

    public static bool thirdPlayerStep;

    IEnumerator player1Process;
    IEnumerator player2Process;


    [SerializeField] CardDeckData cardDeckData;
    [SerializeField] Transform lookAtPoint;

    [Space(10)]
    [SerializeField] Image firstInDeck;

    [Space(10)]
    [SerializeField] Player[] players;
    [SerializeField] List<Sprite> cardsInDeck;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        thirdPlayerStep = false;

        cardsInDeck = cardDeckData.cardSprites.ToList();
        SuffleCards();

        Sprite firstCard = cardsInDeck.First();
        cardsInDeck.Remove(firstCard);
        firstInDeck.sprite = firstCard;

        Deal�ards();

        StartCoroutine(nameof(GameProcess));
    }

    void SuffleCards()
    {
        for(int i = 0; i < cardsInDeck.Count; i++)
        {
            Sprite tmp = cardsInDeck[i];
            int rv = Random.Range(i, cardsInDeck.Count);
            cardsInDeck[i] = cardsInDeck[rv];
            cardsInDeck[rv] = tmp;
        }
    }

    void Deal�ards()
    {
        foreach (Player p in players)
        {
            for(int i = 0; i < maxCards; i++)
            {
                Card card = Instantiate(cardDeckData.cardPrefab, p.cardHand.position, Quaternion.identity, p.cardHand);

                Sprite _cardFace = cardsInDeck.Last();
                cardsInDeck.Remove(_cardFace);

                card.faceSprite = _cardFace;
                if (!p.isBot)
                {
                    card.Flip();
                }

                p.UpdateCards();
            }

            Vector3 diff = lookAtPoint.position - p.cardHand.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            p.cardHand.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }

    IEnumerator PlayerProcess(Player player)
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        player.ThrowCard();
    }

    IEnumerator GameProcess()
    {
        while(true)
        {
            player1Process = PlayerProcess(players[0]);
            player2Process = PlayerProcess(players[1]);

            yield return player1Process;
            yield return player2Process;
            thirdPlayerStep = true;

            while (thirdPlayerStep)
            {
                yield return null;
            }

            yield return StartCoroutine(nameof(CheckGameResult));
            player1Process = PlayerProcess(players[0]);
            player2Process = PlayerProcess(players[1]);
        }
    }

    IEnumerator CheckGameResult()
    {
        yield return new WaitForSeconds(1.25f);

        GameObject first = players[0].cardPlace.GetChild(0).gameObject;
        GameObject second = players[1].cardPlace.GetChild(0).gameObject;
        GameObject third = players[2].cardPlace.GetChild(0).gameObject;

        bool playerWin = Random.Range(0, 100) > 20;

        Destroy(first.gameObject);
        Destroy(second.gameObject);
        Destroy(third.gameObject);
    }
}
