using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000429 RID: 1065
	public class notice_copy_scene_info
	{
		// Token: 0x0200042A RID: 1066
		public class request : SprotoTypeBase
		{
			// Token: 0x060020F7 RID: 8439 RVA: 0x0009F8F0 File Offset: 0x0009DAF0
			public request() : base(notice_copy_scene_info.request.max_field_count)
			{
			}

			// Token: 0x060020F8 RID: 8440 RVA: 0x0009F900 File Offset: 0x0009DB00
			public request(byte[] buffer) : base(notice_copy_scene_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700091F RID: 2335
			// (get) Token: 0x060020FA RID: 8442 RVA: 0x0009F91C File Offset: 0x0009DB1C
			// (set) Token: 0x060020FB RID: 8443 RVA: 0x0009F924 File Offset: 0x0009DB24
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x060020FC RID: 8444 RVA: 0x0009F93C File Offset: 0x0009DB3C
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x060020FD RID: 8445 RVA: 0x0009F94C File Offset: 0x0009DB4C
			// (set) Token: 0x060020FE RID: 8446 RVA: 0x0009F954 File Offset: 0x0009DB54
			public long index
			{
				get
				{
					return this._index;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._index = value;
				}
			}

			// Token: 0x17000922 RID: 2338
			// (get) Token: 0x060020FF RID: 8447 RVA: 0x0009F96C File Offset: 0x0009DB6C
			public bool HasIndex
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000923 RID: 2339
			// (get) Token: 0x06002100 RID: 8448 RVA: 0x0009F97C File Offset: 0x0009DB7C
			// (set) Token: 0x06002101 RID: 8449 RVA: 0x0009F984 File Offset: 0x0009DB84
			public long time
			{
				get
				{
					return this._time;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._time = value;
				}
			}

			// Token: 0x17000924 RID: 2340
			// (get) Token: 0x06002102 RID: 8450 RVA: 0x0009F99C File Offset: 0x0009DB9C
			public bool HasTime
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000925 RID: 2341
			// (get) Token: 0x06002103 RID: 8451 RVA: 0x0009F9AC File Offset: 0x0009DBAC
			// (set) Token: 0x06002104 RID: 8452 RVA: 0x0009F9B4 File Offset: 0x0009DBB4
			public long parm1
			{
				get
				{
					return this._parm1;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._parm1 = value;
				}
			}

			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x06002105 RID: 8453 RVA: 0x0009F9CC File Offset: 0x0009DBCC
			public bool HasParm1
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x06002106 RID: 8454 RVA: 0x0009F9DC File Offset: 0x0009DBDC
			// (set) Token: 0x06002107 RID: 8455 RVA: 0x0009F9E4 File Offset: 0x0009DBE4
			public long parm2
			{
				get
				{
					return this._parm2;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._parm2 = value;
				}
			}

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x06002108 RID: 8456 RVA: 0x0009F9FC File Offset: 0x0009DBFC
			public bool HasParm2
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x06002109 RID: 8457 RVA: 0x0009FA0C File Offset: 0x0009DC0C
			// (set) Token: 0x0600210A RID: 8458 RVA: 0x0009FA14 File Offset: 0x0009DC14
			public long parm3
			{
				get
				{
					return this._parm3;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._parm3 = value;
				}
			}

			// Token: 0x1700092A RID: 2346
			// (get) Token: 0x0600210B RID: 8459 RVA: 0x0009FA2C File Offset: 0x0009DC2C
			public bool HasParm3
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x0600210C RID: 8460 RVA: 0x0009FA3C File Offset: 0x0009DC3C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.id = this.deserialize.read_string();
						break;
					case 1:
						this.index = this.deserialize.read_integer();
						break;
					case 2:
						this.time = this.deserialize.read_integer();
						break;
					case 3:
						this.parm1 = this.deserialize.read_integer();
						break;
					case 4:
						this.parm2 = this.deserialize.read_integer();
						break;
					case 5:
						this.parm3 = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600210D RID: 8461 RVA: 0x0009FB1C File Offset: 0x0009DD1C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.index, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.time, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.parm1, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.parm2, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.parm3, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B9B RID: 7067
			private static int max_field_count = 6;

			// Token: 0x04001B9C RID: 7068
			private string _id;

			// Token: 0x04001B9D RID: 7069
			private long _index;

			// Token: 0x04001B9E RID: 7070
			private long _time;

			// Token: 0x04001B9F RID: 7071
			private long _parm1;

			// Token: 0x04001BA0 RID: 7072
			private long _parm2;

			// Token: 0x04001BA1 RID: 7073
			private long _parm3;
		}
	}
}
