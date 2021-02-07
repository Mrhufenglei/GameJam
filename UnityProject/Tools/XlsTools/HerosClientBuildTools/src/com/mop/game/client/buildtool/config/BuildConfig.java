package com.mop.game.client.buildtool.config;

import com.mop.game.client.buildtool.util.OtherUtils;

public class BuildConfig {
	public String currentPath = OtherUtils.getCurrentPath();
	public String configExcelPath = currentPath + "config";
	public String templatePath = "Data.ftl";
	
	public String getConfigExcelPath() {
		return configExcelPath;
	}
	
	public void setConfigExcelPath(String configXmlPath) {
		this.configExcelPath = configXmlPath;
	}
	
	public String getTemplatePath() {
		return templatePath;
	}
	
	public void setTemplatePath(String templatePath) {
		this.templatePath = templatePath;
	}
}
