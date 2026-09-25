using System;
using UnityEngine;

// Token: 0x02000922 RID: 2338
public class CarMissionStartTimeCountRoot : SingletonUnity<CarMissionStartTimeCountRoot>
{
	// Token: 0x060040FD RID: 16637 RVA: 0x00134150 File Offset: 0x00132350
	public void Reset(float startTime, DelegateDefine.NoParamDelegate onFinish = null)
	{
		this.mStartTime = startTime;
		this.mCurPicIndex = 0;
		for (int i = 0; i < this.NumLabelPic.Length; i++)
		{
			NGUITools.SetActive(this.NumLabelPic[i].gameObject, false);
		}
		NGUITools.SetActive(this.NumLabelPic[0].gameObject, true);
		this.onCountFinish = onFinish;
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.mCountSoundId, 1f, null);
	}

	// Token: 0x060040FE RID: 16638 RVA: 0x001341C8 File Offset: 0x001323C8
	private void Update()
	{
		if (this.mCurPicIndex < this.NumLabelPic.Length)
		{
			this.tempNum = (int)(Time.time - this.mStartTime);
			if (this.tempNum > this.mCurPicIndex)
			{
				this.mCurPicIndex = this.tempNum;
				if (this.mCurPicIndex < this.NumLabelPic.Length)
				{
					NGUITools.SetActive(this.NumLabelPic[this.mCurPicIndex].gameObject, true);
					if (this.mCurPicIndex == this.NumLabelPic.Length - 1)
					{
						SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.StartGame();
						if (this.onCountFinish != null)
						{
							this.onCountFinish();
						}
					}
					SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.mCountSoundId, 1f, null);
				}
				else
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.CarMissionStartTimeCountRoot);
				}
			}
		}
	}

	// Token: 0x04002CA7 RID: 11431
	public UISprite[] NumLabelPic;

	// Token: 0x04002CA8 RID: 11432
	private float mStartTime;

	// Token: 0x04002CA9 RID: 11433
	private int mCurPicIndex;

	// Token: 0x04002CAA RID: 11434
	private DelegateDefine.NoParamDelegate onCountFinish;

	// Token: 0x04002CAB RID: 11435
	private int tempNum;

	// Token: 0x04002CAC RID: 11436
	private int mCountSoundId = 42;
}
