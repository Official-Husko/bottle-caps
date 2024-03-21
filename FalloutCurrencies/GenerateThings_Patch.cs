using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	
	[HarmonyPatch(typeof(StockGenerator_SingleDef), "GenerateThings")]
	public static class GenerateThings_Patch
	{
		
		public static void Prefix(StockGenerator_SingleDef __instance, ref ThingDef ___thingDef, out ThingDef __state, int forTile, Faction faction = null)
		{
			ThingDef thingDef = null;
			bool flag = ___thingDef == CurrencyManager.defaultCurrencyDef && faction.TryGetCurrency(out thingDef);
			if (flag)
			{
				___thingDef = thingDef;
			}
			__state = ___thingDef;
		}

		
		public static IEnumerable<Thing> Postfix(IEnumerable<Thing> __result, StockGenerator_SingleDef __instance, ThingDef __state, int forTile, Faction faction = null)
		{
			foreach (Thing thing in __result)
			{
				ThingDef currency;
				bool flag = faction.TryGetCurrency(out currency) && __state == currency;
				if (flag)
				{
					__state = CurrencyManager.defaultCurrencyDef;
				}
				yield return thing;
				currency = null;
				thing = null;
			}
			IEnumerator<Thing> enumerator = null;
			yield break;
			yield break;
		}
	}
}
