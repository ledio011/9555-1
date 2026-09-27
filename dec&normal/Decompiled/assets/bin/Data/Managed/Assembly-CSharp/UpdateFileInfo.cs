using System;

// Token: 0x020000FE RID: 254
public class UpdateFileInfo
{
	// Token: 0x06000840 RID: 2112 RVA: 0x0003B20C File Offset: 0x0003940C
	public UpdateFileInfo(string fileMd5, long fileSize)
	{
		this.md5 = fileMd5;
		this.size = fileSize;
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0003B224 File Offset: 0x00039424
	public void CopyData(UpdateFileInfo newFileInfo)
	{
		this.md5 = newFileInfo.md5;
		this.size = newFileInfo.size;
	}

	// Token: 0x0400075E RID: 1886
	public string md5;

	// Token: 0x0400075F RID: 1887
	public long size;
}
