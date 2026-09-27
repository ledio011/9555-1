using System;

// Token: 0x0200081F RID: 2079
public class ModelEffectData
{
	// Token: 0x0600336F RID: 13167 RVA: 0x000CA100 File Offset: 0x000C8300
	public ModelEffectData(ModelData mData, FxEffInfoData fxData, bool isFirst, string layerStr = "", int index = -1)
	{
		this.modelData = mData;
		this.fxEffinfoData = fxData;
		this.IsFirst = isFirst;
		this.LayerStr = layerStr;
		this.Index = index;
	}

	// Token: 0x040021D8 RID: 8664
	public ModelData modelData;

	// Token: 0x040021D9 RID: 8665
	public FxEffInfoData fxEffinfoData;

	// Token: 0x040021DA RID: 8666
	public bool IsFirst;

	// Token: 0x040021DB RID: 8667
	public string LayerStr;

	// Token: 0x040021DC RID: 8668
	public int Index;
}
