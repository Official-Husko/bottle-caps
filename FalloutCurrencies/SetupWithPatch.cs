using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
    [HarmonyPatch(typeof(TradeSession), "SetupWith")]
    public static class SetupWithPatch
    {
        /// <summary>
        /// Patches <see cref="TradeSession.SetupWith"/> to use the faction's currency, if set, or the default currency otherwise.
        /// </summary>
        /// <param name="newTrader">The trader being traded with.</param>
        /// <param name="newPlayerNegotiator">The negotiator for the trade session.</param>
        /// <param name="giftMode">Whether the trade session is in gift mode.</param>
        public static void Postfix(ITrader newTrader, Pawn newPlayerNegotiator, bool giftMode)
        {
            // If the faction being traded with has a currency set, use that as the currency for the trade session.
            Faction faction = newTrader.Faction;
            if (faction != null)
            {
                ThingDef currencyDef;
                if (faction.TryGetCurrency(out currencyDef))
                {
                    CurrencyManager.SwapCurrency(currencyDef);
                    return;
                }
            }

            // Otherwise, use the default currency.
            if (ThingDefOf.Silver != CurrencyManager.DefaultCurrencyDef)
            {
                CurrencyManager.SwapCurrency(CurrencyManager.DefaultCurrencyDef);
            }
        }
    }
}
