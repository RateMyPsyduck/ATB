using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;

    namespace ATB.Items
    {
        class LCARS : UIElement
        {
            public bool first = true;
            public Vector2 v;

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/LCARS_Front");
            Asset<Texture2D> Back = ModContent.Request<Texture2D>($"ATB/Items/LCARS_Back");

            public override void Draw(SpriteBatch spriteBatch)
            {
                if(first) {
                    //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                    v = new Vector2(Main.screenWidth, Main.screenHeight);  
                    first = false;
                }
                if(v.Y > (Main.screenHeight / 2f) - 224){
                    v.Y = v.Y - 30;
                }
                spriteBatch.Draw((Texture2D)Back, new Vector2((v.X / 2f) - 300, v.Y), Color.White);
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) - 300, v.Y), Color.White);
            }
        }

        class LCARSButton1 : UIElement
        {
            public bool first = true;
            public Vector2 v;

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/LCARSButton1");

            public override void Draw(SpriteBatch spriteBatch)
            {
                if(first) {
                    //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                    v = new Vector2(Main.screenWidth, Main.screenHeight);  
                    first = false;
                }
                if(v.Y > (Main.screenHeight / 2f) - 224){
                    v.Y = v.Y - 30;
                }
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 200, v.Y + 96), Color.White);
            }
        }

        class LCARSButton2 : UIElement
        {
            public bool first = true;
            public Vector2 v;

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/LCARSButton2");

            public override void Draw(SpriteBatch spriteBatch)
            {
                if(first) {
                    //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                    v = new Vector2(Main.screenWidth, Main.screenHeight);  
                    first = false;
                }
                if(v.Y > (Main.screenHeight / 2f) - 224){
                    v.Y = v.Y - 30;
                }
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 102, v.Y + 96), Color.White);
            }
        }

        class LCARSButton3 : UIElement
        {
            public bool first = true;
            public Vector2 v;

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/LCARSButton4");

            public override void Draw(SpriteBatch spriteBatch)
            {
                if(first) {
                    //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                    v = new Vector2(Main.screenWidth, Main.screenHeight);  
                    first = false;
                }
                if(v.Y > (Main.screenHeight / 2f) - 224){
                    v.Y = v.Y - 30;
                }
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 102, v.Y + 62), Color.White);
            }
        } 

        class LCARSButton4 : UIElement
        {
            public bool first = true;
            public Vector2 v;

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/LCARSButton3");

            public override void Draw(SpriteBatch spriteBatch)
            {
                if(first) {
                    //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                    v = new Vector2(Main.screenWidth, Main.screenHeight);  
                    first = false;
                }
                if(v.Y > (Main.screenHeight / 2f) - 224){
                    v.Y = v.Y - 30;
                }
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 200, v.Y + 62), Color.White);
            }
        }

        class PADDFrame : UIElement
        {
            public bool first = true;
            public Vector2 v;

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/PADD_Frame");

            public override void Draw(SpriteBatch spriteBatch)
            {
                if(first) {
                    //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                    v = new Vector2(Main.screenWidth, Main.screenHeight);  
                    first = false;
                }
                if(v.Y > (Main.screenHeight / 2f) - 224){
                    v.Y = v.Y - 30;
                }
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) - 366, v.Y - 80), Color.White);
            }  
        }
        
    }   