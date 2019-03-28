using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace SharpMatterGH
{
    public class SharpMatterGHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "SharpMatterGH";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("77a87092-5c62-49bd-acc7-28d374affee6");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
