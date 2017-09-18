namespace Sug.Xdb.Model
{
	using Sitecore.Analytics.Model.Framework;

	public interface ICustomerFacet : IFacet
	{
		long CustomerId { get; set; }
	}
}