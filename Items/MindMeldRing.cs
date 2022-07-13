using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ATB.Items
{

	public class MindMeldRing : ModProjectile
	{
        int timer = 0;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Psionic Energy"); // Name of the Projectile. It can be appear in chat
		}

		public override void SetDefaults() {
			Projectile.width = 12; // The width of Projectile hitbox
			Projectile.height = 33; // The height of Projectile hitbox

			Projectile.aiStyle = 72; // The ai style of the Projectile (0 means custom AI). For more please reference the source code of Terraria
			Projectile.DamageType = DamageClass.Magic; // What type of damage does this Projectile affect?
			Projectile.friendly = true; // Can the Projectile deal damage to enemies?
			Projectile.hostile = false; // Can the Projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the Projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the Projectile
			Projectile.tileCollide = true; // Can the Projectile collide with tiles?
			Projectile.timeLeft = 25; // The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 60;

		}

    	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.immune[Projectile.owner] = 10;
			target.AddBuff(ModContent.BuffType<Stun>(), 60);
		}
	}
}
