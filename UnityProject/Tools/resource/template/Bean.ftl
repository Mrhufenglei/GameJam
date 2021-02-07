namespace LocalModels.Bean
{
	public class ${ClassName} : LocalBean 
	{
	
		public static readonly string data_file = "${ClassName}";
		
		<#list FieldList as field>
		
		public ${field.realType} ${field.fieldName}{get;set;}			
		</#list>
		
		protected override bool readImpl()
		{
			<#list FieldList as field>
			${field.fieldName} = read${field.fieldType?cap_first}();
			</#list>
			return true;
		}
		
		public override LocalBean createBean()
		{
			return new ${ClassName}();	
		}

		public override string GetFilename()
		{
			return data_file;
		}
		
	}
}


