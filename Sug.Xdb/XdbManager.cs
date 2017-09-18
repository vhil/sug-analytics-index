namespace Sug.Xdb
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Sitecore.Analytics;
	using Sitecore.Analytics.Data.DataAccess.MongoDb;
	using Sitecore.Analytics.Model;
	using Mongo;

	public class XdbManager : IXdbManager
	{
		protected readonly IXdbContactFactory ContactFactory;

		public XdbManager(IXdbContactFactory contactFactory)
		{
			this.ContactFactory = contactFactory;
		}

		public bool IdentifyCustomer(long customerId)
		{
			return this.Identify(customerId.ToString());
		}

		public bool Identify(string identifier)
		{
			if (!Tracker.Enabled || !Tracker.IsActive || string.IsNullOrEmpty(identifier)) return false;

			if (Tracker.Current?.Contact?.Identifiers?.Identifier == identifier)
			{
				Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
				return true;
			}

			Tracker.Current?.Session.Identify(identifier);

			if (Tracker.Current?.Contact?.Identifiers?.Identifier == identifier)
			{
				Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
				return true;
			}

			if (Tracker.Current?.Contact?.Identifiers != null)
			{
				Tracker.Current.Contact.Identifiers.Identifier = identifier;
				Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
			}

			return false;
		}

		public bool Identify(Guid contactId)
		{
			return this.Identify(this.ContactFactory.GetContactReadonly(contactId)?.Identifiers?.Identifier);
		}

		public IEnumerable<string> GetAllContactIdentifiersFromMongo()
		{
			var driver = MongoDbDriver.FromConnectionString("analytics");
			var contactData = driver.Contacts.FindAllAs<ContactIdentifiersData>();

			return contactData
				.Where(x => !string.IsNullOrWhiteSpace(x?.Identifiers?.Identifier))
				.Select(x => x.Identifiers.Identifier).ToList();
		}
	}
}