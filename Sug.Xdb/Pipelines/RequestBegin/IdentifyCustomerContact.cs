namespace Sug.Xdb.Pipelines.RequestBegin
{
	using System.Web;
	using Sitecore.Mvc.Pipelines.Request.RequestBegin;
	using Sitecore.Analytics;
	using System;

	public class IdentifyCustomerContact : RequestBeginProcessor
	{
		protected readonly IXdbManager XdbManager;

		public IdentifyCustomerContact(IXdbManager xdbManager)
		{
			this.XdbManager = xdbManager;
		}

		public override void Process(RequestBeginArgs args)
		{
			try
			{
				if (!Tracker.IsActive) return;
				if (HttpContext.Current == null || !Sitecore.Context.PageMode.IsNormal) return;

				Guid contactId;
				var c = HttpContext.Current.Request.QueryString["c"];
				if (Guid.TryParse(c, out contactId))
				{
					this.XdbManager.Identify(contactId);
				}
			}
			catch
			{
				// ignored.
			}
		}
	}
}