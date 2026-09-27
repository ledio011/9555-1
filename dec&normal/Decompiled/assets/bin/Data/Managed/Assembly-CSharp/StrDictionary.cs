using System;

// Token: 0x02000A75 RID: 2677
public class StrDictionary
{
	// Token: 0x06004DF6 RID: 19958 RVA: 0x001AA358 File Offset: 0x001A8558
	public static string GetClientDictionaryString(string key, params object[] args)
	{
		if (string.IsNullOrEmpty(key))
		{
			return "Empty ---key erro!";
		}
		if (key.Length < 3)
		{
			return key + " -- ServerDictionaryFormat ERROR2 Length < 3";
		}
		string key2 = key.Substring(2, key.Length - 3);
		string result;
		try
		{
			string text = string.Format(LocalizationManager.Get(key2), args);
			result = text.Replace("#r", "\n");
		}
		catch (Exception ex)
		{
			result = "formate erro!";
		}
		return result;
	}

	// Token: 0x06004DF7 RID: 19959 RVA: 0x001AA3F4 File Offset: 0x001A85F4
	public static string[] ParseArgs(string[] all)
	{
		if (all == null)
		{
			return null;
		}
		string[] array = new string[all.Length];
		for (int i = 0; i < all.Length; i++)
		{
			string text = all[i];
			if (!string.IsNullOrEmpty(text) && text.get_Chars(0) == '#')
			{
				array[i] = StrDictionary.GetClientDictionaryString(text, new object[0]);
			}
			else
			{
				array[i] = all[i];
			}
		}
		return array;
	}

	// Token: 0x06004DF8 RID: 19960 RVA: 0x001AA460 File Offset: 0x001A8660
	public static string GetServerDictionaryString(string keystr)
	{
		if (string.IsNullOrEmpty(keystr))
		{
			return "Empty ---key erro!";
		}
		char c = keystr.get_Chars(0);
		if (c != '#')
		{
			return keystr;
		}
		int num = keystr.IndexOf('*');
		if (num > 0)
		{
			string key = keystr.Substring(0, num);
			string text = keystr.Substring(num + 1, keystr.Length - num - 1);
			string[] args = StrDictionary.ParseArgs(text.Split(new char[]
			{
				'*'
			}));
			return StrDictionary.GetClientDictionaryString(key, args);
		}
		if (keystr.Length < 3)
		{
			return keystr + " -- ServerDictionaryFormat ERROR2 Length < 3";
		}
		string key2 = keystr.Substring(2, keystr.Length - 3);
		string text2 = LocalizationManager.Get(key2);
		return text2.Replace("#r", "\n");
	}

	// Token: 0x06004DF9 RID: 19961 RVA: 0x001AA524 File Offset: 0x001A8724
	public static string GetDictionaryString(string keystr, params object[] args)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(keystr))
		{
			char c = keystr.get_Chars(0);
			if (c != '#')
			{
				result = string.Format(keystr, args);
			}
			else
			{
				result = StrDictionary.GetClientDictionaryString(keystr, args);
			}
		}
		return result;
	}
}
