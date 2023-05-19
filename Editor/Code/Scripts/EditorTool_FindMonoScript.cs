/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

 using System;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEditor;

 namespace ZhongXiaoErBao.Unity.Editor.Tool
 {
     public class EditorTool_FindMonoScript : EditorWindow
     {
         [MenuItem("Tools/ZhongXiaoErBao/Find/Find MonoScript"),
          MenuItem("Assets/Tools/ZhongXiaoErBao/Find/Find MonoScript")]
         private static void CreateWindow()
         {
             Rect rect = new Rect(0, 0, 300, 300);
             EditorTool_FindMonoScript window = (EditorTool_FindMonoScript)EditorWindow.GetWindowWithRect(
                 typeof(EditorTool_FindMonoScript),
                 rect,
                 false,
                 "Find MonoScript");

             window.Show();
         }

         private void OnGUI()
         {
             EditorGUILayout.HelpBox(
                 ("Currently <" + Selection.objects.Length + "> gameobjects are selected"),
                 MessageType.Warning);
             
             EditorGUILayout.Space();
             _ScriptFold = EditorGUILayout.Foldout(_ScriptFold, "Find MonoScript");

             EditorGUILayout.Space();
             if (_ScriptFold ? true : false)
             {
                 GUILayout.Label("MonoScript Name: ");
                 scriptObj = (MonoScript)EditorGUILayout.ObjectField(scriptObj, typeof(MonoScript), true);
                 
                 EditorGUILayout.Space();
                 if (GUILayout.Button("Find"))
                 {
                     results.Clear();
                     loopCount = 0;

                     for (int i = 0; i < Selection.gameObjects.Length; i++)
                     {
                         roots = Selection.gameObjects[i].transform.GetComponentsInChildren<Transform>();

                         FindScript(roots[0], i);
                     }
                 }
             }
         }
         
         private bool _ScriptFold = true;

         int loopCount = 0;
         Transform[] roots = null;
         MonoScript scriptObj = null;
         List<Transform> results = new List<Transform>();

         void FindScript(Transform root, int i)
         {
             if (root != null && scriptObj != null)
             {
                 loopCount++;

                 if (root.GetComponent(scriptObj.GetClass()) != null)
                 {
                     Debug.Log("Found <" + scriptObj.name + "> on " + root.gameObject.name);
                 }

                 foreach (Transform t in root)
                 {
                     FindScript(t, i);
                 }
             }
         }

         private void OnInspectorUpdate()
         {
             this.Repaint();
         }
     }
 }