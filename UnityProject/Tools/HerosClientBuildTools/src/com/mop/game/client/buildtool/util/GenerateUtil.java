package com.mop.game.client.buildtool.util;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Writer;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.Map.Entry;

import javax.naming.directory.NoSuchAttributeException;

import org.apache.mina.common.ByteBuffer;
import org.jdom.Element;
import org.jdom.IllegalDataException;
import org.jdom.JDOMException;

import sun.reflect.FieldInfo;


import freemarker.template.Configuration;
import freemarker.template.DefaultObjectWrapper;
import freemarker.template.Template;
import freemarker.template.TemplateException;

/**
 * 
 * 文件名：GenerateUtil.java
 * <p>
 * 功能：生成工具，用于分析模板文件中的需要解析的字符串，并生成对应文件。
 * <p>
 * 版本：1.0.0
 * <p>
 * 日期：2010-10-22
 * <p>
 * 作者：chang.liu
 * <p>
 * 版权：(c)千橡游戏
 */
@SuppressWarnings("unchecked")
public class GenerateUtil {


	/**
	 * 生成本地数据bean
	 * @param exportPath
	 * @param templatePath
	 * @param fields
	 * @param beanPackage
	 * @param ClassName
	 */
	public static void buildClientLocalBean(String exportPath,String templatePath, List<TypeInfo> fields,
			String beanPackage, String ClassName) {
		String savePath = beanPackage.replaceAll("\\.", "\\" + File.separator);
		// 创建保存路径
		FileUtil.builderPath(exportPath
				+ File.separator + savePath);
		// 解析路径和文件名
		String templatePath_tmp = templatePath.substring(0,
				templatePath.lastIndexOf(File.separator));
		String templateName_tmp = templatePath.substring(templatePath
				.lastIndexOf(File.separator) + 1);
		/* 创建一个合适的configuration */
		Configuration cfg = new Configuration();
		try {
			cfg.setDefaultEncoding("UTF-8");
			cfg.setDirectoryForTemplateLoading(new File(templatePath_tmp));
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(0);
		}
		cfg.setObjectWrapper(new DefaultObjectWrapper());

		/* 获取或创建一个模版 */
		Template template = null;
		try {
			template = cfg.getTemplate(templateName_tmp);
		} catch (IOException e) {
			OtherUtils.showError("创建模版失败!");
			e.printStackTrace();
			System.exit(0);
		}

		// 准备数据
		Map<String, Object> root = new HashMap<String, Object>();
		root.put("ClassName",ClassName );
		root.put("FieldList", fields);

		// 生成文件
		Writer writer;
		try {
			String beanfile = exportPath
			+ File.separator+ savePath + File.separator + ClassName
			+ ".cs";
			writer = new BufferedWriter(new OutputStreamWriter(
					new FileOutputStream(beanfile), "UTF-8"));
			template.process(root, writer);
			writer.flush();
			writer.close();
			OtherUtils.printMsg(String.format("生成文件：%s", beanfile));
		} catch (IOException e) {
			OtherUtils.showError("写文件失败!");
			e.printStackTrace();
			return;
		} catch (TemplateException e) {
			OtherUtils.showError("模板操作异常!");
			e.printStackTrace();
			return;
		}
	}
	
	
	
	public static void buildClientModelImpl(String exportPath,String templatePath, List<TypeInfo> fields,
			String beanPackage, String ClassName) {
		String savePath = beanPackage.replaceAll("\\.", "\\" + File.separator);
		// 创建保存路径
		FileUtil.builderPath(exportPath
				+ File.separator + savePath);
		// 解析路径和文件名
		String templatePath_tmp = templatePath.substring(0,
				templatePath.lastIndexOf(File.separator));
		String templateName_tmp = templatePath.substring(templatePath
				.lastIndexOf(File.separator) + 1);
		/* 创建一个合适的configuration */
		Configuration cfg = new Configuration();
		try {
			cfg.setDefaultEncoding("UTF-8");
			cfg.setDirectoryForTemplateLoading(new File(templatePath_tmp));
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(0);
		}
		cfg.setObjectWrapper(new DefaultObjectWrapper());

		/* 获取或创建一个模版 */
		Template template = null;
		try {
			template = cfg.getTemplate(templateName_tmp);
		} catch (IOException e) {
			OtherUtils.showError("创建模版失败!");
			e.printStackTrace();
			System.exit(0);
		}

		// 准备数据
		Map<String, Object> root = new HashMap<String, Object>();
		root.put("ClassName",ClassName );

		try{
			root.put("KeyType", fields.get(0).getRealType());
			root.put("KeyFieldName",  fields.get(0).getFieldName());
		}catch(Exception e)
		{
			e.printStackTrace();
		}

		// 生成文件
		Writer writer;
		try {
			String beanfile = exportPath
			+ File.separator+ savePath + File.separator + ClassName+"ModelImpl"
			+ ".cs";
			writer = new BufferedWriter(new OutputStreamWriter(
					new FileOutputStream(beanfile), "UTF-8"));
			template.process(root, writer);
			writer.flush();
			writer.close();
			OtherUtils.printMsg(String.format("生成文件：%s", beanfile));
		} catch (IOException e) {
			OtherUtils.showError("写文件失败!");
			e.printStackTrace();
			return;
		} catch (TemplateException e) {
			OtherUtils.showError("模板操作异常!");
			e.printStackTrace();
			return;
		}
	}
	
	
	public static void buildClientModel(String exportPath,String templatePath, List<TypeInfo> fields,
			String beanPackage, String ClassName) {
		String savePath = beanPackage.replaceAll("\\.", "\\" + File.separator);
		// 创建保存路径
		FileUtil.builderPath(exportPath
				+ File.separator + savePath);
		// 解析路径和文件名
		String templatePath_tmp = templatePath.substring(0,
				templatePath.lastIndexOf(File.separator));
		String templateName_tmp = templatePath.substring(templatePath
				.lastIndexOf(File.separator) + 1);
		/* 创建一个合适的configuration */
		Configuration cfg = new Configuration();
		try {
			cfg.setDefaultEncoding("UTF-8");
			cfg.setDirectoryForTemplateLoading(new File(templatePath_tmp));
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(0);
		}
		cfg.setObjectWrapper(new DefaultObjectWrapper());

		/* 获取或创建一个模版 */
		Template template = null;
		try {
			template = cfg.getTemplate(templateName_tmp);
		} catch (IOException e) {
			OtherUtils.showError("创建模版失败!");
			e.printStackTrace();
			System.exit(0);
		}

		// 准备数据
		Map<String, Object> root = new HashMap<String, Object>();
		root.put("ClassName",ClassName );

		try{
			root.put("KeyType", fields.get(0).getRealType());
			root.put("KeyFieldName",  fields.get(0).getFieldName());
		}catch(Exception e)
		{
			e.printStackTrace();
		}

		// 生成文件
		Writer writer;
		try {
			String beanfile = exportPath
			+ File.separator+ savePath + File.separator + ClassName+"Model"
			+ ".cs";
			writer = new BufferedWriter(new OutputStreamWriter(
					new FileOutputStream(beanfile), "UTF-8"));
			template.process(root, writer);
			writer.flush();
			writer.close();
			OtherUtils.printMsg(String.format("生成文件：%s", beanfile));
		} catch (IOException e) {
			OtherUtils.showError("写文件失败!");
			e.printStackTrace();
			return;
		} catch (TemplateException e) {
			OtherUtils.showError("模板操作异常!");
			e.printStackTrace();
			return;
		}
	}
	
	
	public static void buildClientModelManager(String exportPath,String templatePath, Set<String> ClassNames,
			String beanPackage) {
		String savePath = beanPackage.replaceAll("\\.", "\\" + File.separator);
		// 创建保存路径
		FileUtil.builderPath(exportPath
				+ File.separator + savePath);
		// 解析路径和文件名
		String templatePath_tmp = templatePath.substring(0,
				templatePath.lastIndexOf(File.separator));
		String templateName_tmp = templatePath.substring(templatePath
				.lastIndexOf(File.separator) + 1);
		/* 创建一个合适的configuration */
		Configuration cfg = new Configuration();
		try {
			cfg.setDefaultEncoding("UTF-8");
			cfg.setDirectoryForTemplateLoading(new File(templatePath_tmp));
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(0);
		}
		cfg.setObjectWrapper(new DefaultObjectWrapper());

		/* 获取或创建一个模版 */
		Template template = null;
		try {
			template = cfg.getTemplate(templateName_tmp);
		} catch (IOException e) {
			OtherUtils.showError("创建模版失败!");
			e.printStackTrace();
			System.exit(0);
		}

		// 准备数据
		Map<String, Object> root = new HashMap<String, Object>();
		root.put("ClassList",ClassNames );


		// 生成文件
		Writer writer;
		try {
			String beanfile = exportPath
			+ File.separator+ savePath + File.separator + "LocalModelManager"
			+ ".cs";
			writer = new BufferedWriter(new OutputStreamWriter(
					new FileOutputStream(beanfile), "UTF-8"));
			template.process(root, writer);
			writer.flush();
			writer.close();
			OtherUtils.printMsg(String.format("生成文件：%s", beanfile));
		} catch (IOException e) {
			OtherUtils.showError("写文件失败!");
			e.printStackTrace();
			return;
		} catch (TemplateException e) {
			OtherUtils.showError("模板操作异常!");
			e.printStackTrace();
			return;
		}
	}
	
	public static void buildClientLocalStringKeys(String exportPath, List<List<DataInfo>> rows,String templatePath,String beanPackage)
	{
		
		List<LocalStringRow> stringRows = new LinkedList<LocalStringRow>();
		for(List<DataInfo> row : rows)
		{
			DataInfo iddata = row.get(0);
			DataInfo keydata = row.get(1);
			int id = Integer.parseInt(iddata.getValue());
			String keyName = keydata.getValue();
			LocalStringRow localString = new LocalStringRow();
			localString.setId(id);
			localString.setKeyName(keyName);
			stringRows.add(localString);
			
		}
		
		String savePath = beanPackage.replaceAll("\\.", "\\" + File.separator);
		// 创建保存路径
		FileUtil.builderPath(exportPath
				+ File.separator + savePath);
		// 解析路径和文件名
		String templatePath_tmp = templatePath.substring(0,
				templatePath.lastIndexOf(File.separator));
		String templateName_tmp = templatePath.substring(templatePath
				.lastIndexOf(File.separator) + 1);
		/* 创建一个合适的configuration */
		Configuration cfg = new Configuration();
		try {
			cfg.setDefaultEncoding("UTF-8");
			cfg.setDirectoryForTemplateLoading(new File(templatePath_tmp));
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(0);
		}
		cfg.setObjectWrapper(new DefaultObjectWrapper());

		/* 获取或创建一个模版 */
		Template template = null;
		try {
			template = cfg.getTemplate(templateName_tmp);
		} catch (IOException e) {
			OtherUtils.showError("创建模版失败!");
			e.printStackTrace();
			System.exit(0);
		}

		// 准备数据
		Map<String, Object> root = new HashMap<String, Object>();
		root.put("datas",stringRows );


		// 生成文件
		Writer writer;
		try {
			String beanfile = exportPath
			+ File.separator+ savePath + File.separator + "LocalStringKeys"
			+ ".cs";
			writer = new BufferedWriter(new OutputStreamWriter(
					new FileOutputStream(beanfile), "UTF-8"));
			template.process(root, writer);
			writer.flush();
			writer.close();
			OtherUtils.printMsg(String.format("生成文件：%s", beanfile));
		} catch (IOException e) {
			OtherUtils.showError("写文件失败!");
			e.printStackTrace();
			return;
		} catch (TemplateException e) {
			OtherUtils.showError("模板操作异常!");
			e.printStackTrace();
			return;
		}
		
	
	}
	public static void buildClientLocalDatas(String exportPath, List<List<DataInfo>> rows,
			 String ClassName)  throws Exception{
		
		
		String savePath = "datas";
		// 创建保存路径
		FileUtil.builderPath(exportPath
				+ File.separator + savePath);
		// 解析路径和文件名
		/* 创建一个合适的configuration */
		
		String filename = ClassName+".bytes";
		System.out.println("生成数据文件：："+filename);
		String outputfile = exportPath + File.separator + savePath +File.separator+filename;
		//byte[] messageBytes = outputStream.toByteArray();
		ByteBuffer buff = ByteBuffer.allocate(65525);
		
		
		FileOutputStream fos = new FileOutputStream(outputfile);
		int count = rows.size();
		buff.putInt(count);
		buff.flip();
		byte[] lenbuff = new byte[4];
		buff.get(lenbuff,0,4);
		
		fos.write(lenbuff);
		if(filename.equals("Camera_mode.bytes"))
		{
			for(List<DataInfo> row : rows)
			{
				short length = writeARowToBuff(row,buff);
				buff.flip();
				byte[] databuff = new byte[length];
				buff.get(databuff,0,length);
				fos.write(databuff);
				
			}
			return;
			
		
		}
		for(List<DataInfo> row : rows)
		{
			short length = writeARowToBuff(row,buff);
			buff.flip();
			byte[] databuff = new byte[length];
			buff.get(databuff,0,length);
			fos.write(databuff);
			
		}
		
		
		
		fos.flush();
		fos.close();


	
	}
	
	
	
	protected static short writeARowToBuff(List<DataInfo> row,ByteBuffer buff) throws Exception
	{	

		buff.clear();
		short length = 0;
		
		buff.putShort(((short)0));
		length = 2;
		for(DataInfo element : row )
		{
			length +=writeElementToBuff(element,buff);
			
		
		}
		buff.putShort(0, length);
		return length;
		
	
	}
	
	protected static short writeElementToBuff(DataInfo ele ,ByteBuffer buff) throws Exception
	{
		TypeInfo type = ele.getType();
		String value = ele.getValue();
		if(type.isInt())
		{
			
			int iv = getIntByString(value);
			buff.putInt(iv);
			return 4;
		
		}
		
		if(type.isLong())
		{
			long lv = getLongByString(value);
			buff.putLong(lv);
			return 8;
		}
		
		
		if(type.isFloat())
		{
			float fv = getFloatByString(value);
			buff.putFloat(fv);
			return 4;
		
		}
		if(type.isCommonString() || type.isLocalString())
		{
			if(value == null)
			{
				short l = 0;
				buff.putShort(l);
				return 2;
			}
			byte[] bytes = value.getBytes();
			
			
			short length = (short)bytes.length;
			short  slen = (short)(length + 2);
			buff.putShort(slen);
			buff.put(bytes);
			
			return (short)slen;
			
		}
		
		throw new Exception("unknonwn data type");
	
	}
	
	protected static String transCommonStringToKey(String commonString)
	{
		
		return null;
	}

	protected static int getIntByString(String v)
	{
		if(v == null || "".equals(v.trim()))
		{
			return 0;
		}
		return Integer.parseInt(v);
	
	}
	
	
	
	protected static long getLongByString(String v)
	{
		if(v == null || "".equals(v.trim()))
		{
			return 0;
		}
		return Long.parseLong(v);
	
	}
	
	protected static float getFloatByString(String v)
	{
		if(v == null || "".equals(v.trim()))
		{
			return 0.0f;
		}
		return Float.parseFloat(v);
	
	}	
}
