using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private SuikaManager suikaManager;
    public TextMeshProUGUI textScore;
    public Image imageNextFruit;

    public SpriteRenderer redLine;

    private void Start()
    {
        suikaManager = SuikaManager.instance;
        UpdateUIScore();
    }

    public void UpdateUIScore()
    {
        textScore.text = suikaManager.score.ToString();
    }

    public void UpdateUINextFruit(Sprite image)
    {
        imageNextFruit.sprite = image;
    }

    public void RedLineTriggered()
    {
        redLine.gameObject.SetActive(true);
        Debug.Log("Activating Alert");
    }
    public void RedLineUntriggered()
    {
        redLine.gameObject.SetActive(false);
        Debug.Log("Deactivating Alert");
    }
}
