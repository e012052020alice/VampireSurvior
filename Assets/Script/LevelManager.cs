using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChimmyBear
{
    [DefaultExecutionOrder(100)]
    /// <summary>
    ///等級管理器
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField, Header("吸取經驗值半徑")]
        private float getExpRadius = 2.5f;
        [SerializeField, Header("吸取經驗值速度")]
        private float getExpSpeed = 5.5f;
        [SerializeField, Header("經驗值塗層")]
        private LayerMask layerExp;

        private int lv = 1;
        private float expCurrent;
        private Image imgExp;
        private TextMeshProUGUI textLv;

        [SerializeField]
        private float[] expNeeds;

        /// <summary>
        ///升級技能選取介面
        /// </summary>
        private Animator aniUpdateLevelAndChooseSkill;
        private void Awake()
        {
            instance = this;

            imgExp = GameObject.Find("圖片經驗值").GetComponent<Image>();
            textLv = GameObject.Find("文字等級").GetComponent<TextMeshProUGUI>();
            aniUpdateLevelAndChooseSkill = GameObject.Find("升級技能選取介面").GetComponent<Animator>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.1f, 0.8f, 0.7f, 0.3f);
            Gizmos.DrawSphere(transform.position, getExpRadius);
        }
        private void Update()
        {
            GetExpObject();
        }

        public static LevelManager instance;
        public delegate void LevelUP();//委派
        public event LevelUP onLevelup;//事件

        [ContextMenu("更新經驗值需求表")]
        private void UpdateExpNeeds()
        {
            expNeeds = new float[99];
            for(int i = 0; i < expNeeds.Length; i++)
            {
                expNeeds[i] = (i + 1) * 100 + ((i + 1) * 10);
            }
        }

        /// <summary>
        ///等級管理器
        /// </summary>
        private void GetExpObject()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, getExpRadius, layerExp);
            for(int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];
                if (hit)
                {
                    Vector2 pos = Vector2.MoveTowards(
                        hit.transform.position, transform.position, getExpSpeed * Time.deltaTime);
                    hit.transform.position = pos;

                    UpdateExp(hit);
                }
            }
            
        }
        /// <summary>
        ///更新經驗值
        /// </summary>
        private void UpdateExp(Collider2D hit)
        {
            float dis = Vector2.Distance(hit.transform.position, transform.position);
            if (dis <= 0.5f)
            {
                expCurrent += hit.GetComponent<ExpManager>().exp;

                float expNeed = expNeeds[lv - 1];
                if (expCurrent >= expNeed)
                {
                    expCurrent -= expNeed;
                    UpdateLevel();
                }
                imgExp.fillAmount = expCurrent / expNeed;

                Destroy(hit.gameObject);
            }
        }
        ///<summary>
        ///升級
        ///</summary>
        private void UpdateLevel()
        {
            lv++;//升級
            textLv.text = "Lv" + lv;//更新等級介面
            aniUpdateLevelAndChooseSkill.enabled = true;//啟動升級介面動畫
            onLevelup();//觸發事件
        }
    }
}