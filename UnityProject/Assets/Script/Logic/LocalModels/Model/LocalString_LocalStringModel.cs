using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LocalModels.Bean;
using LocalModels.ModelImpl;
namespace LocalModels.Model
{
	public class LocalString_LocalStringModel
	{
	
		private LocalString_LocalStringModelImpl modelImpl = new LocalString_LocalStringModelImpl();
		
		public LocalString_LocalString GetElementById( string id)
		{
			return modelImpl.GetElementById(id);
		}

		public IList<LocalString_LocalString> GetAllElements()
		{
			return modelImpl.GetAllElement();
		}
		
	}
}


