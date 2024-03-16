using PolaroidMenu.Classes;
using UnityEngine;
using static PolaroidMenu.Menu.Main;

namespace PolaroidMenu
{
    internal class Settings
    {
        static GradientColorKey[] BlackAndWhite = new GradientColorKey[]
         {
            new GradientColorKey(Color.black, 0f),
            new GradientColorKey(Color.white, 0.5f),
            new GradientColorKey(Color.black, 1f)
         };

        static GradientColorKey[] BlackAndWhitef = new GradientColorKey[]
        {
            new GradientColorKey(Color.black, 0f),
            new GradientColorKey(Color.white, 0.5f),
            new GradientColorKey(Color.black, 1f)
        };

        static GradientColorKey[] yellowFade = new GradientColorKey[]
        {
            new GradientColorKey(Color.yellow, 0f),
            new GradientColorKey(Color.black, 0.5f),
            new GradientColorKey(Color.yellow, 1f)
        };

        public static ExtGradient magentaColor = new ExtGradient { colors = GetSolidGradient(Color.magenta) };
        public static ExtGradient redFadeGradient = new ExtGradient { colors = BlackAndWhitef };
        public static ExtGradient yellowFadeGradient = new ExtGradient { colors = yellowFade };
        public static ExtGradient rainbowColor = new ExtGradient { isRainbow = true };
        public static ExtGradient backgroundColor = new ExtGradient { isRainbow = true };//isRainbow = true};
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(Color.black) }, // Disabled
            new ExtGradient{colors= GetSolidGradient(Color.green) } // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.white // Enabled
        };

        public static Font currentFont = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
        //public static Font currentFont = Font.CreateDynamicFontFromOSFont("System Bold", 24);
        //public static Font currentFont = Font.CreateDynamicFontFromOSFont("Pokemon Solid Normal", 24);
        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool disableNotifications = false;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;
    }
}
