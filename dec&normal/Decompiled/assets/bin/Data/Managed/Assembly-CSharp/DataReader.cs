using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200013B RID: 315
public static class DataReader
{
	// Token: 0x06000CEF RID: 3311 RVA: 0x0005AA9C File Offset: 0x00058C9C
	public static byte[] GetBytes(object obj)
	{
		TextAsset textAsset = obj as TextAsset;
		if (textAsset == null)
		{
			return null;
		}
		return textAsset.bytes;
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0005AAC4 File Offset: 0x00058CC4
	public static List<T> LoadImportData<T>(string fileName)
	{
		object obj = BundleManager.LoadTable(fileName);
		if (obj == null)
		{
			obj = Resources.Load("Data/" + fileName);
			if (obj == null)
			{
				return null;
			}
		}
		ByteReader byteReader = new ByteReader(DataReader.GetBytes(obj));
		BetterList<string> betterList;
		for (betterList = byteReader.ReadCSV(); betterList != null; betterList = byteReader.ReadCSV())
		{
			if (betterList[0].Contains("*"))
			{
				betterList.RemoveAt(0);
				break;
			}
		}
		List<T> list = new List<T>();
		int size = betterList.size;
		FieldInfo[] array = new FieldInfo[size];
		for (int i = 0; i < size; i++)
		{
			array[i] = typeof(T).GetField(betterList[i]);
		}
		BetterList<string> betterList2 = byteReader.ReadCSV();
		while (betterList2 != null)
		{
			if (string.IsNullOrEmpty(betterList2[0]) || !betterList2[0].Contains("*"))
			{
				betterList2 = byteReader.ReadCSV();
			}
			else
			{
				betterList2.RemoveAt(0);
				T t = (default(T) != null) ? default(T) : Activator.CreateInstance<T>();
				for (int j = 0; j < betterList2.size; j++)
				{
					object obj2 = null;
					string text = betterList2[j];
					if (!string.IsNullOrEmpty(text) && array[j] != null)
					{
						if (array[j].FieldType.IsEnum)
						{
							try
							{
								obj2 = Enum.Parse(array[j].FieldType, text);
							}
							catch (Exception ex)
							{
								Log.WARING_MSG(string.Concat(new string[]
								{
									"error datafile=",
									fileName,
									" fieldinfo=",
									array[j].Name,
									" fieldinfoValue=",
									text
								}));
							}
						}
						else
						{
							try
							{
								obj2 = Convert.ChangeType(text, array[j].FieldType);
							}
							catch (Exception ex2)
							{
								Log.WARING_MSG(string.Concat(new string[]
								{
									"error datafile=",
									fileName,
									" fieldinfo=",
									array[j].Name,
									" fieldinfoValue=",
									text,
									"  ID ",
									betterList2[0]
								}));
							}
						}
						array[j].SetValue(t, obj2);
					}
				}
				list.Add(t);
				betterList2 = byteReader.ReadCSV();
			}
		}
		return list;
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0005AD88 File Offset: 0x00058F88
	public static List<T> LoadImportDicData<T>(string fileName, string DicDataName)
	{
		object obj = BundleManager.LoadTable(fileName);
		if (obj == null)
		{
			obj = Resources.Load("Data/" + fileName);
			if (obj == null)
			{
				return null;
			}
		}
		ByteReader byteReader = new ByteReader(DataReader.GetBytes(obj));
		BetterList<string> betterList;
		for (betterList = byteReader.ReadCSV(); betterList != null; betterList = byteReader.ReadCSV())
		{
			if (betterList[0].Contains("*"))
			{
				betterList.RemoveAt(0);
				break;
			}
		}
		List<string> list = new List<string>();
		for (int i = 0; i < betterList.size; i++)
		{
			list.Add(betterList[i]);
		}
		List<T> list2 = new List<T>();
		int count = list.Count;
		FieldInfo[] array = new FieldInfo[count];
		for (int j = 0; j < count; j++)
		{
			array[j] = typeof(T).GetField(list[j]);
		}
		FieldInfo field = typeof(T).GetField(DicDataName);
		BetterList<string> betterList2 = byteReader.ReadCSV();
		while (betterList2 != null)
		{
			if (string.IsNullOrEmpty(betterList2[0]) || !betterList2[0].Contains("*"))
			{
				betterList2 = byteReader.ReadCSV();
			}
			else
			{
				betterList2.RemoveAt(0);
				T t = (default(T) != null) ? default(T) : Activator.CreateInstance<T>();
				Dictionary<string, string> dictionary = field.GetValue(t) as Dictionary<string, string>;
				for (int k = 0; k < betterList2.size; k++)
				{
					object obj2 = null;
					string text = betterList2[k];
					if (list[k].StartsWith("_"))
					{
						dictionary.Add(list[k], text);
					}
					if (!string.IsNullOrEmpty(text) && array[k] != null)
					{
						if (array[k].FieldType.IsEnum)
						{
							try
							{
								obj2 = Enum.Parse(array[k].FieldType, text);
							}
							catch (Exception ex)
							{
								Log.WARING_MSG(string.Concat(new string[]
								{
									"error datafile=",
									fileName,
									" fieldinfo=",
									array[k].Name,
									" fieldinfoValue=",
									text
								}));
							}
						}
						else
						{
							try
							{
								obj2 = Convert.ChangeType(text, array[k].FieldType);
							}
							catch (Exception ex2)
							{
								Log.WARING_MSG(string.Concat(new string[]
								{
									"error datafile=",
									fileName,
									" fieldinfo=",
									array[k].Name,
									" fieldinfoValue=",
									text,
									"  ID ",
									betterList2[0]
								}));
							}
						}
						array[k].SetValue(t, obj2);
					}
				}
				list2.Add(t);
				betterList2 = byteReader.ReadCSV();
			}
		}
		return list2;
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0005B0CC File Offset: 0x000592CC
	public static Dictionary<T1, T2> LoadDicTable<T1, T2>(string fileName, string keyName, string dicName) where T2 : class
	{
		List<T2> list = DataReader.LoadImportDicData<T2>(fileName, dicName);
		return DataReader.LoadTable<T1, T2>(list, keyName);
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0005B0E8 File Offset: 0x000592E8
	public static Dictionary<string, MissionData> LoadMissionDataTable(string fileName, string keyName, ref Dictionary<int, List<string>> autoAcceptDic)
	{
		autoAcceptDic.Clear();
		List<MissionData> list = DataReader.LoadImportData<MissionData>(fileName);
		return DataReader.LoadMissionDataTable(list, keyName, ref autoAcceptDic);
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0005B10C File Offset: 0x0005930C
	public static Dictionary<string, MissionData> LoadMissionDataTable(List<MissionData> list, string keyName, ref Dictionary<int, List<string>> autoAcceptDic)
	{
		if (list != null)
		{
			Dictionary<string, MissionData> dictionary = new Dictionary<string, MissionData>(list.Count);
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					try
					{
						dictionary.Add(list[i].ID, list[i]);
					}
					catch (Exception ex)
					{
						Debug.Log(i.ToString());
						Debug.LogError(list[i].ID);
					}
					if (list[i].AutoAcceptLv > 0)
					{
						if (!autoAcceptDic.ContainsKey(list[i].AutoAcceptLv))
						{
							autoAcceptDic.Add(list[i].AutoAcceptLv, new List<string>());
						}
						if (!autoAcceptDic[list[i].AutoAcceptLv].Contains(list[i].ID))
						{
							autoAcceptDic[list[i].AutoAcceptLv].Add(list[i].ID);
						}
					}
				}
			}
			return dictionary;
		}
		return null;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0005B238 File Offset: 0x00059438
	public static Dictionary<T1, T2> LoadTable<T1, T2>(string fileName, string keyName) where T2 : class
	{
		List<T2> list = DataReader.LoadImportData<T2>(fileName);
		return DataReader.LoadTable<T1, T2>(list, keyName);
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0005B254 File Offset: 0x00059454
	public static Dictionary<T1, T2> LoadTable<T1, T2>(List<T2> list, string keyName) where T2 : class
	{
		if (list != null)
		{
			Dictionary<T1, T2> dictionary = new Dictionary<T1, T2>(list.Count);
			FieldInfo field = typeof(T2).GetField(keyName);
			if (field != null && list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					T2 t = list[i];
					T1 t2 = (T1)((object)field.GetValue(t));
					try
					{
						dictionary.Add(t2, t);
					}
					catch (Exception ex)
					{
						Debug.Log(field.Name + " " + keyName);
						Debug.Log(typeof(T2).Name);
						Debug.Log(i.ToString());
						Debug.LogError(t2);
					}
				}
			}
			return dictionary;
		}
		return null;
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0005B338 File Offset: 0x00059538
	public static Dictionary<T1, List<T2>> LoadTableList<T1, T2>(string fileName, string keyName) where T2 : class
	{
		List<T2> list = DataReader.LoadImportData<T2>(fileName);
		return DataReader.LoadTableList<T1, T2>(list, keyName);
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0005B354 File Offset: 0x00059554
	public static Dictionary<T1, List<T2>> LoadTableList<T1, T2>(List<T2> list, string keyName) where T2 : class
	{
		Dictionary<T1, List<T2>> dictionary = new Dictionary<T1, List<T2>>();
		FieldInfo field = typeof(T2).GetField(keyName);
		if (field != null && list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				T2 t = list[i];
				T1 t2 = (T1)((object)field.GetValue(t));
				List<T2> list2 = null;
				if (!dictionary.TryGetValue(t2, ref list2))
				{
					list2 = new List<T2>();
					dictionary.Add(t2, list2);
				}
				list2.Add(t);
			}
		}
		return dictionary;
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0005B3E4 File Offset: 0x000595E4
	public static T2 GetTableRow<T1, T2>(Dictionary<T1, T2> dataDict, T1 key) where T2 : class
	{
		T2 result;
		if (dataDict != null && dataDict.TryGetValue(key, ref result))
		{
			return result;
		}
		return (T2)((object)null);
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0005B410 File Offset: 0x00059610
	public static List<T2> GetTableList<T1, T2>(Dictionary<T1, List<T2>> dataDict, T1 key) where T2 : class
	{
		List<T2> result;
		if (dataDict.TryGetValue(key, ref result))
		{
			return result;
		}
		return null;
	}
}
