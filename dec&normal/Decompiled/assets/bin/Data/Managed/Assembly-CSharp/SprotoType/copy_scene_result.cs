using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000363 RID: 867
	public class copy_scene_result
	{
		// Token: 0x02000364 RID: 868
		public class request : SprotoTypeBase
		{
			// Token: 0x060019C2 RID: 6594 RVA: 0x00090B04 File Offset: 0x0008ED04
			public request() : base(copy_scene_result.request.max_field_count)
			{
			}

			// Token: 0x060019C3 RID: 6595 RVA: 0x00090B14 File Offset: 0x0008ED14
			public request(byte[] buffer) : base(copy_scene_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00090B34 File Offset: 0x0008ED34
			// (set) Token: 0x060019C6 RID: 6598 RVA: 0x00090B3C File Offset: 0x0008ED3C
			public long subType
			{
				get
				{
					return this._subType;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._subType = value;
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00090B54 File Offset: 0x0008ED54
			public bool HasSubType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00090B64 File Offset: 0x0008ED64
			// (set) Token: 0x060019C9 RID: 6601 RVA: 0x00090B6C File Offset: 0x0008ED6C
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x060019CA RID: 6602 RVA: 0x00090B84 File Offset: 0x0008ED84
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x060019CB RID: 6603 RVA: 0x00090B94 File Offset: 0x0008ED94
			// (set) Token: 0x060019CC RID: 6604 RVA: 0x00090B9C File Offset: 0x0008ED9C
			public bool win
			{
				get
				{
					return this._win;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._win = value;
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x060019CD RID: 6605 RVA: 0x00090BB4 File Offset: 0x0008EDB4
			public bool HasWin
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x060019CE RID: 6606 RVA: 0x00090BC4 File Offset: 0x0008EDC4
			// (set) Token: 0x060019CF RID: 6607 RVA: 0x00090BCC File Offset: 0x0008EDCC
			public long gradeFlag
			{
				get
				{
					return this._gradeFlag;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._gradeFlag = value;
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00090BE4 File Offset: 0x0008EDE4
			public bool HasGradeFlag
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00090BF4 File Offset: 0x0008EDF4
			// (set) Token: 0x060019D2 RID: 6610 RVA: 0x00090BFC File Offset: 0x0008EDFC
			public long grade
			{
				get
				{
					return this._grade;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._grade = value;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00090C14 File Offset: 0x0008EE14
			public bool HasGrade
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00090C24 File Offset: 0x0008EE24
			// (set) Token: 0x060019D5 RID: 6613 RVA: 0x00090C2C File Offset: 0x0008EE2C
			public List<item> items
			{
				get
				{
					return this._items;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._items = value;
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00090C44 File Offset: 0x0008EE44
			public bool HasItems
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00090C54 File Offset: 0x0008EE54
			// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00090C5C File Offset: 0x0008EE5C
			public long swipe
			{
				get
				{
					return this._swipe;
				}
				set
				{
					this.has_field.set_field(6, true);
					this._swipe = value;
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00090C74 File Offset: 0x0008EE74
			public bool HasSwipe
			{
				get
				{
					return this.has_field.has_field(6);
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x060019DA RID: 6618 RVA: 0x00090C84 File Offset: 0x0008EE84
			// (set) Token: 0x060019DB RID: 6619 RVA: 0x00090C8C File Offset: 0x0008EE8C
			public long parm
			{
				get
				{
					return this._parm;
				}
				set
				{
					this.has_field.set_field(7, true);
					this._parm = value;
				}
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x060019DC RID: 6620 RVA: 0x00090CA4 File Offset: 0x0008EEA4
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(7);
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x060019DD RID: 6621 RVA: 0x00090CB4 File Offset: 0x0008EEB4
			// (set) Token: 0x060019DE RID: 6622 RVA: 0x00090CBC File Offset: 0x0008EEBC
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(8, true);
					this._type = value;
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x060019DF RID: 6623 RVA: 0x00090CD4 File Offset: 0x0008EED4
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(8);
				}
			}

			// Token: 0x060019E0 RID: 6624 RVA: 0x00090CE4 File Offset: 0x0008EEE4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.subType = this.deserialize.read_integer();
						break;
					case 1:
						this.id = this.deserialize.read_string();
						break;
					case 2:
						this.win = this.deserialize.read_boolean();
						break;
					case 3:
						this.gradeFlag = this.deserialize.read_integer();
						break;
					case 4:
						this.grade = this.deserialize.read_integer();
						break;
					case 5:
						this.items = this.deserialize.read_obj_list<item>();
						break;
					case 6:
						this.swipe = this.deserialize.read_integer();
						break;
					case 7:
						this.parm = this.deserialize.read_integer();
						break;
					case 8:
						this.type = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060019E1 RID: 6625 RVA: 0x00090E14 File Offset: 0x0008F014
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.subType, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.id, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.win, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.gradeFlag, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.grade, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_obj<item>(this.items, 5);
				}
				if (this.has_field.has_field(6))
				{
					this.serialize.write_integer(this.swipe, 6);
				}
				if (this.has_field.has_field(7))
				{
					this.serialize.write_integer(this.parm, 7);
				}
				if (this.has_field.has_field(8))
				{
					this.serialize.write_integer(this.type, 8);
				}
				return this.serialize.close();
			}

			// Token: 0x0400199A RID: 6554
			private static int max_field_count = 9;

			// Token: 0x0400199B RID: 6555
			private long _subType;

			// Token: 0x0400199C RID: 6556
			private string _id;

			// Token: 0x0400199D RID: 6557
			private bool _win;

			// Token: 0x0400199E RID: 6558
			private long _gradeFlag;

			// Token: 0x0400199F RID: 6559
			private long _grade;

			// Token: 0x040019A0 RID: 6560
			private List<item> _items;

			// Token: 0x040019A1 RID: 6561
			private long _swipe;

			// Token: 0x040019A2 RID: 6562
			private long _parm;

			// Token: 0x040019A3 RID: 6563
			private long _type;
		}
	}
}
