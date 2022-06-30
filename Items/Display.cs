    using System;
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
    using Terraria.Map;
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
            public UIElement panel3 = new UIElement();
            public bool first = true;
            public int[,] Map;

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

                // Map = new int[Main.maxTilesX, Main.maxTilesY];
                
                // for(int i = 0; i < Main.maxTilesY; i++){
                //     for(int l = 0; l < Main.maxTilesX; l++){
                //         Map[i,l] = Framing.GetTileSafely(i,l).TileType;
                //     }
                // }

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

                panel3.Width.Set(Main.maxTilesX, 0);
                panel3.Height.Set(Main.maxTilesY, 0);
                panel3.HAlign = 0.2f;
                panel3.VAlign = 0.2f;

                Append(panel3);

            }

        public override void OnActivate(){
                Map = new int[Main.maxTilesX, Main.maxTilesY];
                
                for(int i = 0; i < Map.GetLength(0); i++){
                    for(int l = 0; l < Map.GetLength(1); l++){
                        Map[i,l] = Framing.GetTileSafely(i,l).TileType;
                    }
                }
        }

        public override void Update(GameTime gameTime){
                panel.RemoveAllChildren();
                panel2.RemoveAllChildren();
                    text2 = new UIText("X Position: " + ((int)(Main.LocalPlayer.position.X /16)).ToString());
                    // text2 = new UIText(Map[].ToString());
                    text2.HAlign = 0.5f;
                    text2.VAlign = 0.5f;
                    // first = false;
                //panel2.RemoveAllChildren();
                text = new UIText(Map.GetLength(0).ToString());
                //text = new UIText("Y Position: " + ((int)(Main.LocalPlayer.Center.Y / 16)).ToString());
                text.HAlign = 0.5f;
                text.VAlign = 0.5f;

                for(int i = 0; i < Map.GetLength(0); i++){
                    for(int l = 0; l < Map.GetLength(1); l++){
                        panel3.Append(new PADDMapSquare(i, l, Map[i,l]));
                    }
                }

                // text2 = new UIText("X Position: " + ((int)(Main.LocalPlayer.position.X /16)).ToString());
                // text2 = new UIText(ModContent.GetInstance<WorldMap>().MaxHeight.ToString());
                // text2.HAlign = 0.5f;
                // text2.VAlign = 0.5f;

                panel.Append(text);
                if(text2 != null){
                    panel2.Append(text2);
                }
        }
    }
        

}