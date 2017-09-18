namespace Sug.Xdb.Pipelines.ContactIndexable.LoadFields
{
	using System.Collections.Generic;
	using Sitecore.ContentSearch;
	using Sitecore.ContentSearch.Analytics.Pipelines.ContactIndexableLoadFields;
	using Model;
	using ContentSearch;

	public class LoadCustomerIndexedFields : ContactIndexableLoadFieldsProcessor
	{
		protected override IEnumerable<IIndexableDataField> GetFields(ContactIndexableLoadFieldsPipelineArgs args)
		{
			var fields = new List<IIndexableDataField>();

			if (args?.Contact == null) return fields;

			var customerFacet = args.Contact.GetFacet<ICustomerFacet>(XdbFacetNames.Customer);

			if (customerFacet.CustomerId != 0)
			{
				fields.Add(new IndexableDataField<long>(IndexableContactFields.CustomerId, customerFacet.CustomerId));
			}

			return fields;
		}
	}
}