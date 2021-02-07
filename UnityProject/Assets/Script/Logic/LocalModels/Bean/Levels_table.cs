namespace LocalModels.Bean
{
	public class Levels_table : LocalBean 
	{
	
		public static readonly string data_file = "Levels_table";
		
		
		public int id{get;set;}			
		
		public string notes{get;set;}			
		
		public string levelName{get;set;}			
		
		public int titleName{get;set;}			
		
		public int isShow{get;set;}			
		
		protected override bool readImpl()
		{
			id = readInt();
			notes = readLocalString();
			levelName = readLocalString();
			titleName = readInt();
			isShow = readInt();
			return true;
		}
		
		public override LocalBean createBean()
		{
			return new Levels_table();	
		}

		public override string GetFilename()
		{
			return data_file;
		}
		
	}
}


