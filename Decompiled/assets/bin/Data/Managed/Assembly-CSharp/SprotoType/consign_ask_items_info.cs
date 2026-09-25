using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000356 RID: 854
	public class consign_ask_items_info
	{
		// Token: 0x02000357 RID: 855
		public class request : SprotoTypeBase
		{
			// Token: 0x06001954 RID: 6484 RVA: 0x0008FD1C File Offset: 0x0008DF1C
			public request() : base(consign_ask_items_info.request.max_field_count)
			{
			}

			// Token: 0x06001955 RID: 6485 RVA: 0x0008FD2C File Offset: 0x0008DF2C
			public request(byte[] buffer) : base(consign_ask_items_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001957 RID: 6487 RVA: 0x0008FD48 File Offset: 0x0008DF48
			// (set) Token: 0x06001958 RID: 6488 RVA: 0x0008FD50 File Offset: 0x0008DF50
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

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001959 RID: 6489 RVA: 0x0008FD68 File Offset: 0x0008DF68
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x0600195A RID: 6490 RVA: 0x0008FD78 File Offset: 0x0008DF78
			// (set) Token: 0x0600195B RID: 6491 RVA: 0x0008FD80 File Offset: 0x0008DF80
			public long subType
			{
				get
				{
					return this._subType;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._subType = value;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x0600195C RID: 6492 RVA: 0x0008FD98 File Offset: 0x0008DF98
			public bool HasSubType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x0600195D RID: 6493 RVA: 0x0008FDA8 File Offset: 0x0008DFA8
			// (set) Token: 0x0600195E RID: 6494 RVA: 0x0008FDB0 File Offset: 0x0008DFB0
			public long quality
			{
				get
				{
					return this._quality;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._quality = value;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x0600195F RID: 6495 RVA: 0x0008FDC8 File Offset: 0x0008DFC8
			public bool HasQuality
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001960 RID: 6496 RVA: 0x0008FDD8 File Offset: 0x0008DFD8
			// (set) Token: 0x06001961 RID: 6497 RVA: 0x0008FDE0 File Offset: 0x0008DFE0
			public long levelRange
			{
				get
				{
					return this._levelRange;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._levelRange = value;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001962 RID: 6498 RVA: 0x0008FDF8 File Offset: 0x0008DFF8
			public bool HasLevelRange
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06001963 RID: 6499 RVA: 0x0008FE08 File Offset: 0x0008E008
			// (set) Token: 0x06001964 RID: 6500 RVA: 0x0008FE10 File Offset: 0x0008E010
			public bool use
			{
				get
				{
					return this._use;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._use = value;
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001965 RID: 6501 RVA: 0x0008FE28 File Offset: 0x0008E028
			public bool HasUse
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001966 RID: 6502 RVA: 0x0008FE38 File Offset: 0x0008E038
			// (set) Token: 0x06001967 RID: 6503 RVA: 0x0008FE40 File Offset: 0x0008E040
			public long curPage
			{
				get
				{
					return this._curPage;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._curPage = value;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001968 RID: 6504 RVA: 0x0008FE58 File Offset: 0x0008E058
			public bool HasCurPage
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001969 RID: 6505 RVA: 0x0008FE68 File Offset: 0x0008E068
			// (set) Token: 0x0600196A RID: 6506 RVA: 0x0008FE70 File Offset: 0x0008E070
			public long profession
			{
				get
				{
					return this._profession;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._profession = value;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x0600196B RID: 6507 RVA: 0x0008FE88 File Offset: 0x0008E088
			public bool HasProfession
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x0600196C RID: 6508 RVA: 0x0008FE98 File Offset: 0x0008E098
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
						this.subType = this.deserialize.read_integer();
						break;
					case 2:
						this.quality = this.deserialize.read_integer();
						break;
					case 3:
						this.levelRange = this.deserialize.read_integer();
						break;
					case 4:
						this.use = this.deserialize.read_boolean();
						break;
					case 5:
						this.curPage = this.deserialize.read_integer();
						break;
					case 6:
						this.profession = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600196D RID: 6509 RVA: 0x0008FF94 File Offset: 0x0008E194
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.subType, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.quality, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.levelRange, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_boolean(this.use, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_integer(this.curPage, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_integer(this.profession, 6);
				}
				return this.serialize.close();
			}

			// Token: 0x0400197C RID: 6524
			private static int max_field_count = 7;

			// Token: 0x0400197D RID: 6525
			private long _type;

			// Token: 0x0400197E RID: 6526
			private long _subType;

			// Token: 0x0400197F RID: 6527
			private long _quality;

			// Token: 0x04001980 RID: 6528
			private long _levelRange;

			// Token: 0x04001981 RID: 6529
			private bool _use;

			// Token: 0x04001982 RID: 6530
			private long _curPage;

			// Token: 0x04001983 RID: 6531
			private long _profession;
		}
	}
}
