using System;

// Token: 0x020000F9 RID: 249
public class DownloadUrlInfo
{
	// Token: 0x06000818 RID: 2072 RVA: 0x00039EE0 File Offset: 0x000380E0
	public DownloadUrlInfo(string dUrl, string sPath, long sSize)
	{
		this.Url = dUrl;
		this.SavePath = sPath;
		this.size = sSize;
	}

	// Token: 0x04000720 RID: 1824
	public string Url;

	// Token: 0x04000721 RID: 1825
	public string SavePath;

	// Token: 0x04000722 RID: 1826
	public long size;
}
