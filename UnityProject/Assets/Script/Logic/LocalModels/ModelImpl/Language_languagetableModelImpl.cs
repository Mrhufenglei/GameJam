using UnityEngine;
using System.Collections;
using LocalModels.Bean;
namespace LocalModels.ModelImpl
{
	public class Language_languagetableModelImpl : LocalModel<Language_languagetable,int> 
	{
	
		protected override BeanBuilder GetBuilder()
		{
			return new Language_languagetable();
		}
		
		protected override int GetBeanKey(Language_languagetable bean)
		{
			return bean.id;
				
		}

		
	}
}


