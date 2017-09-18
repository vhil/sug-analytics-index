namespace Sug.Xdb.sitecore
{
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Sitecore.DependencyInjection;
	using Extensions;
	using Sitecore.sitecore.admin;

	public partial class UpdateCustomerIds : AdminPage
	{
		protected IXdbManager XdbManager => ServiceLocator.ServiceProvider.GetService<IXdbManager>();
		protected IXdbContactFactory XdbFactory=> ServiceLocator.ServiceProvider.GetService<IXdbContactFactory>();

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CheckSecurity();
		}

		protected void Run(object sender, EventArgs e)
		{
			var contactIdentifiers = this.XdbManager.GetAllContactIdentifiersFromMongo();

			foreach (var cid in contactIdentifiers)
			{
				long identifier;
				if (long.TryParse(cid, out identifier))
				{
					var contact = this.XdbFactory.GetOrCreateContactAndLock(cid);
					contact.GetCustomerFacet().CustomerId = identifier;
					this.XdbFactory.SaveAndReleaseToXdb(contact);
				}
			}
		}
	}
}