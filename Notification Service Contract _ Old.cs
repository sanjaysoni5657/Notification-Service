1.Notification Service - Umbrella Term

2.Terminology -
Channel, Connector

Agnostic RPC or Specific Rpc???

Model Folder
1. Configuration 
2. Message

// designing starts from creation of defining bottom/end product/services classes structure and it's interface

// Abstract class comes into picture when you have complete clearity in process that this is final minimal set contract 
// that every product or services should follow.

// Dto would be created for request
Channel or NotificationType
{
    SMS,
    EMAIL,
    WhatsAPP
}

Send() {
    // Validation
    // Prepare Client Object
    // Send To Client 
    // Logs Save
}

SMSOptions
{
    Application,
    TraiId,
    Priority
}

EmailOptions
{
    Application
}

WhatsAppOptions
{
    Application
}

// Model
SMSMessage: SMSOptions
{
    From,
    To,
    Message
}

EmailMessage: EmailOptions
{
    Source,
    Destination,
    Cc,
    Bcc,
    Subject,
    Body
}

WhatsAppMessage: WhatsAppOptions
{
    From,
    To,
    Message
}

// Either we can have one interface
INotification
{
    Send(T message);
}

// or we can have abstract class :: it would work as template for all the other client processing.
Abstract NotificationBase
{
    Validate(T message);
    PrepareRequest(T message);
    SaveRequest(T message);
    SaveResponse(T message);
    UpdateDeliveryStatus(T message);

    Send(T message) {
        if (Validate())
        {
            PrepareRequest();
            SaveRequest();
            SendMessage();
            SaveResponse();
            UpdateDeliveryStatus();
        }
    }
}

EmailNotificationHandler: NotificationBase or INotification
{

}

SMSNotificationHandler: NotificationBase or INotification, ISMSClient, ISMSClientProvider, IClientRoller
{

}

WhatsAppNotificationHandler: NotificationBase or INotification
{

}

IClientProvider // if have multiple version then create specific Provider like ISMSClientProvider
{
    GetClient(configuration);
}

/// Either A common client interface can be used to represent all clients 
// [depends on how much generic object we can creat for communication]
IClient // if have multiple version then create specific Client like ISMSClient
{
    SendMessage(message); // set url and send sms or email or whatsapp
    SendBatchMessage();
}

/// Or Specific Client Interface can be created to represent specific client 
// [depends on how much generic object we can creat for communication]
SMSClient: ISMSClient
{
    SendMessage(SMSmessage);
}

WhatsAppClient: IWhatsAppClient {
    SendMessage(WhatsAppMessage);
}

EmailClient: IEmailClient {
    SendMessage(MailMessage);
}

/// Info : Every client would manage it's request creation, reading configuration data and http api calls within itself
// Sms Client
GupShupSMSClient: SMSClient, IClient -
GupShupSMSClient - GupShupPrioritySMSClient : SMSClient, IClient -

24x7SMSClient: SMSClient, IClient -

CarTradeSMS Client - CarTradePrioritySMSClient : SMSClient, IClient -
CarTradeSMSClient : SMSClient, IClient -

// OR 


SMSClientProvider
{
    SMSClientProvider(int priorty, int application);
    List<IClient> clientList = new List<string> { "GupShupSMSClient", "24x7SMSClient" };
protected IClientRoller _clientRoller;
_clientRoller.GetClient(List < IClient > clientList);
}

SMSPriorityClientProvider
{
    List<IClient> clientList = new List<string> { "GupShupPrioritySMSClient", "CarTradePrioritySMSClient" };
protected IClientRoller _clientRoller;
_clientRoller.GetRandomProvider(List < IClient > clientList);
}

// Email Client
AmazonSesEmailClient: EmailClient, IClient
// Whatsapp Client
GupShupWhatsAppClient : WhatsAppClient, IClient


IClientRoller // if have multiple version then create specific provider like ISMSClientRoller
{
    GetRandomServiceProvider();
}

MailService
{
    protected _mailer;
MailService(Mailer mailer);
}

SMSService
{
    protected _sender;
SMSService(Sender sender);
}


SMSService->SMSClients
EmailService->EmailClients
WhatsAppService->WhatsAppClients