using Raylib_cs;

namespace HoneycombProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HoneycombGameManager.Initalise();

            while (!Raylib.WindowShouldClose())
            {
                HoneycombGameManager.Update();
                HoneycombGameManager.Draw();
            }

            HoneycombGameManager.Deinitalise();
        }
    }
}