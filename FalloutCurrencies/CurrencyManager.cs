using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
    [StaticConstructorOnStartup]
    public static class CurrencyManager
    {
        /// <summary>
        /// Initializes the mod by patching and creating the custom currency
        /// </summary>
        static CurrencyManager()
        {
            // Use the Harmony library to patch methods
            var harmony = new Harmony("FalloutCurrencies.Mod");
            harmony.PatchAll();

            // Replace silver with bottle caps as a custom currency
            ReplaceSilverWithBottleCaps();
        }

        /// <summary>
        /// Tries to get the custom currency for the given faction, if it exists.
        /// </summary>
        /// <param name="faction">The faction to get the currency for.</param>
        /// <param name="currency">The currency definition, if it exists.</param>
        /// <returns><c>true</c> if the faction has a custom currency, <c>false</c> otherwise.</returns>
        public static bool TryGetCurrency(this Faction faction, out ThingDef currency)
        {
            // Get the mod extension for the faction
            FactionCurrency factionCurrency = faction?.def.GetModExtension<FactionCurrency>();

            // If the faction has a custom currency, set the out parameter and return true
            currency = factionCurrency?.currency;
            return currency != null;
        }

        /// <summary>
        /// Swaps the current silver currency with a new one.
        /// </summary>
        /// <param name="newDef">The new currency definition to use.</param>
        public static void SwapCurrency(ThingDef newDef)
        {
            // Set the Silver thing def to the new currency definition. This is a static
            // property on ThingDefOf, so it will affect all uses of the Silver currency.
            ThingDefOf.Silver = newDef;
        }

        /// <summary>
        /// Replaces all occurrences of Silver with ReplacedSilver in the game's Defs.
        /// This is necessary to replace Silver with a new currency, since Silver is
        /// a static property on ThingDefOf.
        /// </summary>
        private static void ReplaceSilverWithBottleCaps()
        {
            // Get the definition for the new currency
            ThingDef replacedSilver = DefDatabase<ThingDef>.GetNamedSilentFail("ReplacedSilver");
            if (replacedSilver == null)
                return;

            // Create a HashSet of all Silver replacements
            HashSet<ThingDef> silverReplacements = new HashSet<ThingDef> { replacedSilver };

            // Loop through all ThingDefs and replace Silver with ReplacedSilver
            foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
            {
                List<ThingDefCountClass> costList = thingDef.costList;
                if (costList == null)
                    continue;

                foreach (ThingDefCountClass cost in costList.Where(cost => cost.thingDef == ThingDefOf.Silver).ToList())
                {
                    cost.thingDef = replacedSilver;
                }
            }

            // Loop through all TerrainDefs and replace Silver with ReplacedSilver
            foreach (TerrainDef terrainDef in DefDatabase<TerrainDef>.AllDefs)
            {
                List<ThingDefCountClass> costList = terrainDef.costList;
                if (costList == null)
                    continue;

                foreach (ThingDefCountClass cost in costList.Where(cost => cost.thingDef == ThingDefOf.Silver).ToList())
                {
                    cost.thingDef = replacedSilver;
                }
            }

            // Loop through all RecipeDefs and replace Silver with ReplacedSilver
            foreach (RecipeDef recipeDef in DefDatabase<RecipeDef>.AllDefs)
            {
                foreach (IngredientCount ingredientCount in recipeDef?.ingredients ?? Enumerable.Empty<IngredientCount>())
                {
                    if (ingredientCount.filter.AllowedThingDefs.Contains(ThingDefOf.Silver))
                    {
                        // Get the list of allowed ThingDefs and remove all silver replacements
                        List<ThingDef> allowedDefsList = ingredientCount.filter.AllowedThingDefs.ToList();
                        foreach (ThingDef def in silverReplacements)
                        {
                            allowedDefsList.Remove(def);
                        }

                        // Add the new currency to the list of allowed ThingDefs
                        allowedDefsList.Add(replacedSilver);

                        // Create a new ThingFilter with the updated list of allowed ThingDefs
                        var newFilter = new ThingFilter();
                        AccessTools.Field(typeof(ThingFilter), "allowedDefs").SetValue(newFilter, allowedDefsList);

                        // Replace the filter for the ingredient
                        ingredientCount.filter = newFilter;
                    }
                }
            }
        }
        // Default currency definition used when specific currency is not assigned to a faction
        public static readonly ThingDef DefaultCurrencyDef = ThingDefOf.Silver;
    }
}
