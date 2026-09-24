using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class DownloadingFileInfo
{
	// Token: 0x06000819 RID: 2073 RVA: 0x00039F00 File Offset: 0x00038100
	public DownloadingFileInfo()
	{
		this.IsDownloading = false;
		this.CurDownloadUrlInfo = null;
		this.curWWW = null;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00039F20 File Offset: 0x00038120
	public DownloadingFileInfo(DownloadUrlInfo info)
	{
		this.IsDownloading = false;
		this.CurDownloadUrlInfo = info;
		this.curWWW = null;
		this.ExpectDownloadTime = Mathf.Max((float)(info.size / (long)DownloadingFileInfo.MIN_DOWNLOAD_SPEED + 1L), DownloadingFileInfo.MIN_DOWNLOAD_TIME);
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00039F84 File Offset: 0x00038184
	public void SetUrlInfo(DownloadUrlInfo info)
	{
		this.CurDownloadUrlInfo = info;
		this.ExpectDownloadTime = Mathf.Max((float)(info.size / (long)DownloadingFileInfo.MIN_DOWNLOAD_SPEED + 1L), DownloadingFileInfo.MIN_DOWNLOAD_TIME);
	}

	// Token: 0x04000723 RID: 1827
	public bool IsDownloading;

	// Token: 0x04000724 RID: 1828
	public DownloadUrlInfo CurDownloadUrlInfo;

	// Token: 0x04000725 RID: 1829
	public WWW curWWW;

	// Token: 0x04000726 RID: 1830
	public float StartDownLoadTime;

	// Token: 0x04000727 RID: 1831
	public float ExpectDownloadTime;

	// Token: 0x04000728 RID: 1832
	public static int MIN_DOWNLOAD_SPEED = 5120;

	// Token: 0x04000729 RID: 1833
	public static float MIN_DOWNLOAD_TIME = 20f;
}
