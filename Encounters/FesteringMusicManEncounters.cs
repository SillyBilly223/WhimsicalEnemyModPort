using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Encounters
{
    public class FesteringMusicManEncounters
    {
        public static void Add(int EncounterChanceIncrease)
        {
            Portals.AddPortalSign("FesteringMusicSign_Orpheum", ResourceLoader.LoadSprite("BrokenMusicManIcon"), Portals.EnemyIDColor);

            EnemyEncounter_API EnemyEncounter = new EnemyEncounter_API(EncounterType.Random, "FesteringMusic_Orpheum", "FesteringMusicSign_Orpheum");
            EnemyEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("Zone02_Scrungie_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("Zone02_Scrungie_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("Zone02_Scrungie_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_FarShore = new string[]
            {
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.FesteringMusicMan,
            };
            string[] FieldEnemies2_FarShore = new string[]
            {
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.SpoggleWrithing,
                CustomeEnemyInfo.SpoggleWrithing,
            };
            string[] FieldEnemies3_FarShore = new string[]
            {
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.FesteringMusicMan,
            };
            string[] FieldEnemies4_FarShore = new string[]
            {
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.SpoggleResonant,
            };
            string[] FieldEnemies5_FarShore = new string[]
            {
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.Scrungie,
                CustomeEnemyInfo.Scrungie,
                CustomeEnemyInfo.Scrungie,
            };
            string[] FieldEnemies6_FarShore = new string[]
            {
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.OsseousClad,
                CustomeEnemyInfo.FesteringMusicMan,
            };
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies1_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies2_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies3_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies4_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies5_FarShore);
            #endregion Encounters
            EnemyEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("FesteringMusic_Orpheum", 14 + EncounterChanceIncrease, "Orpheum_Hard", BundleDifficulty.Medium);

            string[] FieldEnemies1_Revola = new string[]
            {
                CustomeEnemyInfo.Revola,
                CustomeEnemyInfo.FesteringMusicMan,
                CustomeEnemyInfo.MusicMan,
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")).AddEnemyData(FieldEnemies1_Revola);
        }
    }
}
