using System.IO;
using System.Reflection;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Test.BizTalk.PipelineObjects;
using Winterdom.BizTalk.PipelineTesting;

namespace Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Factories
{
    public static class MessageCreationFactory
    {
        public static IBaseMessage CreateMessageFromResource(string resource)
        {
            Stream stream = LoadStream(resource);
            return (MessageHelper.CreateFromStream(stream));
        }

        public static IBaseMessage CreateMessageFromStream(Stream stream)
        {
            return (MessageHelper.CreateFromStream(stream));
        }

        public static IBaseMessage CreateMessageWithNullBodyPart()
        {
            IBaseMessageFactory factory = new MessageFactory();

            IBaseMessage message = factory.CreateMessage();
            message.Context = factory.CreateMessageContext();
            return (message);
        }

        private static Stream LoadStream(string resource)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(resource);
        }
    }
}