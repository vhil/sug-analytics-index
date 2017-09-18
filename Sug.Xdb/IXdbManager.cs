namespace Sug.Xdb
{
	using System;
	using System.Collections.Generic;

	public interface IXdbManager
	{
		bool IdentifyCustomer(long customerId);
		bool Identify(string identifier);
		bool Identify(Guid contactId);
		IEnumerable<string> GetAllContactIdentifiersFromMongo();
	}
}
