using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Tower
{
    [SerializeField] TowerScriptableObject towerScript;

    private void Start()
    {
        Setup(towerScript, 0);

        StartCoroutine(CorInstantiate());

        IEnumerator CorInstantiate()
        {
            var o = new WaitForSeconds(towerData.produce[curLevel]);

            while (true)
            {
                Coin();
                yield return o;
            }
        }
    }

    public void Coin() {
        GameManager.Ins.ManageCoin(5);
    }
}
