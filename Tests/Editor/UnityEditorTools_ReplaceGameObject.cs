using UnityEngine;
using UnityEditor;

namespace ZhongXiaoErBao.Unity.Editor
{
    public class UnityEditorTools_ReplaceGameObject : EditorWindow
    {
        [MenuItem("EditorTools(zxeb)/Assets/�滻GameObject(s)")]
        static void CreateWindow()
        {
            // ScriptableWizard.DisplayWizard<Tools_ReplaceGameObject>("Replace GameObject");

            Rect theRect = new Rect(0, 0, 500, 500);
            UnityEditorTools_ReplaceGameObject window = (UnityEditorTools_ReplaceGameObject)EditorWindow.GetWindowWithRect(typeof(UnityEditorTools_ReplaceGameObject), theRect, false, "�滻GameObject");
            window.Show();
        }

        GameObject _oldGameObject;
        GameObject _newGameObject;

        Vector3 _positionD_value = new Vector3(0, 0, 0);
        Vector3 _rotationD_value = new Vector3(0, 0, 0);
        Vector3 _scaleD_value = new Vector3(1, 1, 1);
        // �Ƿ��滻�Ӷ���
        bool _isReplaceChild = false;
        // �Ƿ��޸Ĳ�ֵ
        bool _isD_value = false;
        string _oldGameObjectPath;

        private void OnGUI()
        {
            EditorGUILayout.HelpBox(("�����޸�Unity��Դ�������ⲿ������ļ����޷�����������" +
              "\n" + "\n" + "���δ��ѡ���滻�Ӷ���,���滻��ǰѡ������壻" +
              "\n" + "�����ѡ���滻�Ӷ��󡱣���Ŀ���Ӷ���Ϊ�գ����滻��ǰѡ���µ������Ӷ���" +
              "\n" + "�����ѡ���滻�Ӷ��󡱣���Ŀ���Ӷ���Ϊ�գ������õ�ǰѡ���µ�����Ŀ���Ӷ���.\nby ZhongXiaoErBao"), MessageType.Warning);

            GUILayout.Label("");
            _isReplaceChild = EditorGUILayout.BeginToggleGroup("�滻�Ӷ���", _isReplaceChild);
            _oldGameObject = (GameObject)EditorGUILayout.ObjectField("���滻���Ӷ���:", _oldGameObject, typeof(GameObject), true);
            EditorGUILayout.EndToggleGroup();

            _newGameObject = (GameObject)EditorGUILayout.ObjectField("�滻����:", _newGameObject, typeof(GameObject), true);

            _isD_value = EditorGUILayout.BeginToggleGroup("�Ƿ��޸Ĳ�ֵ", _isD_value);
            _positionD_value = EditorGUILayout.Vector3Field("Position��ֵ:", _positionD_value);
            _rotationD_value = EditorGUILayout.Vector3Field("Rotation��ֵ:", _rotationD_value);
            _scaleD_value = EditorGUILayout.Vector3Field("Scale��ֵ:", _scaleD_value);
            EditorGUILayout.EndToggleGroup();

            GUILayout.Label("");
            if (GUILayout.Button("�滻")) { ReplaceGameObject(); }
        }

        private void ReplaceGameObject()
        {
            if (_newGameObject == null)
            {
                this.ShowNotification(new GUIContent("ȱ���滻����"));
                return;
            }

            // ������滻�Ӷ���
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

        // �滻����
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

        // �滻�����Ӷ���
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
                this.ShowNotification(new GUIContent("��ǰѡ��û���Ӷ���"));
            }
        }

        // �滻ָ���Ӷ���
        private void ReplaceAssignChilds()
        {
            if (Selection.activeGameObject.transform.childCount > 0)
            {

            }
            else
            {
                this.ShowNotification(new GUIContent("��ǰѡ��û���Ӷ���"));
            }

        }
    }
}