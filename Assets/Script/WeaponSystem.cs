using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChimmyBear
{

    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        private WeaponData weaponData;
        [SerializeField]
        private int level;

        private WeaponLevelData weaponLevel => weaponData.weaponLevelDatas[level];
        private void Awake()
        {
            //SpawnWeapon();
            InvokeRepeating("SpawnWeapon", 0, weaponLevel.intervalSpawn);
        }
        /// <summary>
        /// 生成武器
        /// </summary>
        private void SpawnWeapon()
        {
            WeaponObject[] weaponObject = weaponLevel.weaponObjects;
            for(int i = 0; i <weaponObject.Length; i++)
            {
                //生成(物件，座標，角度)
                //transform.position此物件的座標
                // Quaternion.identity零角度
                GameObject tempWeapon = Instantiate(weaponData.prefabWeapon, transform.position + transform.TransformDirection(weaponObject[i].pointSpawn), Quaternion.identity);

                Vector2 speedMove;
                if (weaponData.withCharacterDircetion) speedMove = transform.TransformDirection(weaponObject[i].speed);
                else speedMove = weaponObject[i].speed;
                tempWeapon.GetComponent<Rigidbody2D>().AddForce(speedMove);
            }
            
        }

    }


}