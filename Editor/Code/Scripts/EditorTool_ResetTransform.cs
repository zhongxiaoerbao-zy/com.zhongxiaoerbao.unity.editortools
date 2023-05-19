/* Writer:ZhongXiaoErBao
 * Time:2023.05
 */

using System.IO;
using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor.Tool
{
    public class EditorTool_ResetTransform : EditorWindow
    {
        #region Enum
        public enum PivotType
        {
            World, Self
        }

        public enum TransformType
        {
            Position, Rotation, Scale
        }
        #endregion

        [MenuItem("Tools/ZhongXiaoErBao/Reset/Reset Transform")]
        static void CreateWindow()
        {            
            // ScriptableWizard.DisplayWizard<Tools_ResetGameObject>("Reset GameObjects");

            Rect theRect = new Rect(0, 0, 500, 500);
            EditorTool_ResetTransform window = (EditorTool_ResetTransform)EditorWindow.GetWindowWithRect(
                typeof(EditorTool_ResetTransform), 
                theRect, false, 
                "Reset Transform");
            window.Show();
        }

        public PivotType m_PivotType = PivotType.Self;

        bool _childGroundEnable = false;

        bool _positionGroundEnable = false;
        bool _rotationGroundEnable = false;
        bool _scaleGroundEnable = false;

        float _positionTargetValue = 0f;
        float _rotationTargetValue = 0f;
        float _scaleTargetValue = 1;
        string _targetName;

        // 目标子物体
        GameObject _targetGameObject;

        private void OnGUI()
        {
            EditorGUILayout.HelpBox(("仅能修改Unity资源，对于外部导入的文件，无法操作！！！" +
              "\n" + "\n" + "如果未勾选“当前选择的子物体”,则Reset当前选择的物体；" +
              "\n" + "如果勾选“当前选择的子物体”，但目标子物体为空，则Reset当前选择下的所有子物体" +
              "\n" + "如果勾选“当前选择的子物体”，且目标子物体不为空，则Reset当前选择下的所有目标子物体。.\nby ZhongXiaoErBao"), MessageType.Warning);

            GUILayout.Label("");
            m_PivotType = (PivotType)EditorGUILayout.EnumPopup("变换类型:", m_PivotType);

            _childGroundEnable = EditorGUILayout.BeginToggleGroup("Reset子物体", _childGroundEnable);
            _targetGameObject = (GameObject)EditorGUILayout.ObjectField("目标子物体:", _targetGameObject, typeof(GameObject), true);
            EditorGUILayout.EndToggleGroup();

            GUILayout.Label("Position");
            _positionGroundEnable = EditorGUILayout.BeginToggleGroup("Change Position Value", _positionGroundEnable);
            _positionTargetValue = EditorGUILayout.FloatField("Target Value:", _positionTargetValue);
            EditorGUILayout.EndToggleGroup();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset X")) { ResetPositionX(); }
            if (GUILayout.Button("Reset Y")) { ResetPositionY(); }
            if (GUILayout.Button("Reset Z")) { ResetPositionZ(); }
            if (GUILayout.Button("Reset XYZ")) { ResetPositionAll(); }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Label("Rotation");
            _rotationGroundEnable = EditorGUILayout.BeginToggleGroup("Change Rotation Value", _rotationGroundEnable);
            _rotationTargetValue = EditorGUILayout.FloatField("Target Value:", _rotationTargetValue);
            EditorGUILayout.EndToggleGroup();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset X")) { ResetRotationX(); }
            if (GUILayout.Button("Reset Y")) { ResetRotationY(); }
            if (GUILayout.Button("Reset Z")) { ResetRotationZ(); }
            if (GUILayout.Button("Reset XYZ")) { ResetRotationAll(); }
            EditorGUILayout.EndToggleGroup();
            
            GUILayout.Label("Scale");
            _scaleGroundEnable = EditorGUILayout.BeginToggleGroup("Change Scale Value", _scaleGroundEnable);
            _scaleTargetValue = EditorGUILayout.FloatField("Target Value:", _scaleTargetValue);
            EditorGUILayout.EndToggleGroup();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset X")) { ResetScaleX(); }
            if (GUILayout.Button("Reset Y")) { ResetScaleY(); }
            if (GUILayout.Button("Reset Z")) { ResetScaleZ(); }
            if (GUILayout.Button("Reset XYZ")) { ResetScaleAll(); }
            EditorGUILayout.EndToggleGroup();
        }

        private void ResetPositionX()
        {
            if (_targetGameObject != null)
            {
                _targetName = _targetGameObject.name;
            }

            // 如果是Reset自身坐标
            if (m_PivotType == PivotType.Self)
            {
                if(_childGroundEnable == false)
                {
                    for (int i = 0; i < Selection.gameObjects.Length; i++)
                    {
                        Transform target = Selection.gameObjects[i].transform;
                        target.localPosition = new Vector3(_positionTargetValue, target.localPosition.y, target.localPosition.z);
                    }
                }
                else
                {
                    for (int i = 0; i < Selection.gameObjects.Length; i++)
                    {
                        // 如果child,count>0
                        if (Selection.gameObjects[i].transform.childCount > 0)
                        {
                            // 如果targetchild = null
                            if (_targetGameObject == null) 
                            {
                                for (int j = 0; j < Selection.gameObjects[i].transform.childCount; j++)
                                {
                                    Transform target = Selection.gameObjects[i].transform.GetChild(j);
                                    target.localPosition = new Vector3(_positionTargetValue, target.localPosition.y, target.localPosition.z);
                                }
                            }
                            // 如果targetchild !=null
                            else 
                            {
                                for (int j = 0; j < Selection.gameObjects[i].transform.childCount; j++)
                                {
                                    if(Selection.gameObjects[i].transform.GetChild(j).name == _targetName)
                                    {
                                        Transform target = Selection.gameObjects[i].transform.GetChild(j);
                                        target.localPosition = new Vector3(_positionTargetValue, target.localPosition.y, target.localPosition.z);
                                    }
                                }
                            }
                        }
                        // 如果child.count=0
                        else
                        {
                            this.ShowNotification(new GUIContent("当前选择没有子对象"));
                        }
                    }
                }
            }
           // 如果是Reset世界坐标
            else
            {
                if (_childGroundEnable == false)
                {
                    for (int i = 0; i < Selection.gameObjects.Length; i++)
                    {
                        Transform target = Selection.gameObjects[i].transform;
                        target.position = new Vector3(_positionTargetValue, target.position.y, target.position.z);
                    }
                }
                else
                {
                    for (int i = 0; i < Selection.gameObjects.Length; i++)
                    {
                        // 如果child,count>0
                        if (Selection.gameObjects[i].transform.childCount > 0)
                        {
                            // 如果targetchild = null
                            if (_targetGameObject == null)
                            {
                                for (int j = 0; j < Selection.gameObjects[i].transform.childCount; j++)
                                {
                                    Transform target = Selection.gameObjects[i].transform.GetChild(j);
                                    target.position = new Vector3(_positionTargetValue, target.position.y, target.position.z);
                                }
                            }
                            // 如果targetchild !=null
                            else
                            {
                                for (int j = 0; j < Selection.gameObjects[i].transform.childCount; j++)
                                {
                                    if (Selection.gameObjects[i].transform.GetChild(j).name == _targetName)
                                    {
                                        Transform target = Selection.gameObjects[i].transform.GetChild(j);
                                        target.position = new Vector3(_positionTargetValue, target.position.y, target.position.z);
                                    }
                                }
                            }
                        }
                        // 如果child.count=0
                        else
                        {
                            this.ShowNotification(new GUIContent("当前选择没有子对象"));
                        }
                    }
                }
            }
        }
        private void ResetPositionY() { }
        private void ResetPositionZ() { }
        private void ResetPositionAll() { }

        private void ResetRotationX() { }
        private void ResetRotationY() { }
        private void ResetRotationZ() { }
        private void ResetRotationAll() { }

        private void ResetScaleX() { }
        private void ResetScaleY() { }
        private void ResetScaleZ() { }
        private void ResetScaleAll() { }
    }
}