using System;
using System.IO;
using Microsoft.BizTalk.Message.Interop;

namespace Modhul.BizTalk.PipelineComponents.CreateMessageBodyPart.MessagePart
{
    internal class MessagePart : IBaseMessagePart
    {
        // Fields
        private readonly Guid partId = Guid.NewGuid();
        private Stream partData;
        private IBasePropertyBag partProperties;

        // Methods

        #region IBaseMessagePart Members

        public Stream GetOriginalDataStream()
        {
            return partData;
        }

        public void GetSize(out ulong size, out bool implemented)
        {
            size = 0L;
            implemented = false;
        }

        // Properties
        public string Charset
        {
            get { return (string) PartProperties.Read("Charset", ""); }
            set { PartProperties.Write("Charset", "", value); }
        }

        public string ContentType
        {
            get { return (string) PartProperties.Read("ContentType", ""); }
            set { PartProperties.Write("ContentType", "", value); }
        }

        public Stream Data
        {
            get { return partData; }
            set { partData = value; }
        }

        public bool IsMutable
        {
            get { return true; }
        }

        public Guid PartID
        {
            get { return partId; }
        }

        public IBasePropertyBag PartProperties
        {
            get
            {
                if (partProperties == null)
                {
                    partProperties = new PropertyBag();
                }
                return partProperties;
            }
            set { partProperties = value; }
        }

        #endregion
    }
}