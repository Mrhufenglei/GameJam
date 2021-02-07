package com.mop.game.client.buildtool.config;

import com.mop.game.client.buildtool.util.OtherUtils;


/**
 * 
 * 文件名：MsgBuildConfig.java
 * <p>
 * 功能：消息生成配置文件信息
 * <p>
 * 版本：1.0.0
 * <p>
 * 日期：2010-10-22
 * <p>
 * 作者：chang.liu
 * <p>
 * 版权：(c)千橡游戏
 */
public class MsgBuildConfig {
	//生成读写方法体开关 true为无论什么类型消息都生成读写方法体，false为根据消息类型生成
	public static boolean READWRITE_ALL=false;
	
	
	public String currentPath = "";
	

	
	public String clientBeanTemplatePath = currentPath + "resource\\template\\Bean.ftl";
	public String clientBeanPackage = "LocalModels.Bean";
	
	public String clientModelImplTemplatePath = currentPath + "resource\\template\\ModelImpl.ftl";
	public String clientModelImplPackage = "LocalModels.ModelImpl";
	
	public String clientModelTemplatePath = currentPath + "resource\\template\\Model.ftl";
	public String clientModelPackage = "LocalModels.Model";
	
	public String clientModelManagerTemplatePath = currentPath + "resource\\template\\ModelManager.ftl";
	public String clientModelManagerPackage = "LocalModels";
	
	public String clientLocalStringKeysTemplatePath = currentPath + "resource\\template\\LocalStringKey.ftl";
	public String clientLocalStringKeysPackage = "resource.Local";
	
	public String exportPath = currentPath+"export";

	
	public String getExportPath()
	{
		return exportPath;
	}

	public String getClientBeanTemplatePath() {
		return clientBeanTemplatePath;
	}

	public void setClientBeanTemplatePath(String clientBeanTemplatePath) {
		this.clientBeanTemplatePath = clientBeanTemplatePath;
	}

	public String getClientBeanPackage() {
		return clientBeanPackage;
	}

	public void setClientBeanPackage(String clientBeanPackage) {
		this.clientBeanPackage = clientBeanPackage;
	}

}
