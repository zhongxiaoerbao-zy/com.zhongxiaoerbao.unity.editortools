/*
 * 时间：
 * 作者：
 * 备注：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhongXiaoErBao.Unity.Editor.Test

{
    [AddComponentMenu("EditorTools(zxeb)/Test/Scripts/Example_Editor")]//通过菜单栏的Component,快速挂载
    [RequireComponent(typeof(Rigidbody))]//挂载脚本时，同时挂载一个Rigidbody
    public class Example_Editor : MonoBehaviour
    {
        public int m_PlayerHp = 0;

        //鼠标右键点击变量，可以增加属性值
        [ContextMenuItem("Add Mp(+10)", "AddMp")]
        public int m_PlayerMp = 0;

        void AddMp()
        {
            m_PlayerMp += 10;
        }

        //扩展栏增加属性值
        [ContextMenu("Add Hp(+10)")]
        void AddHp()
        {
            m_PlayerHp += 10;
        }

        public Texture _Texture;
        public float _SliderValue = 0.1f;
    }
}