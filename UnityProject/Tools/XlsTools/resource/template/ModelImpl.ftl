using UnityEngine;
using System.Collections;
using LocalModels.Bean;
namespace LocalModels.ModelImpl
{
	public class ${ClassName}ModelImpl : BaseLocalModelImpl<${ClassName},${KeyType}> 
	{
	
		protected override IBeanBuilder GetBuilder()
		{
			return new ${ClassName}();
		}
		
		protected override ${KeyType} GetBeanKey(${ClassName} bean)
		{
			return bean.${KeyFieldName};
				
		}

		
	}
}


