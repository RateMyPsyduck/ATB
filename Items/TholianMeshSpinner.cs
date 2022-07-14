using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ATB.Items
{

	public class TholianMeshSpinner : ModProjectile
	{
		Vector2 preLoc = new Vector2(0,0);

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mesh Spinner"); // Name of the Projectile. It can be appear in chat
		}

		public override void SetDefaults() {
			Projectile.width = 20; // The width of Projectile hitbox
			Projectile.height = 18; // The height of Projectile hitbox
            Projectile.scale = 0.8f;
			Projectile.DamageType = DamageClass.Ranged; // What type of damage does this Projectile affect?
			Projectile.friendly = true; // Can the Projectile deal damage to enemies?
			Projectile.hostile = false; // Can the Projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the Projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the Projectile
			Projectile.tileCollide = true; // Can the Projectile collide with tiles?
			Projectile.timeLeft = 1200; // The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.penetrate = 5;

		}

		public override void AI() {
            Projectile.velocity.X = Projectile.velocity.X * 0.96f;
            Projectile.velocity.Y = Projectile.velocity.Y * 0.96f;
            if(Math.Abs(Projectile.position.X - preLoc.X) < 0.01f && Math.Abs(Projectile.position.Y - preLoc.Y) < 0.01f){
             	Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror, 0f, 0f, 150, Color.White, 1.1f);
            }
			preLoc = Projectile.position;
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
	}
}
