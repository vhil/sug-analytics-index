namespace Sug.Xdb.ContentSearch.Conditions
{
	using System;
	using System.Linq.Expressions;
	using Sitecore.Analytics.Rules.SegmentBuilder;
	using Sitecore.ContentSearch.Analytics.Models;
	using Sitecore.ContentSearch.Rules.Conditions;
	using Sitecore.ContentSearch.SearchTypes;

	public class CustomerIdCompares<TRuleContext> : TypedQueryableOperatorCondition<TRuleContext, IndexedContact>
		where TRuleContext : VisitorRuleContext<IndexedContact>
	{
		public long CustomerId { get; set; }

		protected override Expression<Func<IndexedContact, bool>> GetResultPredicate(TRuleContext ruleContext)
		{
			return this.GetCompareExpression(c => (long)c[(ObjectIndexerKey)IndexableContactFields.CustomerId], this.CustomerId);
		}
	}
}