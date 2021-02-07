package com.mop.game.client.buildtool.util;

public class DataInfo {
	private TypeInfo type;
	private String value;

	
	
	public DataInfo(TypeInfo type,String value)
	{
		this.type = type;
		this.value = value;

		
	}

	public TypeInfo getType() {
		return type;
	}

	public String getValue() {
		return value;
	}
	
}
