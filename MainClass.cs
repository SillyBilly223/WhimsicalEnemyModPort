using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using BepInEx;
using BrutalAPI;
using CrayolapedeModinreallife.Encounters;
using CrayolapedeModinreallife.Enemies;
using CrayolapedeModinreallife.Hooks;
using MonoMod.RuntimeDetour;
using UnityEngine;

namespace CrayolapedeModinreallife
{
    [BepInPlugin("OverheatingNuclearReactor.WhimsicalsEnemyMod", "WhimsicalsEnemyMod", "1.0.0")]
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    public class MainClass : BaseUnityPlugin
    {
        public void Awake()
        {
            
            MainClass.assetBundle = AssetBundle.LoadFromMemory(ResourceLoader.ResourceBinary("meonmungday"));
            MainClass.SaltGibs = AssetBundle.LoadFromMemory(ResourceLoader.ResourceBinary("wowzers"));

            SoundBank.Add();
            ExtraUtils.SetUp();

            LoadStatusAndFieldEffects();
            LoadEnemies();
            LoadEncounters();    
        }
       

        private void LoadStatusAndFieldEffects()
        {
            FumesFE_SO.SetUpFieldEffect();
            PiercedSE_SO.SetUpStatusEffect();
        }

        private void LoadEnemies()
        {
            OsseousClad.Add();
            Lymphropod.Add();
            ColossalSheo.Add();
            FumeFactory.Add();
            FesteringMusicMan.Add();
            Spligis.Add();
            BossSpluglings.Add();
            Convict.Add();
            Evangelists.Add();
        }

        private void LoadEncounters() 
        {
            BossEncounters.Add(BossEncounterChanceIncrease);
            ColossalSheoEncounters.Add(EncounterChanceIncrease);
            FumeFactoryEncounters.Add(EncounterChanceIncrease);
            FesteringMusicManEncounters.Add(EncounterChanceIncrease);
            OsseousCladEncounters.Add(EncounterChanceIncrease);
            LymphropodEncounters.Add(EncounterChanceIncrease);
            EvengelistEncounters.Add(EncounterChanceIncrease);
        }

        public static ZoneBGDataBaseSO[] ZoneData_Easy =
        {
            LoadedAssetsHandler.GetZoneDB("ZoneDB_01") as ZoneBGDataBaseSO,
            LoadedAssetsHandler.GetZoneDB("ZoneDB_02") as ZoneBGDataBaseSO,
            LoadedAssetsHandler.GetZoneDB("ZoneDB_03") as ZoneBGDataBaseSO
        };

        public static ZoneBGDataBaseSO[] ZoneData_Hard =
        {
            LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO,
            LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_02") as ZoneBGDataBaseSO,
            LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03") as ZoneBGDataBaseSO
        };

        public static AssetBundle assetBundle;
        public static AssetBundle SaltGibs;
        
        public int EncounterChanceIncrease = 0;

        public int BossEncounterChanceIncrease = 0;
    }   
}
