namespace LocalModels.Bean
{
	public class LocalString_LocalString : LocalBean 
	{
	
		public static readonly string data_file = "LocalString_LocalString";
		
		
		public string id{get;set;}			
		
		public string keys{get;set;}			
		
		protected override bool readImpl()
		{
			id = readLocalString();
			keys = readLocalString();
			return true;
		}
		
		public override LocalBean createBean()
		{
			return new LocalString_LocalString();	
		}

		public override string GetFilename()
		{
			return data_file;
		}
		
	}
}


