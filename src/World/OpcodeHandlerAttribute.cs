using System;

namespace Classic.World
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OpcodeHandlerAttribute : Attribute
    {
        public OpcodeHandlerAttribute(Opcode opcode)
        {
            this.Opcode = opcode;
        }

        public Opcode Opcode { get; }
    }
}
