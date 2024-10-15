using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("TrannChanhh/WeaponManager")]
public class WeaponManager : MonoBehaviour
{
    private static WeaponManager Instance;
    public static WeaponManager instance
    {
        get => Instance;
    }
    [Header("Arrow")]
    public int totalArrow = 0;
    private void Awake()
    {
        if (name != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }
}
