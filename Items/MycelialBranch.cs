using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace ATB.Items
{
	public class MycelialBranch : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires a sonic blast.");
            DisplayName.SetDefault("Mycelial Branch");
		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.noMelee = true;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 45;
			Item.height = 52;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 5;
			Item.shootSpeed = 6f;
			Item.useAnimation = 20;
			Item.shoot = 6;
			Item.value = Item.sellPrice(silver: 3);
			Item.scale = 0.8f;
		}

		public override Vector2? HoldoutOffset()
        {
			Vector2 g;
            g.Y = -1;
            g.X = -3.5f;
			return g;

        }
	}
}
