using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x02000037 RID: 55
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public sealed class ProcessStartInfo
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003BD0 File Offset: 0x00001DD0
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("Command line agruments for this process.")]
		[System.ComponentModel.DefaultValue("")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.NotifyParentProperty(true)]
		public string Arguments
		{
			get
			{
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003BDC File Offset: 0x00001DDC
		[MonitoringDescription("Start this process with a new window.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DefaultValue(false)]
		public bool CreateNoWindow
		{
			set
			{
				this.create_no_window = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003BE8 File Offset: 0x00001DE8
		[System.ComponentModel.DefaultValue(null)]
		[System.ComponentModel.Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
		[MonitoringDescription("Environment variables used for this process.")]
		public System.Collections.Specialized.StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.envVars == null)
				{
					this.envVars = new ProcessStringDictionary();
					foreach (object obj in Environment.GetEnvironmentVariables())
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						this.envVars.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
					}
				}
				return this.envVars;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003C84 File Offset: 0x00001E84
		internal bool HaveEnvVars
		{
			get
			{
				return this.envVars != null;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003C94 File Offset: 0x00001E94
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003C9C File Offset: 0x00001E9C
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The name of the resource to start this process.")]
		[System.ComponentModel.DefaultValue("")]
		[System.ComponentModel.Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		public string FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003CA8 File Offset: 0x00001EA8
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003CB0 File Offset: 0x00001EB0
		[System.ComponentModel.DefaultValue(false)]
		[MonitoringDescription("Errors of this process are redirected.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirect_standard_error;
			}
			set
			{
				this.redirect_standard_error = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003CBC File Offset: 0x00001EBC
		[System.ComponentModel.NotifyParentProperty(true)]
		[MonitoringDescription("Standard input of this process is redirected.")]
		[System.ComponentModel.DefaultValue(false)]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirect_standard_input;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003CC4 File Offset: 0x00001EC4
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003CCC File Offset: 0x00001ECC
		[MonitoringDescription("Standart output of this process is redirected.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DefaultValue(false)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirect_standard_output;
			}
			set
			{
				this.redirect_standard_output = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.encoding_stderr;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.encoding_stdout;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003CE8 File Offset: 0x00001EE8
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003CF0 File Offset: 0x00001EF0
		[System.ComponentModel.DefaultValue(true)]
		[MonitoringDescription("Use the shell to start this process.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.use_shell_execute;
			}
			set
			{
				this.use_shell_execute = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003CFC File Offset: 0x00001EFC
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The initial directory for this process.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DefaultValue("")]
		public string WorkingDirectory
		{
			get
			{
				return this.working_directory;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003D04 File Offset: 0x00001F04
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.load_user_profile;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003D0C File Offset: 0x00001F0C
		[System.ComponentModel.NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003D14 File Offset: 0x00001F14
		[System.ComponentModel.NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003D1C File Offset: 0x00001F1C
		public SecureString Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x040000B9 RID: 185
		private string arguments = string.Empty;

		// Token: 0x040000BA RID: 186
		private IntPtr error_dialog_parent_handle = (IntPtr)0;

		// Token: 0x040000BB RID: 187
		private string filename = string.Empty;

		// Token: 0x040000BC RID: 188
		private string verb = string.Empty;

		// Token: 0x040000BD RID: 189
		private string working_directory = string.Empty;

		// Token: 0x040000BE RID: 190
		private ProcessStringDictionary envVars;

		// Token: 0x040000BF RID: 191
		private bool create_no_window;

		// Token: 0x040000C0 RID: 192
		private bool error_dialog;

		// Token: 0x040000C1 RID: 193
		private bool redirect_standard_error;

		// Token: 0x040000C2 RID: 194
		private bool redirect_standard_input;

		// Token: 0x040000C3 RID: 195
		private bool redirect_standard_output;

		// Token: 0x040000C4 RID: 196
		private bool use_shell_execute = true;

		// Token: 0x040000C5 RID: 197
		private ProcessWindowStyle window_style;

		// Token: 0x040000C6 RID: 198
		private Encoding encoding_stderr;

		// Token: 0x040000C7 RID: 199
		private Encoding encoding_stdout;

		// Token: 0x040000C8 RID: 200
		private string username;

		// Token: 0x040000C9 RID: 201
		private string domain;

		// Token: 0x040000CA RID: 202
		private SecureString password;

		// Token: 0x040000CB RID: 203
		private bool load_user_profile;

		// Token: 0x040000CC RID: 204
		private static readonly string[] empty = new string[0];
	}
}
