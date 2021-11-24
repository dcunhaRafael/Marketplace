using System;

namespace InsuranceApi.Domain.Common.Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class PlaceholderAttribute : Attribute {
        private string text;

        public PlaceholderAttribute(string text) {
            this.text = text;
        }

        public virtual string Text {
            get { return text; }
        }
    }
}
