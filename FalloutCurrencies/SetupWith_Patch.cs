using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	
	[HarmonyPatch(typeof(TradeSession), "SetupWith")]
	public static class SetupWith_Patch
	{
		
		public static void Postfix(ITrader newTrader, Pawn newPlayerNegotiator, bool giftMode)
		{
			Faction faction = newTrader.Faction;
			ThingDef thingDef;
			bool flag = faction.TryGetCurrency(out thingDef);
			if (flag)
			{
				CurrencyManager.SwapCurrency(thingDef);
			}
			else
			{
				bool flag2 = ThingDefOf.Silver != CurrencyManager.defaultCurrencyDef;
				if (flag2)
				{
					CurrencyManager.SwapCurrency(CurrencyManager.defaultCurrencyDef);
				}
			}
		}
	}
}
