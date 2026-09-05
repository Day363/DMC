using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(WindowGenerator))]
public class WindowGeneratorEditor : Editor
{
    private WindowGenerator generator;

    private int selectedArea = -1;


    // =========================================================
    // ENABLE
    // =========================================================

    private void OnEnable()
    {
        generator =
            (WindowGenerator)target;


        SceneView.duringSceneGui +=
            OnSceneGUI;
    }


    // =========================================================
    // DISABLE
    // =========================================================

    private void OnDisable()
    {
        SceneView.duringSceneGui -=
            OnSceneGUI;
    }


    // =========================================================
    // INSPECTOR
    // =========================================================

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.Space(5);


        EditorGUILayout.LabelField(
            "WINDOW GENERATOR",
            EditorStyles.boldLabel
        );


        EditorGUILayout.Space(5);


        // =====================================================
        // WINDOW PREFAB
        // =====================================================

        EditorGUI.BeginChangeCheck();


        generator.windowPrefab =
            (GameObject)EditorGUILayout.ObjectField(
                "Window Prefab",
                generator.windowPrefab,
                typeof(GameObject),
                false
            );


        if (
            EditorGUI.EndChangeCheck()
        )
        {
            Undo.RecordObject(
                generator,
                "Change Window Prefab"
            );


            EditorUtility.SetDirty(
                generator
            );
        }


        EditorGUILayout.Space(10);


        // =====================================================
        // AREAS
        // =====================================================

        EditorGUILayout.LabelField(
            $"POLYGON AREAS ({generator.areas.Count})",
            EditorStyles.boldLabel
        );


        EditorGUILayout.Space(5);


        for (
            int i = 0;
            i < generator.areas.Count;
            i++
        )
        {
            DrawArea(i);
        }


        EditorGUILayout.Space(5);


        // =====================================================
        // ADD AREA
        // =====================================================

        if (
            GUILayout.Button(
                "+ ADD POLYGON AREA",
                GUILayout.Height(32)
            )
        )
        {
            Undo.RecordObject(
                generator,
                "Add Polygon Area"
            );


            generator.AddArea();


            selectedArea =
                generator.areas.Count - 1;


            EditorUtility.SetDirty(
                generator
            );


            SceneView.RepaintAll();
        }


        EditorGUILayout.Space(10);


        // =====================================================
        // GENERATE
        // =====================================================

        GUI.backgroundColor =
            new Color(
                0.3f,
                0.9f,
                1f
            );


        if (
            GUILayout.Button(
                "GENERATE ALL WINDOWS",
                GUILayout.Height(40)
            )
        )
        {
            Undo.RegisterFullObjectHierarchyUndo(
                generator.gameObject,
                "Generate Windows"
            );


            generator.GenerateAll();


            EditorUtility.SetDirty(
                generator
            );
        }


        GUI.backgroundColor =
            Color.white;


        // =====================================================
        // CLEAR
        // =====================================================

        if (
            GUILayout.Button(
                "CLEAR ALL WINDOWS",
                GUILayout.Height(28)
            )
        )
        {
            Undo.RegisterFullObjectHierarchyUndo(
                generator.gameObject,
                "Clear Windows"
            );


            generator.ClearAll();


            EditorUtility.SetDirty(
                generator
            );
        }


        serializedObject.ApplyModifiedProperties();
    }


    // =========================================================
    // AREA
    // =========================================================

    private void DrawArea(
        int index
    )
    {
        if (
            index < 0 ||
            index >= generator.areas.Count
        )
        {
            return;
        }


        WindowGenerator.WindowArea area =
            generator.areas[index];


        if (area == null)
        {
            return;
        }


        bool selected =
            selectedArea == index;


        EditorGUILayout.BeginVertical(
            EditorStyles.helpBox
        );


        // =====================================================
        // HEADER
        // =====================================================

        EditorGUILayout.BeginHorizontal();


        string title =
            selected
                ? "▼ " + area.name
                : "▶ " + area.name;


        if (
            GUILayout.Button(
                title,
                EditorStyles.boldLabel
            )
        )
        {
            selectedArea =
                selected
                    ? -1
                    : index;


            SceneView.RepaintAll();
        }


        if (
            GUILayout.Button(
                "X",
                GUILayout.Width(25)
            )
        )
        {
            Undo.RecordObject(
                generator,
                "Remove Polygon Area"
            );


            generator.RemoveArea(
                index
            );


            if (
                selectedArea == index
            )
            {
                selectedArea = -1;
            }
            else if (
                selectedArea > index
            )
            {
                selectedArea--;
            }


            EditorUtility.SetDirty(
                generator
            );


            SceneView.RepaintAll();


            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();


            return;
        }


        EditorGUILayout.EndHorizontal();


        // =====================================================
        // SETTINGS
        // =====================================================

        if (selected)
        {
            EditorGUI.indentLevel++;


            // -------------------------------------------------
            // BASIC
            // -------------------------------------------------

            EditorGUILayout.LabelField(
                "Basic",
                EditorStyles.boldLabel
            );


            EditorGUI.BeginChangeCheck();


            area.name =
                EditorGUILayout.TextField(
                    "Name",
                    area.name
                );


            area.density =
                EditorGUILayout.Slider(
                    "Density",
                    area.density,
                    0f,
                    1f
                );


            area.spacing =
                EditorGUILayout.Vector2Field(
                    "Spacing",
                    area.spacing
                );


            area.randomSeed =
                EditorGUILayout.IntField(
                    "Random Seed",
                    area.randomSeed
                );


            if (
                EditorGUI.EndChangeCheck()
            )
            {
                Undo.RecordObject(
                    generator,
                    "Edit Polygon Area"
                );


                EditorUtility.SetDirty(
                    generator
                );
            }


            EditorGUILayout.Space(10);


            // -------------------------------------------------
            // RANDOM SCALE
            // -------------------------------------------------

            EditorGUILayout.LabelField(
                "Random Scale",
                EditorStyles.boldLabel
            );


            EditorGUI.BeginChangeCheck();


            area.randomScale =
                EditorGUILayout.Toggle(
                    "Enable Random Scale",
                    area.randomScale
                );


            if (
                area.randomScale
            )
            {
                EditorGUILayout.Space(3);


                // X
                EditorGUILayout.LabelField(
                    "X Scale Range",
                    EditorStyles.miniBoldLabel
                );


                area.scaleRangeX =
                    EditorGUILayout.Vector2Field(
                        "Min / Max",
                        area.scaleRangeX
                    );


                // Y
                EditorGUILayout.LabelField(
                    "Y Scale Range",
                    EditorStyles.miniBoldLabel
                );


                area.scaleRangeY =
                    EditorGUILayout.Vector2Field(
                        "Min / Max",
                        area.scaleRangeY
                    );


                // 음수 방지
                area.scaleRangeX.x =
                    Mathf.Max(
                        0f,
                        area.scaleRangeX.x
                    );


                area.scaleRangeX.y =
                    Mathf.Max(
                        0f,
                        area.scaleRangeX.y
                    );


                area.scaleRangeY.x =
                    Mathf.Max(
                        0f,
                        area.scaleRangeY.x
                    );


                area.scaleRangeY.y =
                    Mathf.Max(
                        0f,
                        area.scaleRangeY.y
                    );


                EditorGUILayout.Space(3);


                EditorGUILayout.HelpBox(
                    "X와 Y가 독립적으로 랜덤 적용됩니다.\n\n" +
                    "예:\n" +
                    "X = 0.8 ~ 1.2\n" +
                    "Y = 0.5 ~ 1.5",
                    MessageType.None
                );
            }


            if (
                EditorGUI.EndChangeCheck()
            )
            {
                Undo.RecordObject(
                    generator,
                    "Edit Random Scale"
                );


                EditorUtility.SetDirty(
                    generator
                );
            }


            EditorGUILayout.Space(10);


            // -------------------------------------------------
            // RANDOM COLOR
            // -------------------------------------------------

            EditorGUILayout.LabelField(
                "Random Color",
                EditorStyles.boldLabel
            );


            EditorGUI.BeginChangeCheck();


            area.randomColor =
                EditorGUILayout.Toggle(
                    "Enable Random Color",
                    area.randomColor
                );


            if (
                area.randomColor
            )
            {
                EditorGUILayout.Space(3);


                area.colorMin =
                    EditorGUILayout.ColorField(
                        "Min Color",
                        area.colorMin
                    );


                area.colorMax =
                    EditorGUILayout.ColorField(
                        "Max Color",
                        area.colorMax
                    );


                EditorGUILayout.Space(5);


                EditorGUILayout.LabelField(
                    "Color Preview"
                );


                Rect previewRect =
                    GUILayoutUtility.GetRect(
                        0,
                        25
                    );


                EditorGUI.DrawRect(
                    previewRect,
                    Color.Lerp(
                        area.colorMin,
                        area.colorMax,
                        0.5f
                    )
                );


                EditorGUILayout.Space(3);


                EditorGUILayout.HelpBox(
                    "Min Color와 Max Color 사이에서\n" +
                    "각 창문의 색상을 랜덤으로 선택합니다.",
                    MessageType.None
                );
            }


            if (
                EditorGUI.EndChangeCheck()
            )
            {
                Undo.RecordObject(
                    generator,
                    "Edit Random Color"
                );


                EditorUtility.SetDirty(
                    generator
                );
            }


            EditorGUILayout.Space(10);


            // -------------------------------------------------
            // POLYGON
            // -------------------------------------------------

            EditorGUILayout.LabelField(
                $"Polygon Points: {area.points.Count}",
                EditorStyles.boldLabel
            );


            EditorGUILayout.HelpBox(
                "Scene View 조작\n\n" +
                "• 점 드래그 : 꼭짓점 이동\n" +
                "• Ctrl + 클릭 : 점 추가\n" +
                "• Shift + 클릭 : 점 삭제",
                MessageType.Info
            );


            EditorGUI.indentLevel--;
        }


        EditorGUILayout.EndVertical();


        EditorGUILayout.Space(4);
    }


    // =========================================================
    // SCENE GUI
    // =========================================================

    private void OnSceneGUI(
        SceneView sceneView
    )
    {
        if (
            generator == null
        )
        {
            return;
        }


        Transform root =
            generator.transform;


        Event e =
            Event.current;


        // =====================================================
        // DRAW ALL POLYGONS
        // =====================================================

        for (
            int i = 0;
            i < generator.areas.Count;
            i++
        )
        {
            DrawPolygon(
                generator.areas[i],
                i,
                root
            );
        }


        // =====================================================
        // INPUT
        // =====================================================

        HandleSceneInput(
            e,
            root
        );


        DrawSceneInstructions();


        if (GUI.changed)
        {
            SceneView.RepaintAll();
        }
    }


    // =========================================================
    // DRAW POLYGON
    // =========================================================

    private void DrawPolygon(
        WindowGenerator.WindowArea area,
        int areaIndex,
        Transform root
    )
    {
        if (
            area == null ||
            area.points == null ||
            area.points.Count < 2
        )
        {
            return;
        }


        bool selected =
            selectedArea == areaIndex;


        Handles.color =
            selected
                ? Color.yellow
                : Color.cyan;


        // =====================================================
        // LINES
        // =====================================================

        for (
            int i = 0;
            i < area.points.Count;
            i++
        )
        {
            Vector2 next =
                area.points[
                    (i + 1) %
                    area.points.Count
                ];


            Vector3 a =
                root.TransformPoint(
                    new Vector3(
                        area.points[i].x,
                        area.points[i].y,
                        0f
                    )
                );


            Vector3 b =
                root.TransformPoint(
                    new Vector3(
                        next.x,
                        next.y,
                        0f
                    )
                );


            Handles.DrawLine(
                a,
                b,
                selected
                    ? 4f
                    : 2f
            );
        }


        // =====================================================
        // CENTER LABEL
        // =====================================================

        Vector2 center2D =
            CalculateCenter(
                area.points
            );


        Vector3 center =
            root.TransformPoint(
                new Vector3(
                    center2D.x,
                    center2D.y,
                    0f
                )
            );


        Handles.Label(
            center,
            area.name
        );


        // =====================================================
        // POINTS
        // =====================================================

        for (
            int i = 0;
            i < area.points.Count;
            i++
        )
        {
            DrawPointHandle(
                area,
                i,
                root,
                selected
            );
        }
    }


    // =========================================================
    // POINT HANDLE
    // =========================================================

    private void DrawPointHandle(
        WindowGenerator.WindowArea area,
        int pointIndex,
        Transform root,
        bool selected
    )
    {
        Vector3 world =
            root.TransformPoint(
                new Vector3(
                    area.points[pointIndex].x,
                    area.points[pointIndex].y,
                    0f
                )
            );


        float size =
            HandleUtility.GetHandleSize(
                world
            ) * 0.10f;


        Handles.color =
            selected
                ? Color.yellow
                : Color.cyan;


        EditorGUI.BeginChangeCheck();


        var fmh_879_17_639239949171341285 = Quaternion.identity; Vector3 newWorld =
            Handles.FreeMoveHandle(
                world,
                size,
                Vector3.zero,
                Handles.CircleHandleCap
            );


        if (
            EditorGUI.EndChangeCheck()
        )
        {
            Undo.RecordObject(
                generator,
                "Move Polygon Point"
            );


            Vector3 local =
                root.InverseTransformPoint(
                    newWorld
                );


            area.points[pointIndex] =
                new Vector2(
                    local.x,
                    local.y
                );


            EditorUtility.SetDirty(
                generator
            );


            GUI.changed = true;
        }


        if (selected)
        {
            Handles.Label(
                world +
                Vector3.right *
                size,
                pointIndex.ToString()
            );
        }
    }


    // =========================================================
    // SCENE INPUT
    // =========================================================

    private void HandleSceneInput(
        Event e,
        Transform root
    )
    {
        if (
            e.type != EventType.MouseDown
        )
        {
            return;
        }


        if (
            e.button != 0
        )
        {
            return;
        }


        Vector2 localPoint;


        if (
            !GetMouseLocalPoint(
                e.mousePosition,
                root,
                out localPoint
            )
        )
        {
            return;
        }


        // =====================================================
        // SHIFT = DELETE POINT
        // =====================================================

        if (e.shift)
        {
            if (
                selectedArea >= 0 &&
                selectedArea <
                generator.areas.Count
            )
            {
                WindowGenerator.WindowArea area =
                    generator.areas[
                        selectedArea
                    ];


                if (
                    area.points.Count > 3
                )
                {
                    int point =
                        FindClosestPoint(
                            area.points,
                            localPoint
                        );


                    if (point >= 0)
                    {
                        float distance =
                            Vector2.Distance(
                                localPoint,
                                area.points[point]
                            );


                        float threshold =
                            GetPointThreshold(
                                area.points[point],
                                root
                            );


                        if (
                            distance <=
                            threshold
                        )
                        {
                            Undo.RecordObject(
                                generator,
                                "Delete Polygon Point"
                            );


                            area.points.RemoveAt(
                                point
                            );


                            EditorUtility.SetDirty(
                                generator
                            );


                            e.Use();


                            SceneView.RepaintAll();


                            return;
                        }
                    }
                }
            }
        }


        // =====================================================
        // CTRL = ADD POINT
        // =====================================================

        if (e.control)
        {
            if (
                selectedArea >= 0 &&
                selectedArea <
                generator.areas.Count
            )
            {
                WindowGenerator.WindowArea area =
                    generator.areas[
                        selectedArea
                    ];


                int edge =
                    FindClosestEdge(
                        area.points,
                        localPoint
                    );


                if (edge >= 0)
                {
                    Undo.RecordObject(
                        generator,
                        "Add Polygon Point"
                    );


                    area.points.Insert(
                        edge + 1,
                        localPoint
                    );


                    EditorUtility.SetDirty(
                        generator
                    );


                    e.Use();


                    SceneView.RepaintAll();
                }
            }
        }
    }


    // =========================================================
    // MOUSE → LOCAL POSITION
    // =========================================================

    private bool GetMouseLocalPoint(
        Vector2 mousePosition,
        Transform root,
        out Vector2 localPoint
    )
    {
        localPoint =
            Vector2.zero;


        Ray ray =
            HandleUtility.GUIPointToWorldRay(
                mousePosition
            );


        Plane plane =
            new Plane(
                root.forward,
                root.position
            );


        if (
            !plane.Raycast(
                ray,
                out float distance
            )
        )
        {
            return false;
        }


        Vector3 world =
            ray.GetPoint(
                distance
            );


        Vector3 local =
            root.InverseTransformPoint(
                world
            );


        localPoint =
            new Vector2(
                local.x,
                local.y
            );


        return true;
    }


    // =========================================================
    // POINT THRESHOLD
    // =========================================================

    private float GetPointThreshold(
        Vector2 localPoint,
        Transform root
    )
    {
        Vector3 world =
            root.TransformPoint(
                new Vector3(
                    localPoint.x,
                    localPoint.y,
                    0f
                )
            );


        return HandleUtility.GetHandleSize(
            world
        ) * 0.15f;
    }


    // =========================================================
    // CLOSEST POINT
    // =========================================================

    private int FindClosestPoint(
        List<Vector2> points,
        Vector2 position
    )
    {
        if (
            points == null ||
            points.Count == 0
        )
        {
            return -1;
        }


        int closest = -1;


        float distance =
            float.MaxValue;


        for (
            int i = 0;
            i < points.Count;
            i++
        )
        {
            float d =
                Vector2.Distance(
                    points[i],
                    position
                );


            if (
                d < distance
            )
            {
                distance = d;
                closest = i;
            }
        }


        return closest;
    }


    // =========================================================
    // CLOSEST EDGE
    // =========================================================

    private int FindClosestEdge(
        List<Vector2> points,
        Vector2 position
    )
    {
        if (
            points == null ||
            points.Count < 2
        )
        {
            return -1;
        }


        int closest = -1;


        float distance =
            float.MaxValue;


        for (
            int i = 0;
            i < points.Count;
            i++
        )
        {
            Vector2 a =
                points[i];


            Vector2 b =
                points[
                    (i + 1) %
                    points.Count
                ];


            Vector2 nearest =
                ClosestPointOnSegment(
                    position,
                    a,
                    b
                );


            float d =
                Vector2.Distance(
                    position,
                    nearest
                );


            if (
                d < distance
            )
            {
                distance = d;
                closest = i;
            }
        }


        return closest;
    }


    // =========================================================
    // CLOSEST POINT ON SEGMENT
    // =========================================================

    private Vector2 ClosestPointOnSegment(
        Vector2 point,
        Vector2 a,
        Vector2 b
    )
    {
        Vector2 direction =
            b - a;


        float length =
            direction.sqrMagnitude;


        if (
            length < 0.00001f
        )
        {
            return a;
        }


        float t =
            Vector2.Dot(
                point - a,
                direction
            ) /
            length;


        t =
            Mathf.Clamp01(
                t
            );


        return a +
               direction * t;
    }


    // =========================================================
    // CENTER
    // =========================================================

    private Vector2 CalculateCenter(
        List<Vector2> points
    )
    {
        if (
            points == null ||
            points.Count == 0
        )
        {
            return Vector2.zero;
        }


        Vector2 center =
            Vector2.zero;


        for (
            int i = 0;
            i < points.Count;
            i++
        )
        {
            center +=
                points[i];
        }


        return center /
               points.Count;
    }


    // =========================================================
    // SCENE INSTRUCTIONS
    // =========================================================

    private void DrawSceneInstructions()
    {
        Handles.BeginGUI();


        GUILayout.BeginArea(
            new Rect(
                10,
                10,
                310,
                135
            ),
            "Polygon Window Tool",
            GUI.skin.window
        );


        GUILayout.Label(
            "Inspector에서 Area를 선택하세요."
        );


        GUILayout.Label(
            "● 점 드래그 : 꼭짓점 이동"
        );


        GUILayout.Label(
            "Ctrl + 클릭 : 점 추가"
        );


        GUILayout.Label(
            "Shift + 클릭 : 점 삭제"
        );


        GUILayout.Label(
            "Generate All : 창문 생성"
        );


        GUILayout.EndArea();


        Handles.EndGUI();
    }
}