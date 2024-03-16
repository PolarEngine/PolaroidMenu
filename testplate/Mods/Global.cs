using static PolaroidMenu.Menu.Main;

namespace PolaroidMenu.Mods
{
    internal class Global
    {
        public static void ReturnHome()
        {
            buttonsType = 0;
        }

        public static void OpenOverpowered()
        {
            buttonsType = 4;
        }

        public static void OpenMovement()
        {
            buttonsType = 5;
        }

        public static void OpenPlayer()
        {
            buttonsType = 6;
        }
    }
}
