using Raylib_cs;
using System.Numerics;

namespace HoneycombProject
{
    public static class HoneycombCamera
    {
        public static Camera2D mainCamera = new Camera2D()
        {
            target = Vector2.Zero,
            zoom = 1,
            rotation = 0,
            offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
        };

        private static float targetZoom = 1;
        private static Vector2 targetPosition = Vector2.Zero;

        public static float cameraPanSpeed = 800;

        public static void UpdateCamera()
        {
            HandleCameraZooming();
            HandleCameraMovement();
        }

        private static void HandleCameraZooming()
        {
            if (Raylib.GetMouseWheelMove() > 0 && targetZoom < 2)
            {
                targetZoom *= 1.3f;
            }
            if (Raylib.GetMouseWheelMove() < 0 && targetZoom > 0.2f)
            {
                targetZoom /= 1.3f;
            }
            mainCamera.zoom = HoneycombMath.Lerp(mainCamera.zoom, targetZoom, Raylib.GetFrameTime() * 20f);
        }

        private static void HandleCameraMovement()
        {
            Vector2 inputVector = Vector2.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) inputVector.X--;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) inputVector.X++;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) inputVector.Y--;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) inputVector.Y++;

            if (inputVector.X != 0) inputVector.Y *= 0.577f;
            else inputVector.X *= 0.866f;

            targetPosition += inputVector * cameraPanSpeed * Raylib.GetFrameTime() * (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT) ? 2 : 1) / mainCamera.zoom;

            mainCamera.target = Vector2.Lerp(mainCamera.target, targetPosition, Raylib.GetFrameTime() * 20f);
        }

        public static VecInt2 GetMouseGridCoordinate()
        {
            return HoneycombGridHelper.GetWorldToGridCoord(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), mainCamera));
        }
    }
}