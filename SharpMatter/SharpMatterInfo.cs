using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace SharpMatter
{
    public class SharpMatterInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "SharpMatter";
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
                return new Guid("60893576-4fd5-4724-ab71-55a60d01bc48");
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
