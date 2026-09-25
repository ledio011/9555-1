using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class FileUpdateHelper : MonoBehaviour
{
	// Token: 0x1700017D RID: 381
	// (get) Token: 0x0600081F RID: 2079 RVA: 0x0003A0C0 File Offset: 0x000382C0
	public UPDATE_STEP CurUpdateStape
	{
		get
		{
			return this.mCurUpdateStape;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000820 RID: 2080 RVA: 0x0003A0C8 File Offset: 0x000382C8
	public UPDATE_RESULT CurUpdateResult
	{
		get
		{
			return this.mCurUpdateResult;
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000821 RID: 2081 RVA: 0x0003A0D0 File Offset: 0x000382D0
	public long CurDownloadSize
	{
		get
		{
			return (this.mDataFileDownloadHelper == null) ? 0L : this.mDataFileDownloadHelper.AlreadyDownloadSize;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000822 RID: 2082 RVA: 0x0003A0F0 File Offset: 0x000382F0
	public long NeedDownloadSize
	{
		get
		{
			return this.mNeedDownloadSize;
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0003A0F8 File Offset: 0x000382F8
	public static string GetResCachePath()
	{
		if (string.IsNullOrEmpty(FileUpdateHelper.ResCachePath))
		{
			FileUpdateHelper.ResCachePath = string.Format("{0}/CACHERES_UNITY4", Application.temporaryCachePath);
		}
		return FileUpdateHelper.ResCachePath;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0003A130 File Offset: 0x00038330
	public static string GetLocalVersionPath()
	{
		if (string.IsNullOrEmpty(FileUpdateHelper.LocalVersionPath))
		{
			FileUpdateHelper.LocalVersionPath = string.Format("{0}/UpdateInfo_UNITY4", Application.persistentDataPath);
		}
		return FileUpdateHelper.LocalVersionPath;
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0003A168 File Offset: 0x00038368
	public static string GetCacheVersionPath()
	{
		if (string.IsNullOrEmpty(FileUpdateHelper.CacheVersionPath))
		{
			FileUpdateHelper.CacheVersionPath = string.Format("{0}/UpdateInfo_UNITY4", FileUpdateHelper.GetResCachePath());
		}
		return FileUpdateHelper.CacheVersionPath;
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0003A1A0 File Offset: 0x000383A0
	public static string GetLocalPathRoot()
	{
		if (string.IsNullOrEmpty(FileUpdateHelper.LocalPathRoot))
		{
			FileUpdateHelper.LocalPathRoot = string.Format("{0}/ResData_UNITY4", Application.persistentDataPath);
		}
		return FileUpdateHelper.LocalPathRoot;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0003A1D8 File Offset: 0x000383D8
	public static string GetOldPathRoot()
	{
		return string.Format("{0}/ResData", Application.persistentDataPath);
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0003A1EC File Offset: 0x000383EC
	public static string GetOldVersionPathRoot()
	{
		return string.Format("{0}/UpdateInfo", Application.persistentDataPath);
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000829 RID: 2089 RVA: 0x0003A200 File Offset: 0x00038400
	// (set) Token: 0x0600082A RID: 2090 RVA: 0x0003A208 File Offset: 0x00038408
	public bool IsNeedCopyRes
	{
		get
		{
			return this.mIsNeedCopyRes;
		}
		set
		{
			this.mIsNeedCopyRes = value;
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0003A214 File Offset: 0x00038414
	private void Awake()
	{
		if (GameSettingData.IsDownLoadInLocal)
		{
			this.mServerUrl = "http://s16.serv00.com:15678";
		}
		FileUpdateHelper.ResCachePath = Application.temporaryCachePath + "/CACHERES_UNITY4";
		FileUpdateHelper.LocalVersionPath = Application.persistentDataPath + "/UpdateInfo_UNITY4";
		FileUpdateHelper.LocalPathRoot = Application.persistentDataPath + "/ResData_UNITY4";
		FileUpdateHelper.CacheVersionPath = FileUpdateHelper.ResCachePath + "/UpdateInfo_UNITY4";
		this.mCacheDataPath = FileUpdateHelper.ResCachePath + "/" + FileUpdateHelper.DownloadDataFolderName;
		this.mCurUpdateStape = UPDATE_STEP.INVALID;
		this.mCurUpdateResult = UPDATE_RESULT.INVALID;
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0003A2B0 File Offset: 0x000384B0
	public void StartCheckRes(string resServerUrl, FileUpdateHelper.OnChangeUpdateStepDelegate func, bool isNeedSameVersion, bool isNeedCopyRes)
	{
		this.IsNeedSameVersion = isNeedSameVersion;
		this.mServerVersion = -1;
		this.mIsNeedCopyRes = isNeedCopyRes;
		if (this.mVersionFileDownloadHelper != null)
		{
			this.mVersionFileDownloadHelper.DisposeWWW();
		}
		if (this.mDataFileDownloadHelper != null)
		{
			this.mDataFileDownloadHelper.DisposeWWW();
		}
		this.mServerUrl = "http://s16.serv00.com:15678";
		this.mServerResUrl = string.Format("{0}{1}_{2}", this.mServerUrl, this.mServerResRoot, PlayerData.ServerDataVersion);
		this.mServerDataPath = string.Format("{0}{1}_{2}", this.mServerUrl, this.mServerResRoot, PlayerData.ServerDataVersion);
		this.mServerVersion = -1;
		this.mVersionFileDownloadHelper = null;
		this.mDataFileDownloadHelper = null;
		this.mNeedDownloadSize = 0L;
		this.OnChangeUpdateStep = func;
		this.ChangeUpdateStep(UPDATE_STEP.CHECK_VERSION);
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			this.GetResVersion();
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0003A394 File Offset: 0x00038594
	private void GetResVersion()
	{
		this.mLocalVersion = -1;
		int num = -1;
		string text = FileUpdateHelper.LocalVersionPath + "/" + FileUpdateHelper.VersionFileName;
		string apkDataUrl = FileUpdateHelper.GetApkDataUrl(FileUpdateHelper.ApkVersionFolderName, FileUpdateHelper.VersionFileName);
		if (File.Exists(text) && !MyFileUtil.GetIntFromFile(text, out this.mLocalVersion))
		{
			Log.ERROR_MSG("parse version fail");
		}
		string empty = string.Empty;
		if (File.Exists(apkDataUrl) && !MyFileUtil.GetStringFromFile(apkDataUrl, ref empty))
		{
			Log.ERROR_MSG("parse version fail");
		}
		if (!int.TryParse(empty, out num))
		{
			num = 0;
		}
		if (num > this.mLocalVersion)
		{
			MyFileUtil.DeleteFolder(FileUpdateHelper.LocalVersionPath);
			MyFileUtil.DeleteFolder(FileUpdateHelper.LocalPathRoot);
			this.mLocalVersion = num;
		}
		this.mServerVersion = PlayerData.ServerDataVersion;
		this.OnGetResVersion(true);
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0003A468 File Offset: 0x00038668
	private void OnGetResVersion(bool isSuccess)
	{
		if (isSuccess)
		{
			if (this.IsNeedSameVersion)
			{
				if (this.mServerVersion != this.mLocalVersion && PlayerData.downLoadFlag == 1)
				{
					MyFileUtil.DeleteFolder(FileUpdateHelper.CacheVersionPath);
					this.ChangeUpdateStep(UPDATE_STEP.GET_FILELIST);
					this.mVersionFileDownloadHelper = DownloadHelper.StartDownload(string.Format("{0}{1}_{2:D3}/{3}", new object[]
					{
						this.mServerUrl,
						this.mServerResRoot,
						this.mServerVersion,
						FileUpdateHelper.ResFileListName
					}), true, FileUpdateHelper.CacheVersionPath + "/" + FileUpdateHelper.ResFileListName, this, new DownloadHelper.OnDownloadFinished(this.OnDownloadServerResFileList), GameSettingData.DownloadThreadCount[GameSettingData.GetPhoneClass()]);
				}
				else
				{
					this.CheckLocalFileList();
				}
			}
			else if (this.mServerVersion > this.mLocalVersion && PlayerData.downLoadFlag == 1)
			{
				MyFileUtil.DeleteFolder(FileUpdateHelper.CacheVersionPath);
				this.ChangeUpdateStep(UPDATE_STEP.GET_FILELIST);
				this.mVersionFileDownloadHelper = DownloadHelper.StartDownload(string.Format("{0}{1}_{2:D3}/{3}", new object[]
				{
					this.mServerUrl,
					this.mServerResRoot,
					this.mServerVersion,
					FileUpdateHelper.ResFileListName
				}), true, FileUpdateHelper.CacheVersionPath + "/" + FileUpdateHelper.ResFileListName, this, new DownloadHelper.OnDownloadFinished(this.OnDownloadServerResFileList), GameSettingData.DownloadThreadCount[GameSettingData.GetPhoneClass()]);
			}
			else
			{
				this.CheckLocalFileList();
			}
		}
		else
		{
			this.UpdateFinish(UPDATE_RESULT.GET_VERSION_FAIL);
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0003A5E8 File Offset: 0x000387E8
	private void CheckLocalFileList()
	{
		this.ChangeUpdateStep(UPDATE_STEP.COMPARE_RES);
		string text = string.Format("{0}/{1}", FileUpdateHelper.LocalVersionPath, FileUpdateHelper.ResFileListName);
		if (!File.Exists(text))
		{
			if (PlayerData.downLoadFlag == 0)
			{
				this.UpdateFinish(UPDATE_RESULT.SUCCESS);
			}
			else
			{
				this.OnDownloadLocalResFileListLocalCheck(false);
			}
		}
		else
		{
			this.OnDownloadLocalResFileListLocalCheck(true);
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0003A648 File Offset: 0x00038848
	private void OnDownloadLocalResFileListLocalCheck(bool isSuccess)
	{
		if (isSuccess)
		{
			string fileListPath = string.Format("{0}/{1}", FileUpdateHelper.LocalVersionPath, FileUpdateHelper.ResFileListName);
			this.mLocalFileDic.Clear();
			this.mUpdateFilesList.Clear();
			this.ReadFileListToDic(fileListPath, this.mLocalFileDic);
			List<string> list = new List<string>(this.mLocalFileDic.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				if (!MyFileUtil.IsFileExist(FileUpdateHelper.LocalPathRoot + "/" + list[i]) || MyFileUtil.GetFileMD5(FileUpdateHelper.LocalPathRoot + "/" + list[i]) != this.mLocalFileDic[list[i]].md5)
				{
					MyFileUtil.DeleteFolder(FileUpdateHelper.CacheVersionPath);
					this.ChangeUpdateStep(UPDATE_STEP.GET_FILELIST);
					this.mVersionFileDownloadHelper = DownloadHelper.StartDownload(string.Format("{0}{1}_{2:D3}/{3}", new object[]
					{
						this.mServerUrl,
						this.mServerResRoot,
						this.mServerVersion,
						FileUpdateHelper.ResFileListName
					}), true, FileUpdateHelper.CacheVersionPath + "/" + FileUpdateHelper.ResFileListName, this, new DownloadHelper.OnDownloadFinished(this.OnDownloadServerResFileList), GameSettingData.DownloadThreadCount[GameSettingData.GetPhoneClass()]);
					return;
				}
			}
			this.UpdateFinish(UPDATE_RESULT.SUCCESS);
			return;
		}
		MyFileUtil.DeleteFolder(FileUpdateHelper.CacheVersionPath);
		this.ChangeUpdateStep(UPDATE_STEP.GET_FILELIST);
		this.mVersionFileDownloadHelper = DownloadHelper.StartDownload(string.Format("{0}{1}_{2:D3}/{3}", new object[]
		{
			this.mServerUrl,
			this.mServerResRoot,
			this.mServerVersion,
			FileUpdateHelper.ResFileListName
		}), true, FileUpdateHelper.CacheVersionPath + "/" + FileUpdateHelper.ResFileListName, this, new DownloadHelper.OnDownloadFinished(this.OnDownloadServerResFileList), GameSettingData.DownloadThreadCount[GameSettingData.GetPhoneClass()]);
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0003A828 File Offset: 0x00038A28
	private void OnDownloadServerResFileList(bool isSuccess)
	{
		if (isSuccess)
		{
			this.ChangeUpdateStep(UPDATE_STEP.COMPARE_RES);
			string text = string.Format("{0}/{1}", FileUpdateHelper.LocalVersionPath, FileUpdateHelper.ResFileListName);
			if (!File.Exists(text))
			{
				this.mVersionFileDownloadHelper = DownloadHelper.StartDownload(FileUpdateHelper.GetApkDataUrl(FileUpdateHelper.ApkVersionFolderName, FileUpdateHelper.ResFileListName), false, text, this, new DownloadHelper.OnDownloadFinished(this.OnDownloadLocalResFileList), GameSettingData.DownloadThreadCount[GameSettingData.GetPhoneClass()]);
			}
			else
			{
				this.OnDownloadLocalResFileList(true);
			}
		}
		else
		{
			this.UpdateFinish(UPDATE_RESULT.GET_FILELIST_FAIL);
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0003A8B0 File Offset: 0x00038AB0
	private void OnDownloadLocalResFileList(bool isSuccess)
	{
		if (!isSuccess)
		{
			this.UpdateFinish(UPDATE_RESULT.GET_FILELIST_FAIL);
			return;
		}
		string fileListPath = string.Format("{0}/{1}", FileUpdateHelper.LocalVersionPath, FileUpdateHelper.ResFileListName);
		string fileListPath2 = string.Format("{0}/{1}", FileUpdateHelper.CacheVersionPath, FileUpdateHelper.ResFileListName);
		this.mLocalFileDic.Clear();
		this.mServerFileDic.Clear();
		this.mUpdateFilesList.Clear();
		this.ReadFileListToDic(fileListPath, this.mLocalFileDic);
		if (!this.ReadFileListToDic(fileListPath2, this.mServerFileDic))
		{
			this.UpdateFinish(UPDATE_RESULT.LOAD_SERVER_FILELIST_ERROR);
			return;
		}
		if (Directory.Exists(FileUpdateHelper.GetOldPathRoot()))
		{
			MyFileUtil.DeleteFolder(FileUpdateHelper.GetOldPathRoot());
		}
		if (Directory.Exists(FileUpdateHelper.GetOldVersionPathRoot()))
		{
			MyFileUtil.DeleteFolder(FileUpdateHelper.GetOldVersionPathRoot());
		}
		List<string> list = new List<string>(this.mLocalFileDic.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			if (!this.mServerFileDic.ContainsKey(list[i]))
			{
				MyFileUtil.DeleteFile(FileUpdateHelper.LocalPathRoot + "/" + list[i]);
				this.mLocalFileDic.Remove(list[i]);
			}
		}
		List<string> list2 = new List<string>(this.mServerFileDic.Keys);
		for (int j = 0; j < list2.Count; j++)
		{
			if (this.mLocalFileDic.ContainsKey(list2[j]))
			{
				if (!MyFileUtil.IsFileExist(FileUpdateHelper.LocalPathRoot + "/" + list2[j]) || MyFileUtil.GetFileMD5(FileUpdateHelper.LocalPathRoot + "/" + list2[j]) != this.mServerFileDic[list2[j]].md5)
				{
					this.mUpdateFilesList.Add(list2[j]);
				}
			}
			else
			{
				this.mUpdateFilesList.Add(list2[j]);
			}
		}
		if (this.mUpdateFilesList.Count > 0)
		{
			this.mDownloadUrlList.Clear();
			this.mNeedDownloadSize = 0L;
			for (int k = 0; k < this.mUpdateFilesList.Count; k++)
			{
				string text = string.Format("{0}/{1}", this.mCacheDataPath, this.mUpdateFilesList[k]);
				if (!File.Exists(text) || MyFileUtil.GetFileMD5(text) != this.mServerFileDic[this.mUpdateFilesList[k]].md5)
				{
					this.mDownloadUrlList.Add(new DownloadUrlInfo(this.mServerDataPath + "/" + this.mUpdateFilesList[k], text, this.mServerFileDic[this.mUpdateFilesList[k]].size));
					this.mNeedDownloadSize += this.mServerFileDic[this.mUpdateFilesList[k]].size;
				}
			}
			if (this.mDownloadUrlList.Count > 0)
			{
				this.ChangeUpdateStep(UPDATE_STEP.CHECK_IS_DOWNLOAD);
				return;
			}
		}
		this.OnDownloadRes(true);
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0003ABEC File Offset: 0x00038DEC
	public void DownloadFileList()
	{
		this.ChangeUpdateStep(UPDATE_STEP.DOWNLOAD_RES);
		if (this.mDataFileDownloadHelper == null)
		{
			this.mDataFileDownloadHelper = DownloadHelper.StartDownload(this.mDownloadUrlList, true, this, this.mNeedDownloadSize, new DownloadHelper.OnDownloadFinished(this.OnDownloadRes), GameSettingData.DownloadThreadCount[GameSettingData.GetPhoneClass()]);
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0003AC3C File Offset: 0x00038E3C
	public void ContinueDownload()
	{
		this.ChangeUpdateStep(UPDATE_STEP.DOWNLOAD_RES);
		if (this.mDataFileDownloadHelper != null)
		{
			this.mDataFileDownloadHelper.ContinueDownload();
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0003AC5C File Offset: 0x00038E5C
	private void OnDownloadRes(bool isSuccess)
	{
		if (!this.mIsNeedCopyRes)
		{
			this.UpdateFinish(UPDATE_RESULT.SUCCESS);
			return;
		}
		if (isSuccess)
		{
			this.ChangeUpdateStep(UPDATE_STEP.CHECK_RES);
			this.mUpdateErrorFilesList.Clear();
			for (int i = 0; i < this.mUpdateFilesList.Count; i++)
			{
				string text = string.Format("{0}/{1}", this.mCacheDataPath, this.mUpdateFilesList[i]);
				if (!File.Exists(text))
				{
					this.mUpdateErrorFilesList.Add(this.mUpdateFilesList[i]);
				}
				else
				{
					string fileMD = MyFileUtil.GetFileMD5(text);
					if (!this.mServerFileDic.ContainsKey(this.mUpdateFilesList[i]) || fileMD != this.mServerFileDic[this.mUpdateFilesList[i]].md5)
					{
						this.mUpdateErrorFilesList.Add(this.mUpdateFilesList[i]);
					}
					else if (this.mLocalFileDic.ContainsKey(this.mUpdateFilesList[i]))
					{
						this.mLocalFileDic[this.mUpdateFilesList[i]].CopyData(this.mServerFileDic[this.mUpdateFilesList[i]]);
					}
					else
					{
						this.mLocalFileDic.Add(this.mUpdateFilesList[i], this.mServerFileDic[this.mUpdateFilesList[i]]);
					}
				}
			}
			if (this.mUpdateErrorFilesList.Count == 0)
			{
				this.CopyResToDataPath();
			}
			else
			{
				this.UpdateFinish(UPDATE_RESULT.DOWNLOAD_INCOMPLETE);
			}
		}
		else
		{
			this.UpdateFinish(UPDATE_RESULT.DOWNLOAD_FAIL);
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0003AE0C File Offset: 0x0003900C
	private void CopyResToDataPath()
	{
		try
		{
			this.ChangeUpdateStep(UPDATE_STEP.COPY_RES);
			string text = string.Empty;
			for (int i = 0; i < this.mUpdateFilesList.Count; i++)
			{
				text = FileUpdateHelper.LocalPathRoot + "/" + this.mUpdateFilesList[i];
				MyFileUtil.CheckPath(text);
				File.Copy(this.mCacheDataPath + "/" + this.mUpdateFilesList[i], text, true);
			}
		}
		catch (Exception ex)
		{
			Log.ERROR_MSG("copy File fail : " + ex.ToString());
			this.UpdateFinish(UPDATE_RESULT.COPY_FILE_FAIL);
			return;
		}
		this.GenerateLocalFileList();
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0003AED8 File Offset: 0x000390D8
	private void GenerateLocalFileList()
	{
		try
		{
			string path = FileUpdateHelper.LocalVersionPath + "/" + FileUpdateHelper.ResFileListName;
			MyFileUtil.DeleteFile(path);
			if (!MyFileUtil.GenerateFileList(path, this.mLocalFileDic))
			{
				Log.ERROR_MSG("Generate local FileList fail");
				this.UpdateFinish(UPDATE_RESULT.GENERATE_VERSION_FILE_FAIL);
				return;
			}
		}
		catch (Exception ex)
		{
			Log.ERROR_MSG("GenerateLocalFileList : " + ex.ToString());
			this.UpdateFinish(UPDATE_RESULT.GENERATE_VERSION_FILE_FAIL);
			return;
		}
		this.GenerateVersionFile();
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0003AF78 File Offset: 0x00039178
	private void GenerateVersionFile()
	{
		try
		{
			string text = FileUpdateHelper.LocalVersionPath + "/" + FileUpdateHelper.VersionFileName;
			MyFileUtil.CheckPath(text);
			FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.WriteLine(string.Format("{0:D3}", this.mServerVersion));
			streamWriter.Close();
			fileStream.Close();
			PlayerData.LocalDataVersion = this.mServerVersion;
		}
		catch (Exception ex)
		{
			this.UpdateFinish(UPDATE_RESULT.GENERATE_VERSION_FILE_FAIL);
			return;
		}
		this.ClearCacheFiles();
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0003B01C File Offset: 0x0003921C
	private void ClearCacheFiles()
	{
		this.ChangeUpdateStep(UPDATE_STEP.CLEAR_CACHE);
		try
		{
			MyFileUtil.DeleteFolder(this.mCacheDataPath);
			MyFileUtil.DeleteFolder(FileUpdateHelper.CacheVersionPath);
		}
		catch (Exception ex)
		{
			this.UpdateFinish(UPDATE_RESULT.CLEAN_CACHE_FAIL);
			return;
		}
		this.UpdateFinish(UPDATE_RESULT.SUCCESS);
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0003B080 File Offset: 0x00039280
	private void UpdateFinish(UPDATE_RESULT result)
	{
		if (result != UPDATE_RESULT.SUCCESS)
		{
			Debug.Log("DownloadError :: " + result);
		}
		this.mCurUpdateResult = result;
		this.ChangeUpdateStep(UPDATE_STEP.FINISH);
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0003B0AC File Offset: 0x000392AC
	private void ChangeUpdateStep(UPDATE_STEP newStep)
	{
		this.mCurUpdateStape = newStep;
		if (this.OnChangeUpdateStep != null)
		{
			this.OnChangeUpdateStep(newStep);
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0003B0CC File Offset: 0x000392CC
	private bool ReadFileListToDic(string fileListPath, Dictionary<string, UpdateFileInfo> curDic)
	{
		bool result;
		try
		{
			if (!File.Exists(fileListPath))
			{
				result = false;
			}
			else
			{
				FileStream fileStream = new FileStream(fileListPath, FileMode.Open, FileAccess.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				string text = string.Empty;
				while (!streamReader.EndOfStream)
				{
					text = streamReader.ReadLine();
					string[] array = text.Split(new char[]
					{
						','
					});
					UpdateFileInfo updateFileInfo = new UpdateFileInfo(array[1], long.Parse(array[2]));
					curDic.Add(array[0], updateFileInfo);
				}
				streamReader.Close();
				fileStream.Close();
				result = true;
			}
		}
		catch (Exception ex)
		{
			Log.ERROR_MSG("Read FileList Error : " + fileListPath + "  E: " + ex.ToString());
			result = false;
		}
		return result;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0003B1AC File Offset: 0x000393AC
	public static string GetApkDataUrl(string folderPath, string fileName)
	{
		return Application.streamingAssetsPath + folderPath + "/" + fileName;
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0003B1C0 File Offset: 0x000393C0
	public float GetDownloadProgress()
	{
		return this.mDataFileDownloadHelper.GetDownloadProgress();
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0003B1D0 File Offset: 0x000393D0
	private void OnDestroy()
	{
		if (this.mDataFileDownloadHelper != null)
		{
			this.mDataFileDownloadHelper.DisposeWWW();
		}
		if (this.mVersionFileDownloadHelper != null)
		{
			this.mVersionFileDownloadHelper.DisposeWWW();
		}
	}

	// Token: 0x0400072A RID: 1834
	private UPDATE_STEP mCurUpdateStape = UPDATE_STEP.INVALID;

	// Token: 0x0400072B RID: 1835
	private UPDATE_RESULT mCurUpdateResult = UPDATE_RESULT.INVALID;

	// Token: 0x0400072C RID: 1836
	public static string ResCachePath = string.Empty;

	// Token: 0x0400072D RID: 1837
	public static string ApkVersionFolderName = "/UpdateInfo";

	// Token: 0x0400072E RID: 1838
	public static string LocalVersionPath = string.Empty;

	// Token: 0x0400072F RID: 1839
	public static string CacheVersionPath = string.Empty;

	// Token: 0x04000730 RID: 1840
	public static string DownloadDataFolderName = "StreamingAssets";

	// Token: 0x04000731 RID: 1841
	public static string VersionFileName = "Version.info";

	// Token: 0x04000732 RID: 1842
	public static string ResFileListName = "UpdateFileList_1.info";

	// Token: 0x04000733 RID: 1843
	public static string LocalPathRoot = string.Empty;

	// Token: 0x04000734 RID: 1844
	public string mServerUrl = "http://s16.serv00.com:15678";

	// Token: 0x04000735 RID: 1845
	private string mServerResUrl = string.Empty;

	// Token: 0x04000736 RID: 1846
	private string mCacheDataPath = string.Empty;

	// Token: 0x04000737 RID: 1847
	private string mServerDataPath = string.Empty;

	// Token: 0x04000738 RID: 1848
	private string mServerResRoot = "/RES";

	// Token: 0x04000739 RID: 1849
	private int mLocalVersion = -1;

	// Token: 0x0400073A RID: 1850
	private int mServerVersion = -1;

	// Token: 0x0400073B RID: 1851
	private Dictionary<string, UpdateFileInfo> mLocalFileDic = new Dictionary<string, UpdateFileInfo>();

	// Token: 0x0400073C RID: 1852
	private Dictionary<string, UpdateFileInfo> mServerFileDic = new Dictionary<string, UpdateFileInfo>();

	// Token: 0x0400073D RID: 1853
	private List<string> mUpdateFilesList = new List<string>();

	// Token: 0x0400073E RID: 1854
	private List<string> mUpdateErrorFilesList = new List<string>();

	// Token: 0x0400073F RID: 1855
	private List<DownloadUrlInfo> mDownloadUrlList = new List<DownloadUrlInfo>();

	// Token: 0x04000740 RID: 1856
	private DownloadHelper mDataFileDownloadHelper;

	// Token: 0x04000741 RID: 1857
	private DownloadHelper mVersionFileDownloadHelper;

	// Token: 0x04000742 RID: 1858
	private long mNeedDownloadSize;

	// Token: 0x04000743 RID: 1859
	public FileUpdateHelper.OnChangeUpdateStepDelegate OnChangeUpdateStep;

	// Token: 0x04000744 RID: 1860
	private bool IsNeedSameVersion;

	// Token: 0x04000745 RID: 1861
	private bool mIsNeedCopyRes = true;

	// Token: 0x02000AB8 RID: 2744
	// (Invoke) Token: 0x06004F69 RID: 20329
	private delegate void OnGetResVersionDelegate(bool isSuccess);

	// Token: 0x02000AB9 RID: 2745
	// (Invoke) Token: 0x06004F6D RID: 20333
	private delegate void OnDownloadFileListDelegate(bool isSuccess);

	// Token: 0x02000ABA RID: 2746
	// (Invoke) Token: 0x06004F71 RID: 20337
	public delegate void OnChangeUpdateStepDelegate(UPDATE_STEP nStep);
}
