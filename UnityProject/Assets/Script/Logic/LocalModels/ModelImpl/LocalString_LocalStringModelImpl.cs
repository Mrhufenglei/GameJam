using UnityEngine;
using System.Collections;
using LocalModels.Bean;
namespace LocalModels.ModelImpl
{
	public class LocalString_LocalStringModelImpl : BaseLocalModelImpl<LocalString_LocalString,string> 
	{
	
		protected override IBeanBuilder GetBuilder()
		{
			return new LocalString_LocalString();
		}
		
		protected override string GetBeanKey(LocalString_LocalString bean)
		{
			return bean.id;
				
		}

		
	}
}


