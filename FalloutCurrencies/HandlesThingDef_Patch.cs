using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	
	[HarmonyPatch(typeof(StockGenerator_SingleDef), "HandlesThingDef")]
	public static class HandlesThingDef_Patch
	{
		
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
