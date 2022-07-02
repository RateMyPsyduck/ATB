using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using Terraria.GameInput;

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
                spriteBatch.Draw((Texture2D)Back, new Vector2((v.X / 2f) - 300, v.Y), Microsoft.Xna.Framework.Color.White);
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) - 300, v.Y), Microsoft.Xna.Framework.Color.White);
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
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 200, v.Y + 96), Microsoft.Xna.Framework.Color.White);
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
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 102, v.Y + 96), Microsoft.Xna.Framework.Color.White);
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
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 102, v.Y + 62), Microsoft.Xna.Framework.Color.White);
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
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) + 200, v.Y + 62), Microsoft.Xna.Framework.Color.White);
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
                spriteBatch.Draw((Texture2D)Front, new Vector2((v.X / 2f) - 384, v.Y - 80), Microsoft.Xna.Framework.Color.White);
            }  
        }

        class PADDMapSquare : UIElement
        {
            bool first = true;
            int x;
            int y;
            public int type;
            Asset<Texture2D> square;
            int start = (int)Main.screenHeight;

            public PADDMapSquare(int x, int y, int type, Asset<Texture2D> square){
                this.x = (int)(x + (Main.screenWidth / 2f) - 620);
                this.y = (int)(y + (Main.screenHeight/ 2f) - 320);
                this.type = type;
                this.square = square;
                // Apply all SetPixel calls
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                // if(first) {
                //     //Main.NewText(Main.screenHeight.ToString() + ", " + v.Y.ToString(), 100, 0 , 0);
                //     v = new Vector2(Main.screenWidth, Main.screenHeight);  
                //     first = false;
                // }
                if(start > y){
                    start = start - 45;
                }
                else{
                    start = y;
                }


                if(this.type == 0){
                    this.type = 20;
                }
                spriteBatch.Draw((Texture2D)square, new Vector2(x,start), new Color(0,0,(type) * 2));
                // PictureBox b = new PictureBox()
                // Graphics g = System.CreateGraphics();
                // g.DrawRectangle(new Pen((0,0,type), 1), x, y, 4, 4);   
            }  
        }

    internal class TextInput : UIElement
    {
        
    }   