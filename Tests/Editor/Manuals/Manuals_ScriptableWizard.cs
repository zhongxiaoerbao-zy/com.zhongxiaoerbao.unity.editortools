/// <summary>
/// 作者：钟   樾
/// 时间：2018-07-13 14:12:50
/// 备注：创建对话框,以及对话框中的帮助信息、错误信息、提示信息、进度条
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Manual
{
    public class Manuals_ScriptableWizard : ScriptableWizard
    {
        /// <summary>
        /// 创建对话框
        /// </summary>
        [MenuItem("EditorTools(zxeb)/Help/Manual/StudyTest/Create Wizard", false, 0)]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Manuals_ScriptableWizard>("#对话框的标签#", "#CreateButton#", "#OtherButton#");
        }

        public int Hp = 2;
        public int Mp = 1;

        private const string hp = "Hp";
        private const string mp = "Mp";

        /// <summary>
        /// 对话框被打开时，调用
        /// </summary>
        private void OnEnable()
        {
            //获取保存的数据
            Hp = EditorPrefs.GetInt(hp, Hp);
            Mp = EditorPrefs.GetInt(mp, Mp);
        }


        #region 如果有OnGUI()，则在对话框中，不能使用CreatButton等对话框自有的一些UI
        //private void OnGUI()
        //{

        //}

        /// <summary>
        /// 监测Create按钮的点击，会关闭当前对话框
        /// </summary>
        private void OnWizardCreate()
        {
            Debug.Log("Create按钮被点击");

            GameObject[] cube = Selection.gameObjects;

            //进度条
            EditorUtility.DisplayProgressBar("进度", "0/" + cube.Length + "完成修改", 0);
            int count = 0;

            foreach (GameObject go in cube)
            {
                try
                {
                    //获取所选物体上的脚本

                    //记录操作，方便撤回
                    //Undo.RecordObject(ch, "change Hp and Mp");

                    //ch.m_PlayerHp += Hp;
                    //ch.m_PlayerMp += Mp;
                }
                catch
                {
                    Debug.Log("这个GameObject上不能获取到“CubeHealth”");
                }

                //进度条
                count++;
                EditorUtility.DisplayProgressBar("进度", count + "/" + cube.Length + "完成修改", (float)count / cube.Length);
            }

            //进度条：完成后，关闭进度条显示
            EditorUtility.ClearProgressBar();

            //提示信息
            ShowNotification(new GUIContent(Selection.gameObjects.Length + "个游戏物体的值被修改了"));

            //保存数据，方便下次调整时，频繁修改至相同的值
            EditorPrefs.SetInt(hp, Hp);
            EditorPrefs.SetInt(mp, Mp);
        }

        /// <summary>
        /// 按钮，点击后不会关闭对话框
        /// </summary>
        private void OnWizardOtherButton()
        {
            //在创建的对话框中显示提示信息
            ShowNotification(new GUIContent(Selection.gameObjects.Length + "个游戏物体的值被修改了"));
            OnWizardCreate();
            Debug.Log("Other Button被点击了");
        }
        #endregion

        /// <summary>
        /// 打开自定义对话框，或修改字段的值时，调用
        /// </summary>
        private void OnWizardUpdate()
        {
            helpString = null;
            errorString = null;

            if (Selection.gameObjects.Length > 0)
            {
                helpString = "当前选择了" + Selection.gameObjects.Length + "个敌人";
            }
            else
            {
                errorString = "请至少选择一个GameObject";
            }
        }
        
        /// <summary>
        /// 当选择的GameObject发生改变时，调用
        /// </summary>
        private void OnSelectionChange()
        {
            OnWizardUpdate();
        }

        #region 参考
        ///// <summary>
        ///// 编辑自己的编辑器GUI
        ///// </summary>
        //private void OnGUI()
        //{

        //}

        ///// <summary>
        ///// 点击Create按钮后,调用方法并关闭对话框
        ///// </summary>
        //private void OnWizardCreate()
        //{
        //    Debug.Log("Create按钮被点击");
        //}

        ///// <summary>
        ///// 点击OtherButton按钮后，调用
        ///// </summary>
        //private void OnWizardOtherButton()
        //{

        //}

        ///// <summary>
        ///// 当对话框中的数据发生改变时，调用
        ///// </summary>
        //private void OnWizardUpdate()
        //{

        //}

        ///// <summary>
        ///// 对话框被关闭时，调用
        ///// </summary>
        //private void OnDestroy()
        //{

        //}

        ///// <summary>
        ///// 当脚本化对象超出范围时，将调用此函数。
        ///// 当对象被销毁时也可以调用它，并且可以用于任何清理代码。
        ///// 编译完成后重新加载脚本时，将调用OnDisable，然后在加载脚本后调用OnEnable
        ///// </summary>
        //private void OnDisable()
        //{

        //}

        ///// <summary>
        ///// 对话框被打开时，调用
        ///// </summary>
        //private void OnEnable()
        //{

        //}

        ///// <summary>
        ///// 窗口获得焦点时，调用
        ///// </summary>
        //private void OnFocus()
        //{

        //}

        ///// <summary>
        ///// 窗口失去焦点时，调用
        ///// </summary>
        //private void OnLostFocus()
        //{

        //}

        ///// <summary>
        ///// 有GameObject被点击选取后，调用
        ///// </summary>
        //private void OnSelectionChange()
        //{

        //}
        #endregion
    }
}