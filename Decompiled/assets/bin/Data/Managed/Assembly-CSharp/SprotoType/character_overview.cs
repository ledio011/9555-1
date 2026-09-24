using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000347 RID: 839
	public class character_overview : SprotoTypeBase
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x0008E4B0 File Offset: 0x0008C6B0
		public character_overview() : base(character_overview.max_field_count)
		{
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0008E4C0 File Offset: 0x0008C6C0
		public character_overview(byte[] buffer) : base(character_overview.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0008E4DC File Offset: 0x0008C6DC
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x0008E4E4 File Offset: 0x0008C6E4
		public long id
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

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0008E4FC File Offset: 0x0008C6FC
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0008E50C File Offset: 0x0008C70C
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0008E514 File Offset: 0x0008C714
		public general general
		{
			get
			{
				return this._general;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._general = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0008E52C File Offset: 0x0008C72C
		public bool HasGeneral
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0008E53C File Offset: 0x0008C73C
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x0008E544 File Offset: 0x0008C744
		public attribute_overview attribute_other
		{
			get
			{
				return this._attribute_other;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._attribute_other = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0008E55C File Offset: 0x0008C75C
		public bool HasAttribute_other
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0008E56C File Offset: 0x0008C76C
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x0008E574 File Offset: 0x0008C774
		public characterVisual visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._visual = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0008E58C File Offset: 0x0008C78C
		public bool HasVisual
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0008E59C File Offset: 0x0008C79C
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0008E5A4 File Offset: 0x0008C7A4
		public long createtime
		{
			get
			{
				return this._createtime;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._createtime = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0008E5BC File Offset: 0x0008C7BC
		public bool HasCreatetime
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0008E5CC File Offset: 0x0008C7CC
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0008E5D4 File Offset: 0x0008C7D4
		public long forbidden
		{
			get
			{
				return this._forbidden;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._forbidden = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0008E5EC File Offset: 0x0008C7EC
		public bool HasForbidden
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0008E5FC File Offset: 0x0008C7FC
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.general = this.deserialize.read_obj<general>();
					break;
				case 2:
					this.attribute_other = this.deserialize.read_obj<attribute_overview>();
					break;
				case 3:
					this.visual = this.deserialize.read_obj<characterVisual>();
					break;
				case 4:
					this.createtime = this.deserialize.read_integer();
					break;
				case 5:
					this.forbidden = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0008E6DC File Offset: 0x0008C8DC
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_obj(this.general, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj(this.attribute_other, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_obj(this.visual, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.createtime, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.forbidden, 5);
			}
			return this.serialize.close();
		}

		// Token: 0x04001947 RID: 6471
		private static int max_field_count = 6;

		// Token: 0x04001948 RID: 6472
		private long _id;

		// Token: 0x04001949 RID: 6473
		private general _general;

		// Token: 0x0400194A RID: 6474
		private attribute_overview _attribute_other;

		// Token: 0x0400194B RID: 6475
		private characterVisual _visual;

		// Token: 0x0400194C RID: 6476
		private long _createtime;

		// Token: 0x0400194D RID: 6477
		private long _forbidden;
	}
}
