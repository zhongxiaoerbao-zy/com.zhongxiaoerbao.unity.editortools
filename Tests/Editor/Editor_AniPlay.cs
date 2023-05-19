/// <summary>
///	作者：钟樾
///	时间：#CreatTime#
///	备注：
///	</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Test

{
    public class Editor_AniPlay : EditorWindow
    {
        #region 创建自定义窗口
        [MenuItem("EditorTools(zxeb)/Test/StudyTest/Play Editor", false, 1)]
        private static void CreateWindow()
        {
            Rect theRect = new Rect(0, 0, 500, 500);

            Editor_AniPlay aniWindow = (Editor_AniPlay)EditorWindow.GetWindowWithRect(typeof(Editor_AniPlay), theRect, false, "动画播放编辑");
            aniWindow.Show();
        }
        #endregion


        Object[] objs;

        private void OnHierarchyChange()
        {
            Debug.Log("..");
        }

        private void OnSelectionChange()
        {
            objs = Selection.objects;
        }
         
        private void OnGUI()
        {
            if (GUILayout.Button("加载AudioSource组件"))
            {
                AddAudioSource();
            }
        }

        private void AddAudioSource()
        {
            if (objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    //如果选择对象存在组件Animator
                    if (Selection.gameObjects[i].GetComponent<Animator>() != null)
                    {
                        //如果对象没有组件：AudioSource,则加载组件：AudioSource
                        if (Selection.gameObjects[i].GetComponent<AudioSource>() == null)
                        {
                            Selection.gameObjects[i].AddComponent<AudioSource>();
                        }
                        else
                        {
                            EditorGUILayout.HelpBox("当前对象已有AudioSource组件", MessageType.Info);
                        }
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("当前对象没有组件：Animator", MessageType.Warning);
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("请在Hierarchy,至少选择一个对象", MessageType.Error);
            }
        }
    }
}