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
			Item.useTime = 14;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = 3;
			Item.shootSpeed = 6f;
			Item.useAnimation = 14;
			Item.value = Item.sellPrice(silver: 3);
			Item.scale = 1f;
			Item.holdStyle = 1;
		}

        public override bool? UseItem(Player player) {
			if(timesUsed % 2 == 0){
				Item.useStyle = 1;
			}
			else
			{
				Vector2 jump = new Vector2(0,0);
				jump.X = player.direction * 5;
				jump.Y = 0;
				player.velocity = jump;
				player.immune = true;
				player.immuneTime = 18;
				player.immuneAlpha = 0;
				player.immuneNoBlink = true;
				Item.useStyle = 3;
			}
            return true;
        }

		public override bool CanUseItem(Player player){
			timesUsed++;
			return true;
		}

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
		{
			player.itemLocation = player.Center + new Vector2((6 * player.direction), + 6);
		}
	}
}
