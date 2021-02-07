using UnityEngine;
using System.Collections;
using LocalModels.Bean;
namespace LocalModels.ModelImpl
{
	public class LocalString_LocalStringModelImpl : LocalModel<LocalString_LocalString,string> 
	{
	
		protected override BeanBuilder GetBuilder()
		{
			return new LocalString_LocalString();
		}
		
		protected override string GetBeanKey(LocalString_LocalString bean)
		{
			return bean.id;
				
		}

		
	}
}


