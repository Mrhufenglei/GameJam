using UnityEngine;
using System.Collections;
using LocalModels.Model;
namespace LocalModels
{
    public class LocalModelManager : MonoBehaviour
    {

        private LocalString_LocalStringModel LocalString_LocalStringModelInstance;

        private Levels_tableModel Levels_tableModelInstance;

        private Language_languagetableModel Language_languagetableModelInstance;


        public LocalString_LocalStringModel GetLocalString_LocalStringModelInstance()
        {
            if (LocalString_LocalStringModelInstance == null)
            {
                LocalString_LocalStringModelInstance = new LocalString_LocalStringModel();
            }
            return LocalString_LocalStringModelInstance;
        }

        public Levels_tableModel GetLevels_tableModelInstance()
        {
            if (Levels_tableModelInstance == null)
            {
                Levels_tableModelInstance = new Levels_tableModel();
            }
            return Levels_tableModelInstance;
        }

        public Language_languagetableModel GetLanguage_languagetableModelInstance()
        {
            if (Language_languagetableModelInstance == null)
            {
                Language_languagetableModelInstance = new Language_languagetableModel();
            }
            return Language_languagetableModelInstance;
        }


    }
}


