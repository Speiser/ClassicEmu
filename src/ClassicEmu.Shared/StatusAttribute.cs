using System;

namespace ClassicEmu.Shared
{
    public class StatusAttribute : Attribute
    {
        public StatusAttribute(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }
    }
}
