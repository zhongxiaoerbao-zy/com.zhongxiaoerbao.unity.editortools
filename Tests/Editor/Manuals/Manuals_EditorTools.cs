/// <summary>
/// 作者：钟樾
/// 备注：Unity Editor学习手册
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Manual
{
    public class Manuals_EditorTools
    {
        //路径
        [MenuItem("EditorTools(zxeb)/Help/Manual/test/test1")]
        static void Test() { }

        //优先级
        [MenuItem("EditorTools(zxeb)/Help/Manual/StudyTest/test1", false, 0)]
        static void Test1() { }

        //将“按键”加入到“GameObject”，并在“Hierarchy”右键时弹出的菜单栏中显示
        [MenuItem("GameObject/EditorTools(zxeb)/Help/Manual/StudyTest", false, 11)]
        static void Tool() { }

        /// <summary>
        /// 对某个组件里的某些数值一次性进行修改
        /// 类似于“重置”功能
        /// </summary>
        /// <param name="cmd"></param>
        [MenuItem("CONTEXT/PlayerHealth/Init")]//路径："CONTEXT/组件名/按键名"
        static void Init(MenuCommand cmd)//MenuCommand是当前正在操作的组件
        {
            //CompleteProject.PlayerHealth health = cmd.context as CompleteProject.PlayerHealth;
            //health.startingHealth = 200;
            //health.flashSpeed = 10;
        }

        /// <summary>
        /// 添加快捷键
        /// </summary>
        /// % = ctrl, # = shift, & = alt
        [MenuItem("EditorTools(zxeb)/Help/Manual/StudyTest/Hot Key %H")]
        //[MenuItem("StudyTest/Hot Key _H")]//不适用特殊按键时，需要添加下划线
        static void AddHotKey()
        {
            Debug.Log("热键测试");
        }
    }
}
