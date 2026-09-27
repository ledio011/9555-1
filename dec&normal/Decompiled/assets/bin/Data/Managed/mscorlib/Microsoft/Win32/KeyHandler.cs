using System;
using System.Collections;
using System.IO;
using System.Security;
using System.Threading;

namespace Microsoft.Win32
{
	// Token: 0x02000023 RID: 35
	internal class KeyHandler
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002150 File Offset: 0x00000350
		private KeyHandler(RegistryKey rkey, string basedir)
		{
			if (!Directory.Exists(basedir))
			{
				try
				{
					Directory.CreateDirectory(basedir);
				}
				catch (UnauthorizedAccessException)
				{
					throw new SecurityException("No access to the given key");
				}
			}
			this.Dir = basedir;
			this.file = Path.Combine(this.Dir, "values.xml");
			this.Load();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Load()
		{
			this.values = new Hashtable();
			if (!File.Exists(this.file))
			{
				return;
			}
			try
			{
				using (FileStream fileStream = File.OpenRead(this.file))
				{
					StreamReader streamReader = new StreamReader(fileStream);
					string text = streamReader.ReadToEnd();
					if (text.Length != 0)
					{
						SecurityElement securityElement = SecurityElement.FromString(text);
						if (securityElement.Tag == "values" && securityElement.Children != null)
						{
							foreach (object obj in securityElement.Children)
							{
								SecurityElement securityElement2 = (SecurityElement)obj;
								if (securityElement2.Tag == "value")
								{
									this.LoadKey(securityElement2);
								}
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				this.values.Clear();
				throw new SecurityException("No access to the given key");
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("While loading registry key at {0}: {1}", this.file, arg);
				this.values.Clear();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002350 File Offset: 0x00000550
		private void LoadKey(SecurityElement se)
		{
			Hashtable attributes = se.Attributes;
			try
			{
				string text = (string)attributes["name"];
				if (text != null)
				{
					string text2 = (string)attributes["type"];
					if (text2 != null)
					{
						string text3 = text2;
						switch (text3)
						{
						case "int":
							this.values[text] = int.Parse(se.Text);
							break;
						case "bytearray":
							this.values[text] = Convert.FromBase64String(se.Text);
							break;
						case "string":
							this.values[text] = se.Text;
							break;
						case "expand":
							this.values[text] = new ExpandString(se.Text);
							break;
						case "qword":
							this.values[text] = long.Parse(se.Text);
							break;
						case "string-array":
						{
							ArrayList arrayList = new ArrayList();
							if (se.Children != null)
							{
								foreach (object obj in se.Children)
								{
									SecurityElement securityElement = (SecurityElement)obj;
									arrayList.Add(securityElement.Text);
								}
							}
							this.values[text] = arrayList.ToArray(typeof(string));
							break;
						}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000259C File Offset: 0x0000079C
		public RegistryKey Ensure(RegistryKey rkey, string extra, bool writable)
		{
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey result;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler == null)
				{
					keyHandler = new KeyHandler(rkey, text);
				}
				RegistryKey registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
				KeyHandler.key_to_handler[registryKey] = keyHandler;
				KeyHandler.dir_to_handler[text] = keyHandler;
				result = registryKey;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002634 File Offset: 0x00000834
		public RegistryKey Probe(RegistryKey rkey, string extra, bool writable)
		{
			RegistryKey registryKey = null;
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey result;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler != null)
				{
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				else if (Directory.Exists(text))
				{
					keyHandler = new KeyHandler(rkey, text);
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.dir_to_handler[text] = keyHandler;
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				result = registryKey;
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026F8 File Offset: 0x000008F8
		private static string CombineName(RegistryKey rkey, string extra)
		{
			if (extra.IndexOf('/') != -1)
			{
				extra = extra.Replace('/', '\\');
			}
			return rkey.Name + "\\" + extra;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002728 File Offset: 0x00000928
		public static KeyHandler Lookup(RegistryKey rkey, bool createNonExisting)
		{
			Type typeFromHandle = typeof(KeyHandler);
			KeyHandler result;
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					result = keyHandler;
				}
				else if (!rkey.IsRoot || !createNonExisting)
				{
					result = null;
				}
				else
				{
					RegistryHive hive = rkey.Hive;
					RegistryHive registryHive = hive;
					switch (registryHive + -2147483648)
					{
					case (RegistryHive)0:
					case (RegistryHive)2:
					case (RegistryHive)3:
					case (RegistryHive)4:
					case (RegistryHive)5:
					case (RegistryHive)6:
					{
						string text = Path.Combine(KeyHandler.MachineStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text);
						KeyHandler.dir_to_handler[text] = keyHandler;
						break;
					}
					case (RegistryHive)1:
					{
						string text2 = Path.Combine(KeyHandler.UserStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text2);
						KeyHandler.dir_to_handler[text2] = keyHandler;
						break;
					}
					default:
						throw new Exception("Unknown RegistryHive");
					}
					KeyHandler.key_to_handler[rkey] = keyHandler;
					result = keyHandler;
				}
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002854 File Offset: 0x00000A54
		public static void Drop(RegistryKey rkey)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					KeyHandler.key_to_handler.Remove(rkey);
					int num = 0;
					foreach (object obj in KeyHandler.key_to_handler)
					{
						if (((DictionaryEntry)obj).Value == keyHandler)
						{
							num++;
						}
					}
					if (num == 0)
					{
						KeyHandler.dir_to_handler.Remove(keyHandler.Dir);
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002930 File Offset: 0x00000B30
		public object GetValue(string name, RegistryValueOptions options)
		{
			if (this.IsMarkedForDeletion)
			{
				return null;
			}
			if (name == null)
			{
				name = string.Empty;
			}
			object obj = this.values[name];
			ExpandString expandString = obj as ExpandString;
			if (expandString == null)
			{
				return obj;
			}
			if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
			{
				return expandString.Expand();
			}
			return expandString.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002988 File Offset: 0x00000B88
		public void SetValue(string name, object value)
		{
			this.AssertNotMarkedForDeletion();
			if (name == null)
			{
				name = string.Empty;
			}
			if (value is int || value is string || value is byte[] || value is string[])
			{
				this.values[name] = value;
			}
			else
			{
				this.values[name] = value.ToString();
			}
			this.SetDirty();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A00 File Offset: 0x00000C00
		public string[] GetValueNames()
		{
			this.AssertNotMarkedForDeletion();
			ICollection keys = this.values.Keys;
			string[] array = new string[keys.Count];
			keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A34 File Offset: 0x00000C34
		private void SetDirty()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (!this.dirty)
				{
					this.dirty = true;
					new Timer(new TimerCallback(this.DirtyTimeout), null, 3000, -1);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void DirtyTimeout(object state)
		{
			this.Flush();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public void Flush()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (this.dirty)
				{
					this.Save();
					this.dirty = false;
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AFC File Offset: 0x00000CFC
		public bool ValueExists(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			return this.values.Contains(name);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002B18 File Offset: 0x00000D18
		public bool IsMarkedForDeletion
		{
			get
			{
				return !KeyHandler.dir_to_handler.Contains(this.Dir);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B30 File Offset: 0x00000D30
		public void RemoveValue(string name)
		{
			this.AssertNotMarkedForDeletion();
			this.values.Remove(name);
			this.SetDirty();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B4C File Offset: 0x00000D4C
		~KeyHandler()
		{
			this.Flush();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B7C File Offset: 0x00000D7C
		private void Save()
		{
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (!File.Exists(this.file) && this.values.Count == 0)
			{
				return;
			}
			SecurityElement securityElement = new SecurityElement("values");
			foreach (object obj in this.values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				object value = dictionaryEntry.Value;
				SecurityElement securityElement2 = new SecurityElement("value");
				securityElement2.AddAttribute("name", SecurityElement.Escape((string)dictionaryEntry.Key));
				if (value is string)
				{
					securityElement2.AddAttribute("type", "string");
					securityElement2.Text = SecurityElement.Escape((string)value);
				}
				else if (value is int)
				{
					securityElement2.AddAttribute("type", "int");
					securityElement2.Text = value.ToString();
				}
				else if (value is long)
				{
					securityElement2.AddAttribute("type", "qword");
					securityElement2.Text = value.ToString();
				}
				else if (value is byte[])
				{
					securityElement2.AddAttribute("type", "bytearray");
					securityElement2.Text = Convert.ToBase64String((byte[])value);
				}
				else if (value is ExpandString)
				{
					securityElement2.AddAttribute("type", "expand");
					securityElement2.Text = SecurityElement.Escape(value.ToString());
				}
				else if (value is string[])
				{
					securityElement2.AddAttribute("type", "string-array");
					foreach (string str in (string[])value)
					{
						securityElement2.AddChild(new SecurityElement("string")
						{
							Text = SecurityElement.Escape(str)
						});
					}
				}
				securityElement.AddChild(securityElement2);
			}
			using (FileStream fileStream = File.Create(this.file))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(securityElement.ToString());
				streamWriter.Flush();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E0C File Offset: 0x0000100C
		private void AssertNotMarkedForDeletion()
		{
			if (this.IsMarkedForDeletion)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002E20 File Offset: 0x00001020
		private static string UserStore
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".mono/registry");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002E34 File Offset: 0x00001034
		private static string MachineStore
		{
			get
			{
				string text = Environment.GetEnvironmentVariable("MONO_REGISTRY_PATH");
				if (text != null)
				{
					return text;
				}
				text = Environment.GetMachineConfigPath();
				int num = text.IndexOf("machine.config");
				return Path.Combine(Path.Combine(text.Substring(0, num - 1), ".."), "registry");
			}
		}

		// Token: 0x04000047 RID: 71
		private static Hashtable key_to_handler = new Hashtable();

		// Token: 0x04000048 RID: 72
		private static Hashtable dir_to_handler = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());

		// Token: 0x04000049 RID: 73
		public string Dir;

		// Token: 0x0400004A RID: 74
		private Hashtable values;

		// Token: 0x0400004B RID: 75
		private string file;

		// Token: 0x0400004C RID: 76
		private bool dirty;
	}
}
