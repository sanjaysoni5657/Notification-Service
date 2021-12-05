namespace Synapse.Business
{
    public class GupShupSMSClient : IClient
    {
        // client configuration
        public bool SendMessage(ISMSOptions options)
        {
            return true;
        }
    }

    public class AWSSesEmailClient : IClient
    {
        public bool SendMessage(IEmailOptions options)
        {
            return true;
        }
    }

    public class GupShupWhatsAppClient : IClient
    {
        public bool SendMessage(IWhatsAppOptions options)
        {
            return true;
        }
    }
}

namespace Synapse.Interface
{
    public class IOptions
    {
        int id { get; set; }
    }
    public class IClient
    {
        bool SendMessage(IOptions options);
    }
}

namespace Synapse.Model
{
    public class ISMSOptions : IOptions
    {

    }

    public class IEmailOptions : IOptions
    {

    }

    public class IWhatsAppOptions : IOptions
    {

    }
}


namespace Synapse.Business.Providers
{
    public class SMSProvider : IProvider
    {
        INotificationRoller _notificationRoller;

        IProvider SendMessage(ISMSRequest configuration);
        // Map To Channel data
        // implement stratrgy and GetClient
        // call Send Message
    }
    public class EmailProvider : IProvider
    {
        public EmailProvider()
        {

        }
        IProvider SendMessage(IEmailRequest configuration);
    }
    public class WhatsAppProvider : IProvider
    {

        IProvider SendMessage(IWhatsAppRequest configuration);
    }
}

namespace Synapse.Entities
{
    public enum Channel
    {
        SMS = 1,
        Email = 2,
        WhatsApp = 3
    }
}


namespace Synapse.Interface
{
    public interface IRequest
    {
        Channel channel { get; set; }
        string To { get; set; }
        string Message { get; set; }
    }
    public interface ISmsRequest : IRequest
    {

    }

    public interface IEmailRequest : IRequest
    {
        string Subject { get; set; }
        List<string> Cc { get; set; }
        List<string> Bcc { get; set; }
    }

    public interface IWhatsAppRequest : IRequest
    {
        bool userConsent { get; set; }
    }

    public abstract class IProvider
    {
        IProvider SendMessage(IRequest request);
    }
}

namespace Synapse.Provider
{
    public class ProviderFactory
    {
        IProvider GetProvider(Channel channel)
        {
            switch (channel)
            {
                case Channel.SMS: return new SMSProvider();
                case Channel.Email: return new EmailProvider();
                case Channel.WhatsApp:
                    return new WhatsAppProvider();
            }
        }
    }
}

public interface INotificationRoller
{
    string RoundRobin(List<string> keys);
    string PercentageDistribution(List<string> keys, int abtest);
    string RandomDistribution(List<string> keys);
}


GetProvider().SendMessage();

