/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using Folder =ZhongXiaoErBao.Unity.Editor.Tool.Folder;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class Folder
    {
        public string name;
        public Folder[] child;
    }

    public class EditorTool_CreateFolders : EditorWindow
    {
        private Folder _folder = new Folder();
        private string _jsonFile = "<enter json file or browse>";

        #region Windows

        [MenuItem("Tools/ZhongXiaoErBao/Create/Create Folders"),
         MenuItem("Assets/Tools/ZhongXiaoErBao/Create/Create Folders")]
        private static void CreateWindow()
        {
            Rect rect = new Rect(0, 0, 500, 500);
            EditorTool_CreateFolders window = (EditorTool_CreateFolders)EditorWindow.GetWindowWithRect(
                typeof(EditorTool_CreateFolders),
                rect,
                false,
                "Create Folders");

            window.Show();
        }

        #endregion

        protected void OnGUI()
        {
            EditorGUILayout.HelpBox(("Create UnityAssets folders by json file.\nby ZhongXiaoErBao"),
                MessageType.Info);
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Browse..."))
            {
                // open json settings
                string dir = "Assets";
                int lastsep = _jsonFile.LastIndexOf('/');
                if (lastsep >= 0)
                {
                    dir = _jsonFile.Substring(0, lastsep);
                }

                string temp = EditorUtility.OpenFilePanel("Select file", dir, "json");
                if (temp.Length != 0)
                {
                    _jsonFile = temp;
                    this.Repaint();
                }
            }
            EditorGUILayout.TextField(_jsonFile);
            if (GUILayout.Button("Clean..."))
            {
                _jsonFile = "<enter json file or browse>";
                this.Repaint();
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Folders",GUILayout.Height(25)))
            {
                if (string.IsNullOrEmpty(_jsonFile))
                {
                    this.ShowNotification(new GUIContent("json file is not found!"));
                    return;
                }
                CreateFolderWithJsonFile(_jsonFile);
            }  
            if (GUILayout.Button("Close Window",GUILayout.Height(25)))
            {
                this.Close();
            }
            EditorGUILayout.EndHorizontal();
        }

        #region Create Folders

        private void CreateFolder(string path, string folderName)
        {
            if (!Directory.Exists(path + folderName))
            {
                Directory.CreateDirectory(path + folderName);
            }
        }

        private void CreateFolderWithJsonFile(string jsonPath)
        {
            // load json file
            using (StreamReader file = File.OpenText(jsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                _folder = (Folder)serializer.Deserialize(file, typeof(Folder));
            }

            // create folders
            for (int i = 0; i < _folder.child.Length; i++)
            {
                string assetsPath = string.Format("{0}/", "Assets");
                CreateFolder(assetsPath, _folder.child[i].name);

                for (int j = 0; j < _folder.child[i].child.Length; j++)
                {
                    string pathB = string.Format("{0}/{1}/",
                        "Assets",
                        _folder.child[i].name);
                    CreateFolder(pathB, _folder.child[i].child[j].name);

                    for (int k = 0; k < _folder.child[i].child[j].child.Length; k++)
                    {
                        string pathC = string.Format(
                            "{0}/{1}/{2}/",
                            "Assets",
                            _folder.child[i].name,
                            _folder.child[i].child[j].name);
                        CreateFolder(pathC, _folder.child[i].child[j].child[k].name);

                        for (int l = 0; l < _folder.child[i].child[j].child[k].child.Length; l++)
                        {
                            string pathD = string.Format(
                                "{0}/{1}/{2}/{3}/",
                                "Assets",
                                _folder.child[i].name,
                                _folder.child[i].child[j].name,
                                _folder.child[i].child[j].child[k].name);
                            CreateFolder(pathD, _folder.child[i].child[j].child[k].child[l].name);
                        }
                    }
                }
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}