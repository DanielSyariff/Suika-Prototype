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

    private void Start()
    {
        suikaManager = SuikaManager.instance;
    }

    public void UpdateUIScore()
    {
        textScore.text = "Score : " + suikaManager.score.ToString();
    }

    public void UpdateUINextFruit(Sprite image)
    {
        imageNextFruit.sprite = image;
    }
}
