using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("TrannChanhh/GameRenerence")]
public class GameRenerence : MonoBehaviour
{
    public GameObject arrowEffectImpactPrefabs;
    private static GameRenerence intance;

    public static GameRenerence Instance
    { 
        get => intance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(gameObject, 2f);
            return;
        }
        intance = this;
    }
}
