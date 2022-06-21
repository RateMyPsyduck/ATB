using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;
using System.Collections.Generic;   
using System;
// using ExampleMod.Content.Biomes;
// using ExampleMod.Content.Buffs;

namespace ATB.Items
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class Borg : ModNPC
	{
        struct Adaption
        {
            public int proj;
            public int TimesHit;

            public Adaption(int p, int i){
                this.proj = p;
                this.TimesHit = i;

            }
        }
        List<Adaption> adaptions = new List<Adaption>();
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Borg Drone");

			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults() {
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 100;
			NPC.defense = 5;
			NPC.lifeMax = 600;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 3; // Fighter AI, important to choose the aiStyle that matches the NPCID that we want to mimic

			AIType = NPCID.Zombie; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
			AnimationType = NPCID.Zombie; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
			Banner = Item.NPCtoBanner(NPCID.Zombie); // Makes this NPC get affected by the normal zombie banner.
			BannerItem = Item.BannerToItem(Banner); // Makes kills of this NPC go towards dropping the banner it's associated with.
			//SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // Associates this NPC with the ExampleSurfaceBiome in Bestiary
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot) {
			// Since Party Zombie is essentially just another variation of Zombie, we'd like to mimic the Zombie drops.
			// To do this, we can either (1) copy the drops from the Zombie directly or (2) just recreate the drops in our code.
			// (1) Copying the drops directly means that if Terraria updates and changes the Zombie drops, your ModNPC will also inherit the changes automatically.
			// (2) Recreating the drops can give you more control if desired but requires consulting the wiki, bestiary, or source code and then writing drop code.

			// (1) This example shows copying the drops directly. For consistency and mod compatibility, we suggest using the smallest positive NPCID when dealing with npcs with many variants and shared drop pools.
			// var zombieDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.Zombie, false); // false is important here
			// foreach (var zombieDropRule in zombieDropRules) {
			// 	// In this foreach loop, we simple add each drop to the PartyZombie drop pool. 
			// 	npcLoot.Add(zombieDropRule);
			// }

			// (2) This example shows recreating the drops. This code is commented out because we are using the previous method instead.
			// npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 50)); // Drop shackles with a 1 out of 50 chance.
			// npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250)); // Drop zombie arm with a 1 out of 250 chance.

			// Finally, we can add additional drops. Many Zombie variants have their own unique drops: https://terraria.fandom.com/wiki/Zombie
			npcLoot.Add(ItemDropRule.Common(1346, 1)); // 1% chance to drop Confetti
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return SpawnCondition.OverworldNightMonster.Chance * 0.2f; // Spawn with 1/5th the chance of a regular zombie.
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("This type of zombie for some reason really likes to spread confetti around. Otherwise, it behaves just like a normal zombie.")
			});
		}

        public override void ModifyHitByProjectile (Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
        bool news = true;
        for(int i = 0; i < adaptions.Count; i++){

            if(adaptions[i].proj == projectile.type){
               // Main.NewText("Here with " + adaptions[i].proj.ToString() + " and " + projectile.type.ToString() + " at i =" + i.ToString(), 150, 0, 0);
                news = false;
                Adaption temp =  adaptions[i];
                int tempi = temp.TimesHit;
                tempi++;
                temp.TimesHit = tempi;
                adaptions[i] = temp;
                damage = damage / (adaptions[i].TimesHit / 2);
                break;
            }
        }
        if(news == true){
            adaptions.Add(new Adaption(projectile.type, 1));
        }
        //Main.NewText("Adding?: " +news.ToString(), 150, 0, 0);
        
          //Main.NewText("Number of Projectile Types: " +adaptions.Count.ToString(), 150, 0, 0);
          //Main.NewText("Times Hit: " + adaptions[0].TimesHit.ToString(), 150,
        //   if(adaptions.Count > 0){
        //     Main.NewText(adaptions[0].proj.type.ToString(), 150, 0, 0);
        //   }
          for(int i = 0; i < adaptions.Count; i++){
            Main.NewText(adaptions[i].TimesHit.ToString(), 150, 0, 0);
          }
          if(damage <= 1){
            damage = 0;
          }

        //    Main.NewText(projectile.type.ToString(), 150, 0, 0);
        }
    }
}