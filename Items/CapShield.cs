using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ATB.Items;
using Terraria.Net;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Creative;

namespace ATB.Items
{
	public class CapShield : ModItem	
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("This is a modded minigun.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			// Common Properties
			Item.width = 27; // Hitbox width of the item.
			Item.height = 19; // Hitbox height of the item.
			Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 5; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
			 // The sound that this item plays when used.

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 11; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 1f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ModContent.ProjectileType<Items.ShieldProjectile>(); // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 16f; // The speed of the projectile (measured in pixels per frame.)
			//Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
		}

		public override bool AltFunctionUse(Player player)//You use this to allow the Item to be right clicked
		{
			return true;
		}

		public override bool CanUseItem(Player player){
			if (player.altFunctionUse == 2 && player.ownedProjectileCounts[Item.shoot] < 1)//Sets what happens on right click(special ability)
			{
				Item.noUseGraphic = true;
				Item.shootSpeed = 20f; // how quickly the hook is shot.
				Item.shoot = ModContent.ProjectileType<Items.ShieldProjectile>();
				Main.NewText("Throw", 150, 250, 150);
				return player.ownedProjectileCounts[Item.shoot] < 1;
			}
			else{
				TryTogglingShield(true, player);
				
			}
			Main.NewText("Error", 150, 250, 150);
			return false;
			
		}

		

		public void TryTogglingShield(bool shouldGuard, Player player)
		{
			player.shield = 9;
			player.cShield = -1;
			if (shouldGuard == player.shieldRaised)
			{
				return;
			}
			player.shieldRaised = shouldGuard;
			if (player.shieldRaised)
			{
				if (player.shield_parry_cooldown == 0)
				{
					player.shieldParryTimeLeft = 1;
				}
				player.itemAnimation = 0;
				player.itemTime = 0;
				player.reuseDelay = 0;
			}
			else
			{
				player.shield_parry_cooldown = 15;
				player.shieldParryTimeLeft = 0;
				if (player.attackCD < 20)
				{
					player.attackCD = 20;
				}
			}
	}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		// public override void AddRecipes() {
		// 	CreateRecipe()
		// 		.AddIngredient<ExampleItem>()
		// 		.AddTile<Tiles.Furniture.ExampleWorkbench>()
		// 		.Register();
		// }

		// The following method gives this gun a 38% chance to not consume ammo
		// public override bool CanConsumeAmmo(Item ammo, Player player) {
		// 	return Main.rand.NextFloat() >= 0.38f;
		// }

		// The following method allows this gun to shoot when having no ammo, as long as the player has at least 10 example items in their inventory.
		// The gun will then shoot as if the default ammo for it, in this case the musket ball, is being used.
		// public override bool NeedsAmmo(Player player) {
		// 	return player.CountItem(ModContent.ItemType<ExampleItem>(), 10) < 10;
		// }

		// The following method makes the gun slightly inaccurate
		// public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		// 	velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
		// }

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset() {
			return new Vector2(0, 0);
		}
	}
}
	
	