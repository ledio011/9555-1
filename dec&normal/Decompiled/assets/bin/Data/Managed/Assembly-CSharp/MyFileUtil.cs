using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

// Token: 0x02000A6B RID: 2667
public class MyFileUtil
{
	// Token: 0x06004DB9 RID: 19897 RVA: 0x001A9188 File Offset: 0x001A7388
	public static bool IsFileExist(string targetPath)
	{
		targetPath = targetPath.Replace('\\', '/');
		return File.Exists(targetPath);
	}

	// Token: 0x06004DBA RID: 19898 RVA: 0x001A91A4 File Offset: 0x001A73A4
	public static void CheckPath(string targetPath)
	{
		targetPath = targetPath.Replace('\\', '/');
		int num = targetPath.LastIndexOf(".");
		int num2 = targetPath.LastIndexOf("/");
		if (num > 0 && num2 < num)
		{
			targetPath = targetPath.Substring(0, num2);
		}
		if (Directory.Exists(targetPath))
		{
			return;
		}
		string[] array = targetPath.Split(new char[]
		{
			'/'
		});
		string text = string.Empty;
		int num3 = array.Length;
		for (int i = 0; i < array.Length; i++)
		{
			text = text + array[i] + '/';
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
		}
	}

	// Token: 0x06004DBB RID: 19899 RVA: 0x001A9254 File Offset: 0x001A7454
	public static void CheckPathFile(string targetPath)
	{
		targetPath = targetPath.Replace('\\', '/');
		int num = targetPath.LastIndexOf(".");
		int num2 = targetPath.LastIndexOf("/");
		if (num > 0 && num2 < num)
		{
			targetPath = targetPath.Substring(0, num2);
		}
		if (Directory.Exists(targetPath))
		{
			return;
		}
		string[] array = targetPath.Split(new char[]
		{
			'/'
		});
		string text = string.Empty;
		int num3 = array.Length;
		for (int i = 0; i < array.Length - 1; i++)
		{
			text = text + array[i] + '/';
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
		}
	}

	// Token: 0x06004DBC RID: 19900 RVA: 0x001A9304 File Offset: 0x001A7504
	public static bool GetStringFromFile(string path, ref string result)
	{
		bool result2;
		try
		{
			if (!File.Exists(path))
			{
				result = string.Empty;
				result2 = false;
			}
			else
			{
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				result = streamReader.ReadToEnd();
				streamReader.Close();
				fileStream.Close();
				result2 = true;
			}
		}
		catch (Exception ex)
		{
			Log.ERROR_MSG(ex.ToString());
			result = string.Empty;
			result2 = false;
		}
		return result2;
	}

	// Token: 0x06004DBD RID: 19901 RVA: 0x001A9398 File Offset: 0x001A7598
	public static bool GetIntFromFile(string path, out int result)
	{
		bool result2;
		try
		{
			if (!File.Exists(path))
			{
				result = 0;
				result2 = false;
			}
			else
			{
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				string text = streamReader.ReadToEnd();
				streamReader.Close();
				fileStream.Close();
				if (!int.TryParse(text, out result))
				{
					result2 = false;
				}
				else
				{
					result2 = true;
				}
			}
		}
		catch (Exception ex)
		{
			Log.ERROR_MSG(ex.ToString());
			result = 0;
			result2 = false;
		}
		return result2;
	}

	// Token: 0x06004DBE RID: 19902 RVA: 0x001A943C File Offset: 0x001A763C
	public static void DeleteFolder(string path)
	{
		if (!Directory.Exists(path))
		{
			return;
		}
		string[] files = Directory.GetFiles(path);
		for (int i = 0; i < files.Length; i++)
		{
			File.Delete(files[i]);
		}
		string[] directories = Directory.GetDirectories(path);
		for (int j = 0; j < directories.Length; j++)
		{
			MyFileUtil.DeleteFolder(directories[j]);
		}
		Directory.Delete(path);
	}

	// Token: 0x06004DBF RID: 19903 RVA: 0x001A94A4 File Offset: 0x001A76A4
	public static void DeleteFile(string path)
	{
		if (!File.Exists(path))
		{
			return;
		}
		File.Delete(path);
	}

	// Token: 0x06004DC0 RID: 19904 RVA: 0x001A94B8 File Offset: 0x001A76B8
	public static void CopyFolder(string resPath, string targetPath)
	{
		if (!Directory.Exists(resPath))
		{
			Log.DEBUG_MSG("Copy file resPath doesn't exist " + resPath);
		}
		MyFileUtil.CheckPath(targetPath);
		string[] files = Directory.GetFiles(resPath);
		for (int i = 0; i < files.Length; i++)
		{
			Debug.Log(string.Concat(new object[]
			{
				"res file : ",
				files[i],
				" ",
				files[i].LastIndexOf("/")
			}));
			Debug.Log("target file : " + targetPath + "/" + files[i].Substring(files[i].LastIndexOf('/') + 1));
			File.Copy(files[i].Replace("\\", "/"), targetPath + "/" + files[i].Substring(files[i].LastIndexOf("/") + 1), true);
		}
		string[] directories = Directory.GetDirectories(resPath);
		for (int j = 0; j < directories.Length; j++)
		{
			MyFileUtil.CopyFolder(directories[j].Replace("\\", "/"), targetPath + "/" + directories[j].Substring(directories[j].LastIndexOf("/") + 1));
		}
	}

	// Token: 0x06004DC1 RID: 19905 RVA: 0x001A95F4 File Offset: 0x001A77F4
	public static string GetFileMD5(string filePath)
	{
		string result = string.Empty;
		string text = string.Empty;
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		try
		{
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			byte[] array = md5CryptoServiceProvider.ComputeHash(fileStream);
			fileStream.Close();
			text = BitConverter.ToString(array);
			text = text.Replace("-", string.Empty);
			result = text;
		}
		catch (Exception ex)
		{
			Debug.Log("read md5 file error : " + filePath + "  e: " + ex.ToString());
		}
		return result;
	}

	// Token: 0x06004DC2 RID: 19906 RVA: 0x001A9690 File Offset: 0x001A7890
	public static bool GenerateFileList(string path, Dictionary<string, UpdateFileInfo> curDic)
	{
		bool result;
		try
		{
			FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			List<string> list = new List<string>(curDic.Keys);
			string text = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				text = string.Concat(new object[]
				{
					list[i],
					",",
					curDic[list[i]].md5,
					",",
					curDic[list[i]].size
				});
				streamWriter.WriteLine(text);
			}
			streamWriter.Close();
			fileStream.Close();
			result = true;
		}
		catch (Exception ex)
		{
			Log.ERROR_MSG("Generate FileList Fail : " + ex.ToString());
			result = false;
		}
		return result;
	}
}
