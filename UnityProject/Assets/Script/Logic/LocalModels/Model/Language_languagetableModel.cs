using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LocalModels.Bean;
using LocalModels.ModelImpl;
namespace LocalModels.Model
{
	public class Language_languagetableModel : BaseLocalModel
	{
	
        public static readonly string fileName = "Language_languagetable";
	
		private Language_languagetableModelImpl modelImpl = new Language_languagetableModelImpl();
		
		public Language_languagetable GetElementById( int id)
		{
			return modelImpl.GetElementById(id);
		}

		public IList<Language_languagetable> GetAllElements()
		{
			return modelImpl.GetAllElement();
		}
		
		public override void Initialise(string name,byte[] bytes)
        {
            modelImpl.Initialise(name,bytes);
        }
		
	}
}


