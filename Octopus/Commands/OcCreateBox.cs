using System;
using Rhino;
using Rhino.Commands;

namespace Octopus.Commands
{
    public class OcCreateBox : Command
    {
        static OcCreateBox _instance;
        public OcCreateBox()
        {
            _instance = this;
        }

        ///<summary>The only instance of the OcCreateBox command.</summary>
        public static OcCreateBox Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "OcCreateBox"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.
            return Result.Success;
        }
    }
}