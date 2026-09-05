using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class WindowGenerator : MonoBehaviour
{
    // =========================================================
    // WINDOW AREA
    // =========================================================

    [System.Serializable]
    public class WindowArea
    {
        // -----------------------------------------------------
        // BASIC
        // -----------------------------------------------------

        public string name = "Area";

        [Range(0f, 1f)]
        public float density = 0.7f;

        public Vector2 spacing =
            new Vector2(0.5f, 0.5f);

        public int randomSeed = 12345;


        // -----------------------------------------------------
        // RANDOM SCALE
        // -----------------------------------------------------

        [Header("Random Scale")]

        public bool randomScale = false;

        // X축 최소 / 최대
        public Vector2 scaleRangeX =
            new Vector2(1f, 1f);

        // Y축 최소 / 최대
        public Vector2 scaleRangeY =
            new Vector2(1f, 1f);


        // -----------------------------------------------------
        // RANDOM COLOR
        // -----------------------------------------------------

        [Header("Random Color")]

        public bool randomColor = false;

        // 최소 색상
        public Color colorMin =
            Color.white;

        // 최대 색상
        public Color colorMax =
            Color.white;


        // -----------------------------------------------------
        // POLYGON
        // -----------------------------------------------------

        public List<Vector2> points =
            new List<Vector2>();
    }


    // =========================================================
    // WINDOW PREFAB
    // =========================================================

    [Header("Window")]
    public GameObject windowPrefab;


    // =========================================================
    // POLYGON AREAS
    // =========================================================

    [Header("Polygon Areas")]
    public List<WindowArea> areas =
        new List<WindowArea>();


    // =========================================================
    // GENERATED ROOT
    // =========================================================

    private const string GeneratedRootName =
        "Generated Windows";


    // =========================================================
    // ADD AREA
    // =========================================================

    public WindowArea AddArea()
    {
        WindowArea area =
            new WindowArea();

        area.name =
            "Area " + (areas.Count + 1);


        // 기본 사각형
        area.points.Add(
            new Vector2(-2f, -1f)
        );

        area.points.Add(
            new Vector2(2f, -1f)
        );

        area.points.Add(
            new Vector2(2f, 1f)
        );

        area.points.Add(
            new Vector2(-2f, 1f)
        );


        areas.Add(area);

        return area;
    }


    // =========================================================
    // REMOVE AREA
    // =========================================================

    public void RemoveArea(int index)
    {
        if (
            index < 0 ||
            index >= areas.Count
        )
        {
            return;
        }

        areas.RemoveAt(index);
    }


    // =========================================================
    // GENERATE ALL
    // =========================================================

    public void GenerateAll()
    {
        if (windowPrefab == null)
        {
            Debug.LogWarning(
                "Window Generator: Window Prefab이 지정되지 않았습니다."
            );

            return;
        }


        // 기존 생성물 삭제
        ClearAll();


        // 생성물 Root
        GameObject generatedRoot =
            new GameObject(
                GeneratedRootName
            );


        generatedRoot.transform.SetParent(
            transform,
            false
        );


        // 모든 Area 생성
        for (
            int i = 0;
            i < areas.Count;
            i++
        )
        {
            GenerateArea(
                areas[i],
                i,
                generatedRoot.transform
            );
        }


        Debug.Log(
            $"Window Generator: {areas.Count}개 Polygon 영역 생성 완료"
        );
    }


    // =========================================================
    // GENERATE AREA
    // =========================================================

    private void GenerateArea(
        WindowArea area,
        int areaIndex,
        Transform generatedRoot
    )
    {
        if (area == null)
        {
            return;
        }


        if (
            area.points == null ||
            area.points.Count < 3
        )
        {
            return;
        }


        if (
            area.spacing.x <= 0f ||
            area.spacing.y <= 0f
        )
        {
            return;
        }


        // -----------------------------------------------------
        // AREA ROOT
        // -----------------------------------------------------

        GameObject areaRoot =
            new GameObject(
                area.name
            );


        areaRoot.transform.SetParent(
            generatedRoot,
            false
        );


        // -----------------------------------------------------
        // RANDOM SEED
        // -----------------------------------------------------

        Random.InitState(
            area.randomSeed
        );


        // -----------------------------------------------------
        // SCALE RANGE
        // -----------------------------------------------------

        float minX =
            Mathf.Min(
                area.scaleRangeX.x,
                area.scaleRangeX.y
            );

        float maxX =
            Mathf.Max(
                area.scaleRangeX.x,
                area.scaleRangeX.y
            );


        float minY =
            Mathf.Min(
                area.scaleRangeY.x,
                area.scaleRangeY.y
            );

        float maxY =
            Mathf.Max(
                area.scaleRangeY.x,
                area.scaleRangeY.y
            );


        // -----------------------------------------------------
        // POLYGON BOUNDS
        // -----------------------------------------------------

        Vector2 min =
            area.points[0];

        Vector2 max =
            area.points[0];


        for (
            int i = 1;
            i < area.points.Count;
            i++
        )
        {
            min =
                Vector2.Min(
                    min,
                    area.points[i]
                );

            max =
                Vector2.Max(
                    max,
                    area.points[i]
                );
        }


        // -----------------------------------------------------
        // GRID COUNT
        // -----------------------------------------------------

        int xCount =
            Mathf.CeilToInt(
                (max.x - min.x) /
                area.spacing.x
            );


        int yCount =
            Mathf.CeilToInt(
                (max.y - min.y) /
                area.spacing.y
            );


        // -----------------------------------------------------
        // GENERATE
        // -----------------------------------------------------

        for (
            int y = 0;
            y <= yCount;
            y++
        )
        {
            for (
                int x = 0;
                x <= xCount;
                x++
            )
            {
                Vector2 localPoint =
                    new Vector2(
                        min.x +
                        x * area.spacing.x,

                        min.y +
                        y * area.spacing.y
                    );


                // -------------------------------------------------
                // POLYGON 내부인지 확인
                // -------------------------------------------------

                if (
                    !IsPointInsidePolygon(
                        localPoint,
                        area.points
                    )
                )
                {
                    continue;
                }


                // -------------------------------------------------
                // DENSITY
                // -------------------------------------------------

                if (
                    Random.value >
                    area.density
                )
                {
                    continue;
                }


                // -------------------------------------------------
                // LOCAL POSITION
                // -------------------------------------------------

                Vector3 generatorLocalPosition =
                    new Vector3(
                        localPoint.x,
                        localPoint.y,
                        0f
                    );


                // -------------------------------------------------
                // WORLD POSITION
                // -------------------------------------------------

                Vector3 worldPosition =
                    transform.TransformPoint(
                        generatorLocalPosition
                    );


                // -------------------------------------------------
                // CREATE
                // -------------------------------------------------

                GameObject window =
                    Instantiate(
                        windowPrefab
                    );


                window.name =
                    $"Window_{areaIndex}_{x}_{y}";


                // -------------------------------------------------
                // PARENT
                // -------------------------------------------------

                window.transform.SetParent(
                    areaRoot.transform,
                    true
                );


                // -------------------------------------------------
                // POSITION
                // -------------------------------------------------

                window.transform.position =
                    worldPosition;


                // -------------------------------------------------
                // ROTATION
                // -----------------------------------------------------

                window.transform.rotation =
                    transform.rotation;


                // -------------------------------------------------
                // RANDOM SCALE
                // -------------------------------------------------

                ApplyRandomScale(
                    window,
                    area,
                    minX,
                    maxX,
                    minY,
                    maxY
                );


                // -------------------------------------------------
                // RANDOM COLOR
                // -------------------------------------------------

                ApplyRandomColor(
                    window,
                    area
                );
            }
        }
    }


    // =========================================================
    // RANDOM SCALE
    // =========================================================

    private void ApplyRandomScale(
        GameObject window,
        WindowArea area,
        float minX,
        float maxX,
        float minY,
        float maxY
    )
    {
        if (!area.randomScale)
        {
            return;
        }


        // X와 Y를 독립적으로 랜덤
        float randomX =
            Random.Range(
                minX,
                maxX
            );


        float randomY =
            Random.Range(
                minY,
                maxY
            );


        Vector3 scale =
            window.transform.localScale;


        scale.x *= randomX;
        scale.y *= randomY;


        window.transform.localScale =
            scale;
    }


    // =========================================================
    // RANDOM COLOR
    // =========================================================

    private void ApplyRandomColor(
        GameObject window,
        WindowArea area
    )
    {
        if (!area.randomColor)
        {
            return;
        }


        // 최소 ~ 최대 사이의 랜덤 색상
        Color randomColor =
            Color.Lerp(
                area.colorMin,
                area.colorMax,
                Random.value
            );


        // -----------------------------------------------------
        // SPRITE RENDERER
        // -----------------------------------------------------

        SpriteRenderer[] sprites =
            window.GetComponentsInChildren<SpriteRenderer>(
                true
            );


        foreach (
            SpriteRenderer sprite
            in sprites
        )
        {
            sprite.color =
                randomColor;
        }


        // -----------------------------------------------------
        // 일반 Renderer
        // -----------------------------------------------------

        Renderer[] renderers =
            window.GetComponentsInChildren<Renderer>(
                true
            );


        foreach (
            Renderer renderer
            in renderers
        )
        {
            if (
                renderer is SpriteRenderer
            )
            {
                continue;
            }


            MaterialPropertyBlock block =
                new MaterialPropertyBlock();


            renderer.GetPropertyBlock(
                block
            );


            Material material =
                renderer.sharedMaterial;


            if (material != null)
            {
                // URP / HDRP
                if (
                    material.HasProperty(
                        "_BaseColor"
                    )
                )
                {
                    block.SetColor(
                        "_BaseColor",
                        randomColor
                    );
                }


                // Standard Shader
                if (
                    material.HasProperty(
                        "_Color"
                    )
                )
                {
                    block.SetColor(
                        "_Color",
                        randomColor
                    );
                }
            }


            renderer.SetPropertyBlock(
                block
            );
        }
    }


    // =========================================================
    // POINT INSIDE POLYGON
    // =========================================================

    private bool IsPointInsidePolygon(
        Vector2 point,
        List<Vector2> polygon
    )
    {
        bool inside = false;


        int j =
            polygon.Count - 1;


        for (
            int i = 0;
            i < polygon.Count;
            i++
        )
        {
            Vector2 a =
                polygon[i];

            Vector2 b =
                polygon[j];


            bool intersect =
                (
                    (a.y > point.y) !=
                    (b.y > point.y)
                )
                &&
                (
                    point.x <
                    (b.x - a.x) *
                    (point.y - a.y) /
                    (b.y - a.y) +
                    a.x
                );


            if (intersect)
            {
                inside =
                    !inside;
            }


            j = i;
        }


        return inside;
    }


    // =========================================================
    // CLEAR ALL
    // =========================================================

    public void ClearAll()
    {
        Transform generated =
            transform.Find(
                GeneratedRootName
            );


        if (generated == null)
        {
            return;
        }


#if UNITY_EDITOR

        if (!Application.isPlaying)
        {
            DestroyImmediate(
                generated.gameObject
            );

            return;
        }

#endif


        Destroy(
            generated.gameObject
        );
    }


    // =========================================================
    // GIZMOS
    // =========================================================

    private void OnDrawGizmos()
    {
        if (areas == null)
        {
            return;
        }


        Gizmos.matrix =
            transform.localToWorldMatrix;


        for (
            int a = 0;
            a < areas.Count;
            a++
        )
        {
            WindowArea area =
                areas[a];


            if (
                area == null ||
                area.points == null ||
                area.points.Count < 2
            )
            {
                continue;
            }


            Gizmos.color =
                new Color(
                    0f,
                    1f,
                    1f,
                    0.8f
                );


            for (
                int i = 0;
                i < area.points.Count;
                i++
            )
            {
                Vector3 p1 =
                    area.points[i];


                Vector3 p2 =
                    area.points[
                        (i + 1) %
                        area.points.Count
                    ];


                Gizmos.DrawLine(
                    p1,
                    p2
                );
            }
        }
    }
}