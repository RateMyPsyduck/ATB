using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ATB.Items
{
	public class TholianShockwaveSpear : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires a sonic blast.");
            DisplayName.SetDefault("Tholian Shockwave Spear");
		}

		public override void SetDefaults() {
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 52;
			Item.height = 50;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Rapier;
			Item.knockBack = 6;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = new SoundStyle($"{nameof(ATB)}/Items/TholianShockwaveSpearSound") {
				Volume = 0.5f,
				PitchVariance = 1f,
				MaxInstances = 1,
			};
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
			Item.noMelee = true; // The projectile will do the damage and not the item Automatically re-swing/re-use this item after its swinging animation is over.

			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(0, 0, 0, 10);

			Item.shoot = ModContent.ProjectileType<TholianShockwaveSpearProj>(); // The projectile is what makes a shortsword work
			Item.shootSpeed = 2.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
		}

		public override bool? UseItem(Player player) {
			//player.velocity = new Vector2(10, -10);
			Vector2 jump = Main.MouseWorld - player.Center;
			jump.Normalize();
			jump.X = jump.X * 10;
			jump.Y = jump.Y * 15;
			player.velocity = jump;
			player.immune = true;
			player.immuneTime = 18;
			player.immuneAlpha = 0;
			player.immuneNoBlink = true;
			player.noFallDmg = true;
			// player.headPosition = new Vector2(100, 10);
			// player.legPosition = new Vector2(100, 10);
			return true;
		}
	}
}
