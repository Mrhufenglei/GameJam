using UnityEngine;
using System.Collections;
using LocalModels.Model;
namespace LocalModels
{
	public class LocalModelManager : BaseLocalModelManager
	{
		<#list ClassList as ClassName>
		
		private ${ClassName}Model ${ClassName}ModelInstance;
		</#list>
		
		<#list ClassList as ClassName>
		
		public ${ClassName}Model Get${ClassName}ModelInstance()
		{
			return ${ClassName}ModelInstance;
		}
		</#list>
		
		
		public override void InitialiseLocalModels()
		{
			<#list ClassList as ClassName>

			${ClassName}ModelInstance = new ${ClassName}Model();
			localModels[${ClassName}Model.fileName] = ${ClassName}ModelInstance;

			</#list>
		}

		
	}
}


