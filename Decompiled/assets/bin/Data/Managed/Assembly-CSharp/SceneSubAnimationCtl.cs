using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008BB RID: 2235
public class SceneSubAnimationCtl : MonoBehaviour
{
	// Token: 0x06003C40 RID: 15424 RVA: 0x0010750C File Offset: 0x0010570C
	private void OnEnable()
	{
		this.mStartTime = Time.time;
		this.mHasPlayedList = new bool[this.ParticleData.Count];
		for (int i = 0; i < this.mHasPlayedList.Length; i++)
		{
			this.mHasPlayedList[i] = false;
		}
		this.mSoundPlayedList = new bool[this.SoundDataList.Count];
		for (int j = 0; j < this.mSoundPlayedList.Length; j++)
		{
			this.mSoundPlayedList[j] = false;
		}
		this.soundManager = SingletonDontDestoryUnity<SoundManager>.Instance;
	}

	// Token: 0x06003C41 RID: 15425 RVA: 0x001075A0 File Offset: 0x001057A0
	private void OnDisable()
	{
		if (SingletonDontDestoryUnity<SoundManager>.Exists)
		{
			for (int i = 0; i < this.SoundDataList.Count; i++)
			{
				this.soundManager.StopSoundEffect(this.SoundDataList[i].SoundId);
			}
		}
	}

	// Token: 0x06003C42 RID: 15426 RVA: 0x001075F0 File Offset: 0x001057F0
	private void Update()
	{
		if (!this.CameraAnimationObj.AnimationObj.IsPlaying(this.CameraAnimationObj.AnimationNameList[this.mAnimaIndex]))
		{
			this.mAnimaIndex++;
			if (this.mAnimaIndex < this.mAnimaCount)
			{
				this.PlayAnimation();
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
				if (this.onAnimaFinished != null)
				{
					this.onAnimaFinished();
				}
			}
		}
		if (this.ParticleData.Count > 0)
		{
			for (int i = 0; i < this.ParticleData.Count; i++)
			{
				if (!this.mHasPlayedList[i] && Time.time - this.mStartTime >= this.ParticleData[i].ParticleTime)
				{
					this.ParticleData[i].ParticleObj.Play();
					this.mHasPlayedList[i] = true;
				}
			}
		}
		if (this.SoundDataList.Count > 0)
		{
			for (int j = 0; j < this.SoundDataList.Count; j++)
			{
				if (!this.mSoundPlayedList[j] && Time.time - this.mStartTime >= this.SoundDataList[j].SoundTime)
				{
					this.soundManager.PlaySoundEffect(this.SoundDataList[j].SoundId, 1f, null);
					this.mSoundPlayedList[j] = true;
				}
			}
		}
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x00107778 File Offset: 0x00105978
	public void Init(DelegateDefine.NoParamDelegate func)
	{
		this.mAnimaCount = this.CameraAnimationObj.AnimationNameList.Count;
		this.onAnimaFinished = func;
		for (int i = 0; i < this.AnimationObjList.Count; i++)
		{
			if (!string.IsNullOrEmpty(this.AnimationObjList[i].SubModelId))
			{
				this.AnimationObjList[i].SubModelData = DataManager.GetCharacterModelDataByID(this.AnimationObjList[i].SubModelId);
				if (this.AnimationObjList[i].SubModelData.TypeID == 0)
				{
					ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
					if (mainPlayer != null)
					{
						this.AnimationObjList[i].SubFakeObj = new FakeObjLogic();
						this.AnimationObjList[i].SubModelData = mainPlayer.CurrentCharacterModelData;
						this.AnimationObjList[i].SubFakeObj.InitFakeObject(mainPlayer.PartObjId[0], mainPlayer.PartObjId[1], mainPlayer.PartObjId[2], mainPlayer.PartObjId[3], this.AnimationObjList[i].SubObjRoot, null, "Default");
					}
					else
					{
						this.AnimationObjList[i].SubFakeObj = new FakeObjLogic();
						this.AnimationObjList[i].SubFakeObj.InitFakeObject("XD_A_WQ", "XD_A_T", "XD_A_S", "XD_A_X", this.AnimationObjList[i].SubObjRoot, null, "Default");
					}
				}
				else
				{
					this.AnimationObjList[i].SubFakeObj = new FakeObjLogic();
					this.AnimationObjList[i].SubFakeObj.InitAnimaFakeNpcObj(this.AnimationObjList[i].SubModelId, this.AnimationObjList[i].SubObjRoot, this.AnimationObjList[i].SubAniamtionNameList[0]);
				}
			}
		}
	}

	// Token: 0x06003C44 RID: 15428 RVA: 0x00107974 File Offset: 0x00105B74
	public void StartScene()
	{
		this.mAnimaIndex = 0;
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		this.PlayAnimation();
	}

	// Token: 0x06003C45 RID: 15429 RVA: 0x00107990 File Offset: 0x00105B90
	private void PlayAnimation()
	{
		if (this.CameraAnimationObj.AnimationSpeedList.Count > this.mAnimaIndex)
		{
			this.CameraAnimationObj.AnimationObj[this.CameraAnimationObj.AnimationNameList[this.mAnimaIndex]].speed = this.CameraAnimationObj.AnimationSpeedList[this.mAnimaIndex];
		}
		this.CameraAnimationObj.AnimationObj.Play(this.CameraAnimationObj.AnimationNameList[this.mAnimaIndex]);
		for (int i = 0; i < this.AnimationObjList.Count; i++)
		{
			if (!this.AnimationObjList[i].AnimationObj.IsPlaying(this.AnimationObjList[i].AnimationNameList[this.mAnimaIndex]))
			{
				if (this.AnimationObjList[i].AnimationSpeedList.Count > this.mAnimaIndex)
				{
					this.AnimationObjList[i].AnimationObj[this.AnimationObjList[i].AnimationNameList[this.mAnimaIndex]].speed = this.AnimationObjList[i].AnimationSpeedList[this.mAnimaIndex];
				}
				this.AnimationObjList[i].AnimationObj.Play(this.AnimationObjList[i].AnimationNameList[this.mAnimaIndex]);
			}
			if (this.AnimationObjList[i].SubAnimationObj != null && !this.AnimationObjList[i].SubAnimationObj.IsPlaying(this.AnimationObjList[i].SubAniamtionNameList[this.mAnimaIndex]))
			{
				if (this.AnimationObjList[i].AnimationSpeedList.Count > this.mAnimaIndex)
				{
					this.AnimationObjList[i].SubAnimationObj[this.AnimationObjList[i].SubAniamtionNameList[this.mAnimaIndex]].speed = this.AnimationObjList[i].AnimationSpeedList[this.mAnimaIndex];
				}
				this.AnimationObjList[i].SubAnimationObj.Play(this.AnimationObjList[i].SubAniamtionNameList[this.mAnimaIndex]);
			}
			if (this.AnimationObjList[i].SubObjRoot != null && this.AnimationObjList[i].SubFakeObj.FakeObj != null)
			{
				this.AnimationObjList[i].SubFakeObj.PlayAnim(this.AnimationObjList[i].SubAniamtionNameList[this.mAnimaIndex], this.AnimationObjList[i].SubModelData);
			}
		}
	}

	// Token: 0x06003C46 RID: 15430 RVA: 0x00107C94 File Offset: 0x00105E94
	private void OnDestroy()
	{
		for (int i = 0; i < this.AnimationObjList.Count; i++)
		{
			if (this.AnimationObjList[i].SubFakeObj != null)
			{
				this.AnimationObjList[i].SubFakeObj.StopLoadMesh();
			}
		}
	}

	// Token: 0x0400276E RID: 10094
	private int mAnimaIndex;

	// Token: 0x0400276F RID: 10095
	private int mAnimaCount;

	// Token: 0x04002770 RID: 10096
	private DelegateDefine.NoParamDelegate onAnimaFinished;

	// Token: 0x04002771 RID: 10097
	public SceneAnimationObjData CameraAnimationObj;

	// Token: 0x04002772 RID: 10098
	public List<SceneAnimationObjData> AnimationObjList;

	// Token: 0x04002773 RID: 10099
	public List<ParticleData> ParticleData;

	// Token: 0x04002774 RID: 10100
	public List<AnimaSoundData> SoundDataList;

	// Token: 0x04002775 RID: 10101
	private float mStartTime;

	// Token: 0x04002776 RID: 10102
	private bool[] mHasPlayedList;

	// Token: 0x04002777 RID: 10103
	private bool[] mSoundPlayedList;

	// Token: 0x04002778 RID: 10104
	private SoundManager soundManager;
}
