using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200032C RID: 812
	public class car_copy_result
	{
		// Token: 0x0200032D RID: 813
		public class request : SprotoTypeBase
		{
			// Token: 0x0600174B RID: 5963 RVA: 0x0008B7E4 File Offset: 0x000899E4
			public request() : base(car_copy_result.request.max_field_count)
			{
			}

			// Token: 0x0600174C RID: 5964 RVA: 0x0008B7F4 File Offset: 0x000899F4
			public request(byte[] buffer) : base(car_copy_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x0600174E RID: 5966 RVA: 0x0008B810 File Offset: 0x00089A10
			// (set) Token: 0x0600174F RID: 5967 RVA: 0x0008B818 File Offset: 0x00089A18
			public bool win
			{
				get
				{
					return this._win;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._win = value;
				}
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001750 RID: 5968 RVA: 0x0008B830 File Offset: 0x00089A30
			public bool HasWin
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06001751 RID: 5969 RVA: 0x0008B840 File Offset: 0x00089A40
			// (set) Token: 0x06001752 RID: 5970 RVA: 0x0008B848 File Offset: 0x00089A48
			public List<item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._items = value;
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x06001753 RID: 5971 RVA: 0x0008B860 File Offset: 0x00089A60
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x06001754 RID: 5972 RVA: 0x0008B870 File Offset: 0x00089A70
			// (set) Token: 0x06001755 RID: 5973 RVA: 0x0008B878 File Offset: 0x00089A78
			public long rankPos1
			{
				get
				{
					return this._rankPos1;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._rankPos1 = value;
				}
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x06001756 RID: 5974 RVA: 0x0008B890 File Offset: 0x00089A90
			public bool HasRankPos1
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x06001757 RID: 5975 RVA: 0x0008B8A0 File Offset: 0x00089AA0
			// (set) Token: 0x06001758 RID: 5976 RVA: 0x0008B8A8 File Offset: 0x00089AA8
			public long rankPos2
			{
				get
				{
					return this._rankPos2;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._rankPos2 = value;
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06001759 RID: 5977 RVA: 0x0008B8C0 File Offset: 0x00089AC0
			public bool HasRankPos2
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x0600175A RID: 5978 RVA: 0x0008B8D0 File Offset: 0x00089AD0
			// (set) Token: 0x0600175B RID: 5979 RVA: 0x0008B8D8 File Offset: 0x00089AD8
			public long parm
			{
				get
				{
					return this._parm;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._parm = value;
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x0600175C RID: 5980 RVA: 0x0008B8F0 File Offset: 0x00089AF0
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x0600175D RID: 5981 RVA: 0x0008B900 File Offset: 0x00089B00
			// (set) Token: 0x0600175E RID: 5982 RVA: 0x0008B908 File Offset: 0x00089B08
			public long new_record
			{
				get
				{
					return this._new_record;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._new_record = value;
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x0600175F RID: 5983 RVA: 0x0008B920 File Offset: 0x00089B20
			public bool HasNew_record
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x06001760 RID: 5984 RVA: 0x0008B930 File Offset: 0x00089B30
			// (set) Token: 0x06001761 RID: 5985 RVA: 0x0008B938 File Offset: 0x00089B38
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._id = value;
				}
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x06001762 RID: 5986 RVA: 0x0008B950 File Offset: 0x00089B50
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x06001763 RID: 5987 RVA: 0x0008B960 File Offset: 0x00089B60
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.win = this.deserialize.read_boolean();
						break;
					case 1:
						this.items = this.deserialize.read_obj_list<item>();
						break;
					case 2:
						this.rankPos1 = this.deserialize.read_integer();
						break;
					case 3:
						this.rankPos2 = this.deserialize.read_integer();
						break;
					case 4:
						this.parm = this.deserialize.read_integer();
						break;
					case 5:
						this.new_record = this.deserialize.read_integer();
						break;
					case 6:
						this.id = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001764 RID: 5988 RVA: 0x0008BA5C File Offset: 0x00089C5C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.win, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<item>(this.items, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.rankPos1, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.rankPos2, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.parm, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.new_record, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_string(this.id, 6);
				}
				return this.serialize.close();
			}

			// Token: 0x040018DE RID: 6366
			private static int max_field_count = 7;

			// Token: 0x040018DF RID: 6367
			private bool _win;

			// Token: 0x040018E0 RID: 6368
			private List<item> _items;

			// Token: 0x040018E1 RID: 6369
			private long _rankPos1;

			// Token: 0x040018E2 RID: 6370
			private long _rankPos2;

			// Token: 0x040018E3 RID: 6371
			private long _parm;

			// Token: 0x040018E4 RID: 6372
			private long _new_record;

			// Token: 0x040018E5 RID: 6373
			private string _id;
		}
	}
}
