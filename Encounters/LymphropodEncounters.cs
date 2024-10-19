using BrutalAPI;
using CrayolapedeModinreallife.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Encounters
{
    public class LymphropodEncounters
    {
        public static void Add(int EncounterChanceIncrease)
        {
            Portals.AddPortalSign("LymphropodSign_FarShore", ResourceLoader.LoadSprite("BugIcon"), Portals.EnemyIDColor);

            EnemyEncounter_API EnemyEncounter = new EnemyEncounter_API(EncounterType.Random, "Lymphropod_FarShore", "LymphropodSign_FarShore");
            EnemyEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies2_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies3_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.JumbleGutsClotted,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.JumbleGutsClotted,
            };
            string[] FieldEnemies4_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.JumbleGutsWaning,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.JumbleGutsWaning,
            };
            string[] FieldEnemies5_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Keko,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Keko,
            };
            string[] FieldEnemies6_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.FlaMinGoa,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies7_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Lymphropod,
            };
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies1_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies2_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies3_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies4_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies5_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies6_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies7_FarShore);
            #endregion Encounters
            EnemyEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("Lymphropod_FarShore", 14 + EncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Medium);

            EnemyEncounter_API EnemyEncounter2 = new EnemyEncounter_API(EncounterType.Random, "LymphropodHard_FarShore", "LymphropodSign_FarShore");
            EnemyEncounter2.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter2.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter2.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Vaboola,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies2Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Wringle,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies3Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.SpoggleSpitFire,
                CustomeEnemyInfo.SpoggleRuminating,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies4Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Kekastle,
                CustomeEnemyInfo.Lymphropod,
            };
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies1Hard_FarShore);
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies2Hard_FarShore);
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies3Hard_FarShore);
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies4Hard_FarShore);
            #endregion Encounters
            EnemyEncounter2.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("LymphropodHard_FarShore", 12 + EncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Hard);

            string[] FieldEnemies1_Flarb = new string[]
            {
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Flarb,
                CustomeEnemyInfo.Lymphropod,
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")).AddEnemyData(FieldEnemies1_Flarb);
        }
    }
}
