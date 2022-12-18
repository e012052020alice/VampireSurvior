using UnityEngine;

namespace ChimmyBear
{
    /// <summary>
    /// ���˨t��
    /// </summary>
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField, Header("��q"), Range(0, 5000)]
        private float hp;
        [SerializeField, Header("���˥b�|"), Range(0, 50)]
        private float radiusDamage;
        [SerializeField, Header("���˦첾")]
        private Vector2 offsetDamage;
        [SerializeField, Header("�ˮ`�Ȫ���")]
        private GameObject prefabDamage;
        [SerializeField, Header("�ˮ`�Ȫ���첾")]
        private Vector2 offestDamagePrefab;
        [SerializeField, Header("���˹ϼh")]
        private LayerMask layerDamage;
        [SerializeField, Header("���˵L�Įɶ�"), Range(0, 1)]
        private float timeInvisiable = 0.2f;

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
        ///�y���ˮ`�P�w
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
                    //print(hit.name);
                    isDamage = true;
                    Instantiate(
                    prefabDamage,
                    transform.position + transform.TransformDirection(offestDamagePrefab),
                    Quaternion.identity);
                }
                

            }
        }
        ///<summary>
        ///�L�Įɶ��p�ɾ�
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

