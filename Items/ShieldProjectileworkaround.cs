using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ATB.Items
{
    class ShieldProjectileworkaround:ModProjectile
	{
		public override void SetDefaults()
		{
			//Projectile.aiStyle = 5;
			//Projectile.CloneDefaults(ItemID.LightDisc);
			Projectile.aiStyle = 3;
			Projectile.height = 1;
			Projectile.width = 1;
			Projectile.timeLeft = 0;
			Projectile.extraUpdates = 0;
			Projectile.penetrate = 1;
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.localNPCHitCooldown = 20; // up to 20 hits
		}


		public override bool OnTileCollide(Vector2 oldVelocity) {
			// If collide with tile, reduce the penetrate.
			// So the projectile can reflect at most 5 times
            Projectile.Kill();
            return false;
		}

			public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Projectile.Kill();
			// 3b: target.immune[Projectile.owner] = 5;
		}
	}
}