using System;
using HarmonyLib;
using RimWorld;

namespace FalloutCurrencies
{
	
	[HarmonyPatch(typeof(TradeSession), "Close")]
	public static class Close_Patch
	{
		
		public static void Prefix()
		{
			bool flag = ThingDefOf.Silver != CurrencyManager.defaultCurrencyDef;
			if (flag)
			{
				CurrencyManager.SwapCurrency(CurrencyManager.defaultCurrencyDef);
			}
		}
	}
}
