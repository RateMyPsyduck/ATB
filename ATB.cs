using Terraria.GameContent.UI;
using Terraria.ModLoader;
using Terraria;
using ATB.Items;
using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;


namespace ATB
{
	// This is a partial class, meaning some of its parts were split into other files. See ExampleMod.*.cs for other portions.
	public partial class ATB : Mod
	{
		// public const string AssetPath = $"{nameof(ExampleMod)}/Assets/";

		// public static int ExampleCustomCurrencyId;

		public static ModKeybind beamKey;
		public static ModKeybind UIKey;

		public override void Load() {
		// 	// Registers a new custom currency
		// 	ExampleCustomCurrencyId = CustomCurrencyManager.RegisterCurrency(new Content.Currencies.ExampleCustomCurrency(ModContent.ItemType<Content.Items.ExampleItem>(), 999L, "Mods.ExampleMod.Currencies.ExampleCustomCurrency"));
			beamKey = KeybindLoader.RegisterKeybind(this, "Beam", "B");
			UIKey = KeybindLoader.RegisterKeybind(this, "UIup", "L");
			//TextureAssets.Background[21] = ModContent.Request<Texture2D>($"ATB/Items/GornBackground");
			//TextureAssets.Background[108] = ModContent.Request<Texture2D>($"ATB/Items/GornBackground", (AssetRequestMode)2);
			 //TextureAssets.Background[207] = ModContent.Request<Texture2D>($"ATB/Items/GornBackground",  (AssetRequestMode)2);
			//TextureAssets.Background[217] = ModContent.Request<Texture2D>($"ATB/Items/GornBackground", (AssetRequestMode)2);
			// TextureAssets.Background[248] = ModContent.Request<Texture2D>($"ATB/Items/GornBackground");
		}

		// public override void Unload() {
		// 	// The Unload() methods can be used for unloading/disposing/clearing special objects, unsubscribing from events, or for undoing some of your mod's actions.
		// 	// Be sure to always write unloading code when there is a chance of some of your mod's objects being kept present inside the vanilla assembly.
		// 	// The most common reason for that to happen comes from using events, NOT counting On.* and IL.* code-injection namespaces.
		// 	// If you subscribe to an event - be sure to eventually unsubscribe from it.

		// 	// NOTE: When writing unload code - be sure use 'defensive programming'. Or, in other words, you should always assume that everything in the mod you're unloading might've not even been initialized yet.
		// 	// NOTE: There is rarely a need to null-out values of static fields, since TML aims to completely dispose mod assemblies in-between mod reloads.
		// }
	}
}
