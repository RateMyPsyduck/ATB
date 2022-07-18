using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace ATB.Items
{
	public class Foil : ModItem
	{
        int timesUsed = 0;
        bool useMode = false;

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Foil");
		}

		public override void SetDefaults() {
			Item.damage = 25;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 12;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 6f;
			Item.useAnimation = 12;
			Item.value = Item.sellPrice(silver: 3);
			Item.scale = 1f;
		}

        // public override bool? UseItem(Player player) {
        //     if(useMode == false){
        //         timesUsed++;
        //         Main.NewText(timesUsed.ToString());
        //         if(timesUsed % 2 == 0){
        //             return false;
        //         }
        //         return null;
        //     }
        //     useMode == true;
        //     return false;
        // }

		public override Vector2? HoldoutOffset()
        {
			Vector2 g;
            g.Y = -1;
            g.X = -3.5f;
			return g;

        }
	}
}
