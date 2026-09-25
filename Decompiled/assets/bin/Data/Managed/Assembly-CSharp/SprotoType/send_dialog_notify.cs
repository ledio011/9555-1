using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005C2 RID: 1474
	public class send_dialog_notify
	{
		// Token: 0x020005C3 RID: 1475
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A85 RID: 10885 RVA: 0x000B1FB0 File Offset: 0x000B01B0
			public request() : base(send_dialog_notify.request.max_field_count)
			{
			}

			// Token: 0x06002A86 RID: 10886 RVA: 0x000B1FC0 File Offset: 0x000B01C0
			public request(byte[] buffer) : base(send_dialog_notify.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C27 RID: 3111
			// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000B1FDC File Offset: 0x000B01DC
			// (set) Token: 0x06002A89 RID: 10889 RVA: 0x000B1FE4 File Offset: 0x000B01E4
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000C28 RID: 3112
			// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000B1FFC File Offset: 0x000B01FC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C29 RID: 3113
			// (get) Token: 0x06002A8B RID: 10891 RVA: 0x000B200C File Offset: 0x000B020C
			// (set) Token: 0x06002A8C RID: 10892 RVA: 0x000B2014 File Offset: 0x000B0214
			public string key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._key = value;
				}
			}

			// Token: 0x17000C2A RID: 3114
			// (get) Token: 0x06002A8D RID: 10893 RVA: 0x000B202C File Offset: 0x000B022C
			public bool HasKey
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C2B RID: 3115
			// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000B203C File Offset: 0x000B023C
			// (set) Token: 0x06002A8F RID: 10895 RVA: 0x000B2044 File Offset: 0x000B0244
			public List<string> parm
			{
				get
				{
					return this._parm;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._parm = value;
				}
			}

			// Token: 0x17000C2C RID: 3116
			// (get) Token: 0x06002A90 RID: 10896 RVA: 0x000B205C File Offset: 0x000B025C
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002A91 RID: 10897 RVA: 0x000B206C File Offset: 0x000B026C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.type = this.deserialize.read_integer();
						break;
					case 1:
						this.key = this.deserialize.read_string();
						break;
					case 2:
						this.parm = this.deserialize.read_string_list();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002A92 RID: 10898 RVA: 0x000B2100 File Offset: 0x000B0300
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.key, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.parm, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E15 RID: 7701
			private static int max_field_count = 3;

			// Token: 0x04001E16 RID: 7702
			private long _type;

			// Token: 0x04001E17 RID: 7703
			private string _key;

			// Token: 0x04001E18 RID: 7704
			private List<string> _parm;
		}
	}
}
