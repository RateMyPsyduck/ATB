using System;
using Terraria;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using ReLogic.Content;

using static Terraria.ModLoader.ModContent;

namespace ATB.Items
{
    class Phaser:ModItem
	{
		public int proj = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoot a laser beam that can eliminate anything...");
		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.noMelee = true;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 5;
			Item.shootSpeed = 0.01f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<PhaserBeam>();
			Item.value = Item.sellPrice(silver: 3);
			Item.scale = 0.8f;
		}

		public override bool AltFunctionUse(Player player) {
			return true;
		}

		// public override bool? UseItem(Player player) {
		// 	proj++;
		// 	if(proj > 1){
		// 		proj = 0;
		// 	}
		// 	return false;
		// }

		// public override void ModifyShootStats (Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback){
		// 	if(proj == 0){
		// 		type = ProjectileType<PhaserBeam>();
		// 	}
		// 	else if(proj == 1){
		// 		type = ProjectileType<PhaserStun>();
		// 	}
		// }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
