using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit", menuName = "Suika/Fruit")]
public class FruitData : ScriptableObject
{
    public SuikaType suikaType;
    public Sprite fruitSprite;
    public GameObject fruitObject;
    public int incrementScore;
    public float threshold;
}
