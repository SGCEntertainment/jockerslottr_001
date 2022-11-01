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
    [SerializeField] GameObject middleWinGo;
    [SerializeField] GameObject middleLoseGo;

    [Space(10)]
    [SerializeField] AudioSource middleWinSource;
    [SerializeField] AudioSource middleLoseSource;

    [Space(10)]
    [SerializeField] GameObject totalWinGo;
    [SerializeField] GameObject totalLoseGo;

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;

    [Space(10)]
    [SerializeField] Image firstInDeck;

    [Space(10)]
    [SerializeField] Player[] players;
    [SerializeField] List<Sprite> cardsInDeck;

    private void Start()
    {
        game.SetActive(false);
        menu.SetActive(true);
    }

    public void StartGame()
    {
        menu.SetActive(false);
        game.SetActive(true);

        totalWinGo.SetActive(false);
        totalLoseGo.SetActive(false);

        thirdPlayerStep = false;

        cardsInDeck = cardDeckData.cardSprites.ToList();
        SuffleCards();

        Sprite firstCard = cardsInDeck.First();
        cardsInDeck.Remove(firstCard);
        firstInDeck.sprite = firstCard;

        SetCards();

        StartCoroutine(nameof(GameProcess));
    }

    public void Back()
    {
        game.SetActive(false);
        menu.SetActive(true);
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

    void SetCards()
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
        }
    }

    IEnumerator PlayerProcess(Player player)
    {
        yield return new WaitForSeconds(Random.Range(0.25f, 0.5f));
        player.ThrowCard();
    }

    IEnumerator GameProcess()
    {
        while(true)
        {
            middleWinGo.SetActive(false);
            middleLoseGo.SetActive(false);

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
        yield return new WaitForSeconds(0.65f);

        GameObject first = players[0].cardPlace.GetChild(0).gameObject;
        GameObject second = players[1].cardPlace.GetChild(0).gameObject;
        GameObject third = players[2].cardPlace.GetChild(0).gameObject;

        bool playerWin = Random.Range(0, 100) > 70;
        GameObject targetGo = playerWin ? middleWinGo : middleLoseGo;
        AudioSource targetSource = playerWin ? middleWinSource : middleLoseSource;

        if(targetSource.isPlaying)
        {
            targetSource.Stop();
        }
        targetSource.Play();

        for (int i = 0; i < 6; i++)
        {
            targetGo.SetActive(i % 2 == 0);
            yield return new WaitForSeconds(0.15f);
        }

        Destroy(first);
        Destroy(second);
        Destroy(third);

        if (players[0].cardHand.childCount == 0)
        {
            StopCoroutine(nameof(GameProcess));
            bool totalWin = Random.Range(0, 100) > 70;
            GameObject targetPopup = totalWin ? totalWinGo : totalLoseGo;
            
            targetPopup.SetActive(true);
            yield return new WaitForSeconds(5);
            targetPopup.SetActive(false);

            game.SetActive(false);
            menu.SetActive(true);
        }
    }
}
