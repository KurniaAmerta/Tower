using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : Tower
{
    [SerializeField] Knight knight;

    private void Start()
    {
        StartCoroutine(CorInstantiate());

        IEnumerator CorInstantiate()
        {
            var o = new WaitForSeconds(5f);

            while (true)
            {
                Bullet();
                yield return o;
            }
        }
    }

    public void Bullet()
    {
        if (AiManager.Ins.allTower.Count > 0)
        {
            var o = Instantiate(knight, this.transform.position, Quaternion.identity);
            try {
                o.target = AiManager.Ins.allTower[0].gameObject.transform;
            }
            catch (Exception e) {
                Debug.LogError(e.Message);
            }
        }
    }
}
