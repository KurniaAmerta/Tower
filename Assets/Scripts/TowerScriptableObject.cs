using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerScriptableObject : ScriptableObject
{
    public int[] health, coin, time, level; 
    public float[] produce;
    public bool isBase;
    public Tower towerObj;
    public Sprite[] towerSpr;
}