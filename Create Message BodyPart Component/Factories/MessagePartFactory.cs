using Microsoft.BizTalk.Message.Interop;

namespace Modhul.BizTalk.PipelineComponents.CreateMessageBodyPart.Factories
{
    internal static class MessagePartFactory
    {
        internal static IBaseMessagePart CreateMessagePart()
        {
            return (new MessagePart.MessagePart());
        }
    }
}