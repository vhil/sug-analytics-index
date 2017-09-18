namespace Sug.Xdb
{
	using Sitecore.Analytics.Data;
	using Sitecore.Analytics.Tracking;
	using Sitecore.Configuration;
	using System;
	using System.Threading;
	using Sitecore.Analytics.DataAccess;
	using Sitecore.Analytics.Model;

	public class XdbContactFactory : IXdbContactFactory
	{
		protected readonly ContactRepository ContactRepository;
		protected readonly ContactManager ContactManager;

		public XdbContactFactory()
		{
			this.ContactManager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;
			this.ContactRepository = Factory.CreateObject("tracking/contactRepository", true) as ContactRepository;
		}

		public Contact GetOrCreateContactAndLock(string identifier)
		{
			var contact = this.GetOrCreateContactInXdb(identifier);

			if (contact == null) return null;

			var lockResult = this.ContactManager.TryLoadContact(contact.ContactId);

			switch (lockResult.Status)
			{
				case LockAttemptStatus.Success:
					contact = lockResult.Object;
					contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
					return contact;
				case LockAttemptStatus.AlreadyLocked:
					this.ContactManager.FlushContactToXdb(contact);
					this.ContactManager.SaveAndReleaseContactToXdb(contact.ContactId);
					return this.GetOrCreateContactAndLock(identifier);
				case LockAttemptStatus.NotFound:
					return null;
				default:
					return null;
			}
		}

		public void FlushToXdb(Contact contact)
		{
			this.ContactManager.FlushContactToXdb(contact);
		}

		public void SaveAndReleaseToXdb(Contact contact)
		{
			this.ContactManager.FlushContactToXdb(contact);
			this.ContactManager.SaveAndReleaseContactToXdb(contact.ContactId);
			this.ContactManager.ReleaseContact(contact.ContactId);
		}

		public Contact GetContactReadonly(string identifier)
		{
			return this.ContactManager.LoadContactReadOnly(identifier);
		}

		public Contact GetContactReadonly(Guid contactId)
		{
			return this.ContactManager.LoadContactReadOnly(contactId);
		}

		private Contact GetOrCreateContactInXdb(string identifier)
		{
			var contact = this.ContactRepository.LoadContactReadOnly(identifier);

			if (contact != null)
			{
				return contact;
			}

			contact = this.ContactRepository.CreateContact(Guid.NewGuid());

			if (contact == null) return null;

			contact.Identifiers.Identifier = identifier;

			contact.System.Value = 0;
			contact.System.VisitCount = 0;
			contact.Identifiers.IdentificationLevel = ContactIdentificationLevel.Known;

			var locker = this.CreateLocker();
			var contactSaveOptions = new ContactSaveOptions(true, locker);

			this.ContactRepository.SaveContact(contact, contactSaveOptions);

			return contact;
		}

		private LeaseOwner CreateLocker()
		{
			return new LeaseOwner("Xdb.Contact.Repository." + Thread.CurrentThread.ManagedThreadId, LeaseOwnerType.OutOfRequestWorker);
		}
	}
}