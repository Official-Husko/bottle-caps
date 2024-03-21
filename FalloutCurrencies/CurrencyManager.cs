using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FalloutCurrencies
{
	
	[StaticConstructorOnStartup]
	public static class CurrencyManager
	{
		
		static CurrencyManager()
		{
			new Harmony("FalloutCurrencies.Mod").PatchAll();
			CurrencyManager.ReplaceSilverWithBottleCaps();
		}

		
		public static bool TryGetCurrency(this Faction faction, out ThingDef currency)
		{
			FactionCurrency factionCurrency = ((faction != null) ? faction.def.GetModExtension<FactionCurrency>() : null);
			bool flag = factionCurrency != null;
			bool flag2;
			if (flag)
			{
				currency = factionCurrency.currency;
				flag2 = true;
			}
			else
			{
				currency = null;
				flag2 = false;
			}
			return flag2;
		}

		
		public static void SwapCurrency(ThingDef newDef)
		{
			ThingDefOf.Silver = newDef;
		}

		
		private static void ReplaceSilverWithBottleCaps()
		{
			ThingDef namedSilentFail = DefDatabase<ThingDef>.GetNamedSilentFail("ReplacedSilver");
			bool flag = namedSilentFail != null;
			if (flag)
			{
				List<ThingDef> allDefsListForReading = DefDatabase<ThingDef>.AllDefsListForReading;
				foreach (ThingDef thingDef in allDefsListForReading)
				{
					ThingDefCountClass thingDefCountClass;
					if (thingDef == null)
					{
						thingDefCountClass = null;
					}
					else
					{
						List<ThingDefCountClass> costList = thingDef.costList;
						if (costList == null)
						{
							thingDefCountClass = null;
						}
						else
						{
							IEnumerable<ThingDefCountClass> enumerable = costList.Where((ThingDefCountClass x) => x.thingDef == ThingDefOf.Silver);
							thingDefCountClass = ((enumerable != null) ? enumerable.FirstOrDefault<ThingDefCountClass>() : null);
						}
					}
					ThingDefCountClass thingDefCountClass2 = thingDefCountClass;
					bool flag2 = thingDefCountClass2 != null;
					if (flag2)
					{
						ThingDefCountClass thingDefCountClass3 = new ThingDefCountClass();
						thingDefCountClass3.thingDef = namedSilentFail;
						thingDefCountClass3.count = thingDefCountClass2.count;
						thingDef.costList.Add(thingDefCountClass3);
						thingDef.costList.Remove(thingDefCountClass2);
					}
				}
				List<TerrainDef> allDefsListForReading2 = DefDatabase<TerrainDef>.AllDefsListForReading;
				foreach (TerrainDef terrainDef in allDefsListForReading2)
				{
					ThingDefCountClass thingDefCountClass4;
					if (terrainDef == null)
					{
						thingDefCountClass4 = null;
					}
					else
					{
						List<ThingDefCountClass> costList2 = terrainDef.costList;
						if (costList2 == null)
						{
							thingDefCountClass4 = null;
						}
						else
						{
							IEnumerable<ThingDefCountClass> enumerable2 = costList2.Where((ThingDefCountClass x) => x.thingDef == ThingDefOf.Silver);
							thingDefCountClass4 = ((enumerable2 != null) ? enumerable2.FirstOrDefault<ThingDefCountClass>() : null);
						}
					}
					ThingDefCountClass thingDefCountClass5 = thingDefCountClass4;
					bool flag3 = thingDefCountClass5 != null;
					if (flag3)
					{
						ThingDefCountClass thingDefCountClass6 = new ThingDefCountClass();
						thingDefCountClass6.thingDef = ThingDef.Named("ReplacedSilver");
						thingDefCountClass6.count = thingDefCountClass5.count;
						terrainDef.costList.Add(thingDefCountClass6);
						terrainDef.costList.Remove(thingDefCountClass5);
					}
				}
				List<RecipeDef> allDefsListForReading3 = DefDatabase<RecipeDef>.AllDefsListForReading;
				foreach (RecipeDef recipeDef in allDefsListForReading3)
				{
					foreach (IngredientCount ingredientCount in ((recipeDef != null) ? recipeDef.ingredients : null))
					{
						IEnumerable<ThingDef> allowedThingDefs = ingredientCount.filter.AllowedThingDefs;
						ThingDef thingDef2;
						if (allowedThingDefs == null)
						{
							thingDef2 = null;
						}
						else
						{
							IEnumerable<ThingDef> enumerable3 = allowedThingDefs.Where((ThingDef x) => x == ThingDefOf.Silver);
							thingDef2 = ((enumerable3 != null) ? enumerable3.FirstOrDefault<ThingDef>() : null);
						}
						ThingDef thingDef3 = thingDef2;
						bool flag4 = thingDef3 != null;
						if (flag4)
						{
							ingredientCount.filter.AllowedThingDefs.ToList<ThingDef>().Add(ThingDef.Named("ReplacedSilver"));
							ingredientCount.filter.AllowedThingDefs.ToList<ThingDef>().Remove(ThingDefOf.Silver);
						}
					}
				}
			}
		}

		
		public static ThingDef defaultCurrencyDef = ThingDefOf.Silver;
	}
}
