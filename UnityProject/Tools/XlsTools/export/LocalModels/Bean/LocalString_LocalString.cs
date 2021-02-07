namespace LocalModels.Bean
{
	public class LocalString_LocalString : BaseLocalBean 
	{
		
		
		public string id{get;set;}			
		
		public string keys{get;set;}			
		
		protected override bool readImpl()
		{
			id = readLocalString();
			keys = readLocalString();
			return true;
		}
		
		public override BaseLocalBean createBean()
		{
			return new LocalString_LocalString();	
		}
		
	}
}


