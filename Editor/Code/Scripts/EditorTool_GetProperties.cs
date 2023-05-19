/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using System.IO;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class EditorTool_GetProperties : EditorWindow
    {
        [MenuItem("Tools/ZhongXiaoErBao/Get/Get Selection Properties"),
         MenuItem("Assets/Tools/ZhongXiaoErBao/Get/Get Selection Properties")]
        private static void CreateWindow()
        {
            Rect rect = new Rect(0, 0, 500, 500);
            EditorTool_GetProperties window = (EditorTool_GetProperties)EditorWindow.GetWindowWithRect(
                typeof(EditorTool_GetProperties),
                rect,
                false,
                "Get Properties");

            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox(
                ("Get Selection Properties.\nby ZhongXiaoErBao"),
                MessageType.Info);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox(
                ("Current Selected: " + Selection.objects[0].name),
                MessageType.Warning);
            if (GUILayout.Button("Get Type", GUILayout.Height(35)))
            {
                var selectObject = Selection.activeObject;

                if (selectObject != null)
                {
                    this.ShowNotification(new GUIContent(selectObject.GetType().ToString()));
                    Debug.Log(selectObject.GetType().ToString());
                }
                else
                {
                    this.ShowNotification(new GUIContent("Selected is Null"));
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox(
                ("Currently <" + Selection.objects.Length + "> gameobjects are selected"),
                MessageType.Info);
            if (GUILayout.Button("Get Selected Number", GUILayout.Height(35)))
            {
                Debug.Log("Currently <" + Selection.objects.Length + "> gameobjects are selected");
                this.ShowNotification(new GUIContent(Selection.objects.Length.ToString()));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnInspectorUpdate()
        {
            this.Repaint();
        }
    }
}