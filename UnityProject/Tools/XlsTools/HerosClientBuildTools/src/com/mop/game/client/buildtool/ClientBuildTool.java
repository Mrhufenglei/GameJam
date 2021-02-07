package com.mop.game.client.buildtool;

import java.io.File;

import org.apache.commons.io.FileUtils;

import com.mop.game.client.buildtool.util.TranformExcel;
import com.mop.game.client.buildtool.util.TranformExcel2CSharp;

public class ClientBuildTool {

	/**
	 * @param args
	 */
	public static void main(String[] args) throws Exception{
		if (args.length < 2){
			System.out.println("参数不足");
			System.exit(0);
		}
		long s = System.currentTimeMillis();
//		File fil = new File("export");
//		if (fil.exists()){
//			FileUtils.deleteDirectory(fil);
//		}

		
		//BuildConfig config=new BuildConfig();
		TranformExcel2CSharp tfe = new TranformExcel2CSharp();
		tfe.tranformCSharp(args[0], args[1].equals("true") ? true : false);
		System.out.println("build is ok    " + (System.currentTimeMillis() - s));
		
	}

}
