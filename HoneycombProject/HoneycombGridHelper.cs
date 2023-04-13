using Raylib_cs;
using System.Numerics;

namespace HoneycombProject
{
    public static class HoneycombGridHelper
    {
        public const float gridSize = 200;
        public const float unitX = gridSize * 0.8660254387f;
        public const float unitY = gridSize * 1;

        public static Color backgroundColor = new Color(230, 230, 230, 255);
        public static Color gridLineColor = new Color(200, 200, 200, 255);

        public static int gridLineThickness = 12;

        public static Vector2 GetHexagonCenter(int x, int y)
        {
            return new Vector2(unitX * x, unitY * y + (HoneycombMath.Modulo(x, 2) == 1 ? unitY / 2 : 0));
        }

        public static void DrawGridLines(int width, int height)
        {
            for (int x = -width; x < width; x++)
            {
                for (int y = -height; y < height; y++)
                {
                    //Vector2 center = GetHexagonCenter(x, y);
                    //Raylib.DrawCircle((int)(center.X), (int)(center.Y), 5, Color.RED);

                    Vector2 offsetCenter = GetHexagonCenter(x, y) + (new Vector2(-0.577f, 0) * gridSize);
                    Raylib.DrawLineEx(offsetCenter, offsetCenter + (Vector2.UnitX * gridSize * -0.577f), gridLineThickness, gridLineColor);
                    Raylib.DrawLineEx(offsetCenter, offsetCenter + (Vector2.UnitX * gridSize * 0.288f) + (Vector2.UnitY * unitY * 0.5f), gridLineThickness, gridLineColor);
                    Raylib.DrawLineEx(offsetCenter, offsetCenter + (Vector2.UnitX * gridSize * 0.288f) + (Vector2.UnitY * unitY * -0.5f), gridLineThickness, gridLineColor);
                }
            }
        }

        public static void DrawGridSquares(int width, int height)
        {
            for (int x = -width; x < width; x++)
            {
                Raylib.DrawLineV(new Vector2((x - 0.5f) * unitX, unitY * -height), new Vector2((x - 0.5f) * unitX, unitY * height), Color.BLACK);

                for (int y = -height; y < height; y++)
                {
                    float offsetY = y + (HoneycombMath.Modulo(x, 2) == 1 ? 0f : 0.5f);
                    Raylib.DrawLineV(new Vector2((x - 0.5f) * unitX, unitY * offsetY), new Vector2((x + 0.5f) * unitX, unitY * offsetY), Color.BLACK);
                }
            }
        }

        public static float GetGridToWorldX(int x)
        {
            return (int)(unitX * x);
        }

        public static float GetGridToWorldY(int x, int y)
        {
            return (int)(unitY * y + (HoneycombMath.Modulo(x, 2) == 1 ? unitY * 0.5f : 0));
        }

        public static Vector2 GetGridToWorldPos(VecInt2 gridCoord)
        {
            return new Vector2(GetGridToWorldX(gridCoord.x), GetGridToWorldY(gridCoord.x, gridCoord.y));
        }

        public static VecInt2 GetWorldToGridCoord(Vector2 worldPos)
        {
            Vector2 scaledWorldPos = new Vector2()
            {
                X = worldPos.X / unitX,
                Y = worldPos.Y / unitY,
            };

            int gridX = (int)Math.Round(scaledWorldPos.X);

            scaledWorldPos.Y += (HoneycombMath.Modulo(gridX, 2) == 1 ? -0.5f : 0);
            int gridY = (int)Math.Round(scaledWorldPos.Y);

            //Check which tile center mouse is closest to
            float dx = scaledWorldPos.X - gridX;
            float dy = scaledWorldPos.Y - gridY;
            VecInt2 compareCoord = new VecInt2()
            {
                x = gridX + (dx > 0 ? 1 : -1),
                y = gridY + (dy > 0 ? 0 : -1) + (HoneycombMath.Modulo(gridX, 2) == 1 ? 1 : 0),
            };

            Vector2 comparePosition = GetGridToWorldPos(compareCoord);
            Raylib.DrawCircle((int)comparePosition.X, (int)comparePosition.Y, 20, Color.PURPLE);
            Raylib.DrawLineEx(worldPos, comparePosition, 3, Color.GREEN);
            Raylib.DrawLineEx(worldPos, GetGridToWorldPos(new VecInt2(gridX, gridY)), 3, Color.GREEN);

            bool swap = Vector2.DistanceSquared(worldPos, comparePosition) < Vector2.DistanceSquared(worldPos, GetGridToWorldPos(new VecInt2(gridX, gridY)));
            if (swap)
            {
                gridX = compareCoord.x;
                gridY = compareCoord.y;
            }

            return new VecInt2(gridX, gridY);
        }
    }
}