using System;
using HarmonyLib;
using RimWorld;

namespace FalloutCurrencies
{
	// Token: 0x02000004 RID: 4
	[HarmonyPatch(typeof(TradeSession), "Close")]
	public static class Close_Patch
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A8 File Offset: 0x000002A8
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
