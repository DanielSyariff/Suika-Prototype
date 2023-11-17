using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuikaType
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Eleven = 11,
}

public class SuikaManager : MonoBehaviour
{
    public static SuikaManager instance;
    public UIManager uiManager;
    public List<FruitData> fruitData;

    public Transform bucket;

    [Header("Scoring and Data")]
    public int score;

    int triggerCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnTriggeredFruit(int nextFruit, Vector2 pos) 
    {
        triggerCount++;
        if (triggerCount == 2)
        {
            triggerCount = 0;
            score += fruitData[nextFruit - 1].incrementScore;
            GameObject tmpObject = Instantiate(fruitData[nextFruit].fruitObject, pos, Quaternion.identity);
            tmpObject.transform.SetParent(bucket);

            uiManager.UpdateUIScore();
        }
    }
}
