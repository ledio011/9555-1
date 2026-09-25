using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class DownloadHelper
{
	// Token: 0x0600080D RID: 2061 RVA: 0x00039AB8 File Offset: 0x00037CB8
	private DownloadHelper(string url, bool isRemote, string savePath, DownloadHelper.OnDownloadFinished onFinished, MonoBehaviour mono, int maxThreadCount)
	{
		this.NeedDownloadSize = 0f;
		this.mDownloadUrlList = new List<DownloadUrlInfo>();
		this.mDownloadUrlList.Add(new DownloadUrlInfo(url, savePath, 0L));
		this.mFinishUrlList = new List<DownloadUrlInfo>();
		this.mFinishUrlList.Add(new DownloadUrlInfo(url, savePath, 0L));
		this.mErrorUrlList = new List<DownloadUrlInfo>();
		this.mErrorUrlList.Clear();
		this.mCurDownloadDelegate = onFinished;
		this.curMono = mono;
		this.MaxThreadCount = maxThreadCount;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00039B58 File Offset: 0x00037D58
	private DownloadHelper(List<DownloadUrlInfo> urlList, bool isRemote, DownloadHelper.OnDownloadFinished onFinished, long downloadSize, MonoBehaviour mono, int maxThreadCount)
	{
		this.mDownloadUrlList = new List<DownloadUrlInfo>(urlList);
		this.mFinishUrlList = new List<DownloadUrlInfo>(urlList);
		this.mErrorUrlList = new List<DownloadUrlInfo>();
		this.mErrorUrlList.Clear();
		this.mCurDownloadDelegate = onFinished;
		this.NeedDownloadSize = (float)downloadSize;
		this.curMono = mono;
		this.MaxThreadCount = maxThreadCount;
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x0600080F RID: 2063 RVA: 0x00039BCC File Offset: 0x00037DCC
	public long AlreadyDownloadSize
	{
		get
		{
			return this.mAlreadyDownloadSize;
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00039BD4 File Offset: 0x00037DD4
	public static DownloadHelper StartDownload(string url, bool isRemote, string savePath, MonoBehaviour mono, DownloadHelper.OnDownloadFinished onFinished = null, int maxThreadCount = 5)
	{
		DownloadHelper downloadHelper = new DownloadHelper(url, isRemote, savePath, onFinished, mono, maxThreadCount);
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(downloadHelper.DownloadFileList());
		}
		return downloadHelper;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00039C10 File Offset: 0x00037E10
	public static DownloadHelper StartDownload(List<DownloadUrlInfo> urlList, bool isRemote, MonoBehaviour mono, long downloadSize, DownloadHelper.OnDownloadFinished onFinished = null, int maxThreadCount = 5)
	{
		DownloadHelper downloadHelper = new DownloadHelper(urlList, isRemote, onFinished, downloadSize, mono, maxThreadCount);
		if (UnityVersionUtil.IsactiveInHierarchy(mono.gameObject))
		{
			mono.StartCoroutine(downloadHelper.DownloadFileList());
		}
		return downloadHelper;
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00039C4C File Offset: 0x00037E4C
	private IEnumerator DownloadFileList()
	{
		if (this.mDownloadUrlList == null || this.mDownloadUrlList.Count == 0)
		{
			Log.ERROR_MSG("urlInfoList or urlInfoList.Count == null");
			if (this.mCurDownloadDelegate != null)
			{
				this.mCurDownloadDelegate(false);
			}
			yield break;
		}
		for (int i = this.MaxThreadCount - this.mCurDownloadingInfo.Count; i > 0; i--)
		{
			DownloadingFileInfo tempInfo = new DownloadingFileInfo();
			this.mCurDownloadingInfo.Add(tempInfo);
		}
		for (int j = 0; j < this.mCurDownloadingInfo.Count; j++)
		{
			if (!this.mCurDownloadingInfo[j].IsDownloading)
			{
				this.mCurDownloadingInfo[j].SetUrlInfo(this.mDownloadUrlList[0]);
				this.mDownloadUrlList.RemoveAt(0);
				if (UnityVersionUtil.IsactiveInHierarchy(this.curMono.gameObject))
				{
					this.curMono.StartCoroutine(this.DownloadFile(this.mCurDownloadingInfo[j]));
				}
				if (this.mDownloadUrlList.Count <= 0)
				{
					yield break;
				}
			}
		}
		yield break;
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00039C68 File Offset: 0x00037E68
	private IEnumerator DownloadFile(DownloadingFileInfo info)
	{
		info.IsDownloading = true;
		info.StartDownLoadTime = Time.realtimeSinceStartup;
		info.curWWW = new WWW(info.CurDownloadUrlInfo.Url);
		bool timeOutFLag = false;
		while (!info.curWWW.isDone)
		{
			if (Time.realtimeSinceStartup - info.StartDownLoadTime > info.ExpectDownloadTime)
			{
				timeOutFLag = true;
				break;
			}
			yield return null;
		}
		if (timeOutFLag)
		{
			this.OnDownLoadFile(info, false);
			yield break;
		}
		if (!string.IsNullOrEmpty(info.curWWW.error))
		{
			Log.ERROR_MSG("Download File Error : " + info.CurDownloadUrlInfo.Url + "  Error : " + info.curWWW.error);
			this.OnDownLoadFile(info, false);
			yield break;
		}
		try
		{
			MyFileUtil.CheckPath(info.CurDownloadUrlInfo.SavePath);
			MyFileUtil.DeleteFile(info.CurDownloadUrlInfo.SavePath);
			FileStream fs = new FileStream(info.CurDownloadUrlInfo.SavePath, 4);
			fs.Write(info.curWWW.bytes, 0, info.curWWW.size);
			fs.Close();
			this.mAlreadyDownloadSize += (long)info.curWWW.size;
			this.OnDownLoadFile(info, true);
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			Log.ERROR_MSG("download file error : " + ex.ToString());
			this.OnDownLoadFile(info, false);
		}
		yield break;
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00039C94 File Offset: 0x00037E94
	public void OnDownLoadFile(DownloadingFileInfo info, bool isSuccess)
	{
		info.IsDownloading = false;
		info.curWWW = null;
		if (!isSuccess)
		{
			this.mErrorUrlList.Add(info.CurDownloadUrlInfo);
		}
		for (int i = 0; i < this.mFinishUrlList.Count; i++)
		{
			if (this.mFinishUrlList[i].Url.Equals(info.CurDownloadUrlInfo.Url))
			{
				this.mFinishUrlList.RemoveAt(i);
				break;
			}
		}
		if (this.mFinishUrlList.Count <= 0)
		{
			if (this.mCurDownloadDelegate != null)
			{
				if (this.mErrorUrlList.Count <= 0)
				{
					this.mCurDownloadDelegate(true);
				}
				else
				{
					this.mCurDownloadDelegate(false);
				}
			}
		}
		else if (this.mDownloadUrlList.Count > 0)
		{
			info.SetUrlInfo(this.mDownloadUrlList[0]);
			this.mDownloadUrlList.RemoveAt(0);
			if (UnityVersionUtil.IsactiveInHierarchy(this.curMono.gameObject))
			{
				this.curMono.StartCoroutine(this.DownloadFile(info));
			}
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00039DC4 File Offset: 0x00037FC4
	public void ContinueDownload()
	{
		if (UnityVersionUtil.IsactiveInHierarchy(this.curMono.gameObject))
		{
			this.curMono.StartCoroutine(this.DownloadFileList());
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00039DF0 File Offset: 0x00037FF0
	public float GetDownloadProgress()
	{
		float num = 0f;
		for (int i = 0; i < this.mCurDownloadingInfo.Count; i++)
		{
			if (this.mCurDownloadingInfo[i].IsDownloading && this.mCurDownloadingInfo[i].curWWW != null)
			{
				num += this.mCurDownloadingInfo[i].curWWW.progress * (float)this.mCurDownloadingInfo[i].CurDownloadUrlInfo.size;
			}
		}
		return ((float)this.mAlreadyDownloadSize + num) / this.NeedDownloadSize;
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00039E8C File Offset: 0x0003808C
	public void DisposeWWW()
	{
		for (int i = 0; i < this.mCurDownloadingInfo.Count; i++)
		{
			if (this.mCurDownloadingInfo[i].curWWW != null)
			{
				this.mCurDownloadingInfo[i].curWWW = null;
			}
		}
	}

	// Token: 0x04000717 RID: 1815
	private DownloadHelper.OnDownloadFinished mCurDownloadDelegate;

	// Token: 0x04000718 RID: 1816
	private long mAlreadyDownloadSize;

	// Token: 0x04000719 RID: 1817
	public float NeedDownloadSize;

	// Token: 0x0400071A RID: 1818
	private List<DownloadUrlInfo> mDownloadUrlList;

	// Token: 0x0400071B RID: 1819
	private List<DownloadUrlInfo> mFinishUrlList;

	// Token: 0x0400071C RID: 1820
	private List<DownloadUrlInfo> mErrorUrlList;

	// Token: 0x0400071D RID: 1821
	private MonoBehaviour curMono;

	// Token: 0x0400071E RID: 1822
	private int MaxThreadCount = 10;

	// Token: 0x0400071F RID: 1823
	private List<DownloadingFileInfo> mCurDownloadingInfo = new List<DownloadingFileInfo>();

	// Token: 0x02000AB7 RID: 2743
	// (Invoke) Token: 0x06004F65 RID: 20325
	public delegate void OnDownloadFinished(bool isSuccess);
}
