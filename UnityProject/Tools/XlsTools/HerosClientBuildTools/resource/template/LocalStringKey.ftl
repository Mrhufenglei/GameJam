namespace resource.Local{
	public class LocalStringKeys  {
		<#list datas as row>
			public const string ${row.keyName} =  "${row.keyName}";
		</#list>
	}
}