using Microsoft.BizTalk.Message.Interop;
using Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Factories;
using Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Helpers;
using Modhul.BizTalk.PipelineComponents.CreateMessageBodyPart;
using Modhul.BizTalk.PipelineComponents.CreateMessageBodyPart.EventLog;
using NUnit.Framework;
using Winterdom.BizTalk.PipelineTesting;
using PipelineFactory = Winterdom.BizTalk.PipelineTesting.PipelineFactory;

namespace Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests
{
    [TestFixture]
    public class CreateMessageBodyPartTests
    {
        #region Constants

        private const string SAMPLE_MESSAGE_RESOURCE = "Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Samples.SampleMessage.xml";

        private const string BODY_PART_NAME = "SampleBodyPart";
        private const string BODY_PART_CONTENT = "<GetItemMarksReturnResponse xmlns=\"http://www.logistix-software.com/\"><ItemMarksReturnData xmlns=\"http://ucles.org.uk/schemas/ILMR\" /></GetItemMarksReturnResponse>";

        #endregion

        #region Test Setup/Teardown Helpers

        [SetUp]
        public void TestSetup()
        {
            EventLogHelpers.CleanupEventLog();
        }

        [TearDown]
        public void TestTeardown()
        {
            EventLogHelpers.CleanupEventLog();
        }

        #endregion

        #region Tests

        [Test]
        public void TestComponentReturnsOriginalMessageWhenBodyPartNamePropertyNotSet()
        {
            CreateMessageBodyPartComp createMessageBodyPartComp = new CreateMessageBodyPartComp();
            createMessageBodyPartComp.BodyPartName = "";
            createMessageBodyPartComp.BodyPartContent = "<SomeContent />";

            ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            pipeline.AddComponent(createMessageBodyPartComp, PipelineStage.Decode);

            IBaseMessage inputMessage = MessageCreationFactory.CreateMessageFromResource(SAMPLE_MESSAGE_RESOURCE);

            MessageCollection outputMessages = pipeline.Execute(inputMessage);

            AssertionHelpers.MessageBodyPartIsNotNull(outputMessages[0]);
            AssertionHelpers.ErrorMessageIsWrittenToEventLog((int)EventLogCategories.ComponentPropertyValidation.BodyPartNamePropertyNotConfigured);
        }

        [Test]
        public void TestComponentReturnsOriginalMessageWhenBodyPartContentPropertyNotSet()
        {
            CreateMessageBodyPartComp createMessageBodyPartComp = new CreateMessageBodyPartComp();
            createMessageBodyPartComp.BodyPartName = "SomeBodyPart";
            createMessageBodyPartComp.BodyPartContent = "";

            ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            pipeline.AddComponent(createMessageBodyPartComp, PipelineStage.Decode);

            IBaseMessage inputMessage = MessageCreationFactory.CreateMessageFromResource(SAMPLE_MESSAGE_RESOURCE);

            MessageCollection outputMessages = pipeline.Execute(inputMessage);

            AssertionHelpers.MessageBodyPartIsNotNull(outputMessages[0]);
            AssertionHelpers.ErrorMessageIsWrittenToEventLog((int)EventLogCategories.ComponentPropertyValidation.BodyPartContentPropertyNotConfigured);
        }

        [Test]
        public void TestBodyPartIsNotAddedToMessageWithExistingBodyPart()
        {
            CreateMessageBodyPartComp createMessageBodyPartComp = new CreateMessageBodyPartComp();
            createMessageBodyPartComp.BodyPartName = "";
            createMessageBodyPartComp.BodyPartContent = "";

            ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            pipeline.AddComponent(createMessageBodyPartComp, PipelineStage.Decode);

            IBaseMessage inputMessage = MessageCreationFactory.CreateMessageFromResource(SAMPLE_MESSAGE_RESOURCE);

            MessageCollection outputMessages = pipeline.Execute(inputMessage);

            AssertionHelpers.MessageBodyPartIsNotNull(outputMessages[0]);
        }

        [Test]
        public void TestBodyPartIsAddedToMessageWithoutBodyPart()
        {
            CreateMessageBodyPartComp createMessageBodyPartComp = new CreateMessageBodyPartComp();
            createMessageBodyPartComp.BodyPartName = BODY_PART_NAME;
            createMessageBodyPartComp.BodyPartContent = BODY_PART_CONTENT;

            ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            pipeline.AddComponent(createMessageBodyPartComp, PipelineStage.Decode);

            IBaseMessage inputMessage = MessageCreationFactory.CreateMessageWithNullBodyPart();

            MessageCollection outputMessages = pipeline.Execute(inputMessage);

            AssertionHelpers.MessageBodyPartIsNotNull(outputMessages[0]);
            Assert.AreEqual(outputMessages[0].BodyPartName, BODY_PART_NAME);
        }

        #endregion
    }
}