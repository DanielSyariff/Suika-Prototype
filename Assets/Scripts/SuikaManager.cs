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
    public PoolerManager poolerManager;
    public UIManager uiManager;
    public List<FruitData> fruitData;

    [Header("Witch Respon and Expression")]
    public SpriteRenderer expression;
    public List<Sprite> expressionList;
    private Coroutine changeExpression;

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

    public GameObject GetObjectPool(SuikaType type)
    {
        GameObject tmpObj = poolerManager.CallSuika(type - 1);
        return tmpObj;
    }

    public void SpawnTriggeredFruit(int nextFruit, Vector2 pos) 
    {
        triggerCount++;
        if (triggerCount == 2)
        {
            triggerCount = 0;
            score += fruitData[nextFruit - 1].incrementScore;
            //GameObject tmpObject = Instantiate(fruitData[nextFruit].fruitObject, pos, Quaternion.identity);

            GameObject tmpObject = GetObjectPool(fruitData[nextFruit].suikaType);
            tmpObject.transform.localPosition = pos;
            tmpObject.transform.rotation = Quaternion.identity;

            tmpObject.transform.SetParent(bucket);

            uiManager.UpdateUIScore();

            //Set Witch Expression
            if (changeExpression == null)
            {
                changeExpression = StartCoroutine(ChangingExpression());
            }
        }
    }

    IEnumerator ChangingExpression()
    {
        ChangeExpressionHappy();
        yield return new WaitForSeconds(1f);
        changeExpression = null;
        ChangeExpressionNormal();
    }

    public void ChangeExpressionNormal()
    {
        expression.sprite = expressionList[0];
    }

    public void ChangeExpressionHappy()
    {
        expression.sprite = expressionList[1];
    }

    public void ChangeExpressionPanic()
    {
        expression.sprite = expressionList[2];
    }
    public void Alert(bool alert)
    {
        if (alert)
        {
            uiManager.RedLineTriggered();
            ChangeExpressionPanic();
        }
        else
        {
            uiManager.RedLineUntriggered();
            ChangeExpressionNormal();
        }
    }
}
