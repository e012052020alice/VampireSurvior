using UnityEngine;

namespace ChimmyBear
{
    /// <summary>
    /// �ĤH�t��:�l�ܪ��a
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("���ʳt��"),Range(0, 10)]
        private float speed = 3.5f;
        [SerializeField, Header("����Z��"), Range(0, 10)]
        private float stopDistance = 1.5f;

        private string nameTarget = "�I�M��";
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
        ///�l��
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
        ///½��
        ///</summary>
        ///<param name="xCurrent">������X</param>
        ///<param name="Target">���ؼЪ���X</param>
        private void Flip(float xCurrent,float xTarget)
        {
            float annle = xCurrent > xTarget ? 0 : 180;
            transform.eulerAngles = new Vector3(0, annle, 0);
        }
    }
}

