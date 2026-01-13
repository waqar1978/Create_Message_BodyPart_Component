using System;
using System.Collections;
using Microsoft.BizTalk.Message.Interop;

namespace Modhul.BizTalk.PipelineComponents.CreateMessageBodyPart.MessagePart
{
    internal class PropertyBag : IBasePropertyBag, IEnumerable
    {
        // Fields
        private Hashtable properties = new Hashtable();

        public Hashtable Properties
        {
            get { return properties; }
        }

        // Methods

        #region IBasePropertyBag Members

        public object Read(string name, string namesp)
        {
            return properties[GetKey(name, namesp)];
        }

        public object ReadAt(int index, out string name, out string namesp)
        {
            name = null;
            namesp = null;
            int num = 0;
            foreach (string str in properties.Keys)
            {
                if (num++ == index)
                {
                    string[] strArray = str.Split(new char[] {'@'});
                    if (strArray.Length == 0)
                    {
                        throw new InvalidOperationException("No name and namespace are found");
                    }
                    name = strArray[0];
                    if (strArray.Length > 1)
                    {
                        namesp = strArray[1];
                    }
                    return properties[str];
                }
            }
            return null;
        }

        public void Write(string name, string namesp, object obj)
        {
            properties[GetKey(name, namesp)] = obj;
        }

        // Properties
        public uint CountProperties
        {
            get { return (uint) properties.Count; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        #endregion

        public void CloneProperties(Hashtable properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            this.properties = (Hashtable) properties.Clone();
        }

        protected string GetKey(string name, string namesp)
        {
            string str = name;
            if (namesp != null)
            {
                str = str + '@' + namesp;
            }
            return str;
        }
    }
}