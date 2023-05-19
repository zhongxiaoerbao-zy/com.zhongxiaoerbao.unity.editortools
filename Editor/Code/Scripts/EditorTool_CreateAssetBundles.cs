/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using System.IO;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class EditorTool_CreateAssetBundles : EditorWindow
    {
        [MenuItem("Tools/ZhongXiaoErBao/Create/Create AssetBundles")]
        static void CreateWindow()
        {
            Rect theRect = new Rect(0, 0, 500, 500);
            EditorTool_CreateAssetBundles window = (EditorTool_CreateAssetBundles)EditorWindow.GetWindowWithRect(
                    typeof(EditorTool_CreateAssetBundles), 
                    theRect, 
                    false, 
                    "Create AssetBundles");
            window.Show();
        }

        private bool _isNewName = false;

        private string _AssetBundleName;
        private string _AssetBundleVariant;


        private void OnGUI()
        {
            EditorGUILayout.HelpBox(("Create AssetBundles.\nby ZhongXiaoErBao"), MessageType.Info);

            _isNewName = EditorGUILayout.BeginToggleGroup("new name?", _isNewName);
            _AssetBundleName = EditorGUILayout.TextField("New Name", _AssetBundleName);
            _AssetBundleVariant = EditorGUILayout.TextField("Suffix", _AssetBundleVariant);

            if (GUILayout.Button("Set New Name"))
            {
                SetAssetBundle();
            }
            EditorGUILayout.EndToggleGroup();
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Remove Unused Name"))
            {
                RemoveUnusedName();
            }

            EditorGUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Build Target -- Android");
            if (GUILayout.Button("To Android"))
            {
                BuildToAndroid();
                this.ShowNotification(new GUIContent("build to Android"));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Build Target -- Linux64");
            if (GUILayout.Button("To Linux64"))
            {
                BuildToLinux64();
                this.ShowNotification(new GUIContent("build to Linux64"));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Build Target -- Windows64");
            if (GUILayout.Button("To Windows64"))
            {
                BuildToWindows64();
                this.ShowNotification(new GUIContent("build to Windows64"));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Build Target -- UWP");
            if (GUILayout.Button("To UWP"))
            {
                BuildToUWP();
                this.ShowNotification(new GUIContent("build to UWP"));
            }
            EditorGUILayout.EndHorizontal();
        }

        #region Set AssetBundle

        private void GetPrefabPath(Object obj, string name, string variant)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            var build = AssetImporter.GetAtPath(path);
            build.assetBundleName = name;
            build.assetBundleVariant = variant;
        }

        private void SetAssetBundle()
        {
            GetPrefabPath(Selection.activeObject, _AssetBundleName, _AssetBundleVariant);
        }

        private void RemoveUnusedName()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        #endregion

        #region Build AssetBundle

        private void BuildToAndroid()
        {
            string dir = "AssetBundles Android";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        private void BuildToLinux64()
        {
            string dir = "AssetBundles Linux64";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);
        }

        private void BuildToWindows64()
        {
            string dir = "AssetBundles Windows64";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        private void BuildToUWP()
        {
            string dir = "AssetBundles UWP";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.WSAPlayer);
        }

        #endregion
    }
}
