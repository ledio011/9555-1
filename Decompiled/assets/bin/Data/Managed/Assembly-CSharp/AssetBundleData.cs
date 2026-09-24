using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class AssetBundleData
{
	// Token: 0x06000805 RID: 2053 RVA: 0x00039914 File Offset: 0x00037B14
	public AssetBundleData(AssetBundle bundle, int count, string selfURL, string dependURL, bool isDepend)
	{
		this.Bundle = bundle;
		this.UsingCount = count;
		this.SelfURL = selfURL;
		this.DependURL = dependURL;
		this.IsDependObj = isDepend;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00039944 File Offset: 0x00037B44
	public void SetBundle(AssetBundle bundle)
	{
		this.Bundle = bundle;
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00039950 File Offset: 0x00037B50
	public bool IsBundleValid()
	{
		return this.Bundle != null;
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00039960 File Offset: 0x00037B60
	public AssetBundle GetBundle()
	{
		return this.Bundle;
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00039968 File Offset: 0x00037B68
	public void AddBundleUsingCount()
	{
		this.UsingCount++;
		if (!string.IsNullOrEmpty(this.DependURL))
		{
			BundleManager.ModelBundleCacheDic[this.DependURL].AddBundleUsingCount();
		}
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x000399A0 File Offset: 0x00037BA0
	public bool UnLoadBundle()
	{
		this.UsingCount--;
		if (!string.IsNullOrEmpty(this.DependURL) && BundleManager.ModelBundleCacheDic[this.DependURL].UnLoadBundle())
		{
			BundleManager.ModelBundleCacheDic.Remove(this.DependURL);
		}
		if (this.UsingCount > 0)
		{
			return false;
		}
		if (this.IsMainPlayerPart())
		{
			return false;
		}
		if (this.Bundle == null)
		{
			return true;
		}
		if (BundleManager.UnloadBundle(this.Bundle, true))
		{
			this.Bundle = null;
			return true;
		}
		return false;
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00039A40 File Offset: 0x00037C40
	public bool IsMainPlayerPart()
	{
		return Singleton<ObjManager>.Instance.IsMainPlayerPart(this.SelfURL);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00039A54 File Offset: 0x00037C54
	public bool Clear(bool isClearAll = false)
	{
		this.UsingCount = 0;
		if (!(this.Bundle != null))
		{
			return true;
		}
		if (!isClearAll && this.IsMainPlayerPart())
		{
			return false;
		}
		if (BundleManager.UnloadBundle(this.Bundle, true))
		{
			this.Bundle = null;
			this.UsingCount = 0;
			return true;
		}
		return false;
	}

	// Token: 0x04000712 RID: 1810
	public AssetBundle Bundle;

	// Token: 0x04000713 RID: 1811
	public int UsingCount;

	// Token: 0x04000714 RID: 1812
	public string SelfURL;

	// Token: 0x04000715 RID: 1813
	public string DependURL;

	// Token: 0x04000716 RID: 1814
	public bool IsDependObj;
}
