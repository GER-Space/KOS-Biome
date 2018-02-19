using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kOS;
using kOS.Safe;
using UnityEngine;

using kOS.Safe.Encapsulation;

namespace kOS.AddOns.kOSBiome
{
    [kOSAddon("BIOME")]
    [kOS.Safe.Utilities.KOSNomenclature("BiomeAddon")]
    public class Addon : Suffixed.Addon
    {
        public Addon(SharedObjects shared) : base(shared)
        {
            InitializeSuffixes();
        }

        private void InitializeSuffixes()
        {
            AddSuffix("CURRENT", new kOS.Safe.Encapsulation.Suffixes.NoArgsSuffix<StringValue>(GetCurrentBiome, "Get Name of current Biome"));
            AddSuffix("AT", new kOS.Safe.Encapsulation.Suffixes.TwoArgsSuffix<StringValue, kOS.Suffixed.BodyTarget, kOS.Suffixed.GeoCoordinates>(GetBiomeAt, "Get Name of Biome of Body,GeoCoordinates"));
            AddSuffix("SITUATION", new kOS.Safe.Encapsulation.Suffixes.NoArgsSuffix<StringValue>(GetCurrentSituation, "Get Current Science Situation"));
        }

        public override BooleanValue Available()
        {
            return true;
        }
        private StringValue GetCurrentBiome()
        {
           var vessel = FlightGlobals.ActiveVessel;
           var body = FlightGlobals.ActiveVessel.mainBody;
           var Biome = string.IsNullOrEmpty(vessel.landedAt)
           ? ScienceUtil.GetExperimentBiome(body, vessel.latitude, vessel.longitude)
           : Vessel.GetLandedAtString(vessel.landedAt).Replace(" ", "");
          
            return Biome;
        }
        private StringValue GetBiomeAt(kOS.Suffixed.BodyTarget body, kOS.Suffixed.GeoCoordinates coordinate)
        {
            return ScienceUtil.GetExperimentBiome(body.Body, coordinate.Latitude, coordinate.Longitude);
        }
        private StringValue GetCurrentSituation()
        {
            var vessel = FlightGlobals.ActiveVessel;
            return ScienceUtil.GetExperimentSituation(vessel).ToString();

        }
    }
}