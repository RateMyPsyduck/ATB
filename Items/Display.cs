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
    using Terraria.Audio;

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
            public LCARSButton SaveButton;
            public LCARSButton PrevButton;
            public LCARSButton NextButton;
            public LCARSButton LCARSButton4;
            public PADDFrame PADDFrame;
            public UIText textY;
            public UIText textX; 
            public UIText scanText; 
            public UIElement panel = new UIElement();
            public UIElement panel2 = new UIElement();
            public UIElement panel3 = new UIElement();
            public UIElement panel4 = new UIElement();
            public UIElement panel5 = new UIElement();
            public UIElement panel6 = new UIElement();
            public UIElement panel7 = new UIElement();
            public UIElement panel8 = new UIElement();
            public bool first = true;
            public bool freeDraw = true;
            public int[,] Map;
            public int timer = 0;
            Asset<Texture2D> square = ModContent.Request<Texture2D>($"ATB/Items/Square", AssetRequestMode.ImmediateLoad);
            public int xhi;
            public int yhi;
            public Vector2 pointA =  Main.LocalPlayer.Center;
            bool firstupdate = false;
            int hold;

            public override void OnInitialize()
            {
                LCARS = new LCARS();
                //LCARSButton1 = new LCARSButton1();
                SaveButton = new LCARSButton(ModContent.Request<Texture2D>($"ATB/Items/LCARSButton2"), 1);

                SaveButton.OnClick += SaveClick;
                SaveButton.Width.Set(94, 0);
                SaveButton.Height.Set(27, 0);
                SaveButton.HAlign = 0.0f;
                SaveButton.VAlign = 0.0f;

                PrevButton = new LCARSButton(ModContent.Request<Texture2D>($"ATB/Items/LCARSButton4"), 2);

                PrevButton.OnClick += PrevClick;
                PrevButton.Width.Set(94, 0);
                PrevButton.Height.Set(27, 0);
                PrevButton.HAlign = 0.0f;
                PrevButton.VAlign = 0.0f;

                NextButton = new LCARSButton(ModContent.Request<Texture2D>($"ATB/Items/LCARSButton3"), 3);

                NextButton.OnClick += NextClick;
                NextButton.Width.Set(94, 0);
                NextButton.Height.Set(27, 0);
                NextButton.HAlign = 0.0f;
                NextButton.VAlign = 0.0f;

                // LCARSButton3 = new LCARSButton3();
                // LCARSButton4 = new LCARSButton4();
                PADDFrame = new PADDFrame();

                Append(LCARS);
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

                panel3.Width.Set(Main.screenWidth, 0);
                panel3.Height.Set(Main.screenHeight, 0);
                panel3.HAlign = 0f;
                panel3.VAlign = 0f;

                Append(panel3);

                panel4.Width.Set(10, 0);
                panel4.Height.Set(10, 0);
                panel4.HAlign = 0.5167f;
                panel4.VAlign = 0.44f;

                Append(panel4);

                panel5.Width.Set(100, 0);
                panel5.Height.Set(30, 0);
                panel5.HAlign = 0.6035f;
                panel5.VAlign = 0.362f;

                Append(panel5);

                panel6.Width.Set(100, 0);
                panel6.Height.Set(30, 0);
                panel6.HAlign = 0.6035f;
                panel6.VAlign = 0.323f;

                Append(panel6);

                panel7.Width.Set(100, 0);
                panel7.Height.Set(30, 0);
                panel7.HAlign = 0.67f;
                panel7.VAlign = 0.323f;

                Append(panel7);

                // panel8.Width.Set(200, 0);
                // panel8.Height.Set(100, 0);
                // panel8.HAlign = 0.647f;
                // panel8.VAlign = 0.3947f;

                // Append(panel8);

                panel5.Append(SaveButton);

                panel6.Append(PrevButton);

                panel7.Append(NextButton);
            }

            private void SaveClick(UIMouseEvent evt, UIElement listeningElement) {
                Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeamLocations.Add(Main.LocalPlayer.BottomLeft);
                Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeeamLocationPointer = Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeamLocations.Count - 1;
            }

            private void PrevClick(UIMouseEvent evt, UIElement listeningElement) {
                Main.LocalPlayer.GetModPlayer<BeamPlayer>().DecreaseBeamPointer(); 
            }

            private void NextClick(UIMouseEvent evt, UIElement listeningElement) {
                Main.LocalPlayer.GetModPlayer<BeamPlayer>().IncreaseBeamPointer(); 
            }

        public override void OnDeactivate(){
            panel3.RemoveAllChildren();
            firstupdate = false;
        }

        public override void Update(GameTime gameTime){
                panel.RemoveAllChildren();
                panel2.RemoveAllChildren();
                panel4.RemoveAllChildren();
                pointA = Main.LocalPlayer.Center;

                textX = new UIText("X Position: " + ((int)(Main.LocalPlayer.position.X /16)).ToString());
                textX.HAlign = 0.5f;
                textX.VAlign = 0.5f;

                textY = new UIText("Y Position: " + ((int)(Main.LocalPlayer.Center.Y / 16)).ToString());
                textY.HAlign = 0.5f;
                textY.VAlign = 0.5f;

                scanText = new UIText("Local Area Scan:");
                scanText.HAlign = 0.5f;
                scanText.VAlign = 0.5f;

                if(Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeamLocations.Count != 0){
                    scanText.SetText("X: " + Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeamLocations[Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeeamLocationPointer].X / 16 + " - " + "Y: " + Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeamLocations[Main.LocalPlayer.GetModPlayer<BeamPlayer>().BeeamLocationPointer].Y / 16);
                }

                panel.Append(textY);

                panel2.Append(textX);

                panel4.Append(scanText);
                timer++;
                if(timer > 30 || firstupdate == false && freeDraw == true){
                    timer = 0;
                    panel3.RemoveAllChildren();
                    pointA =  Main.LocalPlayer.Center;
                    int x = 0;
                    int y = 0;

                    xhi = (int)pointA.X / 16 + 155;
                    yhi = (int)pointA.Y / 16 + 75;

                    for(int xlow = ((int)pointA.X / 16) - 155; xlow < xhi; xlow++){
                        y = y + 1;
                        for(int ylow = ((int)pointA.Y /16) - 75; ylow < yhi; ylow++){
                            PADDMapSquare squ = new PADDMapSquare(y, x, Framing.GetTileSafely(xlow, ylow).TileType, square, y, firstupdate);
                            panel3.Append(squ);
                            x = x + 1;
                        }
                        x = 0;
                    }
                    firstupdate = true;
                }
                
        }
    }
}