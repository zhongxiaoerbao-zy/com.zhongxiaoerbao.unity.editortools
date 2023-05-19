/*
 * 作  者：钟小二宝
 * e-mail：1023462838@qq.com
 * 说  明：Unity资源重命名
 */

using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor
{
    public class UnityEditorTools_RenameUnityObjects : ScriptableWizard
    {
        [MenuItem("EditorTools(zxeb)/Assets/Rename/Rename Unity Objects (Obsolete)")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<UnityEditorTools_RenameUnityObjects>("Unity Assets 批量重命名(Obsolete)", "取消", "重命名");
        }

        private void OnWizardOtherButton()
        {
            RenameObjects();
        }

        private void OnWizardUpdate()
        {
            helpString = null;
            errorString = null;

            if (Selection.objects.Length > 0) { helpString = "当前选择了" + Selection.objects.Length + "Unity Object"; }
            else { errorString = "请至少选择一个Unity Object"; }
        }

        private void OnSelectionChange()
        {
            OnWizardUpdate();
        }
        // 类型
        public string m_Type = "null";
        // 名称
        public string m_Name = "null";
        // ID
        public int UniqueID = 1;
        // 后缀
        public string m_Variation = "null";

        void RenameObjects()
        {
            if (m_Name == null || m_Name == "" || m_Name == "null" || m_Name.Trim().Length ==0)
            {
                errorString = "请输入正确的“m_Name”";
                return;
            }

            foreach (var asset in Selection.objects)
            {
                if (asset.GetType() == typeof(GameObject))
                {
                    RenameGameObjects(asset);
                }
                else
                {
                    RenameAssets(asset);
                }

                UniqueID += 1;
            }

            UniqueID = 1;
        }

        private void RenameAssets(Object asset)
        {
            string path = AssetDatabase.GetAssetPath(asset);

            string newName = m_Name + "_" + UniqueID.ToString("d3");

            if (m_Type != null && m_Type != "" && m_Type != "null".Trim()) { newName = m_Type.Trim() + "_" + newName; }
            if (m_Variation != null && m_Variation != "" && m_Variation != "null".Trim()) { newName = newName + "_" + m_Variation.Trim(); }

            AssetDatabase.RenameAsset(path, newName);
        }
  
        private void RenameGameObjects(Object gameObject)
        {
            string newName = m_Name.Trim() + "_" + UniqueID.ToString("d3");

            if (m_Type != null && m_Type != "" && m_Type != "null".Trim()) { newName = m_Type.Trim() + "_" + newName; }
            if (m_Variation != null && m_Variation != "" && m_Variation != "null".Trim()) { newName = newName + "_" + m_Variation.Trim(); }

            gameObject.name = newName;
        }
    }
}