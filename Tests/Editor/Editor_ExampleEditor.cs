//================================
//作者：钟   樾
//时间：2018-07-20 09:58:55
//备注：
//================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Test
{
    //[CustomEditor(typeof(ExampleView))]
    public class Editor_ExampleEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //ExampleView ev = (ExampleView)target;

            //ev._Texture = EditorGUILayout.ObjectField("选择贴图", ev._Texture, typeof(Texture), true) as Texture;
            //ev._SliderValue = EditorGUILayout.Slider("Slider Value", ev._SliderValue, 0, 1);

            GUILayout.Label("===========分割线===========");
            if (GUILayout.Button("按钮"))
            {
                Debug.Log("检测到点击");
            }
        }
    }
}