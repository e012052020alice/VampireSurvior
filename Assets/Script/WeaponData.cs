﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChimmyBear
{

    /// <summary>
    /// 武器資料
    /// </summary>
    [CreateAssetMenu(menuName = "ChimmyBear/Weapon Data",fileName = "New Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("武器攻擊力"), Range(0, 10000)]
        public float attack;
        [Header("武器物件數量、速度與位置")]
        public WeaponObject[] weaponObjects;
    }
    [System.Serializable]
    public class WeaponObject
    {
        [Header("武器速度")]
        public Vector2 speed;
        [Header("武器生成位置")]
        public Vector3 pointSpawn;
    }
}
