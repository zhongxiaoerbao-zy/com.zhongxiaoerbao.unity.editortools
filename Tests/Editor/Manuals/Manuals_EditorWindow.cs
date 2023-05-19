/// <summary>
/// 作者：钟   樾
/// 时间：2018-07-13 15:41:27
/// 备注：创建自定义窗口 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Manual
{
    public class Manuals_EditorWindow : EditorWindow
    {
        bool groundEnable = false;
        float value1 = 0f;

        [MenuItem("EditorTools(zxeb)/Help/Manual/StudyTest/My Window")]
        private static void ShowMyWindow()
        {
            Rect theRect = new Rect(0, 0, 500, 500);
            //创建自定义窗口方法一：只能创建一个
            //Manual_3 window = (Manual_3)EditorWindow.GetWindowWithRect(typeof(Manual_3), theRect, true, "我的窗口");//独立窗口
            Manuals_EditorWindow window = (Manuals_EditorWindow)EditorWindow.GetWindowWithRect(typeof(Manuals_EditorWindow), theRect, false, "我的窗口");
            //选项卡窗口
            //Manual_3 window = EditorWindow.GetWindow<Manual_3>();
            window.Show();

            //创建自定义窗口方法二：可以创建无数个
            //Manual_3.CreateInstance<Manual_3>().Show();
        }

        private string name = "gameObject";

        private void OnGUI()
        {
            if (GUILayout.Button("打开通知", GUILayout.Width(220)))
            {
                this.ShowNotification(new GUIContent("通知的内容"));
            }
            if (GUILayout.Button("关闭通知", GUILayout.Width(220)))
            {
                this.RemoveNotification();
            }

            GUILayout.Label("以下是基础设置");
            groundEnable = EditorGUILayout.BeginToggleGroup("启用", groundEnable);
            value1 = EditorGUILayout.FloatField("值1", value1);
            EditorGUILayout.EndToggleGroup();

            name = GUILayout.TextField(name);
            if (GUILayout.Button("创建一个GameObject"))
            {
                //创建一个GameObejct
                GameObject go = new GameObject(name);
                //记录操作，方便撤回
                Undo.RegisterCreatedObjectUndo(go, "Create GameObject");
            }
        }

        /// <summary>
        /// 窗口获得焦点的时候调用
        /// </summary>
        private void OnFocus()
        {

        }

        /// <summary>
        /// 窗口失去焦点的时候调用
        /// </summary>
        private void OnLostFocus()
        {

        }

        /// <summary>
        /// 层次面板的对象发生改变时，调用
        /// </summary>
        private void OnHierarchyChange()
        {

        }

        /// <summary>
        /// 项目面板的对象发生改变时，调用
        /// </summary>
        private void OnProjectChange()
        {

        }

        /// <summary>
        /// 当物体被选中时，调用
        /// </summary>
        private void OnSelectionChange()
        {

        }

        /// <summary>
        /// 当窗口被关闭时,调用
        /// </summary>
        private void OnDestroy()
        {

        }
    }
}
