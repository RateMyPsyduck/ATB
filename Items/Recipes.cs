using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ATB.Items
{
	// This class contains thoughtful examples of item recipe creation.
	public class Recipes : ModSystem
	{
        Recipe recipe;
    public static RecipeGroup ModRec;

		public override void Unload() {
			ModRec = null;
		}

	public override void AddRecipes() {
		recipe = Recipe.Create(3, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(2, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(9, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(169, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();

		recipe = Recipe.Create(4614, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(357, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(4625, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(353, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(5009, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
		recipe = Recipe.Create(5042, 1);
		recipe.AddTile<Items.replicator>();
		recipe.Register();
        }
    }
}