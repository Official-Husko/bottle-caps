using HarmonyLib;
using RimWorld;

namespace FalloutCurrencies
{
    [HarmonyPatch(typeof(TradeSession), "Close")]
    public static class ClosePatch
    {
        /// <summary>
        /// Patches <see cref="TradeSession.Close"/> to restore the default currency after a trade session.
        /// This is necessary because the <see cref="TradeSession"/> does not reset the currency
        /// to the default after the trade session is closed.
        /// </summary>
        public static void Prefix()
        {
            // If the default currency is not Silver, then restore Silver as the currency
            if (CurrencyManager.DefaultCurrencyDef != ThingDefOf.Silver)
            {
                // Restore Silver as the currency
                CurrencyManager.SwapCurrency(ThingDefOf.Silver);
            }
        }
    }
}
