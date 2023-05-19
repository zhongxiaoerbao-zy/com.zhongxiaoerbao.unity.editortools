/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using System.IO;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class EditorTool_RemoveMissingScript : EditorWindow
    {
        [MenuItem("Tools/ZhongXiaoErBao/Remove/remove missing script with Selected")]
        private static void SelectedRemoveMissingScript() 
        {
            foreach (var asset in Selection.gameObjects)
            {
                RemoveMissingScript(asset);
            }
            AssetDatabase.Refresh();
            Debug.Log("removed missing script with Selected");
        }

        [MenuItem("Tools/ZhongXiaoErBao/Remove/remove missing script with Selected and Childs")]
        private static void SelectedAndChildRemoveMissingScript()
        {
            foreach (var asset in Selection.gameObjects)
            {
                // get children
                Transform[] children = asset.GetComponentsInChildren<Transform>(true);
                foreach (var child in children)
                {
                    RemoveMissingScript(child.gameObject);
                }
            }
            AssetDatabase.Refresh();
            Debug.Log("removed missing script with Selected and Childs");
        }

        private static void RemoveMissingScript(GameObject go)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }
    }
}