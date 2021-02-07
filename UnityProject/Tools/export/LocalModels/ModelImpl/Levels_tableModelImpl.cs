using UnityEngine;
using System.Collections;
using LocalModels.Bean;
namespace LocalModels.ModelImpl
{
	public class Levels_tableModelImpl : LocalModel<Levels_table,int> 
	{
	
		protected override BeanBuilder GetBuilder()
		{
			return new Levels_table();
		}
		
		protected override int GetBeanKey(Levels_table bean)
		{
			return bean.id;
				
		}

		
	}
}


