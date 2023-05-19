using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace ZhongXiaoErBao.Unity.Editor.Test
{
    public class UnityAssets
    {
        // 更改文件编码格式
        private static void MonoScripts(Encoding encoding)
        {
            UnityEngine.Object seleObject = Selection.activeObject;

            string path = AssetDatabase.GetAssetPath(seleObject);

            string content = File.ReadAllText(path, Encoding.Default);
            File.WriteAllText(path, content, encoding);

            Debug.Log(encoding);
        }
        [MenuItem("EditorTools(zxeb)/Test/File/UTF8")]
        private static void FileToUTF8()
        {
            var encoding = new UTF8Encoding(false);
            MonoScripts(encoding);
        }
        [MenuItem("EditorTools(zxeb)/Test/File/ASCII")]
        private static void FileToASCII()
        {
            MonoScripts(Encoding.ASCII);
        }
        [MenuItem("EditorTools(zxeb)/Test/File/UTF32")]
        private static void FileToUTF32()
        {
            MonoScripts(Encoding.UTF32);
        }
    }
}