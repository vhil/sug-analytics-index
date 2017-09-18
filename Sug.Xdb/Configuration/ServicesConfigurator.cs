namespace Sug.Xdb.Configuration
{
	using Microsoft.Extensions.DependencyInjection;
	using Sitecore.DependencyInjection;

	public class ServicesConfigurator : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IXdbContactFactory, XdbContactFactory>();
			serviceCollection.AddSingleton<IXdbManager, XdbManager>();
		}
	}
}