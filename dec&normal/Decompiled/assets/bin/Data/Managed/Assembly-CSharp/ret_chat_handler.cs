using System;
using System.Collections.Generic;
using Sproto;
using SprotoType;

// Token: 0x0200025F RID: 607
public class ret_chat_handler
{
	// Token: 0x06001345 RID: 4933 RVA: 0x0007D370 File Offset: 0x0007B570
	public static SprotoTypeBase ret_chat_request(SprotoTypeBase req)
	{
		ret_chat.request request = req as ret_chat.request;
		if (request != null && request.HasChat_list)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			List<chat_item> chat_list = request.chat_list;
			for (int i = 0; i < chat_list.Count; i++)
			{
				if (chat_list[i].chattype != 6L && chat_list[i].chattype != 7L)
				{
					playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
				}
				else if (chat_list[i].chattype == 7L)
				{
					string text = string.Empty;
					string id = string.Empty;
					string text2 = string.Empty;
					if (chat_list[i].HasIntdata && chat_list[i].intdata.Count > 0)
					{
						switch ((int)chat_list[i].intdata[0])
						{
						case 0:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							id = chat_list[i].stringdata[1];
							ItemData itemDataByID = DataManager.GetItemDataByID(id);
							if (GameManager.IsSupportCurDataVersion56())
							{
								if (itemDataByID.Type == GameDefine.ITEM_TYPE.EXCHANGE)
								{
									MountData mountDataById = DataManager.GetMountDataById(itemDataByID.Function.ToString());
									if (mountDataById != null)
									{
										text2 = StrDictionary.GetDictionaryString("#{101914}", new object[]
										{
											text,
											GameDefine.GetYellowColor(mountDataById.MCarName)
										});
									}
								}
								else if (itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
								{
									text2 = StrDictionary.GetDictionaryString("#{101915}", new object[]
									{
										text,
										GameDefine.GetYellowColor(itemDataByID.MName)
									});
								}
								else if (itemDataByID.Type == GameDefine.ITEM_TYPE.BADGE)
								{
									text2 = StrDictionary.GetDictionaryString("#{101916}", new object[]
									{
										text,
										GameDefine.GetYellowColor(itemDataByID.MName)
									});
								}
							}
							if (string.IsNullOrEmpty(text2))
							{
								text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
								{
									text,
									GameDefine.GetYellowColor(itemDataByID.MName)
								});
							}
							break;
						}
						case 1:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							id = chat_list[i].stringdata[1];
							NpcData npcDataByID = DataManager.GetNpcDataByID(id);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text,
								GameDefine.GetYellowColor(npcDataByID.MName)
							});
							break;
						}
						case 2:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							id = chat_list[i].stringdata[1];
							ItemData itemDataByID = DataManager.GetItemDataByID(id);
							if (GameManager.IsSupportCurDataVersion56())
							{
								if (itemDataByID.Type == GameDefine.ITEM_TYPE.EXCHANGE)
								{
									MountData mountDataById2 = DataManager.GetMountDataById(itemDataByID.Function.ToString());
									if (mountDataById2 != null)
									{
										text2 = StrDictionary.GetDictionaryString("#{101914}", new object[]
										{
											text,
											GameDefine.GetYellowColor(mountDataById2.MCarName)
										});
									}
								}
								else if (itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
								{
									text2 = StrDictionary.GetDictionaryString("#{101915}", new object[]
									{
										text,
										GameDefine.GetYellowColor(itemDataByID.MName)
									});
								}
								else if (itemDataByID.Type == GameDefine.ITEM_TYPE.BADGE)
								{
									text2 = StrDictionary.GetDictionaryString("#{101916}", new object[]
									{
										text,
										GameDefine.GetYellowColor(itemDataByID.MName)
									});
								}
							}
							if (string.IsNullOrEmpty(text2))
							{
								text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
								{
									text,
									GameDefine.GetYellowColor(itemDataByID.MName)
								});
							}
							break;
						}
						case 3:
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 4:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							int num = (int)chat_list[i].intdata[1];
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text,
								GameDefine.GetYellowColor(StrDictionary.GetDictionaryString(GameDefine.RefinePartName[num], new object[0]))
							});
							break;
						}
						case 5:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							id = chat_list[i].stringdata[1];
							ItemData itemDataByID = DataManager.GetItemDataByID(id);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text,
								GameDefine.GetYellowColor(itemDataByID.MName)
							});
							break;
						}
						case 6:
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 7:
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 8:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							int value = (int)chat_list[i].intdata[1];
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text,
								GameDefine.GetYellowColor(value)
							});
							break;
						}
						case 10:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							int num2 = (int)chat_list[i].intdata[1];
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text,
								GameDefine.GetYellowColor(StrDictionary.GetDictionaryString(GameDefine.RefinePartName[num2], new object[0])),
								(int)chat_list[i].intdata[2]
							});
							break;
						}
						case 14:
							text = GameDefine.GetYellowColor(StrDictionary.GetDictionaryString("#{101516}", new object[0]));
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 15:
							text = GameDefine.GetYellowColor(StrDictionary.GetDictionaryString("#{101517}", new object[0]));
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 16:
							text = GameDefine.GetYellowColor(StrDictionary.GetDictionaryString("#{101519}", new object[0]));
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 17:
							text = GameDefine.GetYellowColor(StrDictionary.GetDictionaryString("#{101515}", new object[0]));
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 18:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							id = chat_list[i].stringdata[1];
							NpcData npcDataByID2 = DataManager.GetNpcDataByID(id);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								GameDefine.GetYellowColor(npcDataByID2.MName),
								text
							});
							chat_list[i].chattype = 0L;
							chat_list[i].chatInfo = text2;
							chat_list[i].chatInfo2 = chat_list[i].chatInfo;
							playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							break;
						}
						case 19:
							text = GameDefine.GetYellowColor(StrDictionary.GetDictionaryString("#{100748}", new object[0]));
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 20:
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 21:
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text
							});
							break;
						case 22:
						{
							text = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
							id = chat_list[i].stringdata[2];
							NpcData npcDataByID3 = DataManager.GetNpcDataByID(chat_list[i].stringdata[1]);
							ItemData itemDataByID = DataManager.GetItemDataByID(id);
							text2 = StrDictionary.GetDictionaryString(chat_list[i].chatInfo, new object[]
							{
								text,
								GameDefine.GetYellowColor(npcDataByID3.MName),
								GameDefine.GetYellowColor(itemDataByID.MName)
							});
							break;
						}
						}
					}
					else
					{
						text2 = StrDictionary.GetServerDictionaryString(chat_list[i].chatInfo);
					}
					if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene() && !string.IsNullOrEmpty(text2))
					{
						BroadCastRootLogic.AddMessage(text2);
					}
				}
				else if (chat_list[i].chattype == 6L)
				{
					GameManager instance = SingletonDontDestoryUnity<GameManager>.Instance;
					GameDefine.ACTIVITY_TYPE activity_TYPE = (GameDefine.ACTIVITY_TYPE)chat_list[i].intdata[0];
					int num3 = (int)chat_list[i].intdata[1];
					if (chat_list[i].HasIntdata)
					{
						if (num3 == 1)
						{
							if (activity_TYPE == GameDefine.ACTIVITY_TYPE.ESCORT)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{101513}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 5L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{101514}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 10L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{101519}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 8L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.CITY_DANCE)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{101515}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 6L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.BAR_FIGHT)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{101516}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 7L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{101517}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 9L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.GUILD_BOSS)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{100748}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 4L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.GUILD_DANCE)
							{
								chat_list[i].chattype = 4L;
								chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString("#{100264}*#{105100}");
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 11L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.GUILD_DONMINE)
							{
								chat_list[i].chattype = 0L;
								chat_list[i].chatInfo = StrDictionary.GetDictionaryString("#{106034}", new object[0]);
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 13L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
								if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
								{
									chat_list[i].chattype = 4L;
									playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
								}
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.GUILD_DONMINE_RES)
							{
								chat_list[i].chattype = 0L;
								GuildCaptureData guildCaptureDataByID = DataManager.GetGuildCaptureDataByID(chat_list[i].stringdata[1]);
								MapInfoData mapInfoDataByID = DataManager.GetMapInfoDataByID(guildCaptureDataByID.MapID);
								string yellowColor = GameDefine.GetYellowColor(chat_list[i].stringdata[0]);
								chat_list[i].chatInfo = StrDictionary.GetDictionaryString("#{106035}", new object[]
								{
									yellowColor,
									StrDictionary.GetDictionaryString(mapInfoDataByID.Name, new object[0])
								});
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = 14L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
								if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
								{
									chat_list[i].chattype = 4L;
									playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
								}
							}
							else if (activity_TYPE == GameDefine.ACTIVITY_TYPE.KILL_PLAYER)
							{
								chat_list[i].chattype = 0L;
								if (chat_list[i].stringdata.Count >= 4 && !string.IsNullOrEmpty(chat_list[i].stringdata[3]))
								{
									MapInfoData mapInfoDataByID2 = DataManager.GetMapInfoDataByID(chat_list[i].stringdata[2]);
									chat_list[i].chatInfo = StrDictionary.GetDictionaryString("#{103309}", new object[]
									{
										chat_list[i].stringdata[0],
										StrDictionary.GetDictionaryString(mapInfoDataByID2.Name, new object[0]),
										chat_list[i].stringdata[3],
										chat_list[i].stringdata[1]
									});
								}
								else
								{
									MapInfoData mapInfoDataByID3 = DataManager.GetMapInfoDataByID(chat_list[i].stringdata[2]);
									chat_list[i].chatInfo = StrDictionary.GetDictionaryString("#{103310}", new object[]
									{
										chat_list[i].stringdata[0],
										StrDictionary.GetDictionaryString(mapInfoDataByID3.Name, new object[0]),
										chat_list[i].stringdata[1]
									});
								}
								chat_list[i].chatInfo2 = chat_list[i].chatInfo;
								chat_list[i].linktype = -1L;
								playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
								if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
								{
									chat_list[i].chattype = 4L;
									playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
								}
							}
						}
						else if (num3 == 2 && activity_TYPE == GameDefine.ACTIVITY_TYPE.BAR_FIGHT)
						{
							chat_list[i].chattype = 0L;
							chat_list[i].chatInfo = "#{100264}*#{101516}";
							chat_list[i].chatInfo = StrDictionary.GetServerDictionaryString(chat_list[i].chatInfo);
							chat_list[i].chatInfo2 = chat_list[i].chatInfo;
							chat_list[i].linktype = 7L;
							playerData.ChatHistory.OnReceiveChatMessage(chat_list[i]);
						}
						instance.PlayerData.ActivityData.UpdateActivity(chat_list[i]);
						if (instance.SceneManager != null)
						{
							instance.SceneManager.CheckSceneActivity();
						}
					}
				}
			}
		}
		return null;
	}
}
