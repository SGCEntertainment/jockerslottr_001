using UnityEngine;

[CreateAssetMenu]
public class CardDeckData : ScriptableObject
{
    public Sprite shirt;
    public Card cardPrefab;

    [Space(10)]
    public Sprite[] cardSprites;
}
