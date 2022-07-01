    using System;
    using Terraria.UI;
    using Terraria.GameContent.UI.Elements;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ReLogic.Graphics;
    using ReLogic.Content;
    using Terraria;
    using Terraria.GameContent;
    using Terraria.Graphics;
    using Terraria.ID;
    using Terraria.Localization;
    using Terraria.ModLoader;
    using Terraria.Map;
    using Terraria.UI.Chat;

//     using Terraria;
// using Terraria.UI;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using ReLogic.Content;
// using Terraria.ModLoader;
// using Terraria.Audio;
// using Terraria.ID;
// using Terraria.GameContent.UI.Elements;

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
            public UIText text3; 
            public UIElement panel = new UIElement();
            public UIElement panel2 = new UIElement();
            public UIElement panel3 = new UIElement();
            public bool first = true;
            public int[,] Map;
            Asset<Texture2D> square = ModContent.Request<Texture2D>($"ATB/Items/Square");
            public int xhi;
            public int yhi;
            public Vector2 pointA =  Main.LocalPlayer.Center;

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
                panel3.HAlign = 0f;
                panel3.VAlign = 0f;

                Append(panel3);

            //     int x = 0;
            //     int y = 0;

            //    Vector2 pointA =  Main.LocalPlayer.Center;

            //    //int xlow = (int)pointA.X - 60;
            //    int xhi = (int)pointA.X / 16 + 60;
            //    int yhi = (int)pointA.Y / 16 + 60;

            //    Main.NewText(Main.LocalPlayer.Center.ToString());


            //     // for(int xlow = ((int)pointA.X /16) - 60; xlow < xhi; xlow++){
            //     //     y = y + 1;
            //     //     for(int ylow = ((int)pointA.Y /16) - 60; ylow < yhi; ylow++){
            //     //         PADDMapSquare squ = new PADDMapSquare(y, x, Main.tile[ylow,xlow].TileType);
            //     //         squ.HAlign = 0.5f;
            //     //         squ.VAlign = 0.2f;
            //     //         panel3.Append(squ);
            //     //         x = x + 1;
            //     //     }
            //     //     x = 0;
            //     // }

            }

        public override void OnActivate(){
                panel3.RemoveAllChildren();
                int x = 0;
                int y = 0;

               //int xlow = (int)pointA.X - 60;
                xhi = (int)pointA.X / 16 + 155;
                yhi = (int)pointA.Y / 16 + 75;

                for(int xlow = ((int)pointA.X / 16) - 155; xlow < xhi; xlow++){
                    y = y + 1;
                    for(int ylow = ((int)pointA.Y /16) - 75; ylow < yhi; ylow++){
                        PADDMapSquare squ = new PADDMapSquare(y, x, Framing.GetTileSafely(xlow, ylow).TileType, square);
                        squ.HAlign = 0.5f;
                        squ.VAlign = 0.2f;
                        panel3.Append(squ);
                        x = x + 1;
                    }
                    x = 0;
                }
        }

        // public override void OnDeactivate(){
        //         panel3.RemoveAllChildren();
        // }

        public override void Update(GameTime gameTime){
                panel.RemoveAllChildren();
                panel2.RemoveAllChildren();
                pointA =  Main.LocalPlayer.Center;
                //     text2 = new UIText("X Position: " + ((int)(Main.LocalPlayer.position.X /16)).ToString());
                //     text3 = new UIText("Test");
                //     // text2 = new UIText(Map[].ToString());
                //     text2.HAlign = 0.5f;
                //     text2.VAlign = 0.5f;
                //     text3.HAlign = 0.5f;
                //     text3.VAlign = 0.5f;
                //     // first = false;
                // //panel2.RemoveAllChildren();
                // // text = new UIText(Map.GetLength(0).ToString());
                // //text = new UIText("Y Position: " + ((int)(Main.LocalPlayer.Center.Y / 16)).ToString());
                // text.HAlign = 0.5f;
                // text.VAlign = 0.5f;

                // // int x = 0;
                // // int y = 0;

                // // for(int i = 940; i < 1020; i++){
                // //     y = y + 2;
                // //     for(int l = 940; l < 1020; l++){
                // //         PADDMapSquare squ = new PADDMapSquare(x, y, Map[i,l]);
                // //         squ.HAlign = 0.5f;
                // //         squ.VAlign = 0.2f;
                // //         panel3.Append(squ);
                // //         x = x + 2;
                // //     }
                // //     x = 0;
                // // }

                // // text2 = new UIText("X Position: " + ((int)(Main.LocalPlayer.position.X /16)).ToString());
                // // text2 = new UIText(ModContent.GetInstance<WorldMap>().MaxHeight.ToString());
                // // text2.HAlign = 0.5f;
                // // text2.VAlign = 0.5f;

                // panel.Append(text);
                // if(text2 != null){
                //     panel2.Append(text2);
                // }
               //panel3.Append(text3);
        }
    }
        

}