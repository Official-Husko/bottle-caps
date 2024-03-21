using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
    [HarmonyPatch(typeof(StockGenerator_SingleDef), "GenerateThings")]
    public static class GenerateThingsPatch
    {
        /// <summary>
        /// Patches <see cref="StockGenerator_SingleDef.GenerateThings"/> to use the faction's currency, if set, as the thing def.
        /// </summary>
        /// <param name="__instance">The StockGenerator_SingleDef instance being called.</param>
        /// <param name="___thingDef">The thing def passed to <see cref="StockGenerator_SingleDef.GenerateThings"/>.</param>
        /// <param name="__state">The thing def to be used as the stock's currency, if the faction has one set. Is undefined otherwise.</param>
        /// <param name="forTile">The tile being stocked.</param>
        /// <param name="faction">The faction for which the stock is being generated. Defaults to the tile's faction if null.</param>
        public static void Prefix(StockGenerator_SingleDef __instance, ref ThingDef ___thingDef, ref ThingDef __state, int forTile, Faction faction = null)
        {
            // If the thing def is the default currency and the faction has a currency set, use that instead.
            if (___thingDef == CurrencyManager.DefaultCurrencyDef && faction.TryGetCurrency(out __state))
            {
                ___thingDef = __state;
            }
        }

        /// <summary>
        /// Resets the stock's currency to the default currency if the faction's currency is being used.
        /// </summary>
        /// <param name="__result">The stock's generated things.</param>
        /// <param name="__instance">The StockGenerator_SingleDef instance being called.</param>
        /// <param name="__state">The thing def used as the stock's currency, if the faction has one set. Is undefined otherwise.</param>
        /// <param name="forTile">The tile being stocked.</param>
        /// <param name="faction">The faction for which the stock is being generated. Defaults to the tile's faction if null.</param>
        /// <returns>The modified stock's generated things.</returns>
        public static IEnumerable<Thing> Postfix(IEnumerable<Thing> __result, StockGenerator_SingleDef __instance, ThingDef __state, int forTile, Faction faction = null)
        {
            foreach (Thing thing in __result)
            {
                if (faction.TryGetCurrency(out ThingDef currency) && __state == currency)
                {
                    // Reset the stock's currency to the default currency.
                    __state = CurrencyManager.DefaultCurrencyDef;
                }
                yield return thing;
            }
        }
    }
}
