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
	class UISystem : ModSystem 
	{
        public static UISystem Instance { get; private set; }
        internal Display PADD;
        internal UserInterface inter;
        bool flip = false;

        public override void Load()
        {
            if (!Main.dedServ) {
                PADD = new Display();
                PADD.Activate();
                inter = new UserInterface();
                //_menuBar.SetState(MenuBar);
            }
        }

        public override void Unload(){
            // MyUI?.SomeKindOfUnload(); // If you hold data that needs to be unloaded, call it in OO-fashion
            // MyUI = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            inter?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate
                    {
                        inter.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        internal void ShowMyUI() {
            //inter.SetState(PAD);
            if(flip == false){
                inter.SetState(PADD);
                flip = true;
            }
            else{
                inter.SetState(null);
                PADD.LCARS.v.Y = Main.screenHeight;
                PADD.LCARS.first = true;
                PADD.SaveButton.v.Y = Main.screenHeight;
                PADD.SaveButton.first = true;
                PADD.PrevButton.v.Y = Main.screenHeight;
                PADD.PrevButton.first = true;
                PADD.NextButton.v.Y = Main.screenHeight;
                PADD.NextButton.first = true;
                PADD.DeleteButton.v.Y = Main.screenHeight;
                PADD.DeleteButton.first = true;
                PADD.PADDFrame.v.Y = Main.screenHeight;
                PADD.PADDFrame.first = true;
                flip = false;
            }
        }

        internal void HideMyUI() {
            inter.SetState(null);
        }


    }
}