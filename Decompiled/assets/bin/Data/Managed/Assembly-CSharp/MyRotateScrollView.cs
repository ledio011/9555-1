using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008B5 RID: 2229
public class MyRotateScrollView : MonoBehaviour
{
	// Token: 0x06003C1B RID: 15387 RVA: 0x00106BA8 File Offset: 0x00104DA8
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x06003C1C RID: 15388 RVA: 0x00106BB0 File Offset: 0x00104DB0
	public void Reset()
	{
		this.mTopPos = this.PicList[0].transform.position;
		this.mBottomPos = this.PicList[this.PicList.Count - 1].transform.position;
		this.mTopDepth = this.PicList[0].depth;
		this.mDeltaPos = (this.mTopPos - this.mBottomPos) / (float)(this.PicList.Count - 1);
		for (int i = 0; i < this.PicList.Count; i++)
		{
			this.mPosList.Add(this.mTopPos - this.mDeltaPos * (float)i);
			this.PicList[i].transform.position = this.mPosList[i];
		}
		this.mCurTopIndex = 0;
		this.mMovePercent = 1f;
		this.mPicCount = this.PicList.Count;
		this.mPrePos = new Vector3[this.PicList.Count];
		for (int j = 0; j < this.PicList.Count; j++)
		{
			UIEventListener uieventListener = this.PicListener[j];
			uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragPic));
			UIEventListener uieventListener2 = this.PicListener[j];
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPress));
		}
	}

	// Token: 0x06003C1D RID: 15389 RVA: 0x00106D54 File Offset: 0x00104F54
	public void MoveLeft()
	{
		if (!this.EnableChangeFlag)
		{
			return;
		}
		this.EnableChangeFlag = false;
		this.mCurTopIndex = (this.mCurTopIndex + this.mPicCount - 1) % this.mPicCount;
		this.mMovePercent = 0f;
		this.mLeftFlag = true;
		this.SetPicColor();
		for (int i = 0; i < this.PicList.Count; i++)
		{
			this.mPrePos[i] = this.PicList[i].transform.position;
		}
		this.onMoveOver(this.mCurTopIndex);
	}

	// Token: 0x06003C1E RID: 15390 RVA: 0x00106DFC File Offset: 0x00104FFC
	public void MoveRight()
	{
		if (!this.EnableChangeFlag)
		{
			return;
		}
		this.EnableChangeFlag = false;
		this.mCurTopIndex = (this.mCurTopIndex + this.mPicCount + 1) % this.mPicCount;
		this.mMovePercent = 0f;
		this.SetPicTop();
		this.SetPicColor();
		for (int i = 0; i < this.PicList.Count; i++)
		{
			this.mPrePos[i] = this.PicList[i].transform.position;
		}
		this.onMoveOver(this.mCurTopIndex);
	}

	// Token: 0x06003C1F RID: 15391 RVA: 0x00106EA4 File Offset: 0x001050A4
	private void SetPicTop()
	{
		for (int i = 0; i < this.PicList.Count; i++)
		{
			this.PicList[(i + this.mCurTopIndex) % this.mPicCount].depth = this.mTopDepth - i;
		}
	}

	// Token: 0x06003C20 RID: 15392 RVA: 0x00106EF4 File Offset: 0x001050F4
	private void SetPicColor()
	{
		for (int i = 0; i < this.PicList.Count; i++)
		{
			if (i == 0)
			{
				this.PicList[(i + this.mCurTopIndex) % this.mPicCount].color = Color.white;
			}
			else
			{
				this.PicList[(i + this.mCurTopIndex) % this.mPicCount].color = Color.gray;
			}
		}
	}

	// Token: 0x06003C21 RID: 15393 RVA: 0x00106F70 File Offset: 0x00105170
	public void Update()
	{
		if (this.mMovePercent < 1f)
		{
			this.mMovePercent += this.mMoveSpeed * Time.deltaTime;
			for (int i = 0; i < this.PicList.Count; i++)
			{
				this.PicList[(i + this.mCurTopIndex) % this.mPicCount].transform.position = Vector3.Lerp(this.mPrePos[(i + this.mCurTopIndex) % this.mPicCount], this.mPosList[i], this.mMovePercent);
			}
		}
		else if (this.mLeftFlag)
		{
			this.mLeftFlag = false;
			this.SetPicTop();
		}
		if (Input.GetKeyUp(276))
		{
			this.EnableChangeFlag = true;
		}
	}

	// Token: 0x06003C22 RID: 15394 RVA: 0x00107050 File Offset: 0x00105250
	public void OnDragPic(GameObject obj, Vector2 deltaPos)
	{
		if (deltaPos.x > 0f)
		{
			this.dragLeft = false;
		}
		else
		{
			this.dragLeft = true;
		}
	}

	// Token: 0x06003C23 RID: 15395 RVA: 0x00107084 File Offset: 0x00105284
	public void OnPress(GameObject obj, bool isPress)
	{
		if (!isPress)
		{
			if (this.dragLeft)
			{
				this.MoveLeft();
			}
			else
			{
				this.MoveRight();
			}
		}
		else
		{
			this.dragLeft = false;
		}
	}

	// Token: 0x04002744 RID: 10052
	public List<UIWidget> PicList;

	// Token: 0x04002745 RID: 10053
	public List<UIEventListener> PicListener;

	// Token: 0x04002746 RID: 10054
	private int mCurTopIndex;

	// Token: 0x04002747 RID: 10055
	private Vector3 mTopPos;

	// Token: 0x04002748 RID: 10056
	private Vector3 mBottomPos;

	// Token: 0x04002749 RID: 10057
	private Vector3 mDeltaPos;

	// Token: 0x0400274A RID: 10058
	private List<Vector3> mPosList = new List<Vector3>();

	// Token: 0x0400274B RID: 10059
	private int mTopDepth;

	// Token: 0x0400274C RID: 10060
	private float mMovePercent;

	// Token: 0x0400274D RID: 10061
	private float mMoveSpeed = 4f;

	// Token: 0x0400274E RID: 10062
	private int mPicCount;

	// Token: 0x0400274F RID: 10063
	private Vector3[] mPrePos;

	// Token: 0x04002750 RID: 10064
	public DelegateDefine.OneIntParamDelegate onMoveOver;

	// Token: 0x04002751 RID: 10065
	public bool EnableChangeFlag;

	// Token: 0x04002752 RID: 10066
	private bool mLeftFlag;

	// Token: 0x04002753 RID: 10067
	private bool dragLeft;
}
