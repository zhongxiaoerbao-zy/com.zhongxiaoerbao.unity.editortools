/// <summary>
/// 作者：钟樾
/// 备注：钟樾的Unity Editor Tools
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Test
{
    public class Editor_Tools
    {
        #region 删除操作
        [MenuItem("GameObject/EditorTools(zxeb)Test/我的删除", true, 11)]
        static bool MyDeleteValidata()  //验证是否可用
        {
            //如果当前没有选择object，则无法点击该工具
            if (Selection.objects.Length > 0) { return true; }
            else { return false; }
        }
        [MenuItem("GameObject/EditorTools(zxeb)/Test/我的删除", false, 11)]
        static void MyDelete() //如果可用，就调用
        {
            foreach (Object obj in Selection.objects)
            {
                //删除操作(不可被撤销)
                //GameObject.DestroyImmediate(obj);

                //删除操作(可被撤销)
                Undo.DestroyObjectImmediate(obj);
            }
        }
        #endregion
    }
}