using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	// Token: 0x02000005 RID: 5
	[HarmonyPatch(typeof(StockGenerator_SingleDef), "HandlesThingDef")]
	public static class HandlesThingDef_Patch
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020D8 File Offset: 0x000002D8
		public static void Postfix(StockGenerator_SingleDef __instance, ref bool __result, ref ThingDef ___thingDef)
		{
			bool flag = ___thingDef == CurrencyManager.defaultCurrencyDef && ___thingDef != ThingDefOf.Silver;
			if (flag)
			{
				__result = true;
			}
		}
	}
}
