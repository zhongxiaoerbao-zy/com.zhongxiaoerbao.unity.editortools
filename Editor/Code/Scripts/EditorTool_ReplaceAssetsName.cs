/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using System.IO;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class EditorTool_ReplaceAssetsName : EditorWindow
    {
  [MenuItem("Tools/ZhongXiaoErBao/Rename/replace Selected Assets Name")]
        static void CreateWindow()
        {
            Rect theRect = new Rect(0, 0, 500, 500);
            EditorTool_ReplaceAssetsName window = 
                (EditorTool_ReplaceAssetsName)EditorWindow.GetWindowWithRect(
                    typeof(EditorTool_ReplaceAssetsName), theRect, false, "Replace Name");
            window.Show();
        }

        public string _text1;
        public string _text2;

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("/Replace Selected Assets Name.\nby zhongXiaoErBao", MessageType.Info);

            _text1 = EditorGUILayout.TextField("Find", _text1);
            _text2 = EditorGUILayout.TextField("Replace", _text2);

            GUILayout.Label("");

            if (GUILayout.Button("Done"))
            {
                foreach (var asset in Selection.objects)
                {
                    if (asset.GetType() == typeof(GameObject))
                    {
                        ReplaceWordInGameObject(asset);
                    }
                    else
                    {
                        ReplaceWordInObjects(asset);
                    }
                }
            }
        }

        private void ReplaceWordInObjects(Object asset)
        {
            string newName;

            if (string.IsNullOrEmpty(_text2))
            {
                newName = asset.name.Replace(_text1, "").Trim();
            }
            else
            {
                newName = asset.name.Replace(_text1, _text2).Trim();
            }
            string path = AssetDatabase.GetAssetPath(asset);

            string renameRes = AssetDatabase.RenameAsset(path, newName);
            if (renameRes != "")
            {
                Debug.LogError(renameRes);
            }
        }

        private void ReplaceWordInGameObject(Object gameObject)
        {
            string newName;

            if (string.IsNullOrEmpty(_text2))
            {
                newName = gameObject.name.Replace(_text1, "").Trim();
            }
            else
            {
                newName = gameObject.name.Replace(_text1, _text2).Trim();
            }

            gameObject.name = newName;
        }
    }
}