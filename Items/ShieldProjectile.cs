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
    class ShieldProjectile:ModProjectile
	{
		public override void SetDefaults()
		{
			//Projectile.aiStyle = 5;
			//Projectile.CloneDefaults(ItemID.LightDisc);
			Projectile.aiStyle = 3;
			Projectile.height = 27;
			Projectile.width = 27;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 0;
			Projectile.penetrate = 5000;
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.localNPCHitCooldown = 20; // up to 20 hits
		}


		public override bool OnTileCollide(Vector2 oldVelocity) {
			// If collide with tile, reduce the penetrate.
			// So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
					Projectile.velocity.X = -oldVelocity.X;
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}

			return false;
		}

			public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.immune[Projectile.owner] = 20;
			// 3b: target.immune[Projectile.owner] = 5;
		}
	}
}