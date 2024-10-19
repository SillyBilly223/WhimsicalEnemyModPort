using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Encounters
{
    public class FumeFactoryEncounters
    {
        public static void Add(int EncounterChanceIncrease)
        {
            Portals.AddPortalSign("FumeFactory_Orpheum", ResourceLoader.LoadSprite("InfectedSmokeStacksIcon"), Portals.EnemyIDColor);

            EnemyEncounter_API EnemyEncounter = new EnemyEncounter_API(EncounterType.Random, "FumeFactory_Orpheum", "FumeFactory_Orpheum");
            EnemyEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_FarShore = new string[]
            {
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.MusicMan,
                CustomeEnemyInfo.MusicMan,
            };
            string[] FieldEnemies2_FarShore = new string[]
            {
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.SpoggleSpitFire,
                CustomeEnemyInfo.SpoggleWrithing,
            };
            string[] FieldEnemies3_FarShore = new string[]
            {
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.SpoggleSpitFire,
                CustomeEnemyInfo.SpoggleResonant,
            };
            string[] FieldEnemies4_FarShore = new string[]
            {
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.SingingStone,
                CustomeEnemyInfo.SingingStone,
            };
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies1_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies2_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies3_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies4_FarShore);
            #endregion Encounters
            EnemyEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("FumeFactory_Orpheum", 14 + EncounterChanceIncrease, "Orpheum_Hard", BundleDifficulty.Medium);

            string[] FieldEnemies1_Revola = new string[]
            {
                CustomeEnemyInfo.Revola,
                CustomeEnemyInfo.FumeFactory,
                CustomeEnemyInfo.FumeFactory,
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Revola_Hard_EnemyBundle")).AddEnemyData(FieldEnemies1_Revola);
        }
    }
}
