using UnityEngine;
using System.Collections;
using LocalModels.Model;
namespace LocalModels
{
	public class LocalModelManager : MonoBehaviour
	{
		<#list ClassList as ClassName>
		
		private ${ClassName}Model ${ClassName}ModelInstance;
		</#list>
		
		<#list ClassList as ClassName>
		
		public ${ClassName}Model Get${ClassName}ModelInstance()
		{
			if (${ClassName}ModelInstance == null)
			{
				${ClassName}ModelInstance = new ${ClassName}Model();
			}
			return ${ClassName}ModelInstance;
		}
		</#list>

		
	}
}


