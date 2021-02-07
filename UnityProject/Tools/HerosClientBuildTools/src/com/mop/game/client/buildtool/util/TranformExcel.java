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
import java.util.List;
import java.util.Map;

import java.util.Set;
import java.util.zip.DeflaterOutputStream;

import net.sourceforge.pinyin4j.PinyinHelper;
import net.sourceforge.pinyin4j.format.HanyuPinyinCaseType;
import net.sourceforge.pinyin4j.format.HanyuPinyinOutputFormat;
import net.sourceforge.pinyin4j.format.HanyuPinyinToneType;
import net.sourceforge.pinyin4j.format.exception.BadHanyuPinyinOutputFormatCombination;

import org.apache.commons.io.FilenameUtils;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.output.XMLOutputter;

import com.mop.game.client.buildtool.config.FieldBean;

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
public class TranformExcel {

	 Element root;
	 Document doc;
	String outputfile = "clientdata.xml";
	String outputDatafile = "clientdata.dat";
	String outputDatafile2 = "clientdata2.dat";
	String outputDatafileAmf = "clientdata.amf";
	String[] rootNames = { "client", "data" };
	boolean isXml = false;
	Set<String> whiteSet = new HashSet<String>();
	Set<String> whiteSetOld = new HashSet<String>();
	Map<String, Map<String, Map<String, String>>> mainMap = new HashMap<String, Map<String, Map<String, String>>>();
	/**
	 * 转换一个根路径下所有的xls文件
	 * 
	 * @param path
	 *            根路径
	 * @throws IOException
	 */
	public void tranformPath(String path, boolean line) throws IOException {
//		whiteSet();
		// File f = new File(path);
		// File[] files = f.listFiles();
		// for (int i = 0; i < files.length; i++) {
		// if (files[i].isFile() && (files[i].getName()).endsWith(".xls")) {
		// this.tranformExcel(files[i].getAbsolutePath());
		// }
		// }

		root = new Element(rootNames[0]);
		doc = new Document(root);


		List<String> list = getAllFiles(path);

		for (String s : list) {
			if (FilenameUtils.isExtension(s, "xls")) {
				System.out.println("file:" + s);
				this.tranformExcel(s);
			}
		}
		XMLOutputter xmlout = new XMLOutputter("    ", line, "utf-8");
		try {
			xmlout.output(doc, new FileOutputStream(outputfile));
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		for(String key:mainMap.keySet()){
			System.out.println(key + "  "+mainMap.get(key).size());
		}
		SerializationContext serializationContext = new SerializationContext();
		Amf3Output amfOut = new Amf3Output(serializationContext);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(outputStream);
		amfOut.setOutputStream(dataOutputStream);
		amfOut.writeObject(mainMap);
		dataOutputStream.flush();
	
		byte[] messageBytes = outputStream.toByteArray();
		FileOutputStream fos = new FileOutputStream(outputDatafileAmf);
		fos.write(messageBytes);
		fos.flush();
		try {

			DeflaterOutputStream outputStream2 = new DeflaterOutputStream(
					new BufferedOutputStream(new FileOutputStream(
							outputDatafile)));
			outputStream2.write(messageBytes);

			outputStream2.flush();
			outputStream2.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		//writeWhiteSet();
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
				if (isXml){
					this.tranformSheet(configExcelPath, sheetName);
				} else {
					this.tranformSheet2(configExcelPath, sheetName);
				}

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

	/**
	 * 将excel文件一个sheet转换成as
	 * 
	 * @param path
	 *            xls的路径
	 * @param sheetName
	 *            sheet的名字
	 */
	private void tranformSheet(String path, String sheetName) throws Exception {
		// List<FieldBean> fields=new ArrayList<FieldBean>();
		if ("changelog".equalsIgnoreCase(sheetName)) {
			return;
		}
		// 获取原始数据
		// long s = System.currentTimeMillis();
		String[][] data = ExcelUtil.getSheetData(path, sheetName);

		if (data.length == 0) {
			return;
		}
		String xlsName = FilenameUtils.getBaseName(path);
		String className = changeChineseName(xlsName) + "_"
				+ changeChineseName(sheetName);

		// 首字母大写
		className = className.substring(0, 1).toUpperCase()
				+ className.substring(1);
		if (!whiteSetOld.contains(className)) {
			whiteSet.add(className);
		}
		Element subroot = new Element(className);
		// 获取是否辅助列
		String[] assistColumn = data[3];
		boolean isAssist = false;
		for (int j = 1; j < assistColumn.length; j++) {
			if (assistColumn[j].equals("1") || assistColumn[j].equals("3")) {
				isAssist = true;
				break;
			}
		}
		if (!isAssist) {
			return;
		}
		String tmp;
		FieldBean bean;
		// JSONObject json;
		// long s = System.currentTimeMillis();
		for (int i = 5; i < data.length; i++) {

			Element ssubroot = new Element(rootNames[1]);
			bean = new FieldBean();
			// 设置id
			bean.setId(data[i][0]);
			// json = new JSONObject();
			ssubroot.setAttribute("id", data[i][0]);
			if (!whiteSetOld.contains("id")) {
				whiteSet.add("id");
			}
			for (int j = 1; j < assistColumn.length; j++) {

				// 数据有效
				if (assistColumn[j].equals("1") || assistColumn[j].equals("3")) {
					if (!whiteSetOld.contains(data[0][j].trim())) {
						whiteSet.add(data[0][j].trim());
					}
					// 需要国际化 生成key
					if (data[4][j].equals("1")) {
						// json.put(data[0][j],getKey(path,sheetName,data[i][0],data[0][j]));
						ssubroot.setAttribute(data[0][j].trim(), getKey(path,
								sheetName, data[i][0], data[0][j].trim()));
					} else {
						// 数据类型
						tmp = (data[1][j]).toLowerCase();
						if (tmp.indexOf("string") != -1) {
							// json.put(data[0][j].trim(),data[i][j]);
							ssubroot.setAttribute(data[0][j].trim(), data[i][j]);
						} else {
							if (data[i][j] == null || "".equals(data[i][j])) {
								// json.put(data[0][j],DEFAULT);
								ssubroot.setAttribute(data[0][j].trim(), String
										.valueOf(DEFAULT));
								continue;
							} else {
								ssubroot.setAttribute(data[0][j].trim(), data[i][j]);
							}
							// if (tmp.indexOf("short") != -1) {
							// json.put(data[0][j],(short)Double.parseDouble(data[i][j]));
							//								
							// } else if (tmp.indexOf("int") != -1) {
							// json.put(data[0][j],(int)Double.parseDouble(data[i][j]));
							// } else if (tmp.indexOf("double") != -1) {
							// json.put(data[0][j],Double.parseDouble(data[i][j]));
							// } else if (tmp.indexOf("float") != -1) {
							// json.put(data[0][j],Float.parseFloat(data[i][j]));
							// } else if (tmp.indexOf("long") != -1) {
							// json.put(data[0][j],Long.parseLong(data[i][j]));
							// } else {
							// System.out.println(tmp);
							// throw new IllegalArgumentException("参数有误");
							// }
						}
					}
				}
			}
			// bean.setData(WebUtils.toString(json));
			// fields.add(bean);
			subroot.addContent(ssubroot);
		}
		root.addContent(subroot);
		// System.out.println(sheetName + "    data:" +
		// (System.currentTimeMillis() - s));
		// String savePath= "export" + File.separator;

		// 创建保存路径
		// FileUtil.builderPath(savePath);
		// BuildConfig config=new BuildConfig();
		// String templatePath=config.getTemplatePath();
		// 解析路径和文件名
		// String templatePath_tmp = templatePath.substring(0,
		// templatePath.lastIndexOf(File.separator));
		// String templateName_tmp = templatePath.substring(templatePath
		// .lastIndexOf(File.separator) + 1);
		/* 创建一个合适的configuration */
		// Configuration cfg = new Configuration();
		// try {
		// cfg.setDefaultEncoding("UTF-8");
		// cfg.setDirectoryForTemplateLoading(new File("./"));
		// } catch (IOException e) {
		// e.printStackTrace();
		// System.exit(0);
		// }
		// cfg.setObjectWrapper(new DefaultObjectWrapper());
		//		
		/* 获取或创建一个模版 */
		// Template template = null;
		// try {
		// template = cfg.getTemplate(templatePath);
		// } catch (IOException e) {
		// OtherUtils.showError("创建模版失败!");
		// e.printStackTrace();
		// System.exit(0);
		// }
		// 准备数据
		// Map<String, Object> root = new HashMap<String, Object>();
		// root.put("ClassName", className);
		// root.put("FieldList", fields);
		// root.put("Date", OtherUtils.timestampToDate());
		// // 生成文件
		// Writer writer;
		// try {
		// writer = new BufferedWriter(new OutputStreamWriter(
		// new FileOutputStream(savePath+className+"Data.as"), "UTF-8"));
		// template.process(root, writer);
		// writer.flush();
		// writer.close();
		// OtherUtils.printMsg(String.format("生成文件：%s",savePath+className+"Data.as"));
		// } catch (IOException e) {
		// OtherUtils.showError("写文件失败!");
		// e.printStackTrace();
		// return;
		// } catch (TemplateException e) {
		// OtherUtils.showError("模板操作异常!");
		// e.printStackTrace();
		// return;
		// }
	}
	/**
	 * 将excel文件一个sheet转换成as
	 * 
	 * @param path
	 *            xls的路径
	 * @param sheetName
	 *            sheet的名字
	 */
	private void tranformSheet2(String path, String sheetName) throws Exception {
		if ("changelog".equalsIgnoreCase(sheetName)) {
			return;
		}
		// 获取原始数据
		String[][] data = ExcelUtil.getSheetData(path, sheetName);

		if (data.length == 0) {
			return;
		}
		String xlsName = FilenameUtils.getBaseName(path);
		String className = changeChineseName(xlsName) + "_"
				+ changeChineseName(sheetName);

		// 首字母大写
		className = className.substring(0, 1).toUpperCase()
				+ className.substring(1);
		if (!whiteSetOld.contains(className)) {
			whiteSet.add(className);
		}
		Element subroot = new Element(className);
		Map<String, Map<String, String>> subMap= new HashMap<String, Map<String, String>>();
		// 获取是否辅助列
		String[] assistColumn = data[3];
		boolean isAssist = false;
		for (int j = 1; j < assistColumn.length; j++) {
			if (assistColumn[j].equals("1") || assistColumn[j].equals("3")) {
				isAssist = true;
				break;
			}
		}
		if (!isAssist) {
			return;
		}
		String tmp;

		for (int i = 5; i < data.length; i++) {
			Map<String, String> ssubMap = new HashMap<String, String>();
			Element ssubroot = new Element(rootNames[1]);

			ssubroot.setAttribute("id", data[i][0]);
			ssubMap.put("id", data[i][0]);
			if (!whiteSetOld.contains("id")) {
				whiteSet.add("id");
			}
			for (int j = 1; j < assistColumn.length; j++) {

				// 数据有效
				if (assistColumn[j].equals("1") || assistColumn[j].equals("3")) {
					if (!whiteSetOld.contains(data[0][j].trim())) {
						whiteSet.add(data[0][j].trim());
					}
					// 需要国际化 生成key
					if (data[4][j].equals("1")) {
						ssubroot.setAttribute(data[0][j].trim(), getKey(path,
								sheetName, data[i][0], data[0][j]));
						ssubMap.put(data[0][j].trim(), getKey(path,
								sheetName, data[i][0], data[0][j].trim()));
					} else {
						// 数据类型
						tmp = (data[1][j]).toLowerCase();
						if (tmp.indexOf("string") != -1) {
							ssubroot.setAttribute(data[0][j].trim(), data[i][j]);
							ssubMap.put(data[0][j].trim(), data[i][j]);
						} else {
							if (data[i][j] == null || "".equals(data[i][j].trim())) {
								ssubroot.setAttribute(data[0][j].trim(), String
										.valueOf(DEFAULT));
								ssubMap.put(data[0][j].trim(), String
										.valueOf(DEFAULT));
								continue;
							} else {
								ssubroot.setAttribute(data[0][j].trim(), data[i][j]);
								ssubMap.put(data[0][j].trim(), data[i][j]);
							}
						}
					}
				}
			}
			subroot.addContent(ssubroot);
			subMap.put(data[i][0], ssubMap);
		}
		root.addContent(subroot);
		mainMap.put(className, subMap);
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
		String xlsName = FilenameUtils.getBaseName(path);
		HSSFWorkbook workBook = ExcelUtil.getWorkBook(path);
		// 得到key
		return changeChineseName(xlsName) + "_"
				+ ExcelUtil.getSheetIndex(workBook, sheetName) + "_" + id + "_"
				+ ExcelUtil.getColumnIndex(workBook, sheetName, columnName);
	}

	// private String toPinyin(String data) {
	// // 转拼音
	// data = this.toPinyin1(data);
	// // 去掉特殊字符
	// data = data.replaceAll("\\[", "");
	// data = data.replaceAll("\\]", "");
	// data = data.replaceAll("1", "");
	// data = data.replaceAll("2", "");
	// data = data.replaceAll("3", "");
	// data = data.replaceAll("4", "");
	// // 装小写
	// data = data.toLowerCase();
	// return data;
	// }
	//
	// private String toPinyin1(String data) {
	// StringBuilder sb = new StringBuilder();
	// char[] tmp = data.toCharArray();
	// String[] pinyin_tmp;
	// for (char o : tmp) {
	// if (((int) o) < 127) {
	// sb.append(o);
	// } else {
	// sb.append("[");
	// pinyin_tmp = PinyinHelper.toHanyuPinyinStringArray(o);
	// for (String t : pinyin_tmp) {
	// sb.append(t);
	// }
	// sb.append("]");
	// }
	// }
	// return sb.toString();
	// }

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

	public void whiteSet() throws IOException {
		BufferedReader bufferedReader = new BufferedReader(new FileReader(
				"resource/template/whitelist.h"));
		String str = null;

		while ((str = bufferedReader.readLine()) != null) {
			String[] args = str.split(" ");
			if (args.length > 1) {
				whiteSetOld.add(args[1]);
			}
		}
		// bufferedReader.
		if (!whiteSetOld.contains("client")) {
			whiteSet.add("client");
		}
		if (!whiteSetOld.contains("data")) {
			whiteSet.add("data");
		}
	}

	public void writeWhiteSet() throws IOException {
		BufferedReader bufferedReader = new BufferedReader(new FileReader(
				"resource/template/whitelist.h"));
		Writer out = new OutputStreamWriter(
				new FileOutputStream("whitelist.h"), "utf-8");
		BufferedWriter bufferedWriter = new BufferedWriter(out);
		String str = null;

		while ((str = bufferedReader.readLine()) != null) {
			bufferedWriter.write(str, 0, str.length());
			bufferedWriter.newLine();
		}
		for (String tmp : whiteSet) {
			tmp = "#undef " + tmp;
			bufferedWriter.write(tmp, 0, tmp.length());
			bufferedWriter.newLine();
		}
		bufferedWriter.close();
		out.close();
		bufferedReader.close();
		System.out.println("1");
	}

	public static void main(String[] args) throws IOException {
		TranformExcel tranformExcel = new TranformExcel();
		tranformExcel.whiteSet();
		tranformExcel.writeWhiteSet();
	}
}
