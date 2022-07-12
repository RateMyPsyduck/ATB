using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ATB.Items
{
	// This Example show how to implement simple homing Projectile
	// Can be tested with ExampleCustomAmmoGun
	public class MindMeldRing : ModProjectile
	{
        int timer = 0;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Psionic Energy"); // Name of the Projectile. It can be appear in chat
		}

		// Setting the default parameters of the Projectile
		// You can check most of Fields and Properties here https://github.com/tModLoader/tModLoader/wiki/Projectile-Class-Documentation
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
			Projectile.timeLeft = 30; // The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.scale = 0.5f;
		}

		// // Custom AI
		// // public override void AI() {
		// // 	float maxDetectRadius = 300f; // The maximum radius at which a Projectile can detect a target
		// // 	float projSpeed = 12f; // The speed at which the Projectile moves towards the target
        // //     NPC closestNPC = null;

		// // 	// Trying to find NPC closest to the Projectile
        // //     if(timer > 20){
		// // 	    closestNPC = FindClosestNPC(maxDetectRadius);
        // //     }
        // //     else{
        // //         timer++;
        // //     }

		// // 	if (closestNPC == null)
		// // 		return;

		// // 	// If found, change the velocity of the Projectile and turn it in the direction of the target
		// // 	// Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
        // //     if(Projectile.velocity !=  (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed){
        // //         Projectile.velocity += ((closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed) / 10;
        // //     }
		// // 	//Projectile.velocity =  (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
		// // 	Projectile.rotation = Projectile.velocity.ToRotation();
		// // }

		// // Finding the closest NPC to attack within maxDetectDistance range
		// // If not found then returns null
		// public NPC FindClosestNPC(float maxDetectDistance) {
		// 	NPC closestNPC = null;

		// 	// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
		// 	float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

		// 	// Loop through all NPCs(max always 200)
        //         for (int k = 0; k < Main.maxNPCs; k++) {
        //             NPC target = Main.npc[k];
        //             // Check if NPC able to be targeted. It means that NPC is
        //             // 1. active (alive)
        //             // 2. chaseable (e.g. not a cultist archer)
        //             // 3. max life bigger than 5 (e.g. not a critter)
        //             // 4. can take damage (e.g. moonlord core after all it's parts are downed)
        //             // 5. hostile (!friendly)
        //             // 6. not immortal (e.g. not a target dummy)
        //             if (target.CanBeChasedBy()) {
        //                 // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
        //                 float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

        //                 // Check if it is within the radius
        //                 if (sqrDistanceToTarget < sqrMaxDetectDistance) {
        //                     sqrMaxDetectDistance = sqrDistanceToTarget;
        //                     closestNPC = target;
        //                 }
        //             }
        //         }
		// 	return closestNPC;
		// }

        // public override void Kill(int timeLeft){
        //     Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, 696, Projectile.damage, Projectile.knockBack * 50, Main.myPlayer, 0f, 0f);
		// 	// Main.Projectile[proj].timeLeft = 30;
		// 	// Main.Projectile[proj].netUpdate = true;
		// 	//Projectile.netUpdate = true;
		// 	// Point scanAreaStart = Projectile.TopLeft.ToTileCoordinates();
		// 	// Point scanAreaEnd = Projectile.BottomRight.ToTileCoordinates();
        //     // Projectile.CreateImpactExplosion(2, Projectile.Center, ref scanAreaStart, ref scanAreaEnd, Projectile.width, out bool causedShockwaves);
        //     // Projectile.ExplodeTiles(Projectile.Center * 16, 100, 100, 100, 100, 100, true);
        //     Main.NewText(Main.tile[(int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f)].ToString(), 100, 0 , 0);
		// 		int explosionRadius = 3;
		// 		//if (Projectile.type == 29 || Projectile.type == 470 || Projectile.type == 637)
		// 		// {
		// 		// 	explosionRadius = 7;
		// 		// }
		// 		int minTileX = (int)(Projectile.position.X / 16f - (float)explosionRadius);
		// 		int maxTileX = (int)(Projectile.position.X / 16f + (float)explosionRadius);
		// 		int minTileY = (int)(Projectile.position.Y / 16f - (float)explosionRadius);
		// 		int maxTileY = (int)(Projectile.position.Y / 16f + (float)explosionRadius);
		// 		if (minTileX < 0) {
		// 			minTileX = 0;
		// 		}
		// 		if (maxTileX > Main.maxTilesX) {
		// 			maxTileX = Main.maxTilesX;
		// 		}
		// 		if (minTileY < 0) {
		// 			minTileY = 0;
		// 		}
		// 		if (maxTileY > Main.maxTilesY) {
		// 			maxTileY = Main.maxTilesY;
		// 		}
		// 		bool canKillWalls = false;
		// 		for (int x = minTileX; x <= maxTileX; x++) {
		// 			for (int y = minTileY; y <= maxTileY; y++) {
		// 				float diffX = Math.Abs((float)x - Projectile.position.X / 16f);
		// 				float diffY = Math.Abs((float)y - Projectile.position.Y / 16f);
		// 				double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
		// 				if (distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].WallType == 0) {
		// 					canKillWalls = true;
		// 					break;
		// 				}
		// 			}
		// 		}
		// 		AchievementsHelper.CurrentlyMining = true;
		// 		for (int i = minTileX; i <= maxTileX; i++) {
		// 			for (int j = minTileY; j <= maxTileY; j++) {
		// 				float diffX = Math.Abs((float)i - Projectile.position.X / 16f);
		// 				float diffY = Math.Abs((float)j - Projectile.position.Y / 16f);
		// 				double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
		// 				if (distanceToTile < (double)explosionRadius) {
		// 					bool canKillTile = true;
		// 					if (Main.tile[i, j] != null && Main.tile[i, j].HasTile) {
		// 						canKillTile = true;
		// 						if (Main.tileDungeon[(int)Main.tile[i, j].TileType] || Main.tile[i, j].TileType == 88 || Main.tile[i, j].TileType == 21 || Main.tile[i, j].TileType == 26 || Main.tile[i, j].TileType == 107 || Main.tile[i, j].TileType == 108 || Main.tile[i, j].TileType == 111 || Main.tile[i, j].TileType == 226 || Main.tile[i, j].TileType == 237 || Main.tile[i, j].TileType == 221 || Main.tile[i, j].TileType == 222 || Main.tile[i, j].TileType == 223 || Main.tile[i, j].TileType == 211 || Main.tile[i, j].TileType == 404) {
		// 							canKillTile = false;
		// 						}
		// 						if (!Main.hardMode && Main.tile[i, j].TileType == 58) {
		// 							canKillTile = false;
		// 						}
		// 						if (!TileLoader.CanExplode(i, j)) {
		// 							canKillTile = false;
		// 						}
		// 						if (canKillTile) {
		// 							WorldGen.KillTile(i, j, false, false, false);
		// 							if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer) {
		// 								NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
		// 							}
		// 						}
		// 					}
		// 					if (canKillTile) {
		// 						for (int x = i - 1; x <= i + 1; x++) {
		// 							for (int y = j - 1; y <= j + 1; y++) {
		// 								if (Main.tile[x, y] != null && Main.tile[x, y].WallType > 0 && canKillWalls && WallLoader.CanExplode(x, y, Main.tile[x, y].WallType)) {
		// 									WorldGen.KillWall(x, y, false);
		// 									if (Main.tile[x, y].WallType == 0 && Main.netMode != NetmodeID.SinglePlayer) {
		// 										NetMessage.SendData(MessageID.TileChange, -1, -1, null, 2, (float)x, (float)y, 0f, 0, 0, 0);
		// 									}
		// 								}
		// 							}
		// 						}
		// 					}
		// 				}
		// 			}
		// 		}
		// 		AchievementsHelper.CurrentlyMining = false;
        // }

        // // public bool OnTileCollide(Vector2 oldVelocity){
        // //     Main.NewText("COLLIDE", 150, 0, 0);
        // //     Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 1, 1, 5, Projectile.damage, Projectile.knockBack * 50, Main.myPlayer, 0f, 0f);
		// // 	// Main.Projectile[proj].timeLeft = 30;
		// // 	// Main.Projectile[proj].netUpdate = true;
		// // 	// Projectile.netUpdate = true;
        // //     return true;
        // // }
	}
}
