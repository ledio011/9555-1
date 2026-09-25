using System;
using UnityEngine;

// Token: 0x020008CD RID: 2253
public class BossXueTiaoLogic : SingletonUnity<BossXueTiaoLogic>
{
	// Token: 0x06003CB2 RID: 15538 RVA: 0x0010B1C0 File Offset: 0x001093C0
	private void OnEnable()
	{
		this.topDeth = this.topSprite.depth;
		this.bottomDeth = this.bottomSprite.depth;
	}

	// Token: 0x06003CB3 RID: 15539 RVA: 0x0010B1F0 File Offset: 0x001093F0
	public void SetOneLineVal(int val)
	{
		this.OneLineHPVal = val;
	}

	// Token: 0x06003CB4 RID: 15540 RVA: 0x0010B1FC File Offset: 0x001093FC
	public void resetHPinfo(CharacterAttributeData AttributeData)
	{
		this.levellabel.text = string.Format("Lv.{0}", AttributeData.Level);
		this.namelabel.text = AttributeData.Name;
		this.ChangeHP(AttributeData.HP);
	}

	// Token: 0x06003CB5 RID: 15541 RVA: 0x0010B248 File Offset: 0x00109448
	public void ChangeHP(long curHP)
	{
		if (curHP > 0L)
		{
			if (!UnityVersionUtil.IsActive(this.topSlider.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.topSlider.gameObject, true);
			}
			if (!UnityVersionUtil.IsActive(this.middleSlider.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.middleSlider.gameObject, true);
			}
			if (!UnityVersionUtil.IsActive(this.bottomSprite.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.bottomSprite.gameObject, true);
			}
			this.mTopSliderProgress = ((float)curHP - 0.01f) % (float)this.OneLineHPVal / (float)this.OneLineHPVal;
			this.mMiddleSliderProgress = this.mTopSliderProgress;
			this.curColorIndex = (curHP - 1L) / (long)this.OneLineHPVal;
			this.HPVal.text = curHP.ToString();
			if (this.preProgress < this.mTopSliderProgress)
			{
				if (this.curColorIndex % 2L == 1L)
				{
					this.topSlider = this.oddSlider;
					this.evenSlider.value = 1f;
					this.middleSlider.value = 1f;
					this.topSprite = this.oddSprite;
					this.bottomSprite = this.evenSprite;
					this.oddSprite.depth = this.topDeth;
					this.evenSprite.depth = this.bottomDeth;
				}
				else
				{
					this.topSlider = this.evenSlider;
					this.oddSlider.value = 1f;
					this.middleSlider.value = 1f;
					this.topSprite = this.evenSprite;
					this.bottomSprite = this.oddSprite;
					this.oddSprite.depth = this.bottomDeth;
					this.evenSprite.depth = this.topDeth;
				}
			}
			this.preProgress = this.mTopSliderProgress;
			if (this.curColorIndex == 0L)
			{
				if (UnityVersionUtil.IsActive(this.bottomSprite.gameObject))
				{
					UnityVersionUtil.SetActiveRecursive(this.bottomSprite.gameObject, false);
				}
			}
			else if (!UnityVersionUtil.IsActive(this.bottomSprite.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.bottomSprite.gameObject, true);
			}
			this.curColorIndex %= 5L;
			checked
			{
				this.topSprite.color = this.hpColor[(int)((IntPtr)this.curColorIndex)];
				this.bottomSprite.color = this.hpColor[(int)((IntPtr)(unchecked(this.curColorIndex - 1L + 5L) % 5L))];
			}
		}
		else
		{
			if (UnityVersionUtil.IsActive(this.topSlider.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.topSlider.gameObject, false);
			}
			if (UnityVersionUtil.IsActive(this.middleSlider.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.middleSlider.gameObject, false);
			}
			if (UnityVersionUtil.IsActive(this.bottomSprite.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.bottomSprite.gameObject, false);
			}
		}
	}

	// Token: 0x06003CB6 RID: 15542 RVA: 0x0010B54C File Offset: 0x0010974C
	private void UpdateSliderValue()
	{
		if (this.topSlider.value > this.mTopSliderProgress)
		{
			this.topSlider.value -= Time.deltaTime * 2f;
			if (this.topSlider.value < this.mTopSliderProgress)
			{
				this.topSlider.value = this.mTopSliderProgress;
			}
		}
		else if (this.topSlider.value < this.mTopSliderProgress)
		{
			this.topSlider.value += Time.deltaTime * 2f;
			if (this.topSlider.value > this.mTopSliderProgress)
			{
				this.topSlider.value = this.mTopSliderProgress;
			}
		}
		if (this.middleSlider.value > this.mMiddleSliderProgress)
		{
			this.middleSlider.value -= Time.deltaTime / 2f;
			if (this.middleSlider.value < this.mMiddleSliderProgress)
			{
				this.middleSlider.value = this.mMiddleSliderProgress;
			}
		}
		else if (this.middleSlider.value < this.mMiddleSliderProgress)
		{
			this.middleSlider.value += Time.deltaTime / 2f;
			if (this.middleSlider.value > this.mMiddleSliderProgress)
			{
				this.middleSlider.value = this.mMiddleSliderProgress;
			}
		}
	}

	// Token: 0x06003CB7 RID: 15543 RVA: 0x0010B6CC File Offset: 0x001098CC
	private void Update()
	{
		this.UpdateSliderValue();
	}

	// Token: 0x040027EA RID: 10218
	public UILabel levellabel;

	// Token: 0x040027EB RID: 10219
	public UILabel namelabel;

	// Token: 0x040027EC RID: 10220
	public UISlider oddSlider;

	// Token: 0x040027ED RID: 10221
	public UISlider evenSlider;

	// Token: 0x040027EE RID: 10222
	public UISprite oddSprite;

	// Token: 0x040027EF RID: 10223
	public UISprite evenSprite;

	// Token: 0x040027F0 RID: 10224
	private float preProgress;

	// Token: 0x040027F1 RID: 10225
	public UISlider topSlider;

	// Token: 0x040027F2 RID: 10226
	public UISlider middleSlider;

	// Token: 0x040027F3 RID: 10227
	public UISprite topSprite;

	// Token: 0x040027F4 RID: 10228
	public UISprite bottomSprite;

	// Token: 0x040027F5 RID: 10229
	public float mTopSliderProgress;

	// Token: 0x040027F6 RID: 10230
	public float mMiddleSliderProgress;

	// Token: 0x040027F7 RID: 10231
	public int OneLineHPVal = 100;

	// Token: 0x040027F8 RID: 10232
	public int topDeth = 53;

	// Token: 0x040027F9 RID: 10233
	public int bottomDeth = 51;

	// Token: 0x040027FA RID: 10234
	public UILabel HPVal;

	// Token: 0x040027FB RID: 10235
	public Color[] hpColor = new Color[]
	{
		Color.red,
		new Color(1f, 0.4f, 0f, 1f),
		new Color(1f, 0.733f, 0f, 1f),
		Color.blue,
		new Color(0.07f, 0.718f, 0.004f, 1f)
	};

	// Token: 0x040027FC RID: 10236
	private long curColorIndex;
}
