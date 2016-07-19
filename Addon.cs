using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kOS;
using kOS.Safe;
using UnityEngine;
using System.Reflection;

using kOS.Safe.Encapsulation;

namespace kOS.AddOns.kOSBiome
{
    [kOSAddon("BIOME")]
    [kOS.Safe.Utilities.KOSNomenclature("BiomeAddon")]
    public class Addon : Suffixed.Addon
    {
        private const BindingFlags BINDINGS = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
        public Addon(SharedObjects shared) : base(shared)
        {
            InitializeSuffixes();
        }

        private void InitializeSuffixes()
        {
            AddSuffix("CURRENT", new kOS.Safe.Encapsulation.Suffixes.NoArgsSuffix<StringValue>(GetCurrentBiome, "Get Name of current Biome"));
            AddSuffix("BIOMEAT", new kOS.Safe.Encapsulation.Suffixes.TwoArgsSuffix<StringValue, kOS.Suffixed.BodyTarget, kOS.Suffixed.GeoCoordinates>(GetBiomeAt, "Get Name of Biome of Body,GeoCoordinates"));
        }

        public override BooleanValue Available()
        {
            return true;
        }
        private StringValue GetCurrentBiome()
        {
           var vessel = FlightGlobals.ActiveVessel;
           var body = FlightGlobals.ActiveVessel.mainBody;
           return ScienceUtil.GetExperimentBiome(body, vessel.latitude, vessel.longitude);
        }
        private StringValue GetBiomeAt(kOS.Suffixed.BodyTarget body, kOS.Suffixed.GeoCoordinates coordinate)
        {
            return ScienceUtil.GetExperimentBiome(body.Body, coordinate.Latitude, coordinate.Longitude);
        }

    }
}