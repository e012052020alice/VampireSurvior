using UnityEngine;

namespace ChimmyBear
{
    /// <summary>
    /// 敵人系統:追蹤玩家
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("移動速度"),Range(0, 10)]
        private float speed = 3.5f;
        [SerializeField, Header("停止距離"), Range(0, 10)]
        private float stopDistance = 1.5f;

        private string nameTarget = "鐮刀怪";
        private Transform traTarget;
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.1f, 0.1f, 0.3f);
            Gizmos.DrawSphere(transform.position, stopDistance);
            
        }
        private void Awake()
        {
            traTarget = GameObject.Find(nameTarget).transform;
        }
        private void Update()
        {
            Track();
        }
        ///<summary>
        ///追蹤
        ///</summary>
        private void Track()
        {
            Vector3 posTarget = traTarget.position;
            Vector3 posCurrent = transform.position;
            Flip(posCurrent.x, posTarget.x);
            if (Vector3.Distance(posCurrent, posTarget) <= stopDistance) return;

            posCurrent = Vector3.MoveTowards(posCurrent, posTarget, speed*Time.deltaTime);
            transform.position = posCurrent;
        }
        ///<summary>
        ///翻面
        ///</summary>
        ///<param name="xCurrent">此物件的X</param>
        ///<param name="Target">此目標物件的X</param>
        private void Flip(float xCurrent,float xTarget)
        {
            float annle = xCurrent > xTarget ? 0 : 180;
            transform.eulerAngles = new Vector3(0, annle, 0);
        }
    }
}

