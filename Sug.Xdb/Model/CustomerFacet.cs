namespace Sug.Xdb.Model
{
	using System;
	using Sitecore.Analytics.Model.Framework;

	[Serializable]
	public class CustomerFacet : Facet, ICustomerFacet
	{
		public CustomerFacet()
		{
			this.EnsureAttribute<long>(nameof(this.CustomerId));
		}

		public long CustomerId
		{
			get { return this.GetAttribute<long>(nameof(this.CustomerId)); }
			set { this.SetAttribute(nameof(this.CustomerId), value); }
		}
	}
}