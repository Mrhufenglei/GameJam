package com.mop.game.client.buildtool.util;

import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.io.Writer;
import java.util.ArrayList;

import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import java.util.Set;
import java.util.zip.DeflaterOutputStream;

import net.sourceforge.pinyin4j.PinyinHelper;
import net.sourceforge.pinyin4j.format.HanyuPinyinCaseType;
import net.sourceforge.pinyin4j.format.HanyuPinyinOutputFormat;
import net.sourceforge.pinyin4j.format.HanyuPinyinToneType;
import net.sourceforge.pinyin4j.format.exception.BadHanyuPinyinOutputFormatCombination;

import org.apache.commons.io.FileUtils;
import org.apache.commons.io.FilenameUtils;
import org.apache.commons.lang.StringUtils;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.output.XMLOutputter;

import com.mop.game.client.buildtool.config.FieldBean;
import com.mop.game.client.buildtool.config.MsgBuildConfig;

import flex.messaging.io.SerializationContext;
import flex.messaging.io.amf.Amf3Output;

/**
 * 标题: Excel读取工具.<br>
 * 
 * 描述：Excel读取工具，主要用于读取excel中的数据.<br>
 * 
 * 版权：Copyright (C) 1997-2008 Oak Pacific Interactive. <br>
 * 
 * @author 黄杰 2010-09-08 10:50 新建
 * 
 * @since jdk1.6
 * 
 * @version 0.1
 */
public class TranformExcel2CSharp {

	
	MsgBuildConfig config = new MsgBuildConfig();
	
	Map<String, List<List<DataInfo> > > mainMap = new HashMap<String, List<List<DataInfo>>>();
	

	Map<String, List<TypeInfo>> typeMap = new HashMap<String,List<TypeInfo>>();
	
	
	/**
	 * 转换一个根路径下所有的xls文件
	 * 
	 * @param path
	 *            根路径
	 * @throws IOException
	 */


	
	public void tranformCSharp(String path, boolean line) throws IOException {



		List<String> list = getAllFiles(path);

		for (String s : list) {
			if (FilenameUtils.isExtension(s, "xls")) {
				System.out.println("file:" + s);
				this.tranformExcel(s);
			}
		}
	
		for(String key:mainMap.keySet()){
			System.out.println(key + "  "+mainMap.get(key).size());
		}

		String exportpath = config.getExportPath();
		try{
			makePath(exportpath);
	
			productCSharpType(exportpath);
			
			productCSharpModelImpl(exportpath);
			
			productCSharpModel(exportpath);
			
			productCSharpModelManager(exportpath);
			
			productCSharpData(exportpath);
			
			productCSharpLocalStringKeys(exportpath);
			
			
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		
	}
	
	
	protected  void productCSharpType(String export)
	{
		
		try{
			Set<String>  classNames = typeMap.keySet();
			for(String className : classNames)
			{
				List<TypeInfo> subType = typeMap.get(className);
				makeBean(export,className,subType);
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
			
		}
		
		
		
		
	}
	
	protected  void productCSharpModelImpl(String export)
	{
		
		try{
			Set<String>  classNames = typeMap.keySet();
			for(String className : classNames)
			{
				List<TypeInfo> subType = typeMap.get(className);
				makeModelImpl(export,className,subType);
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
			
		}
		
		
		
		
	}
	
	protected  void productCSharpModel(String export)
	{
		
		try{
			Set<String>  classNames = typeMap.keySet();
			for(String className : classNames)
			{
				List<TypeInfo> subType = typeMap.get(className);
				makeModel(export,className,subType);
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
			
		}
		
		
		
		
	}
	
	protected  void productCSharpModelManager(String export)
	{
		
		try{
			Set<String>  classNames = typeMap.keySet();
			GenerateUtil.buildClientModelManager(export,config.clientModelManagerTemplatePath, classNames, config.clientModelManagerPackage);
		}
		catch(Exception e)
		{
			e.printStackTrace();
			
		}
		
		
		
		
	}
	
	protected void makePath(String path) throws Exception
	{
		File dir = new File(path);
		if(dir.exists() && dir.isDirectory())
		{
			FileUtils.deleteDirectory(dir);
		}
		FileUtils.forceMkdir(dir);
		
	
	}
	
	protected void makeBean(String path,String className,List<TypeInfo> type)
	{
		GenerateUtil.buildClientLocalBean(path,config.getClientBeanTemplatePath(), type, config.getClientBeanPackage(), className);
		
		
	}

	protected void makeModelImpl(String path,String className,List<TypeInfo> type)
	{
		GenerateUtil.buildClientModelImpl(path,config.clientModelImplTemplatePath, type, config.clientModelImplPackage, className);
	
	}

	protected void makeModel(String path,String className,List<TypeInfo> type)
	{
		GenerateUtil.buildClientModel(path,config.clientModelTemplatePath, type, config.clientModelPackage, className);
	
	}
	protected void productCSharpData(String exportpath)
	{
		try{
			Set<String> keys = mainMap.keySet();
			for(String className : keys)
			{
				
				List<List<DataInfo>> rows = mainMap.get(className);
				
				GenerateUtil.buildClientLocalDatas(exportpath, rows, className);				
			}
		}catch(Exception e)
		{
			e.printStackTrace();
			
		}
	
	}
	
	protected void productCSharpLocalStringKeys(String exportpath)
	{
		try{
			List<List<DataInfo>> rows = mainMap.get("LocalString_LocalString");
			GenerateUtil.buildClientLocalStringKeys(exportpath, rows,config.clientLocalStringKeysTemplatePath,config.clientLocalStringKeysPackage);
		}
		catch(Exception e)
		{
			e.printStackTrace();
		
		}
		
	
	}
	public static List<String> getAllFiles(String absDir) {
		List<String> files = new ArrayList<String>();

		File fileDir = new File(absDir);
		String[] list = fileDir.list();
		// File file = new File(absDir);
		// if (!file.exists()){
		// System.out.println(file.getPath());
		// file.mkdirs();
		// }
		for (String s : list) {
			String name = absDir + "/" + s;
			File ins = new File(name);
			if (ins.isFile()) {
				files.add(name);
			} else {
				files.addAll(getAllFiles(name));
			}
		}

		return files;
	}

	/**
	 * 转换一个xls文件
	 * 
	 * @param configExcelPath
	 *            要转换的xls的路径
	 */
	private void tranformExcel(String configExcelPath) {
		String filename = getFilename(configExcelPath);
		String[] sheetNames = null;
		try {
			// 获取sheet的名字
			// long s = System.currentTimeMillis();
			sheetNames = ExcelUtil.getSheetName(configExcelPath);
			// System.out.println("sheetNames:" +(System.currentTimeMillis() -
			// s));
		} catch (Exception e) {
			OtherUtils.showError("获取sheet的名字失败");
			e.printStackTrace();
			System.exit(0);
		}
		// 转换所有sheet的数据
		String sheetName = null;
		try {
			for (int i = 0; i < sheetNames.length; i++) {
				sheetName = sheetNames[i];
				// long s = System.currentTimeMillis();
//				if (isXml){
//					this.tranformSheet(configExcelPath, sheetName);
//				} else {
//					this.tranformSheet2(configExcelPath, sheetName);
//				}
				this.tranformSheet2CSharp(configExcelPath, sheetName);

				// System.out.println(sheetName +":"
				// +(System.currentTimeMillis() - s));
			}
		} catch (Exception e) {
			OtherUtils.showError(String.format("读取配置文件失败，请检查!\n路径：%s\n页名:%s",
					configExcelPath, sheetName));
			e.printStackTrace();
			System.exit(0);
		}
	}

	private static short DEFAULT = 0;



	
	protected String getFilename(String Fullfile)
	{
		int idx = StringUtils.indexOf(Fullfile, "/");
		String filename = StringUtils.substring(Fullfile, idx+1);
		return filename;
		
	}
	
	/**
	 * 将excel文件一个sheet转换成as
	 * 
	 * @param path
	 *            xls的路径
	 * @param sheetName
	 *            sheet的名字
	 */
	private void tranformSheet2CSharp(String path, String sheetName) throws Exception {
		if ("changelog".equalsIgnoreCase(sheetName)) {
			return;
		}
		// 获取原始数据
		String[][] data = ExcelUtil.getSheetData(path, sheetName);

		if (data.length == 0) {
			return;
		}
		String xlsName = FilenameUtils.getBaseName(path);
		String pinyinXlsName =changeChineseName(xlsName); 
		String className =  pinyinXlsName+ "_"
				+ changeChineseName(sheetName);

		// 首字母大写
		className = className.substring(0, 1).toUpperCase()
				+ className.substring(1);
	
	
		String[] fieldColumn = data[0];
		//Map<String, Map<String, String>> subMap= new HashMap<String, Map<String, String>>();
		List<List<DataInfo>> rows = new LinkedList<List<DataInfo>>();
		// 获取是否辅助列
		//System.out.println("data llength is::"+data.length);
		if(data.length < 4)
		{
			int i = 0;
			int a = i;
		}
		String[] assistColumn = data[3];
		boolean isAssist = false;
		if(!(assistColumn[0].equals("1") || assistColumn[0].equals("3")))
		{
			return;
		}
		for (int j = 1; j < fieldColumn.length; j++) {
			if (assistColumn[j].equals("1") || assistColumn[j].equals("3")) {
				isAssist = true;
				break;
			}
		}
		if (!isAssist) {
			return;
		}
		String tmp;
		
		
		String[] typeColumn = data[1];
		String[] lanColumn  = data[4];
		
		List<TypeInfo> fieldTypes = new LinkedList<TypeInfo>();
		Map<Integer,TypeInfo> indexTypes = new HashMap<Integer,TypeInfo>();
		for(int j=0;j < fieldColumn.length;j++)
		{
			if(!(assistColumn[j].equals("1") || assistColumn[j].equals("3")))
			{
				continue;
			}
			
			String fieldName = normalizeFieldName(fieldColumn[j]);
			String typeStr = typeColumn[j];
			typeStr = typeStr.trim();
			if(typeStr.equalsIgnoreCase(TypeInfo.TYPE_INT))
			{
				TypeInfo type =new TypeInfo(fieldName, TypeInfo.CTYPE_INT); 
				fieldTypes.add(type);
				indexTypes.put(j, type);
				
			}else if(typeStr.equalsIgnoreCase(TypeInfo.TYPE_LONG))
			{
				TypeInfo type =new TypeInfo(fieldName, TypeInfo.CTYPE_LONG);
				fieldTypes.add(type);
				indexTypes.put(j, type);
			}else if(typeStr.equals(TypeInfo.TYPE_FLOAT))
			{
				TypeInfo type =new TypeInfo(fieldName, TypeInfo.CTYPE_FLOAT);
				fieldTypes.add(type);
				indexTypes.put(j, type);				
				
			}else if(typeStr.equalsIgnoreCase(TypeInfo.TYPE_STRING))
			{
				if(lanColumn[j].equals("1"))
				{
					TypeInfo type =new TypeInfo(fieldName, TypeInfo.CTYPE_COMMONSTRING);
					fieldTypes.add(type);
					indexTypes.put(j, type);
					
				}
				else
				{
					TypeInfo type =new TypeInfo(fieldName, TypeInfo.CTYPE_LOCALSTRING);
					fieldTypes.add(type);
					indexTypes.put(j, type);
				}
				
			}else
			{
				throw new Exception("unknonw data type::"+typeStr);
				
			}
			
			
		}
		
		typeMap.put(className, fieldTypes);

		for (int i = 5; i < data.length; i++) {
//			Map<String, String> ssubMap = new HashMap<String, String>();
//			ssubMap.put("id", data[i][0]);
			List<DataInfo> row = new LinkedList<DataInfo>();
			for (int j = 0; j < fieldColumn.length; j++) {

				

				// 数据有效
				if (assistColumn[j].equals("1") || assistColumn[j].equals("3")) {
					TypeInfo type = indexTypes.get(j);
					// 需要国际化 生成key
					DataInfo element = null;
					if (type.isCommonString()) {
						String key = getKey(path,sheetName, data[i][0], data[0][j].trim());
						element = new DataInfo(type,key);
					} else {
						element = new DataInfo(type,data[i][j]);
						
					}
					row.add(element);
				}
			}
			rows.add(row);
		}
		mainMap.put(className, rows);
	}

	/**
	 * 得到key excel名_sheet的index（从0开始）_每行的id_列的index（从0开始）
	 * 
	 * @param path
	 * @param sheetName
	 * @param columnName
	 * @return
	 * @throws Exception
	 */
	private String getKey(String path, String sheetName, String id,
			String columnName) throws Exception {
		String xlsName = FilenameUtils.getName(path);
		HSSFWorkbook workBook = ExcelUtil.getWorkBook(path);
		// 得到key
		return changeChineseName(xlsName) + "_"
				+ ExcelUtil.getSheetIndex(workBook, sheetName) + "_" + id + "_"
				+ ExcelUtil.getColumnIndex(workBook, sheetName, columnName);
	}



	public static String changeChineseName(String str) {
		// StringBuilder sb = new StringBuilder();
		StringBuffer sb = new StringBuffer();
		char[] arr = str.toCharArray();
		HanyuPinyinOutputFormat outFormat = new HanyuPinyinOutputFormat();
		outFormat.setCaseType(HanyuPinyinCaseType.LOWERCASE);
		outFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);
		// outFormat.setVCharType(HanyuPinyinVCharType.WITH_U_AND_COLON);
		for (int i = 0; i < arr.length; i++) {
			if (arr[i] > 128) {
				try {
					String[] temp = PinyinHelper.toHanyuPinyinStringArray(
							arr[i], outFormat);
					// for (String s : temp) {
					// sb.append(s);
					// }
					sb.append(temp[0]);
				} catch (BadHanyuPinyinOutputFormatCombination e) {
					e.printStackTrace();
				}
			} else {
				sb.append(arr[i]);
			}
		}
		return sb.toString();
	}



	public static void main(String[] args) throws IOException {
		TranformExcel tranformExcel = new TranformExcel();
	}
	
	protected String normalizeFieldName(String name)
	{
		return name.replaceAll(" ", "_");
		
	
	}
}
