using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LocalModels.Bean;
using LocalModels.ModelImpl;
namespace LocalModels.Model
{
	public class ${ClassName}Model : BaseLocalModel
	{
	
        public static readonly string fileName = "${ClassName}";
	
		private ${ClassName}ModelImpl modelImpl = new ${ClassName}ModelImpl();
		
		public ${ClassName} GetElementById( ${KeyType} id)
		{
			return modelImpl.GetElementById(id);
		}

		public IList<${ClassName}> GetAllElements()
		{
			return modelImpl.GetAllElement();
		}
		
		public override void Initialise(string name,byte[] bytes)
        {
            modelImpl.Initialise(name,bytes);
        }
		
	}
}


