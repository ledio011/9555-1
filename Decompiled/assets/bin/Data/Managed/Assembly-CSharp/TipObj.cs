using System;
using UnityEngine;

// Token: 0x02000A51 RID: 2641
public class TipObj : MonoBehaviour
{
	// Token: 0x06004CFA RID: 19706 RVA: 0x001A2A8C File Offset: 0x001A0C8C
	public void Reset(GameObject targetObj, Vector3 offset, TUTORIAL_STEP step = TUTORIAL_STEP.INVALID, float duration = -1f, bool isChild = true, Transform parentObj = null, bool isWorldObj = false)
	{
		this.TargetObj = targetObj;
		this.OffsetPos = offset;
		this.Duration = duration;
		this.IsChild = isChild;
		if (this.IsChild)
		{
			this.TempRoot = new GameObject();
			this.TempRoot.transform.parent = this.TargetObj.transform;
			this.TempRoot.transform.localPosition = Vector3.zero;
			this.TempRoot.transform.localScale = Vector3.one;
			base.transform.parent = this.TempRoot.transform;
			base.transform.localScale = Vector3.one;
			base.transform.localPosition = offset;
		}
		else
		{
			this.OffsetPos = new Vector3(offset.x * targetObj.transform.lossyScale.x, offset.y * targetObj.transform.lossyScale.y, offset.z * targetObj.transform.lossyScale.z);
			if (parentObj != null)
			{
				base.transform.parent = parentObj;
			}
		}
		NGUITools.SetActive(base.gameObject, true);
		if (this.Duration > 0f)
		{
			this.TargetTime = Time.time + this.Duration;
		}
		this.TutorialStep = step;
		this.IsWorldObj = isWorldObj;
	}

	// Token: 0x06004CFB RID: 19707 RVA: 0x001A2BFC File Offset: 0x001A0DFC
	public void Reset(GameObject targetObj, Vector3 offset, string tips, SCREEN_DIRECTION tipPos, Vector3 tipOffset, TUTORIAL_STEP step = TUTORIAL_STEP.INVALID, float duration = -1f, bool isChild = true, Transform parentObj = null, bool isWorldObj = false, bool isShowHand = true)
	{
		this.TargetObj = targetObj;
		this.OffsetPos = offset;
		this.Duration = duration;
		this.IsChild = isChild;
		if (this.IsChild)
		{
			this.TempRoot = new GameObject();
			this.TempRoot.transform.parent = this.TargetObj.transform;
			this.TempRoot.transform.localPosition = Vector3.zero;
			this.TempRoot.transform.localScale = Vector3.one;
			base.transform.parent = this.TempRoot.transform;
			base.transform.localScale = Vector3.one;
			base.transform.localPosition = offset;
		}
		else
		{
			this.OffsetPos = new Vector3(offset.x * targetObj.transform.lossyScale.x, offset.y * targetObj.transform.lossyScale.y, offset.z * targetObj.transform.lossyScale.z);
			if (parentObj != null)
			{
				base.transform.parent = parentObj;
			}
		}
		NGUITools.SetActive(base.gameObject, UnityVersionUtil.IsActive(targetObj));
		this.TutorialStep = step;
		this.TipText = tips;
		if (string.IsNullOrEmpty(tips))
		{
			this.TipTextLabel.enabled = false;
			this.TipTextBottomSprite.alpha = 0f;
		}
		else
		{
			this.TipPos = tipPos;
			if (UnityVersionUtil.IsActive(targetObj))
			{
				this.TipTextLabel.enabled = true;
				this.TipTextBottomSprite.alpha = 1f;
			}
			this.TipTextLabel.text = tips;
			if (this.TipTextLabel.printedSize.x < (float)this.tipLabelLength)
			{
				this.TipTextLabel.width = Mathf.CeilToInt(this.TipTextLabel.printedSize.x);
			}
			else
			{
				this.TipTextLabel.width = this.tipLabelLength;
			}
			this.TipTextBottomSprite.width = this.TipTextLabel.width + 84;
			this.TipTextBottomSprite.height = this.TipTextLabel.height + 20;
			int width = this.TipTextBottomSprite.width;
			int height = this.TipTextBottomSprite.height;
			int num = 0;
			int num2 = 0;
			int num3 = 20;
			switch (tipPos)
			{
			case SCREEN_DIRECTION.TOP:
				num2 = num3 + height / 2 + 50;
				break;
			case SCREEN_DIRECTION.BOTTOM:
				num2 = -(num3 + height / 2 + 50);
				break;
			case SCREEN_DIRECTION.LEFT:
				num = -(num3 + width / 2 + 50);
				break;
			case SCREEN_DIRECTION.RIGHT:
				num = num3 + width / 2 + 50;
				break;
			case SCREEN_DIRECTION.TOP_LEFT:
				num2 = num3 + height / 2 + height / 2;
				num = -(num3 + width / 2 + width / 2);
				break;
			case SCREEN_DIRECTION.BOTTOM_LEFT:
				num2 = -(num3 + height / 2 + height / 2);
				num = -(num3 + width / 2 + width / 2);
				break;
			case SCREEN_DIRECTION.TOP_RIGHT:
				num2 = num3 + height / 2 + height / 2;
				num = num3 + width / 2 + width / 2;
				break;
			case SCREEN_DIRECTION.BOTTOM_RIGHT:
				num2 = -(num3 + height / 2 + height / 2);
				num = num3 + width / 2 + width / 2;
				break;
			}
			this.TipTextBottomSprite.transform.localPosition = new Vector3((float)num + tipOffset.x, (float)num2 + tipOffset.y, 0f);
			if (tipPos == SCREEN_DIRECTION.LEFT)
			{
				this.SetPicLeft();
			}
			else if (tipPos == SCREEN_DIRECTION.RIGHT)
			{
				this.SetPicRight();
			}
			else if (this.TipTextBottomSprite.transform.position.x > SingletonUnity<FunctionTipsRootLogic>.Instance.CenterObj.position.x)
			{
				this.SetPicLeft();
			}
			else
			{
				this.SetPicRight();
			}
		}
		if (this.Duration > 0f)
		{
			this.TargetTime = Time.time + this.Duration;
		}
		this.IsWorldObj = isWorldObj;
		if (isShowHand)
		{
			NGUITools.SetActive(this.HandRoot, true);
			NGUITools.SetActive(this.CircleRoot, true);
		}
		else
		{
			NGUITools.SetActive(this.HandRoot, false);
			NGUITools.SetActive(this.CircleRoot, false);
		}
	}

	// Token: 0x06004CFC RID: 19708 RVA: 0x001A3044 File Offset: 0x001A1244
	private void SetPicRight()
	{
		this.TipPic.transform.localPosition = new Vector3((float)(this.TipTextBottomSprite.width / 2), (float)(-(float)this.TipTextBottomSprite.height / 2), 0f);
		this.TipPic.transform.localScale = new Vector3(1f, 1f, 1f);
		this.TipTextLabel.transform.localPosition = new Vector3((float)(-(float)this.TipTextBottomSprite.width / 2 + 10), 0f, 0f);
	}

	// Token: 0x06004CFD RID: 19709 RVA: 0x001A30E0 File Offset: 0x001A12E0
	private void SetPicLeft()
	{
		this.TipPic.transform.localPosition = new Vector3((float)(-(float)this.TipTextBottomSprite.width / 2), (float)(-(float)this.TipTextBottomSprite.height / 2), 0f);
		this.TipPic.transform.localScale = new Vector3(-1f, 1f, 1f);
		this.TipTextLabel.transform.localPosition = new Vector3((float)(this.TipTextBottomSprite.width / 2 - 10 - this.TipTextLabel.width), 0f, 0f);
	}

	// Token: 0x06004CFE RID: 19710 RVA: 0x001A3188 File Offset: 0x001A1388
	private void OnEnable()
	{
		if (this.TipTextLabel != null)
		{
			if (string.IsNullOrEmpty(this.TipText))
			{
				this.TipTextLabel.enabled = false;
				this.TipTextBottomSprite.alpha = 0f;
			}
			else
			{
				this.TipTextLabel.enabled = true;
				this.TipTextBottomSprite.alpha = 0f;
			}
		}
	}

	// Token: 0x06004CFF RID: 19711 RVA: 0x001A31F4 File Offset: 0x001A13F4
	private void OnDestroy()
	{
		FunctionTipsRootLogic.RemoveFunctionTips(this.TargetObj);
	}

	// Token: 0x04003A92 RID: 14994
	public GameObject TargetObj;

	// Token: 0x04003A93 RID: 14995
	public Vector3 OffsetPos;

	// Token: 0x04003A94 RID: 14996
	public float Duration = -1f;

	// Token: 0x04003A95 RID: 14997
	public float TargetTime;

	// Token: 0x04003A96 RID: 14998
	public UILabel TipTextLabel;

	// Token: 0x04003A97 RID: 14999
	public UISprite TipTextBottomSprite;

	// Token: 0x04003A98 RID: 15000
	public SCREEN_DIRECTION TipPos;

	// Token: 0x04003A99 RID: 15001
	public string TipText;

	// Token: 0x04003A9A RID: 15002
	public TUTORIAL_STEP TutorialStep;

	// Token: 0x04003A9B RID: 15003
	public bool IsChild = true;

	// Token: 0x04003A9C RID: 15004
	public UITexture TipPic;

	// Token: 0x04003A9D RID: 15005
	public bool IsWorldObj;

	// Token: 0x04003A9E RID: 15006
	public GameObject HandRoot;

	// Token: 0x04003A9F RID: 15007
	public GameObject CircleRoot;

	// Token: 0x04003AA0 RID: 15008
	private int tipLabelLength = 286;

	// Token: 0x04003AA1 RID: 15009
	public GameObject TempRoot;
}
