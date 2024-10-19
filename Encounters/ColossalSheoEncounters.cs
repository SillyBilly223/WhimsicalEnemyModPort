using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Encounters
{
    public class ColossalSheoEncounters
    {
        public static void Add(int EncounterChanceIncrease)
        {
            Portals.AddPortalSign("SheoSign_FarShore", ResourceLoader.LoadSprite("BoneBirdIcon"), Portals.EnemyIDColor);

            EnemyEncounter_API EnemyEncounter = new EnemyEncounter_API(EncounterType.Random, "Sheo_FarShore", "SheoSign_FarShore");
            EnemyEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_SkinningHomunculus_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.ColossalSheo,
            };
            string[] FieldEnemies2_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.MudLung,
            };
            string[] FieldEnemies3_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.Mung,
                CustomeEnemyInfo.Mung,
                CustomeEnemyInfo.Mung,
                CustomeEnemyInfo.Mung,
            };
            string[] FieldEnemies4_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.Wringle,
                CustomeEnemyInfo.Mung,
            };
            string[] FieldEnemies5_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.JumbleGutsWaning,
                CustomeEnemyInfo.JumbleGutsWaning,
            };
            string[] FieldEnemies6_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.JumbleGutsClotted,
                CustomeEnemyInfo.JumbleGutsClotted,
            };
            string[] FieldEnemies7_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.MudLung,
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
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("Sheo_FarShore", 14 + EncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Medium);

            EnemyEncounter_API EnemyEncounter2 = new EnemyEncounter_API(EncounterType.Random, "SheoHard_FarShore", "SheoSign_FarShore");
            EnemyEncounter2.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_SkinningHomunculus_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter2.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter2.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.Vaboola,
            };
            string[] FieldEnemies2Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.Kekastle,
            };
            string[] FieldEnemies3Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.SpoggleSpitFire,
                CustomeEnemyInfo.SpoggleSpitFire,
            };
            string[] FieldEnemies4Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.ColossalSheo,
                CustomeEnemyInfo.FlaMinGoa,
            };
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies1Hard_FarShore);
            #endregion Encounters
            EnemyEncounter2.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("SheoHard_FarShore", 12 + EncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Hard);
        }
    }
}
