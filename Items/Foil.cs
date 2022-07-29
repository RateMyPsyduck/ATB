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
			Item.useTime = 13;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 6f;
			Item.useAnimation = 13;
			Item.value = Item.sellPrice(silver: 3);
			Item.scale = 1f;
			Item.holdStyle = 1;
		}

        public override bool? UseItem(Player player) {
			timesUsed++;
			if(timesUsed % 2 == 0){
				Item.useStyle = 1;
			}
			else
			{
				Main.NewText("HERE");
				Item.useStyle = 3;
				player.ApplyItemAnimation(ModContent.ItemType<CapShield>());
			}
            return true;
        }

		public override Vector2? HoldoutOffset()
        {
			Vector2 g;
            g.Y = -1;
            g.X = -3.5f;
			return g;

        }


        public override void HoldStyle(Player player, Rectangle heldItemFrame)
		{
			player.itemLocation = player.Center + new Vector2((6 * player.direction), + 6);
		}
	}
}
