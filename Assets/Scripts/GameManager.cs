using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins { get; private set; }

    [SerializeField] TowerScriptableObject[] allTowerData;
    
    [Header("Basic UI")]
    [SerializeField] Button[] towerBtn;
    [SerializeField] Text coinTxt, winTxt, towerTxt;
    [SerializeField] GameObject winObj;

    [Header("Update UI")]
    [SerializeField] Text updateTimeTxt, updateCoinTxt, levelTxt;
    [SerializeField] Button updateTimeBtn, updateCoinBtn;
    [SerializeField] GameObject panelUpdateObj;

    private PlayerData playerData;

    private int index = -1, towerDestroy;

    Tower towerUpdated;

    private void Awake()
    {
        Ins = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount>0 )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Transform obj = hit.transform;
                Tower twr = obj.GetComponent<Tower>();
                if (twr) {
                    ShowUpgrade(twr); 
                }
                else if (obj.childCount == 0 && index >= 0) {
                    var o = Instantiate(allTowerData[index].towerObj, obj);
                    o.Setup(allTowerData[index], 0);

                    index = -1;
                    SetupButton();
                }
            }
        }
    }

    public void InstantiateTower(int _index) {
        index = index == _index ? -1 : _index;
        SetupButton();
    }

    public void SetupButton() {
        for (int i = 0; i < towerBtn.Length; i++)
        {
            towerBtn[i].interactable = index == i || index == -1;
        }
    }

    public void ShowUpgrade(Tower tower) {
        if (tower.curLevel + 1 < tower.towerData.level.Length) {  
            towerUpdated = tower;
            panelUpdateObj.SetActive(true);
            updateCoinBtn.interactable = playerData.coin >= towerUpdated.towerData.coin[towerUpdated.curLevel + 1];
            levelTxt.text = "Upgrade Tower Level " + (towerUpdated.curLevel + 2);
            updateCoinTxt.text = "Upgrade " + towerUpdated.towerData.coin[towerUpdated.curLevel+1]+"c";
            StopCoroutine(CorUpdateTime());
            StartCoroutine(CorUpdateTime());
        }
    }

    public void CloseUpgrade() {
        towerUpdated = null;
    }

    IEnumerator CorUpdateTime() {
        int second = towerUpdated.towerData.time[towerUpdated.curLevel+1] - (int)Mathf.Ceil((float)(DateTime.Now - towerUpdated.curTime).TotalSeconds);
        var waiting = new WaitForSeconds(1f);

        updateTimeBtn.interactable = false;

        while (towerUpdated && second > 0) {
            second--;
            updateTimeTxt.text = "Upgrade "+second+"s";
            yield return waiting;
        }

        updateTimeBtn.interactable = true;

        updateTimeTxt.text = "Upgrade";
    }

    public void UpdateTower(bool isCoin) {
        if(isCoin) ManageCoin(towerUpdated.towerData.coin[towerUpdated.curLevel + 1]*-1);
        towerUpdated.ResetTime();
        towerUpdated.Setup(towerUpdated.towerData, towerUpdated.curLevel+1);
        panelUpdateObj.SetActive(false);
        towerUpdated = null;
    }

    public void ShowWinner(bool isPlayer) {
        winTxt.text = isPlayer ? "Player Win" : "Player Lose";
        winObj.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }

    public void ManageCoin(int coin) {
        playerData.coin += coin;

        if (towerUpdated && towerUpdated.towerData.coin.Length > towerUpdated.curLevel + 1) updateCoinBtn.interactable = playerData.coin >= towerUpdated.towerData.coin[towerUpdated.curLevel + 1];

        coinTxt.text = "Coin: " + playerData.coin.ToString();
    }

    public void DestroyTower() {
        towerDestroy++;
        towerTxt.text = "Tower: "+towerDestroy.ToString();
    }
}

public struct PlayerData {
    public int coin, towerDestroy;
}
