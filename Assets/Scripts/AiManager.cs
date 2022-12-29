using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public static AiManager Ins { get; private set; }

    public List<Tower> allTower = new List<Tower> { };

    private void Awake()
    {
        Ins = this;
    }
}
