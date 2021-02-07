using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LocalModels.Bean;
using LocalModels.ModelImpl;
namespace LocalModels.Model
{
	public class Levels_tableModel
	{
	
		private Levels_tableModelImpl modelImpl = new Levels_tableModelImpl();
		
		public Levels_table GetElementById( int id)
		{
			return modelImpl.GetElementById(id);
		}

		public IList<Levels_table> GetAllElements()
		{
			return modelImpl.GetAllElement();
		}
		
	}
}


