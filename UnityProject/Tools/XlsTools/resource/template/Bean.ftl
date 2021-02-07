namespace LocalModels.Bean
{
	public class ${ClassName} : BaseLocalBean 
	{
		
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
		
		public override BaseLocalBean createBean()
		{
			return new ${ClassName}();	
		}
		
	}
}


