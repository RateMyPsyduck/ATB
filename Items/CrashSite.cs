using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ATB.Items
{

	public class CrashSite : ModSystem
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {
			// Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

			// The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
			// First, we find out which step "Shinies" is.
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

			if (ShiniesIndex != -1) {
				// Next, we insert our pass directly after the original "Shinies" pass.
				// ExampleOrePass is a class seen bellow
				tasks.Insert(ShiniesIndex - 1, new CrashSitePass("Crashing!", 2080f));
			}
		}

		public override void PreUpdateWorld() {
            // Main.NewText("Test");
		}
	}

	public class CrashSitePass : GenPass
	{
		public CrashSitePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			// progress.Message is the message shown to the user while the following code is running.
			// Try to make your message clear. You can be a little bit clever, but make sure it is descriptive enough for troubleshooting purposes.
			progress.Message = "Crashing Shuttle";

			// Ores are quite simple, we simply use a for loop and the WorldGen.TileRunner to place splotches of the specified Tile in the world.
			// "6E-05" is "scientific notation". It simply means 0.00006 but in some ways is easier to read.
				// The inside of this for loop corresponds to one single splotch of our Ore.
				// First, we randomly choose any coordinate in the world by choosing a random x and y value.
				int x = WorldGen.genRand.Next(Main.spawnTileX - 70, Main.spawnTileX + 70);
				while(Math.Abs(x - Main.spawnTileX) < 20){
					x = WorldGen.genRand.Next(Main.spawnTileX - 70, Main.spawnTileX + 70);
				}

				// WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
				int y = Main.spawnTileY;

                int SizeRandX = 20;
                int SizeRandY = 50;

				// Then, we call WorldGen.TileRunner with random "strength" and random "steps", as well as the Tile we wish to place.
				// Feel free to experiment with strength and step to see the shape they generate.


				WorldGen.KillWall(x, y + 1, false);
				WorldGen.KillWall(x, y + 2, false);
				WorldGen.KillWall(x, y + 3, false);
				WorldGen.KillWall(x, y + 4, false);
				WorldGen.KillWall(x - 1, y + 1, false);
				WorldGen.KillWall(x - 1, y + 2, false);
				WorldGen.KillWall(x - 1, y + 3, false);
				WorldGen.KillWall(x - 1, y + 4, false);

				WorldGen.KillWall(x + 1, y + 1, false);
				WorldGen.KillWall(x + 1, y + 2, false);
				WorldGen.KillWall(x + 1, y + 3, false);

				WorldGen.KillWall(x - 2, y + 1, false);
				WorldGen.KillWall(x - 2, y + 2, false);
				WorldGen.KillWall(x - 2, y + 3, false);

				WorldGen.KillWall(x - 3, y + 1, false);
				WorldGen.KillWall(x - 3, y + 2, false);

				WorldGen.KillWall(x + 2, y + 1, false);
				WorldGen.KillWall(x + 2, y + 2, false);


				//WorldGen.TileRunner(x, y, SizeRandX, SizeRandY, -1, false);

				circle(x, y + 10);

				// for(int xt = x - 20; xt < x + 20; xt++){
				// 	for(int yt = y - 20; yt < y + 20; yt++){
				// 		if((Framing.GetTileSafely(xt, yt).HasTile == true && Framing.GetTileSafely(xt, yt).TileType == 0) && Framing.GetTileSafely(xt, yt - 1).HasTile == false){
				// 			WorldGen.PlaceTile(xt, yt -1, 284);
				// 		}
				// 		if(Framing.GetTileSafely(xt, yt).TileType == 5){
				// 			WorldGen.KillTile(xt,yt,false);
				// 		}
				// 	}
				// }



				WorldGen.PlaceTile(x,y, ModContent.TileType<Items.replicator>());

				// Alternately, we could check the tile already present in the coordinate we are interested.
				// Wrapping WorldGen.TileRunner in the following condition would make the ore only generate in Snow.
				// Tile tile = Framing.GetTileSafely(x, y);
				// if (tile.active() && tile.type == TileID.SnowBlock)
				// {
				// 	WorldGen.TileRunner(.....);
				// }
		}

		private void circle(int xx, int yy){  

			double radius = 5;
			int j = 0;
			for (j = 0; j <= 10; j++)
			{

					radius = j + 1;
					for (double i = 0.0; i < 360.0; i += 0.1)
					{
						double angle = i * System.Math.PI / 180;
						int x = (int)(5 - radius * System.Math.Cos(angle));
						int y = (int)(5 - radius * System.Math.Sin(angle));
						WorldGen.KillTile(x + xx,y + yy - 18, false);
					}
			}
			for (int p = 0; p < 21; p++){
				for(int l = yy; l < 0; l--){
					WorldGen.KillTile(p + xx, l, false);
				}
			}
			WorldGen.PlaceTile(xx + 5, yy - 2, (Framing.GetTileSafely(xx - 1, yy).TileType));
			//WorldGen.PlaceTile(xx + 5, yy - 1, ModContent.TileType<replicator>());
			WorldGen.PlaceTile(xx + 5, yy - 3, ModContent.TileType<ShuttleHead>());
		}
	}
}
