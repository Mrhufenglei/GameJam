using UnityEngine;
using System.Collections;
using LocalModels.Bean;
namespace LocalModels.ModelImpl
{
	public class ${ClassName}ModelImpl : LocalModel<${ClassName},${KeyType}> 
	{
	
		protected override BeanBuilder GetBuilder()
		{
			return new ${ClassName}();
		}
		
		protected override ${KeyType} GetBeanKey(${ClassName} bean)
		{
			return bean.${KeyFieldName};
				
		}

		
	}
}


