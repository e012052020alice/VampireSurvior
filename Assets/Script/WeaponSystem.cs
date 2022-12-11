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

        private void Awake()
        {
            SpawnWeapon();

        }
        /// <summary>
        /// 生成武器
        /// </summary>
        private void SpawnWeapon()
        {
            //生成(物件，座標，角度)
            //transform.position此物件的座標
            // Quaternion.identity零角度
            GameObject tempWeapon=Instantiate(weaponData.prefabWeapon, transform.position + weaponData.weaponObjects[0].pointSpawn, Quaternion.identity);

            tempWeapon.GetComponent<Rigidbody2D>().AddForce(weaponData.weaponObjects[0].speed);
        }

    }


}