using System;
using Verse;

namespace FalloutCurrencies
{
    /// <summary>
    /// Represents a mod extension defining a custom currency for a faction.
    /// </summary>
    public class FactionCurrency : DefModExtension
    {
        /// <summary>
        /// The ThingDef representing the currency.
        /// </summary>
        public ThingDef currency;
    }
}
