using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch(typeof(StockGenerator_SingleDef), "GenerateThings")]
	public static class GenerateThings_Patch
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
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

		// Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
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
