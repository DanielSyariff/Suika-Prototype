using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool gameOver;
    public int score;

    int triggerCount;

    [Header("Game Over Parameter")]
    public bool _Alert;
    public float timeToGameOver;
    float timer;

    [Header("Audio")]
    public AudioManager audioManager;
    public GameObject xBGM;
    public GameObject xSFX;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #region
    public void ReplayCheckerAudioIndicator()
    {
        if (audioManager.BGM.mute)
        {
            xBGM.SetActive(true);
        }
        else
        {
            xBGM.SetActive(false);
        }

        if (audioManager.SFX.mute)
        {
            xSFX.SetActive(true);
        }
        else
        {
            xSFX.SetActive(false);
        }
    }
    public void AudioBGMControl()
    {
        audioManager.SetBgmVolume(xBGM);
    }

    public void AudioSFXControl()
    {
        audioManager.SetSfxVolume(xSFX);
    }
    #endregion

    private void Start()
    {
        gameOver = false;
        audioManager = FindObjectOfType<AudioManager>();
        ReplayCheckerAudioIndicator();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public GameObject GetObjectPool(SuikaType type)
    {
        GameObject tmpObj = poolerManager.CallSuika(type - 1);
        return tmpObj;
    }

    public void SpawnTriggeredFruit(int nextFruit, Vector2 pos) 
    {
        Debug.Log("Next Fruit : " + nextFruit);
        triggerCount++;
        if (triggerCount == 2)
        {
            triggerCount = 0;
            score += fruitData[nextFruit - 1].incrementScore;
            //GameObject tmpObject = Instantiate(fruitData[nextFruit].fruitObject, pos, Quaternion.identity);

            //Jika bukan Potion terakhir maka lanjutkan Potion selanjutnya
            if (nextFruit != 11)
            {
                GameObject tmpObject = GetObjectPool(fruitData[nextFruit].suikaType);
                tmpObject.transform.localPosition = pos;
                tmpObject.transform.rotation = Quaternion.identity;

                tmpObject.transform.SetParent(bucket);
            }

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

            if (!_Alert)
            {
                _Alert = alert;
                timer = timeToGameOver;
            }
        }
        else
        {
            if (_Alert)
            {
                ChangeExpressionNormal();
            }

            _Alert = alert;
            timer = timeToGameOver;



            gameOver = false;

            uiManager.RedLineUntriggered();
        }

        
    }
    private void Update()
    {
        if (_Alert)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (!gameOver)
                {
                    Debug.Log("GAME OVER, BREWING ALL POTION");
                    uiManager.StartGameOverAnimation();
                    gameOver = true;
                }
            }
        }
    }
}
