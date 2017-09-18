namespace Sug.Xdb
{
	using System;
	using Sitecore.Analytics.Tracking;

	public interface IXdbContactFactory
	{
		Contact GetOrCreateContactAndLock(string identifier);
		void FlushToXdb(Contact contact);
		void SaveAndReleaseToXdb(Contact contact);
		Contact GetContactReadonly(string identifier);
		Contact GetContactReadonly(Guid contactId);
	}
}