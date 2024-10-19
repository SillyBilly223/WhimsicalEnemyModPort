using BrutalAPI;
using CrayolapedeModinreallife.Enemies;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife.Encounters
{
    public static class BossEncounters
    {
        public static string GetRandomSplugling()
        {
            int Splug = Random.Range(0, CustomeEnemyInfo.BOSSpluglings.Length);
            return CustomeEnemyInfo.BOSSpluglings[Splug];
        }

        public static void Add(int BossEncounterChanceIncrease)
        {

            Portals.AddPortalSign("CrayolaPede_Sign", ResourceLoader.LoadSprite("RedBlueCrayolaIcon"), Portals.BossIDColor);

            EnemyEncounter_API CrayolaPedeEncounter = new EnemyEncounter_API(EncounterType.Specific, "CrayolaPede_BOSS", "CrayolaPede_Sign");
            CrayolaPedeEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_JumbleGuts_Flummoxing_Medium_EnemyBundle")._roarReference.roarEvent;
            CrayolaPedeEncounter.AddSpecialEnvironment(LoadedAssetsHandler.GetEnemyBundle("BOSS_Zone02_Charcarrion_EnemyBundle")._specialCombatEnvironment);
            CrayolaPedeEncounter.MusicEvent = "event:/SpligisMusicEvent";
            string[] Crayola_FieldEnemies = new string[]
            {
                GetRandomSplugling(),
                GetRandomSplugling(),
                CustomeEnemyInfo.Spligis,
                GetRandomSplugling(),
                GetRandomSplugling(),
            };
            int[] Crayola_Positions = new int[]
            {
                0,
                1,
                2,
                3,
                4
            };
            CrayolaPedeEncounter.CreateNewEnemyEncounterData(Crayola_FieldEnemies, Crayola_Positions);
            CrayolaPedeEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("CrayolaPede_BOSS", 18 + BossEncounterChanceIncrease, "Orpheum_Hard", BundleDifficulty.Boss);

            ///

            Portals.AddPortalSign("Convict_Sign", ResourceLoader.LoadSprite("ConvictIcon"), Portals.BossIDColor);

            EnemyEncounter_API ConvictEncounter = new EnemyEncounter_API(EncounterType.Specific, "Convict_BOSS", "Convict_Sign");
            ConvictEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("BOSS_Zone01_Roids_EnemyBundle")._roarReference.roarEvent;
            ConvictEncounter.AddSpecialEnvironment(LoadedAssetsHandler.GetEnemyBundle("BOSS_Zone02_SmoothSkin_EnemyBundle")._specialCombatEnvironment);
            ConvictEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("BOSS_Zone02_Charcarrion_EnemyBundle")._musicEventReference;
            string[] Convict_FE = new string[]
            {
                CustomeEnemyInfo.Stalactite,
                CustomeEnemyInfo.Convict,
                CustomeEnemyInfo.Stalactite,
            };
            int[] Convict_P = new int[]
            {
                0,
                2,
                4,
            };
            ConvictEncounter.CreateNewEnemyEncounterData(Convict_FE, Convict_P);
            ConvictEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("Convict_BOSS", 18 + BossEncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Boss);
        }
    }
}
