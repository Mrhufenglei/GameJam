package com.mop.game.client.buildtool.util;

public class TypeInfo
{
	
	public static final String TYPE_INT = "int";
	public static final String TYPE_LONG = "long";
	public static final String TYPE_STRING = "string";
	public static final String TYPE_FLOAT = "float";
	
	public static final String CTYPE_INT = "int";
	public static final String CTYPE_LONG = "long";
	public static final String CTYPE_FLOAT = "float";
	public static final String CTYPE_LOCALSTRING = "localString";
	public static final String CTYPE_COMMONSTRING = "commonString";

	private String fieldName;
	private String fieldType;
	public String getFieldName() {
		return fieldName;
	}
	public String getFieldType() {
		return fieldType;
	}
	public TypeInfo(String fieldName, String fieldType) {
		super();
		this.fieldName = fieldName;
		this.fieldType = fieldType;
	}
	
	public String getRealType() throws Exception
	{
		if(fieldType.equalsIgnoreCase(CTYPE_LOCALSTRING))
		{
			return "string";
			
		}
		if(fieldType.equalsIgnoreCase(CTYPE_COMMONSTRING))
		{
			return "string";
			
		}
		if(fieldType.equalsIgnoreCase(CTYPE_INT))
		{
			return "int";
			
		}

		if(fieldType.equalsIgnoreCase(CTYPE_LONG))
		{
			return "long";
			
		}
		
		if(fieldType.equalsIgnoreCase(CTYPE_FLOAT))
		{
			return "float";
		
		}
		throw new Exception("unknown c# type!!");
	}
	
	
	public boolean isLocalString()
	{
		return fieldType.equalsIgnoreCase(CTYPE_LOCALSTRING);
		
	}
	
	public boolean isCommonString()
	{
		return fieldType.equalsIgnoreCase(CTYPE_COMMONSTRING);
	
	}
	
	public boolean isInt()
	{
		return fieldType.equalsIgnoreCase(CTYPE_INT);
		
	}
	
	
	public boolean isLong()
	{
		return fieldType.equalsIgnoreCase(CTYPE_LONG);
	}
	
	public boolean isFloat()
	{
		
		return fieldType.equalsIgnoreCase(CTYPE_FLOAT);
	}
	
	

}
