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
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Map;
using Terraria.DataStructures;
using System;
using System.Windows.Input;
using static Terraria.ModLoader.ModContent;

namespace ATB.Items
{
    class TholianMeshLauncher:ModItem
	{
		public int proj = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("My mind to your mind...");
            DisplayName.SetDefault("Vulcan Mind Meld");
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.noMelee = true;
			Item.channel = false;
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 36;
			Item.height = 33;
			Item.useTime = 15;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = 5;
			Item.shootSpeed = 10f;
			Item.useAnimation = 15;
			Item.shoot = ProjectileType<TholianMeshSpinner>();
			Item.value = Item.sellPrice(silver: 3);
            Item.noUseGraphic = false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			const int NumProjectiles = 3; // The humber of projectiles that this gun will shoot.

			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 30 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(60));

				// Decrease velocity randomly for nicer visuals.
				newVelocity *= 1f - Main.rand.NextFloat(0.6f);

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, 0f, player.whoAmI);
			}

			return false; // Return false because we don't want tModLoader to shoot projectile
		}

	}
}