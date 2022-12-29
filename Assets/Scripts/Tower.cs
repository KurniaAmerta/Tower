using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerData { get; private set; }

    public int curHealth;
    public int curLevel { get; private set; }

    public DateTime curTime; 

    [SerializeField] SpriteRenderer towerSpr;

    public void Setup(TowerScriptableObject data, int level) {
        curLevel = level;
        towerData = data;
        curTime = DateTime.Now;
        curHealth = towerData.health[level];
        towerSpr.sprite = towerData.towerSpr[level];
    }

    public void ResetTime() {
        curTime = DateTime.Now;
    }

    public void Damage(int damage) {
        curHealth -= damage;
        if (curHealth < 0) {
            curHealth = 0;
            if (towerData.isBase) {
                GameManager.Ins.ShowWinner(true);
            }
            try {
                Destroy(this.gameObject);
                GameManager.Ins.DestroyTower();
            }
            catch (Exception e) {
                Debug.LogError(e.Message);
            }
        }
    }
}
