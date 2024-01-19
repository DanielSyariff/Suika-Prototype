using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private SuikaManager suikaManager;
    public TextMeshProUGUI textScore;
    public Image imageNextFruit;

    public SpriteRenderer redLine;

    [Header("Setting Menu")]
    public GameObject settingPanel;
    public Transform settingPopUp;

    [Header("Recipe Menu")]
    public GameObject recipePanel;
    public Transform recipePopUp;

    private void Start()
    {
        suikaManager = SuikaManager.instance;
        UpdateUIScore();
    }

    #region Gameplay UI
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
    #endregion

    #region Setting Menu
    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        settingPopUp.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        settingPopUp.DOScale(Vector3.zero, 0.5f);
    }
    #endregion

    #region Setting Menu
    public void OpenRecipe()
    {
        recipePanel.SetActive(true);
        recipePopUp.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void CloseRecipe()
    {
        recipePanel.SetActive(false);
        recipePopUp.DOScale(Vector3.zero, 0.5f);
    }
    #endregion
}
