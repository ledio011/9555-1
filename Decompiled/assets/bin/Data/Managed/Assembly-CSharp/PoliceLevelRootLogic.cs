using System;

// Token: 0x02000973 RID: 2419
public class PoliceLevelRootLogic : SingletonUnity<PoliceLevelRootLogic>
{
	// Token: 0x06004453 RID: 17491 RVA: 0x00154414 File Offset: 0x00152614
	public void SetPoliceLevel(int level)
	{
		if (this.mCurLevel != level)
		{
			this.mCurLevel = level;
			for (int i = 0; i < this.MaskPic.Length; i++)
			{
				if (level < 0)
				{
					this.MaskPic[i].enabled = true;
					this.TweenStar[i].enabled = false;
					this.TweenStar[i].ResetToBeginning();
				}
				else if (i <= level)
				{
					this.MaskPic[i].enabled = false;
					this.TweenStar[i].enabled = true;
					this.TweenStar[i].Play();
				}
				else
				{
					this.MaskPic[i].enabled = true;
					this.TweenStar[i].enabled = false;
					this.TweenStar[i].ResetToBeginning();
				}
			}
			if (this.mCurLevel >= 0)
			{
				SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(this.mPoliceCarSoundId, 1f, null);
			}
			else
			{
				SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(this.mPoliceCarSoundId);
			}
		}
	}

	// Token: 0x06004454 RID: 17492 RVA: 0x00154518 File Offset: 0x00152718
	private void OnDisable()
	{
		this.mCurLevel = -9;
		if (SingletonDontDestoryUnity<SoundManager>.Exists)
		{
			SingletonDontDestoryUnity<SoundManager>.Instance.StopSoundEffect(this.mPoliceCarSoundId);
		}
	}

	// Token: 0x040030E6 RID: 12518
	public UISprite[] MaskPic;

	// Token: 0x040030E7 RID: 12519
	public TweenColor[] TweenStar;

	// Token: 0x040030E8 RID: 12520
	private int mCurLevel = -9;

	// Token: 0x040030E9 RID: 12521
	private int mPoliceCarSoundId = 35;
}
