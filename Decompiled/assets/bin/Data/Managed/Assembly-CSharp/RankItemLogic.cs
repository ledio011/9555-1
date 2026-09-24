using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200097B RID: 2427
public class RankItemLogic : MonoBehaviour
{
	// Token: 0x0600449A RID: 17562 RVA: 0x001566BC File Offset: 0x001548BC
	public void Init(RankItemLogic.onClickBtn Btnfun, int itemindex)
	{
		this.OnClick = Btnfun;
		this.curItemIndex = itemindex;
	}

	// Token: 0x0600449B RID: 17563 RVA: 0x001566CC File Offset: 0x001548CC
	public void Reset(int rankNum, sort_item curinfo, RANK_TYPE type)
	{
		if (this.RankPic != null)
		{
			if (rankNum < 3)
			{
				this.RankPic.enabled = true;
				this.RankPic.spriteName = GameDefine.RANK_PICNAME[rankNum];
				this.RankPic.MakePixelPerfect();
				this.RankLabel.enabled = false;
			}
			else
			{
				this.RankPic.enabled = false;
				this.RankLabel.enabled = true;
			}
		}
		this.RankLabel.text = string.Format("NO.{0}", rankNum + 1);
		this.curRankNum = rankNum;
		switch (type)
		{
		case RANK_TYPE.FIGHT:
		case RANK_TYPE.LEVEL:
			this.nameLabel.text = curinfo.name;
			this.infoLabel.text = string.Format("{0}", curinfo.score >> 32);
			this.VacationFlag.spriteName = GameDefine.Profession_PicName[(int)(checked((IntPtr)curinfo.profession))];
			this.carnameLabel.enabled = false;
			this.VacationFlag.enabled = true;
			break;
		case RANK_TYPE.LADDER:
		{
			string[] array = curinfo.name.Split(new char[]
			{
				'#'
			});
			this.nameLabel.text = array[0];
			if (array.Length > 1)
			{
				this.infoLabel.text = string.Format("{0}", array[1]);
			}
			else
			{
				this.infoLabel.text = string.Format("{0}", "error");
			}
			this.VacationFlag.spriteName = GameDefine.Profession_PicName[(int)(checked((IntPtr)curinfo.profession))];
			this.carnameLabel.enabled = false;
			this.VacationFlag.enabled = true;
			break;
		}
		case RANK_TYPE.TOWER:
			this.nameLabel.text = curinfo.name;
			this.infoLabel.text = string.Format("{0}", (curinfo.score >> 32) + 1L);
			this.VacationFlag.spriteName = GameDefine.Profession_PicName[(int)(checked((IntPtr)curinfo.profession))];
			this.carnameLabel.enabled = false;
			this.VacationFlag.enabled = true;
			break;
		case RANK_TYPE.GUILD:
		{
			string[] array = curinfo.name.Split(new char[]
			{
				'#'
			});
			this.nameLabel.text = array[0];
			this.chairmanlabel.text = array[1];
			this.lvlabel.text = string.Empty + curinfo.profession;
			this.infoLabel.text = string.Format("{0}", curinfo.score >> 32);
			if (curinfo.HasParm3 && curinfo.HasParm4)
			{
				this.MemberLabel.text = string.Format("{0}/{1}", curinfo.parm3, curinfo.parm4);
			}
			else
			{
				this.MemberLabel.text = string.Empty;
			}
			if (this.guildIcon != null)
			{
				if (curinfo.HasParm1)
				{
					this.guildIcon.spriteName = GameDefine.GuildIcon[(int)curinfo.parm1];
				}
				else
				{
					this.guildIcon.spriteName = GameDefine.GuildIcon[0];
				}
			}
			if (curinfo.HasParm2)
			{
				List<dict_hash> list = new List<dict_hash>(curinfo.parm2.Values);
				int num = list.Count - this.cityList.Count;
				if (num > 0)
				{
					for (int i = 0; i < num; i++)
					{
						GameObject gameObject = Object.Instantiate(this.cityList[0].gameObject) as GameObject;
						UISprite component = gameObject.GetComponent<UISprite>();
						gameObject.name = string.Format("city{0:D2}", this.cityList.Count);
						gameObject.transform.parent = this.CityGrid.transform;
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localPosition = Vector3.zero;
						this.cityList.Add(component);
					}
				}
				for (int j = 0; j < this.cityList.Count; j++)
				{
					if (j < list.Count)
					{
						MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(list[j].id);
						if (mapInfoDataByID != null)
						{
							this.cityList[j].spriteName = mapInfoDataByID.MapIcon;
							NGUITools.SetActive(this.cityList[j].gameObject, true);
						}
						else
						{
							NGUITools.SetActive(this.cityList[j].gameObject, false);
						}
					}
					else
					{
						NGUITools.SetActive(this.cityList[j].gameObject, false);
					}
				}
				if (list.Count > 5)
				{
					this.CityGrid.transform.localScale = Vector3.one * 0.7f;
				}
				else
				{
					this.CityGrid.transform.localScale = Vector3.one;
				}
				this.CityGrid.Reposition();
			}
			else
			{
				for (int k = 0; k < this.cityList.Count; k++)
				{
					NGUITools.SetActive(this.cityList[k].gameObject, false);
				}
			}
			break;
		}
		case RANK_TYPE.CAR:
		{
			string[] array2 = curinfo.name.Split(new char[]
			{
				'#'
			});
			this.nameLabel.text = array2[0];
			MountData mountDataById = DataManager.GetMountDataById(array2[1]);
			this.carnameLabel.enabled = true;
			this.VacationFlag.enabled = false;
			this.carnameLabel.text = StrDictionary.GetDictionaryString(mountDataById.CarName, new object[0]);
			this.infoLabel.text = string.Format("{0}", TimeTools.GetCentiSecondStr((int)((long)GameDefine.DayCentiSecond - (curinfo.score >> 32))));
			break;
		}
		case RANK_TYPE.SEX:
		{
			string[] array3 = curinfo.name.Split(new char[]
			{
				'#'
			});
			this.nameLabel.text = array3[0];
			this.infoLabel.text = string.Format("{0}", curinfo.score >> 32);
			this.VacationFlag.spriteName = GameDefine.Profession_PicName[(int)(checked((IntPtr)curinfo.profession))];
			this.carnameLabel.enabled = false;
			this.VacationFlag.enabled = true;
			break;
		}
		}
	}

	// Token: 0x0600449C RID: 17564 RVA: 0x00156D50 File Offset: 0x00154F50
	public void OnClickBtn()
	{
		if (this.OnClick != null)
		{
			this.OnClick(this.curRankNum, this.curItemIndex);
		}
	}

	// Token: 0x04003142 RID: 12610
	public int curRankNum;

	// Token: 0x04003143 RID: 12611
	public int curItemIndex;

	// Token: 0x04003144 RID: 12612
	public UILabel RankLabel;

	// Token: 0x04003145 RID: 12613
	public UISprite RankPic;

	// Token: 0x04003146 RID: 12614
	public RankItemLogic.onClickBtn OnClick;

	// Token: 0x04003147 RID: 12615
	public UILabel nameLabel;

	// Token: 0x04003148 RID: 12616
	public UILabel infoLabel;

	// Token: 0x04003149 RID: 12617
	public UISprite VacationFlag;

	// Token: 0x0400314A RID: 12618
	public UILabel chairmanlabel;

	// Token: 0x0400314B RID: 12619
	public UILabel lvlabel;

	// Token: 0x0400314C RID: 12620
	public UILabel carnameLabel;

	// Token: 0x0400314D RID: 12621
	public UISprite guildIcon;

	// Token: 0x0400314E RID: 12622
	public UILabel MemberLabel;

	// Token: 0x0400314F RID: 12623
	public UIGrid CityGrid;

	// Token: 0x04003150 RID: 12624
	public List<UISprite> cityList = new List<UISprite>();

	// Token: 0x02000AF7 RID: 2807
	// (Invoke) Token: 0x06005065 RID: 20581
	public delegate void onClickBtn(int ranknum, int itemindex);
}
