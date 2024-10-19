using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Encounters
{
    public class EvengelistEncounters
    {
        public static void Add(int EncounterChanceIncrease)
        {
            Portals.AddPortalSign("EvengelistSign", ResourceLoader.LoadSprite("MissionaryIcon"), Portals.EnemyIDColor);

            EnemyEncounter_API EnemyEncounter = new EnemyEncounter_API(EncounterType.Random, "Evengelist_Garden", "EvengelistSign");
            EnemyEncounter.RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Voboola_Hard_EnemyBundle")._roarReference.roarEvent;
            EnemyEncounter.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_InHisImage_Medium_EnemyBundle")._specialCombatEnvironment;
            EnemyEncounter.MusicEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_InHisImage_Medium_EnemyBundle")._musicEventReference;
            #region Encounters
            string[] FieldEnemies1_FarShore = new string[]
            {
                CustomeEnemyInfo.Evangelists,
                CustomeEnemyInfo.ChoirBoy_EN,
                CustomeEnemyInfo.Evangelists,
            };
            string[] FieldEnemies2_FarShore = new string[]
            {
                CustomeEnemyInfo.Evangelists,
                CustomeEnemyInfo.SkinningHomunculus,
                CustomeEnemyInfo.SkinningHomunculus,
            };
            string[] FieldEnemies3_FarShore = new string[]
            {
                CustomeEnemyInfo.Evangelists,
                CustomeEnemyInfo.GigglingMinister,
                CustomeEnemyInfo.ScatteringHomunculus,
            };
            string[] FieldEnemies4_FarShore = new string[]
            {
                CustomeEnemyInfo.Evangelists,
                CustomeEnemyInfo.GigglingMinister,
                CustomeEnemyInfo.ScatteringHomunculus,
            };
            string[] FieldEnemies5_FarShore = new string[]
            {
                CustomeEnemyInfo.Evangelists,
                CustomeEnemyInfo.HisImage,
                CustomeEnemyInfo.HisImage,
                CustomeEnemyInfo.HisImage,
            };
            string[] FieldEnemies6_FarShore = new string[]
            {
                CustomeEnemyInfo.Evangelists,
                CustomeEnemyInfo.SkinningHomunculus,
                CustomeEnemyInfo.ShiveringHomunculus_,
                CustomeEnemyInfo.ShiveringHomunculus_,
            };
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies1_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies2_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies3_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies4_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies5_FarShore);
            EnemyEncounter.CreateNewEnemyEncounterData(FieldEnemies6_FarShore);
            #endregion Encounters
            EnemyEncounter.AddEncounterToDataBases();
            LoadedDBsHandler._EnemyDB.AddBundleToSelector("Evengelist_Garden", 14 + EncounterChanceIncrease, MainClass.ZoneData_Hard[2].m_ZoneTypeID, BundleDifficulty.Medium);

        }
    }
}
