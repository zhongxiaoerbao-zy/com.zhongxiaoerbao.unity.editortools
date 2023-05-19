using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor
{
    public class UnityEditorTools_ReplaceGameObject : EditorWindow
    {
        [MenuItem("EditorTools(zxeb)/Assets/替换GameObject(s)")]
        static void CreateWindow()
        {
            // ScriptableWizard.DisplayWizard<Tools_ReplaceGameObject>("Replace GameObject");

            Rect theRect = new Rect(0, 0, 500, 500);
            UnityEditorTools_ReplaceGameObject window = (UnityEditorTools_ReplaceGameObject)EditorWindow.GetWindowWithRect(typeof(UnityEditorTools_ReplaceGameObject), theRect, false, "替换GameObject");
            window.Show();
        }

        GameObject _oldGameObject;
        GameObject _newGameObject;

        Vector3 _positionD_value = new Vector3(0, 0, 0);
        Vector3 _rotationD_value = new Vector3(0, 0, 0);
        Vector3 _scaleD_value = new Vector3(1, 1, 1);
        // 是否替换子对象
        bool _isReplaceChild = false;
        // 是否修改差值
        bool _isD_value = false;
        string _oldGameObjectPath;

        private void OnGUI()
        {
            EditorGUILayout.HelpBox(("仅能修改Unity资源，对于外部导入的文件，无法操作！！！" +
              "\n" + "\n" + "如果未勾选“替换子对象”,则替换当前选择的物体；" +
              "\n" + "如果勾选“替换子对象”，但目标子对象为空，则替换当前选择下的所有子对象" +
              "\n" + "如果勾选“替换子对象”，且目标子对象不为空，则重置当前选择下的所有目标子对象。.\nby ZhongXiaoErBao"), MessageType.Warning);

            GUILayout.Label("");
            _isReplaceChild = EditorGUILayout.BeginToggleGroup("替换子对象", _isReplaceChild);
            _oldGameObject = (GameObject)EditorGUILayout.ObjectField("被替换的子对象:", _oldGameObject, typeof(GameObject), true);
            EditorGUILayout.EndToggleGroup();

            _newGameObject = (GameObject)EditorGUILayout.ObjectField("替换对象:", _newGameObject, typeof(GameObject), true);

            _isD_value = EditorGUILayout.BeginToggleGroup("是否修改差值", _isD_value);
            _positionD_value = EditorGUILayout.Vector3Field("Position差值:", _positionD_value);
            _rotationD_value = EditorGUILayout.Vector3Field("Rotation差值:", _rotationD_value);
            _scaleD_value = EditorGUILayout.Vector3Field("Scale差值:", _scaleD_value);
            EditorGUILayout.EndToggleGroup();

            GUILayout.Label("");
            if (GUILayout.Button("替换")) { ReplaceGameObject(); }
        }

        private void ReplaceGameObject()
        {
            if (_newGameObject == null)
            {
                this.ShowNotification(new GUIContent("缺少替换对象"));
                return;
            }

            // 如果不替换子对象
            if (_isReplaceChild == false)
            {
                ReplaceSelf();
            }
            else
            {
                if (_oldGameObject == null)
                {
                    ReplaceAllChilds();
                }
                else
                {
                    ReplaceAssignChilds();
                }
            }
        }

        // 替换自身
        private void ReplaceSelf()
        {
            GameObject go = Selection.activeGameObject;

            Vector3 localPosition = new Vector3(go.transform.localPosition.x - _positionD_value.x, go.transform.localPosition.y - _positionD_value.y, go.transform.localPosition.z - _positionD_value.z);
            Vector3 localEulerAngles = new Vector3(go.transform.localEulerAngles.x - _rotationD_value.x, go.transform.localEulerAngles.y - _rotationD_value.y, go.transform.localEulerAngles.z - _rotationD_value.z);
            Vector3 localScale = new Vector3(go.transform.localScale.x - _scaleD_value.x + 1, go.transform.localScale.y - _scaleD_value.y + 1, go.transform.localScale.z - _scaleD_value.z + 1);

            GameObject newGo = (GameObject)Instantiate(_newGameObject);
            newGo.transform.SetParent(go.transform.parent);
            newGo.transform.localPosition = localPosition;
            newGo.transform.localEulerAngles = localEulerAngles;
            newGo.transform.localScale = localScale;

            DestroyImmediate(go);
        }

        // 替换所有子对象
        private void ReplaceAllChilds()
        {
            if (Selection.activeGameObject.transform.childCount > 0)
            {
                for (int i = 0; i < Selection.activeGameObject.transform.childCount; i++)
                {
                    Transform go = Selection.activeGameObject.transform.GetChild(i);

                    Vector3 localPosition = new Vector3(go.localPosition.x - _positionD_value.x, go.localPosition.y - _positionD_value.y, go.localPosition.z - _positionD_value.z);
                    Vector3 localEulerAngles = new Vector3(go.localEulerAngles.x - _rotationD_value.x, go.localEulerAngles.y - _rotationD_value.y, go.localEulerAngles.z - _rotationD_value.z);
                    Vector3 localScale = new Vector3(go.localScale.x - _scaleD_value.x + 1, go.localScale.y - _scaleD_value.y + 1, go.localScale.z - _scaleD_value.z + 1);

                    GameObject newGo = (GameObject)Instantiate(_newGameObject);
                    newGo.transform.SetParent(go.parent);
                    newGo.transform.localPosition = localPosition;
                    newGo.transform.localEulerAngles = localEulerAngles;
                    newGo.transform.localScale = localScale;

                    DestroyImmediate(go);
                }
            }
            else
            {
                this.ShowNotification(new GUIContent("当前选择没有子对象"));
            }
        }

        // 替换指定子对象
        private void ReplaceAssignChilds()
        {
            if (Selection.activeGameObject.transform.childCount > 0)
            {

            }
            else
            {
                this.ShowNotification(new GUIContent("当前选择没有子对象"));
            }

        }
    }
}