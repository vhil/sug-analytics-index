namespace Sug.Xdb.Rules.Conditions
{
	using Sitecore.Analytics;
	using Sitecore.Analytics.Automation.Rules.Workflows;
	using Sitecore.Exceptions;
	using Sitecore.Rules;
	using Sitecore.Rules.Conditions;
	using Extensions;

	public class CustomerIdCompares<TRuleContext> : OperatorCondition<TRuleContext> 
		where TRuleContext : RuleContext
	{
		public long CustomerId { get; set; }

		protected override bool Execute(TRuleContext ruleContext)
		{
			var automationContext = ruleContext as AutomationRuleContext;
			var contact = automationContext?.Contact ?? Tracker.Current?.Contact;

			if (contact == null) throw new RequiredObjectIsNullException("Contextual Contact can't be null");

			return this.Compare(contact.GetCustomerFacet().CustomerId);
		}

		protected bool Compare(long customerId)
		{
			switch (this.GetOperator())
			{
				case ConditionOperator.Equal:
					return customerId == this.CustomerId;
				case ConditionOperator.GreaterThanOrEqual:
					return customerId >= this.CustomerId;
				case ConditionOperator.GreaterThan:
					return customerId > this.CustomerId;
				case ConditionOperator.LessThanOrEqual:
					return customerId <= this.CustomerId;
				case ConditionOperator.LessThan:
					return customerId < this.CustomerId;
				case ConditionOperator.NotEqual:
					return customerId != this.CustomerId;
				default:
					return false;
			}
		}
	}
}