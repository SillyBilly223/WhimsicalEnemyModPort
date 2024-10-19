using System;
using BepInEx;
using UnityEngine;
using System.IO;

namespace CrayolapedeModinreallife
{
    public  static class SoundBank
    {
        public static void CreateSoundBankFile(string resourceName, bool onlyIfNotExist = false)
        {
            CreateResourceFile(resourceName, Application.dataPath + "/StreamingAssets", resourceName + ".bank", onlyIfNotExist);
        }

        public static void CreateResourceFile(string resourceName, string path, string outputName, bool onlyIfNotExist = false)
        {
            byte[] resource = new byte[0] { };
            try
            {
                resource = ResourceLoader.ResourceBinary(resourceName);
            }
            catch
            {

            }
            if (resource.Length > 0 && !(onlyIfNotExist && File.Exists(path + "/" + outputName)))
            {
                File.WriteAllBytes(path + "/" + outputName, resource);
            }
        }

        public static void Add()
        {
            SoundBank.CreateSoundBankFile("BankA");
            SoundBank.CreateSoundBankFile("BankA.strings");
            FMODUnity.RuntimeManager.LoadBank("BankA", true);
            FMODUnity.RuntimeManager.LoadBank("BankA.strings", true);
        }
    }
}
