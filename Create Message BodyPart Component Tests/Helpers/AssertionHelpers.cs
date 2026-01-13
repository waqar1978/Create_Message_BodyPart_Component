using Microsoft.BizTalk.Message.Interop;
using NUnit.Framework;

namespace Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Helpers
{
    public static class AssertionHelpers
    {
        public static void MessageBodyPartIsNotNull(IBaseMessage message)
        {
            Assert.IsTrue(message.BodyPart != null);
        }

        public static void ErrorMessageIsWrittenToEventLog(int eventLogId)
        {
            Assert.IsTrue(EventLogHelpers.CheckForErrorEvent(eventLogId));
        }
    }
}
