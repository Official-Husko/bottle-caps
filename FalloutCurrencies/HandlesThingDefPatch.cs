using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
    [HarmonyPatch(typeof(StockGenerator_SingleDef), "HandlesThingDef")]
    public static class HandlesThingDefPatch
    {
        /// <summary>
        /// Patches <see cref="StockGenerator_SingleDef.HandlesThingDef"/> to return true if the thing
        /// def being stocked is the default currency def.
        /// </summary>
        /// <param name="__instance">The StockGenerator_SingleDef instance being called.</param>
        /// <param name="__result">The result of the original method.</param>
        /// <param name="___thingDef">The thing def being passed to the original method.</param>
        public static void Postfix(StockGenerator_SingleDef __instance, ref bool __result, ref ThingDef ___thingDef)
        {
            // If the thing def is the default currency def, return true.
            if (___thingDef == CurrencyManager.DefaultCurrencyDef)
            {
                __result = true;
                return;
            }
        }
    }
}
