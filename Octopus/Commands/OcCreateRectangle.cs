using System;
using Rhino;
using Rhino.Commands;

namespace Octopus.Commands
{
    public class OcCreateRectangle : Command
    {
        static OcCreateRectangle _instance;
        public OcCreateRectangle()
        {
            _instance = this;
        }

        ///<summary>The only instance of the OcCreateRectangle command.</summary>
        public static OcCreateRectangle Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "OcCreateRectangle"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.
            return Result.Success;
        }
    }
}