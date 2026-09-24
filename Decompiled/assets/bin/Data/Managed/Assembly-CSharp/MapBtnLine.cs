using System;
using UnityEngine;

// Token: 0x02000951 RID: 2385
public class MapBtnLine : MonoBehaviour
{
	// Token: 0x060042BB RID: 17083 RVA: 0x00146CBC File Offset: 0x00144EBC
	public void ResetRightBtn(MapPoint mapPoint)
	{
		this.RightBtnFlag = true;
		this.mMapPoint = mapPoint;
		this.NameLabel.text = this.mMapPoint.PointName;
		switch (this.mMapPoint.PointType)
		{
		case MAP_POINT_TYPE.NPC:
			this.PicSprite.spriteName = "CZ_diTU_anQuanQu";
			break;
		case MAP_POINT_TYPE.TELEPORT:
			this.PicSprite.spriteName = "CZ_diTU_chuanSongDian";
			break;
		case MAP_POINT_TYPE.MONSTER:
			this.PicSprite.spriteName = "CZ_diTU_guai";
			break;
		}
	}

	// Token: 0x060042BC RID: 17084 RVA: 0x00146D50 File Offset: 0x00144F50
	public void ResetLeftBtn(int index)
	{
		this.NameLabel.text = "Line   " + (index + 1);
		this.mBtnIndex = index;
	}

	// Token: 0x060042BD RID: 17085 RVA: 0x00146D84 File Offset: 0x00144F84
	public void OnClickBtn()
	{
		if (this.RightBtnFlag)
		{
			SingletonUnity<MapUIRootLogic>.Instance.OnClickRightNPCBtn(this.mMapPoint);
		}
		else
		{
			SingletonUnity<MapUIRootLogic>.Instance.OnClickLeftMapBtn(this.mBtnIndex);
		}
	}

	// Token: 0x04002F0C RID: 12044
	public bool RightBtnFlag = true;

	// Token: 0x04002F0D RID: 12045
	public UILabel NameLabel;

	// Token: 0x04002F0E RID: 12046
	public UISprite PicSprite;

	// Token: 0x04002F0F RID: 12047
	private MapPoint mMapPoint;

	// Token: 0x04002F10 RID: 12048
	private int mBtnIndex;
}
