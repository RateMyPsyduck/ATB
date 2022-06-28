using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

    namespace ATB.Items
    {
        class LCARS : UIElement
        {
            Color color = new Color(0, 0, 0, 100);
            Vector2 v = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;  

            Asset<Texture2D> Front = ModContent.Request<Texture2D>($"ATB/Items/LCARS_Front");
            Asset<Texture2D> Back = ModContent.Request<Texture2D>($"ATB/Items/LCARS_Back");

            public override void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw((Texture2D)Back, new Vector2(v.X - 300, v.Y - 225), Color.White);
                spriteBatch.Draw((Texture2D)Front, new Vector2(v.X - 300, v.Y - 225), Color.White);
            }   
        }
        
    }   