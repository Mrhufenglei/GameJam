using UnityEngine;
using System.Collections;
using LocalModels.Model;
namespace LocalModels
{
	public class LocalModelManager : BaseLocalModelManager
	{
				
		private LocalString_LocalStringModel LocalString_LocalStringModelInstance;
		
		private Levels_tableModel Levels_tableModelInstance;
		
		private Language_languagetableModel Language_languagetableModelInstance;
		
		
		public LocalString_LocalStringModel GetLocalString_LocalStringModelInstance()
		{
			return LocalString_LocalStringModelInstance;
		}
		
		public Levels_tableModel GetLevels_tableModelInstance()
		{
			return Levels_tableModelInstance;
		}
		
		public Language_languagetableModel GetLanguage_languagetableModelInstance()
		{
			return Language_languagetableModelInstance;
		}
		
		
		public override void InitialiseLocalModels()
		{

			LocalString_LocalStringModelInstance = new LocalString_LocalStringModel();
			m_localModels[LocalString_LocalStringModel.fileName] = LocalString_LocalStringModelInstance;


			Levels_tableModelInstance = new Levels_tableModel();
			m_localModels[Levels_tableModel.fileName] = Levels_tableModelInstance;


			Language_languagetableModelInstance = new Language_languagetableModel();
			m_localModels[Language_languagetableModel.fileName] = Language_languagetableModelInstance;

		}

		
	}
}


