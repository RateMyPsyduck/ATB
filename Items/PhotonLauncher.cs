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
    class PhotonLauncher:ModItem
	{
		public int proj = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("The power of the sun in the palm of my hand!");
            DisplayName.SetDefault("Photon Launcher");
		}

		public override void SetDefaults() {
			Item.damage = 65;
			Item.noMelee = true;
			Item.channel = false; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 5;
			Item.shootSpeed = 12f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<Photon>();
			Item.value = Item.sellPrice(gold: 11);
			Item.scale = 1f;
			Item.UseSound = new SoundStyle($"{nameof(ATB)}/Items/PhotonLaunch") {
				Volume = 0.9f,
				PitchVariance = 2f,
				MaxInstances = 3,
			};
		}

		public override bool AltFunctionUse(Player player) {
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
