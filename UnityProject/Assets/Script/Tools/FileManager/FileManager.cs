using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// 包外文件管理器
/// </summary>
public class FileManager
{
    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ReadAllText(string path)
    {
        string _info = string.Empty;
        try
        {
            if (File.Exists(path))
            {
                _info = File.ReadAllText(path);
            }
        }
        catch (System.Exception e)
        {
            _info = string.Empty;
            throw new System.Exception("FileManager.ReadAllText ------>\n" + e.Message);
        }
        return _info;
    }
    public static byte[] ReadAllBytes(string path)
    {
        byte[] bytes = null;
        try
        {
            if (File.Exists(path))
            {
                bytes = File.ReadAllBytes(path);
            }
        }
        catch (System.Exception e)
        {
            bytes = null;
            throw new System.Exception("FileManager.ReadAllBytes ------>\n" + e.Message);
        }
        return bytes;
    }
    /// <summary>
    /// 创建文件夹根据有后缀的目录
    /// </summary>
    /// <param name="path">有后缀的目录</param>
    public static void CreateDirectory(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("CreateDirectory is miss  " + e);
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager.CreateDirectory ------>" + "b    \n" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager.CreateDirectory ------>\n" + e.Message);
            }
        }
    }
    public static void CopyDirectory(string from, string to)
    {
        try
        {
            //检查是否存在目的目录  
            if (!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }
            if (Directory.Exists(from) == true)
            {
                //先来复制文件  
                DirectoryInfo directoryInfo = new DirectoryInfo(from);
                FileInfo[] files = directoryInfo.GetFiles();

                //复制所有文件  
                for (int i = 0; i < files.Length; i++)
                {
                    string _toPath = Path.Combine(to, files[i].Name);
                    Debug.Log("拷贝文件 --->" + files[i].DirectoryName + "-->" + _toPath);
                    DeleteFile(_toPath);
                    files[i].CopyTo(_toPath);
                }
                //最后复制目录  
                DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
                for (int d = 0; d < directoryInfoArray.Length; d++)
                {
                    CopyDirectory(Path.Combine(from, directoryInfoArray[d].Name), Path.Combine(to, directoryInfoArray[d].Name));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("CreateDirectory is miss  " + e);
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager.CopyDirectory ------>" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager.CopyDirectory ------>\n" + e.Message);
            }
        }
    }
    public static void CopyFile(string from, string to)
    {
        try
        {
            if (File.Exists(from))
            {
                CreateDirectory(Path.GetDirectoryName(to));
                DeleteFile(to);
                FileInfo _fileInfo = new FileInfo(from);
                _fileInfo.CopyTo(to, true);
            }
        }
        catch (System.Exception e)
        {
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager..CopyFile(string from, string to) ------>" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager..CopyFile(string from, string to) ------>\n" + e.Message);
            }
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <param name="enconding"></param>
    public static void WriteAllBytes(string path, string info, Encoding enconding)
    {
        try
        {
            File.WriteAllText(path, info, enconding);
        }
        catch (System.Exception e)
        {
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager.Save(string path, string info, Encoding enconding) ------>" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager.Save(string path, string info, Encoding enconding) ------>\n" + e.Message);
            }
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="bytes"></param>
    public static void WriteAllBytes(string path, byte[] bytes)
    {
        try
        {
            File.WriteAllBytes(path, bytes);
        }
        catch (System.Exception e)
        {
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager.WriteAllBytes(string path, byte[] bytes) ------>" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager.WriteAllBytes(string path, byte[] bytes) ------>\n" + e.Message);
            }
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    public static void WriteAllText(string path, string info)
    {
        try
        {
            File.WriteAllText(path, info);
        }
        catch (System.Exception e)
        {
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager.WriteAllText(string path, string info) ------>" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager.WriteAllText(string path, string info) ------>\n" + e.Message);
            }
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <param name="encoding"></param>
    public static void WriteAllText(string path, string info, Encoding encoding)
    {
        try
        {
            File.WriteAllText(path, info, encoding);
        }
        catch (System.Exception e)
        {
            if (e.Message.Contains("Disk full"))
            {
                throw new System.Exception("FileManager.WriteAllText(string path, string info, Encoding encoding) ------>" + e.Message);
            }
            else
            {
                throw new System.Exception("FileManager.WriteAllText(string path, string info, Encoding encoding) ------>\n" + e.Message);
            }
        }
    }

    /// <summary>
    /// 删除文件夹以及文件夹内的文件
    /// </summary>
    /// <param name="path">路径</param>
    public static void DelectDirectory(string path)
    {
        try
        {
            //如果存在目录文件，就将其目录文件删除 
            if (Directory.Exists(path))
            {
                string[] _entries = Directory.GetFileSystemEntries(path);
                for (int e = 0; e < _entries.Length; e++)
                {
                    if (File.Exists(_entries[e]))
                    {
                        FileInfo file = new FileInfo(_entries[e]);
                        if (file.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            file.Attributes = FileAttributes.Normal;//去掉文件属性 
                        }
                        file.Delete();//直接删除其中的文件 
                    }
                    else
                    {
                        DelectDirectory(_entries[e]);//递归删除 
                    }
                }
                //删除顶级文件夹
                DirectoryInfo DirInfo = new DirectoryInfo(path);

                if (DirInfo.Exists)
                {
                    //Debug.Log(path);
                    DirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;    //去掉文件夹属性  
                    DirInfo.Delete(true);
                }
            }
        }
        catch (System.Exception e)
        {
            // 异常信息 
            throw new System.Exception("FileManager.DelectDirectory(string path) ------>\n" + e.Message);
        }
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFile(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("FileManager.DeleteFile(string path) ------>\n" + e.Message);
        }
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFile(string[] paths)
    {
        try
        {
            for (int i = 0; i < paths.Length; i++)
            {
                DeleteFile(paths[i]);
            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("FileManager.DeleteFile(string[] paths) ------>\n" + e.Message);
        }

    }
    public static void DeleteFiles(string directoryName, params string[] searchPatterns)
    {
        try
        {
            if (searchPatterns == null) return;
            if (Directory.Exists(directoryName) == true)
            {
                List<FileInfo> _fileInfos = GetFiles(directoryName, searchPatterns);
                for (int i = 0; i < _fileInfos.Count; i++)
                {
                    _fileInfos[i].Delete();
                }
            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("FileManager.DeleteFiles(string directoryName, params string[] searchPatterns) ------>\n" + e.Message);
        }
    }
    private static List<FileInfo> GetFiles(string directoryName, params string[] searchPatterns)
    {
        List<FileInfo> _infos = new List<FileInfo>();
        DirectoryInfo _dirs = new DirectoryInfo(directoryName);
        for (int i = 0; i < searchPatterns.Length; i++)
        {
            _infos.AddRange(_dirs.GetFiles(searchPatterns[i]).ToList());
        }
        DirectoryInfo[] _dirinfos = _dirs.GetDirectories();
        for (int i = 0; i < _dirinfos.Length; i++)
        {
            _infos.AddRange(GetFiles(_dirinfos[i].FullName, searchPatterns));
        }
        return _infos;
    }
    /// <summary>
    /// 文件大小
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static long GetLength(string path)
    {
        System.IO.FileInfo _fileInfo = new System.IO.FileInfo(path);
        return _fileInfo.Length;
    }
    /// <summary>
    /// string转byte
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static byte[] GetBytesByString(string info)
    {
        return System.Text.Encoding.UTF8.GetBytes(info);
    }
    /// <summary>
    /// byte转string
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string GetStringByBytes(byte[] bytes)
    {
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
    /// <summary>
    /// md5加密方法
    /// </summary>
    /// <param name="pwd">传入需要加密的字符串</param>
    /// <returns>返回加密后的md5值</returns>    
    public static string GetMD5String(byte[] bytes)
    {
        MD5 md5 = MD5.Create();
        StringBuilder sb = new StringBuilder();
        byte[] buffer = md5.ComputeHash(bytes);
        for (int i = 0; i < buffer.Length; i++)
        {
            sb.Append(buffer[i].ToString("x2"));
        }
        return sb.ToString();
    }
}
