/// <summary>
/// 作者：钟樾
/// 备注：创建对话框,以及对话框中的帮助信息、错误信息、提示信息、进度条
/// </summary>
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Test
{
    public class Editor_ScriptableWizard : ScriptableWizard
    {
        /// <summary>
        /// 创建对话框
        /// </summary>
        [MenuItem("EditorTools(zxeb)/Test/钟樾的对话框")]
        private static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Editor_ScriptableWizard>("钟樾的对话框", "Change And Close", "Change");
        }
        
        /// <summary>
        /// 打开自定义对话框，或修改字段的值时，调用
        /// </summary>
        private void OnWizardUpdate()
        {
            helpString = null;
            errorString = null;

            if (Selection.gameObjects.Length > 0)
            {
                helpString = "当前选择了 " + Selection.gameObjects.Length + " 个对象";
            }
            else
            {
                errorString = "请至少选择一个GameObject";
            }
        }

    }
}
