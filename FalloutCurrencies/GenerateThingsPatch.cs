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
		
		public static void Prefix(StockGenerator_SingleDef __instance, ref ThingDef ___thingDef, ref ThingDef __state, int forTile, Faction faction = null)
		{
			if (___thingDef == CurrencyManager.defaultCurrencyDef && faction.TryGetCurrency(out __state))
			{
				___thingDef = __state;
			}
		}


		
		public static IEnumerable<Thing> Postfix(IEnumerable<Thing> __result, StockGenerator_SingleDef __instance, ThingDef __state, int forTile, Faction faction = null)
		{
			using (IEnumerator<Thing> enumerator = __result.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Thing thing = enumerator.Current;
					ThingDef currency;
					if (faction.TryGetCurrency(out currency) && __state == currency)
					{
						__state = CurrencyManager.defaultCurrencyDef;
					}
					yield return thing;
				}
			}
			yield break;
		}
	}
}
