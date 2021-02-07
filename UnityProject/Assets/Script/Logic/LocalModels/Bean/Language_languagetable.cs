namespace LocalModels.Bean
{
	public class Language_languagetable : LocalBean 
	{
	
		public static readonly string data_file = "Language_languagetable";
		
		
		public int id{get;set;}			
		
		public string notes{get;set;}			
		
		public string english{get;set;}			
		
		public string spanish{get;set;}			
		
		public string chinesesimplified{get;set;}			
		
		public string chinesetraditional{get;set;}			
		
		public string japanese{get;set;}			
		
		public string french{get;set;}			
		
		public string german{get;set;}			
		
		public string italian{get;set;}			
		
		public string dutch{get;set;}			
		
		public string russian{get;set;}			
		
		public string arabic{get;set;}			
		
		public string korean{get;set;}			
		
		protected override bool readImpl()
		{
			id = readInt();
			notes = readLocalString();
			english = readLocalString();
			spanish = readLocalString();
			chinesesimplified = readLocalString();
			chinesetraditional = readLocalString();
			japanese = readLocalString();
			french = readLocalString();
			german = readLocalString();
			italian = readLocalString();
			dutch = readLocalString();
			russian = readLocalString();
			arabic = readLocalString();
			korean = readLocalString();
			return true;
		}
		
		public override LocalBean createBean()
		{
			return new Language_languagetable();	
		}

		public override string GetFilename()
		{
			return data_file;
		}
		
	}
}


