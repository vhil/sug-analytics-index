namespace Sug.Xdb.Extensions
{
	using System;
	using Model;
	using Sitecore.Analytics.Model.Entities;
	using Sitecore.Analytics.Tracking;

	public static class ContactFacetExtensions
	{
		#region built-in

		public static IContactPersonalInfo GetPersonalFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactPersonalInfo>(XdbFacetNames.Personal);
		}

		public static IContactAddresses GetAddressesFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactAddresses>(XdbFacetNames.Addresses);
		}

		public static IContactEmailAddresses GetEmailsFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactEmailAddresses>(XdbFacetNames.Emails);
		}

		public static IContactPhoneNumbers GetPhoneNumbersFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactPhoneNumbers>(XdbFacetNames.PhoneNumbers);
		}

		public static IContactPicture GetPictureFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactPicture>(XdbFacetNames.Picture);
		}

		public static IContactCommunicationProfile GetCommunicationProfileFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactCommunicationProfile>(XdbFacetNames.CommunicationProfile);
		}

		public static IContactPreferences GetPreferencesFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<IContactPreferences>(XdbFacetNames.Preferences);
		}

		#endregion

		public static ICustomerFacet GetCustomerFacet(this Contact contact)
		{
			if (contact == null) throw new ArgumentNullException(nameof(contact));

			return contact.GetFacet<ICustomerFacet>(XdbFacetNames.Customer);
		}
	}
}