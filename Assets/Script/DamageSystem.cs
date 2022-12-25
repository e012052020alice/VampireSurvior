using UnityEngine;
using TMPro;

namespace ChimmyBear
{
    /// <summary>
    /// 受傷系統
    /// </summary>
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField, Header("血量"), Range(0, 5000)]
        private float hp;
        [SerializeField, Header("受傷半徑"), Range(0, 50)]
        private float radiusDamage;
        [SerializeField, Header("受傷位移")]
        private Vector2 offsetDamage;
        [SerializeField, Header("傷害值物件")]
        private GameObject prefabDamage;
        [SerializeField, Header("傷害值物件位移")]
        private Vector2 offestDamagePrefab;
        [SerializeField, Header("受傷圖層")]
        private LayerMask layerDamage;
        [SerializeField, Header("受傷無敵時間"), Range(0, 1)]
        private float timeInvisiable = 0.2f;
        [SerializeField, Header("經驗值"), Range(0, 5000)]
        private float exp;
        [SerializeField]
        private GameObject prefabExp;

        private float timer;
        private bool isDamage;
        

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.1f, 0.3f, 0.5f);
            Gizmos.DrawSphere(transform.position + transform.TransformDirection(offsetDamage), radiusDamage);
        }
        private void Update()
        {
            GetDamage();
            InvisiableTimer();
        }
        ///<summary>
        ///造成傷害判定
        ///</summary>
        private void GetDamage()
        {
            Collider2D hit = Physics2D.OverlapCircle(
                transform.position + transform.TransformDirection(offsetDamage), 
                radiusDamage,layerDamage);
           
            if (hit)
            {
                if (!isDamage)
                {
                    float attack = hit.GetComponent<WeaponAttack>().attack;
                    hp -= attack;
                    if (hp <= 0) Dead();
                    //print(hit.name);
                    isDamage = true;
                    GameObject tempDamge=Instantiate(
                    prefabDamage,
                    transform.position + transform.TransformDirection(offestDamagePrefab),
                    Quaternion.identity);
                    tempDamge.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attack.ToString();
                }
                

            }
        }
        ///<summary>
        ///死亡
        ///</summary>
        private void Dead()
        {
            GameObject tempExp = Instantiate(prefabExp, transform.position, Quaternion.identity);
            tempExp.GetComponent<ExpManager>().exp = exp;
            
            Destroy(gameObject);

        }
        



        ///<summary>
        ///無敵時間計時器
        ///</summary>
        private void InvisiableTimer()
        {
            if (isDamage)
            {
                if (timer < timeInvisiable) timer += Time.deltaTime;
                else
                {
                    timer = 0;
                    isDamage = false;
                }
            }
        }
    }
}

