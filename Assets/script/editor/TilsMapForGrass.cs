using System;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace script.editor
{
    public class TilsMapForGrass : EditorWindow
    {
        private GameObject parent;
        private GameObject prefab;
        private int numX = 1;
        private int numY = 1;
        private int numZ = 1;
        private float intervalX = 1;
        private float intervalY = 1;
        private float intervalZ = 1;

        [MenuItem("Custom/Create Grass")]
        static void Init()
        {
            var window = GetWindow<TilsMapForGrass>(true, "Create Grass");
            var pos = window.position;
            pos.width = 250;
            pos.height = 300;
            window.position = pos;
        }

        void OnEnable()
        {
            if (Selection.gameObjects.Length > 0) parent = Selection.gameObjects[0];
        }

        void OnSelectionChange()
        {
            if (Selection.gameObjects.Length > 0) prefab = Selection.gameObjects[0];
            Repaint();
        }

        void OnGUI()
        {
            try
            {
                parent = EditorGUILayout.ObjectField("Parent", parent, typeof(GameObject), true) as GameObject;
                prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true) as GameObject;

                GUILayout.Label("X : ", EditorStyles.boldLabel);
                numX = int.Parse(EditorGUILayout.TextField("num", numX.ToString()));
                intervalX = int.Parse(EditorGUILayout.TextField("interval",
                    intervalX.ToString(CultureInfo.CurrentCulture)));

                GUILayout.Label("Y : ", EditorStyles.boldLabel);
                numY = int.Parse(EditorGUILayout.TextField("num", numY.ToString()));
                intervalY = int.Parse(EditorGUILayout.TextField("interval",
                    intervalY.ToString(CultureInfo.CurrentCulture)));

                GUILayout.Label("Z : ", EditorStyles.boldLabel);
                numZ = int.Parse(EditorGUILayout.TextField("num", numZ.ToString()));
                intervalZ = int.Parse(EditorGUILayout.TextField("interval",
                    intervalZ.ToString(CultureInfo.CurrentCulture)));

                GUILayout.Label("", EditorStyles.boldLabel);
                if (GUILayout.Button("Create")) Create();
            }
            catch (FormatException)
            {
            }
        }

        private void Create()
        {
            if (prefab == null) return;

            var count = 0;
            Vector3 pos;

            pos.x = -(numX - 1) * intervalX / 2;
            foreach (var unusedX in Enumerable.Range(0, numX))
            {
                pos.y = -(numY - 1) * intervalY / 2;
                foreach (var unusedY in Enumerable.Range(0, numY))
                {
                    pos.z = -(numZ - 1) * intervalZ / 2;
                    foreach (var unusedZ in Enumerable.Range(0, numZ))
                    {
                        var obj = Instantiate(prefab, pos, Quaternion.identity);
                        obj.name = prefab.name + count++;
                        if (parent) obj.transform.parent = parent.transform;
                        Undo.RegisterCreatedObjectUndo(obj, "Create Grass");

                        pos.z += intervalZ;
                    }
                    pos.y += intervalY;
                }
                pos.x += intervalX;
            }
        }
    }
}