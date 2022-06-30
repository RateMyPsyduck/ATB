    using Terraria.UI;
    using Terraria.GameContent.UI.Elements;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

    namespace ATB.Items
    {
        class Display : UIState
        {
            public LCARS LCARS;
            public LCARSButton1 LCARSButton1;
            public LCARSButton2 LCARSButton2;
            public LCARSButton3 LCARSButton3;
            public LCARSButton4 LCARSButton4;
            public PADDFrame PADDFrame;
            public UIText text;
            public UIText text2; 
            public UIElement panel = new UIElement();
            public UIElement panel2 = new UIElement();

            public override void OnInitialize()
            {
                LCARS = new LCARS();
                LCARSButton1 = new LCARSButton1();
                LCARSButton2 = new LCARSButton2();
                LCARSButton3 = new LCARSButton3();
                LCARSButton4 = new LCARSButton4();
                PADDFrame = new PADDFrame();

                Append(LCARS);
                Append(LCARSButton1);
                Append(LCARSButton2);
                Append(LCARSButton3);
                Append(LCARSButton4);
                Append(PADDFrame);

                panel.Width.Set(10, 0);
                panel.Height.Set(10, 0);
                panel.HAlign = 0.43f;
                panel.VAlign = 0.352f;

                Append(panel);

                panel2.Width.Set(10, 0);
                panel2.Height.Set(10, 0);
                panel2.HAlign = 0.43f;
                panel2.VAlign = 0.33f;
                Append(panel2);
            }

        public override void Update(GameTime gameTime){
                panel.RemoveAllChildren();
                panel2.RemoveAllChildren();
                text = new UIText("Y Position: " + ((int)(Main.LocalPlayer.Center.Y / 16)).ToString());
                text.HAlign = 0.5f; // 1
                text.VAlign = 0.5f; // 1

                text2 = new UIText("X Position: " + ((int)(Main.LocalPlayer.position.X /16)).ToString());
                text2.HAlign = 0.5f; // 1
                text2.VAlign = 0.5f; // 1

                // panel = new UIElement();
                // panel.Width.Set(300, 0);
                // panel.Height.Set(300, 0);
                // panel.HAlign = 0.5f;
                // panel.VAlign = 0.3f;
                // Append(panel);

                // panel2 = new UIElement();
                // panel2.Width.Set(300, 0);
                // panel2.Height.Set(300, 0);
                // panel2.HAlign = 0.5f;
                // panel2.VAlign = 0.3f;
                // Append(panel2);

                panel.Append(text);
                panel2.Append(text2);
        }
    }
        

}