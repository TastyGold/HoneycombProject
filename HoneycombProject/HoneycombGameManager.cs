using Raylib_cs;

namespace HoneycombProject
{
    public static class HoneycombGameManager
    {

        public static void Initalise()
        {
            Raylib.InitWindow(1600, 900, "Honeycomb Project");
        }

        public static void Deinitalise()
        {
            Raylib.CloseWindow();
        }

        public static void Update()
        {
            HoneycombCamera.UpdateCamera();
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(HoneycombCamera.mainCamera);

            Raylib.ClearBackground(HoneycombGridHelper.backgroundColor);

            HoneycombGridHelper.DrawGridLines(26, 14);
            HoneycombGridHelper.DrawGridSquares(26, 14);
            VecInt2 mouseCoord = HoneycombCamera.GetMouseGridCoordinate();
            Raylib.DrawCircle((int)HoneycombGridHelper.GetGridToWorldX(mouseCoord.x), (int)HoneycombGridHelper.GetGridToWorldY(mouseCoord.x, mouseCoord.y), 20, Color.RED);

            Raylib.EndMode2D();
            Raylib.EndDrawing();
        }
    }
}