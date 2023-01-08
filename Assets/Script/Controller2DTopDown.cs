﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChimmyBear
{
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// 2D Top Down 類型控制器
    /// </summary>
    public class Controller2DTopDown : MonoBehaviour
    {
        [SerializeField,Header("移動速度"),Range(0,100)]
        private float speed = 3.5f;
        private Animator ani;
        private Rigidbody2D rig;
        private string parWalk = "走路開關";

        private void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            LevelManager.instance.onLevelup += WhenPlayerLevelUp;
        }

        private void Update()
        {
            Move();
        }
        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            //print($"玩家的水平軸向值:{h}");
            rig.velocity = new Vector2(h, v) * speed;
            UpdateAnimation(h, v);
            Flip(h);
        }
        private void UpdateAnimation(float h,float v)
        {
            ani.SetBool(parWalk, h != 0 || v != 0);
        }
        private void Flip(float h)
        {
            if (Mathf.Abs(h) < 0.1f) return;
            transform.eulerAngles = new Vector2(0, h < 0 ? 0 : 180);
        }
        ///<summary>
        ///當玩家升級時
        ///</summary>
        private void WhenPlayerLevelUp()
        {
            if (this)
                enabled = false;
        }
    }

}

