/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using System;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class EditorTool_RenameAssets : EditorWindow
    {
        [MenuItem("Tools/ZhongXiaoErBao/Rename/rename Selected Assets Name")]
        static void CreateWindow()
        {
            Rect theRect = new Rect(0, 0, 500, 500);
            EditorTool_RenameAssets window =
                (EditorTool_RenameAssets)EditorWindow.GetWindowWithRect(
                    typeof(EditorTool_RenameAssets), 
                    theRect, 
                    false, 
                    "Rename");
            window.Show();
        }

        private string _type = "";
        private string _name = "";
        private int _id = 1;
        private string _variation = "";

        private string _finalName = String.Empty;
    
        private void OnInspectorUpdate()
        {
            _finalName = _name + "_" + _id.ToString("d3");

            if(!string .IsNullOrWhiteSpace(_type)){ _finalName = _type.Trim() + "_" + _finalName; }
            if (!string .IsNullOrWhiteSpace(_variation)) { _finalName = _finalName + "_" + _variation.Trim(); }
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("Rename Selected Assets Name\nby zhongXiaoErBao", MessageType.Info);
            EditorGUILayout.Space();
            
            _type = EditorGUILayout.TextField("Prefix (前缀)", _type);
            EditorGUILayout.Space();
            
            _name = EditorGUILayout.TextField("Name (名称)", _name);
            EditorGUILayout.Space();
            
            _id = EditorGUILayout.IntField("Start ID (起始ID)", _id);
            EditorGUILayout.Space();
            
            _variation = EditorGUILayout.TextField("Suffix (后缀)", _variation);
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Final Name (最终名称): ", _finalName);
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Rename"))
            {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    EditorGUILayout.HelpBox("Name is null!",MessageType.Error);
                    this.ShowNotification(new GUIContent("Name is null!"));
                    return;
                }
                
                RenameFile();
                _id = 1;
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Reset"))
            {
                _type = string.Empty;
                _name = string.Empty;
                _id = 1;
                _variation = string.Empty;
            }
        }

        private void RenameFile()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                return;
            }

            foreach (var asset in Selection.objects)
            {
                Debug.Log(asset.GetType());

                string path = AssetDatabase.GetAssetPath(asset);
                string newName = _name + "_" + _id.ToString("d3");

                if(!string .IsNullOrWhiteSpace(_type)){ newName = _type.Trim() + "_" + newName; }
                if (!string .IsNullOrWhiteSpace(_variation)) { newName = newName + "_" + _variation.Trim(); }

                if (asset.GetType() == typeof(GameObject) && !AssetDatabase.Contains(asset))
                {
                    asset.name = newName;
                }
                else if (AssetDatabase.Contains(asset))
                {
                    string name = asset.name;
                    string newPath = path.Replace(name, newName);

                    System.IO.FileInfo fi = new System.IO.FileInfo(path);
                    if (fi.Exists)
                    {
                        fi.MoveTo(newPath);
                    }
                }
                _id += 1;
            }
        }
        
    }
}