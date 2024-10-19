using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Encounters
{
    public class OsseousCladEncounters
    {
        public static void Add(int EncounterChanceIncrease)
        {
            Portals.AddPortalSign("CladSign", ResourceLoader.LoadSprite("SpikeGuyIcon"), Portals.EnemyIDColor);

            EnemyEncounter_API EnemyEncounter = new EnemyEncounter_API(EncounterType.Random, "Clad_FarShore", "CladSign");
            EnemyEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies2_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.Wringle,
                CustomeEnemyInfo.Wringle,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies3_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Lymphropod,
                CustomeEnemyInfo.Lymphropod,
            };
            string[] FieldEnemies4_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.Vaboola,
            };
            string[] FieldEnemies5_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.SpoggleSpitFire,
                CustomeEnemyInfo.MudLung,
                CustomeEnemyInfo.MudLung,
            };
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies1_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies2_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies3_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies4_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies5_FarShore);
            #endregion Encounters
            EnemyEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("Clad_FarShore", 12 + EncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Medium);

            EnemyEncounter_API EnemyEncounter2 = new EnemyEncounter_API(EncounterType.Random, "CladHard_FarShore", "CladSign");
            EnemyEncounter2.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_SkinningHomunculus_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter2.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._specialCombatEnvironment   ;
            EnemyEncounter2.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Spoggle_Writhing_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.Flarb,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies2Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.JumbleGutsHollowing,
                CustomeEnemyInfo.JumbleGutsClotted,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies3Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.SpoggleRuminating,
                CustomeEnemyInfo.SpoggleRuminating,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies4Hard_FarShore = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.MunglingMudLung,
                CustomeEnemyInfo.MunglingMudLung,
                CustomeEnemyInfo.OsseousClad,
            };
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies1Hard_FarShore);
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies2Hard_FarShore);
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies3Hard_FarShore);
            EnemyEncounter2.CreateNewEnemyEncounterData(FieldEnemies4Hard_FarShore);
            #endregion Encounters
            EnemyEncounter2.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("CladHard_FarShore", 14 + EncounterChanceIncrease, "FarShore_Hard", BundleDifficulty.Hard);

            EnemyEncounter_API EnemyEncounter3 = new EnemyEncounter_API(EncounterType.Random, "Clad_Orpheum", "CladSign");
            EnemyEncounter3.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter3.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter3.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_Orpheum = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies2_Orpheum = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.JumbleGutsHollowing,
                CustomeEnemyInfo.JumbleGutsHollowing,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies3_Orpheum = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.Scrungie,
                CustomeEnemyInfo.Scrungie,
                CustomeEnemyInfo.Scrungie,
            };
            string[] FieldEnemies4_Orpheum = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.SpoggleResonant,
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.OsseousClad,
            };
            string[] FieldEnemies5_Orpheum = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.FesteringMusicMan,
            };
            string[] FieldEnemies6_Orpheum = new string[]
            {
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.FumeFactory,
            };
            EnemyEncounter3.CreateNewEnemyEncounterData(FieldEnemies1_Orpheum);
            EnemyEncounter3.CreateNewEnemyEncounterData(FieldEnemies2_Orpheum);
            EnemyEncounter3.CreateNewEnemyEncounterData(FieldEnemies3_Orpheum);
            EnemyEncounter3.CreateNewEnemyEncounterData(FieldEnemies4_Orpheum);
            EnemyEncounter3.CreateNewEnemyEncounterData(FieldEnemies5_Orpheum);
            EnemyEncounter3.CreateNewEnemyEncounterData(FieldEnemies6_Orpheum);
            #endregion Encounters
            EnemyEncounter3.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("Clad_Orpheum", 12 + EncounterChanceIncrease, "Orpheum_Hard", BundleDifficulty.Medium);

            string[] FieldEnemies1_Revola = new string[]
            {
                CustomeEnemyInfo.Revola,
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.OsseousClad,
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")).AddEnemyData(FieldEnemies1_Revola);
        }
    }
}
