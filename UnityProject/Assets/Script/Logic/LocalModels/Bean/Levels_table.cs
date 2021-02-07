namespace LocalModels.Bean
{
	public class Levels_table : BaseLocalBean 
	{
		
		
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
		
		public override BaseLocalBean createBean()
		{
			return new Levels_table();	
		}
		
	}
}


