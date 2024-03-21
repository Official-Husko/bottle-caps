using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	// Token: 0x02000003 RID: 3
	[HarmonyPatch(typeof(TradeSession), "SetupWith")]
	public static class SetupWith_Patch
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
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
