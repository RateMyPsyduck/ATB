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

namespace ATB.Items
{
    class PhaserBeam:ModProjectile
	{
		bool NCPFLAG = false;
		public int Timer;
		bool TimerTrig = false;
		bool TimerFin = false;
		// Use a different style for constant so it is very clear in code when a constant is used

		// The maximum charge value
		private const float MAX_CHARGE = 0f;
		//The distance charge particle from the player center
		private const float MOVE_DISTANCE = 50f;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// The actual charge value is stored in the localAI0 field
		public float Charge {
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
		public bool IsAtMaxCharge => Charge == MAX_CHARGE;

		public override void SetDefaults() {
			Projectile.width = 2;
			Projectile.height = 2;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = true;
			Projectile.hide = true;
            Projectile.scale = 0.3f;
		}

		public override bool PreDraw(ref Color lightColor) {
			// We start drawing the laser if we have charged up
			if (IsAtMaxCharge) {
				DrawLaser(TextureAssets.Projectile[Projectile.type].Value, Main.player[Projectile.owner].Center,
					Projectile.velocity, 2, Projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			}
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 1000f, Color color = default(Color), int transDist = 50) {
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step) {
				Color c = Color.Orange;
				var origin = start + i * unit;
				Main.spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 2, 2, 2), i < transDist ? Color.Transparent : c, r,
					new Vector2(0 * .5f, 60 * .5f), scale, 0, 0);
			}

			// Draws the laser 'tail'
			Main.spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 2, 2, 2), Color.Orange, r, new Vector2(0 * .5f, 60 * .5f), scale, 0, 0);

			// Draws the laser 'head'
			Main.spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 2, 2, 2), Color.Orange, r, new Vector2(0 * .5f, 60 * .5f), scale, 0, 0);
				

		}

		// Change the way of collision check of the Projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			// We can only collide if we are at max charge, which is when the laser is actually fired
			// if (!IsAtMaxCharge) return false;

			Player player = Main.player[Projectile.owner];
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				player.Center + unit * Distance, 22, ref point);
		}

		// Set custom immunity time on hitting an NPC
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.immune[Projectile.owner] = 10;
			NCPFLAG = true;
		}

		// The AI of the Projectile
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
			Projectile.timeLeft = 2;

			// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
			// First we update player variables that are needed to channel the laser
			// Then we run our charging laser logic
			// If we are fully charged, we proceed to update the laser's position
			// Finally we spawn some effects like dusts and light

			UpdatePlayer(player);
			ChargeLaser(player);

			// If laser is not charged yet, stop the AI here.
			if (Charge < MAX_CHARGE) return;

			SetLaserPosition(player);
			SpawnDusts(player);
			CastLights();
		}

	    public override bool OnTileCollide(Vector2 velocityChange)  //On Contact do an action
        {
            //Nums for producing the radius of the effect, this setting should work for at least one tile to be destroyed
            // int num101 = (int)(Projectile.position.X / 16f) - 2;
            // int num102 = (int)((Projectile.position.X + (float)1) / 16f) + 1;
            // int num103 = (int)(Projectile.position.Y / 16f) - 2;
            // int num104 = (int)((Projectile.position.Y + (float)1) / 16f) + 1;
            // if (num101 < 0)
            // {
            //     num101 = 0;
            // }
            // if (num102 > Main.maxTilesX)
            // {
            //     num102 = Main.maxTilesX;
            // }
            // if (num103 < 0)
            // {
            //     num103 = 0;
            // }
            // if (num104 > Main.maxTilesY)
            // {
            //     num104 = Main.maxTilesY;
            // }

            // //Searches at the radius
            // for (int num105 = num101; num105 < num102; num105++)
            // {
            //     for (int num106 = num103; num106 < num104; num106++)
            //     {
            //         WorldGen.KillTile(num105, num106, false, false, false); //kills tiles
            //     }
            // }
            return false; //Makes sure the projectile dies on tile collide.
        }

		private void SpawnDusts(Player player) {
			Vector2 tempVel = Projectile.velocity;
			Vector2 unit = Projectile.velocity * -1;
			Vector2 dustPos = player.Center + tempVel * (Distance - 23);

			for (int i = 0; i < 2; ++i) {
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 6, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.5f;
				dust.color = Color.Orange;
				
				// dust = Dust.NewDustDirect(Main.player[Projectile.owner].Center, 0, 0, 31,
				// 	-unit.X * Distance, -unit.Y * Distance);
				// dust.fadeIn = 0f;
				// dust.noGravity = true;
				// dust.scale = 0.88f;
				// dust.color = Color.Orange;
			}

			// if (Main.rand.NextBool(5)) {
			// 	Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
			// 	Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
			// 	dust.velocity *= 0.5f;
			// 	dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			// 	unit = dustPos - Main.player[Projectile.owner].Center;
			// 	unit.Normalize();
			// 	dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
			// 	dust.velocity = dust.velocity * 0.5f;
			// 	dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			// }
		}

		/*
		* Sets the end of the laser position based on where it collides with something
		*/
		private void SetLaserPosition(Player player) {
			bool Flag = false;
			float point = 0f;
			for (Distance = MOVE_DISTANCE; Distance <= 1500f; Distance += 5f) {
				var start = player.Center + Projectile.velocity * Distance;
				for(int i = 0; i < 200; i++){
					if(Collision.CheckAABBvLineCollision(Main.npc[i].getRect().Center(), Main.npc[i].getRect().Size(), player.Center, player.Center + Projectile.velocity * Distance, 22, ref point) && Main.npc[i].active == true){
						Flag = true;
						// Main.NewText("Colliding with: " + Main.npc[i].ToString(), 150, 250, 0);
						// Main.NewText(Projectile.velocity.ToString(), 150, 0, 0);
						// Main.NewText(Main.npc[i].FullName, 150, 0, 0);
						break;
					}
				}
				if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1) || Flag == true) {
					Distance += 20f;
					//GivenName
					//Main.NewText(Main.npc[i].FullName, 150, 0, 0);
                    Main.NewText(Projectile.velocity.ToString(), 150, 250, 150);
                    //Main.NewText(start.ToString(), 0, 250, 150);
                    //Main.NewText(Projectile.velocity.ToString(), 150, 0, 0);
					int num101 = (int)((player.Center + Projectile.velocity * Distance).X / 16f) - 2;
					int num102 = (int)(((player.Center + Projectile.velocity * Distance).X + (float)1) / 16f) + 1;
					int num103 = (int)((player.Center + Projectile.velocity * Distance).Y / 16f) - 2;
					int num104 = (int)(((player.Center + Projectile.velocity * Distance).Y + (float)1) / 16f) + 1;
					if (num101 < 0)
					{
						num101 = 0;
					}
					if (num102 > Main.maxTilesX)
					{
						num102 = Main.maxTilesX;
					}
					if (num103 < 0)
					{
						num103 = 0;
					}
					if (num104 > Main.maxTilesY)
					{
						num104 = Main.maxTilesY;
					}

					//Searches at the radius
					for (int num105 = num101; num105 < num102; num105++)
					{
						for (int num106 = num103; num106 < num104; num106++)
						{
							Timer++;
							if(Timer > 50){
								WorldGen.KillTile(num105, num106, false, false, false); //kills tiles
								Timer = 0;
							}
						}
					}
					break;
				}
                else{
                    Main.NewText("Not Colliding", 150, 0, 0);
                }
			}
		}

		private void ChargeLaser(Player player) {
			// Kill the Projectile if the player stops channeling
			if (!player.channel) {
				Projectile.Kill();
			}
			else {
				// Do we still have enough mana? If not, we kill the Projectile because we cannot use it anymore
				// if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true)) {
				// 	Projectile.Kill();
				// }
				Vector2 offset = Projectile.velocity;
				offset *= MOVE_DISTANCE - 20;
				Vector2 pos = player.Center + offset - new Vector2(10, 10);
				if (Charge < MAX_CHARGE) {
					Charge++;
				}
				int chargeFact = (int)(Charge / 20f);
				// Vector2 dustVelocity = Vector2.UnitX * 18f;
				// dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
				// Vector2 spawnPos = Projectile.Center + dustVelocity;
				// for (int k = 0; k < chargeFact + 1; k++) {
				// 	Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
				// 	Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, 226, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
				// 	dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
				// 	dust.noGravity = true;
				// 	dust.scale = Main.rand.Next(10, 20) * 0.05f;
				// }
			}
		}

		private void UpdatePlayer(Player player) {
			// Multiplayer support here, only run this code if the client running it is the owner of the Projectile
			if (Projectile.owner == Main.myPlayer) {
				Vector2 diff = Main.MouseWorld - player.Center;
				diff.Normalize();
				Projectile.velocity = diff;
				Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
				Projectile.netUpdate = true;
			}
			int dir = Projectile.direction;
			player.ChangeDir(dir); // Set player direction to where we are shooting
			player.heldProj = Projectile.whoAmI; // Update player's held Projectile
			player.itemTime = 2; // Set Item time to 2 frames while we are used
			player.itemAnimation = 2; // Set Item animation time to 2 frames while we are used
			player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the Item rotation to where we are shooting
		}

		private void CastLights() {
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		/*
		* Update CutTiles so the laser will cut tiles (like grass)
		*/
		public override void CutTiles() {
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}

		public override void PostAI(){
			NCPFLAG = false;
		}
	}
}
