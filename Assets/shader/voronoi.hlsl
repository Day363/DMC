
#ifndef VORONOI_INCLUDED
#define VORONOI_INCLUDED

float2 hash22(float2 p)
{
    p = float2(dot(p, float2(127.1, 311.7)),
               dot(p, float2(269.5, 183.3)));
    return frac(sin(p) * 43758.5453);
}

float hash21(float2 p)
{
    return frac(sin(dot(p, float2(12.9898, 78.233))) * 43758.5453);
}

float CellIDFromCoord(float2 cell)
{
    return hash21(cell * 123.45);
}

void VoronoiFull_float_float(
    float2 UV,
    float Scale,
    float DensityPower,
    float2 Center,
    float OffsetPower,
    out float CellID,
    out float Distance,
    out float2 CellUV,
    out float2 OffsetUV)
{
    float2 g = floor(UV * Scale);
    float2 f = frac(UV * Scale);

    float minDist = 999.0;
    float cellID = 0.0;
    float2 localUV = float2(0, 0);
    float2 bestCell = float2(0, 0);

    int range = 5;

    for (int y = -range; y <= range; y++)
    {
        for (int x = -range; x <= range; x++)
        {
            float2 offset = float2(x, y);
            float2 cell = g + offset;
            float2 cellCenterUV = (cell + 0.5) / Scale;

            float dist = distance(cellCenterUV, Center);
            float density = pow(saturate(1.0 - dist), DensityPower);

            float rnd = hash21(cell);
            if (rnd > density)
                continue;

            float2 rand = hash22(cell);
            float2 diff = offset + rand - f;
            float d = dot(diff, diff);

            if (d < minDist)
            {
                minDist = d;
                cellID = CellIDFromCoord(cell);
                bestCell = cell;

                float2 cellCenter = cell + rand;
                float2 currentPos = g + f;
                localUV = currentPos - cellCenter;
            }
        }
    }

    CellID = cellID;
    Distance = sqrt(minDist);
    CellUV = frac(localUV + 1.0);

    float2 randOffset = hash22(bestCell);
    float2 dir = randOffset * 2.0 - 1.0;
    float strength = OffsetPower * 0.3;
    float2 scaledOffset = dir * strength;

    
    OffsetUV = UV + scaledOffset;
}

#endif

