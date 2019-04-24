//using System;
//using System.Collections.Generic;

//using Grasshopper.Kernel;
//using Rhino.Geometry;

//using SharpMatter.SharpMatterGH.Types;

//namespace SharpMatter.SharpMatterGH.Components.Parameters
//{
//    public class Vec3Param : GH_PersistentParam<Vec3_GH>
//    {
//        /// <summary>
//        /// Initializes a new instance of the Vec3_GH class.
//        /// </summary>
//        public Vec3Param()
//           : base("Vec3", "Vec3", "", "SharpMatter", "Parameters")
//        {
//        }


//        /// <inheritdoc />
//        public override GH_Exposure Exposure
//        {
//            get { return GH_Exposure.primary; }
//        }




//        /// <summary>
//        /// Provides an Icon for the component.
//        /// </summary>
//        protected override System.Drawing.Bitmap Icon
//        {
//            get
//            {
//                //You can add image files to your project resources and access them like this:
//                // return Resources.IconForThisComponent;
//                return null;
//            }
//        }

//        /// <summary>
//        /// Gets the unique ID for this component. Do not change this ID after release.
//        /// </summary>
//        public override Guid ComponentGuid
//        {
//            get { return new Guid("95ef6d8f-ab88-415b-8d38-96b6254cebe3"); }
//        }


//        /// <inheritdoc />
//        protected override GH_GetterResult Prompt_Singular(ref Vec3_GH value)
//        {
//            value = new Vec3_GH();
//            return GH_GetterResult.success;
//        }


//        /// <inheritdoc />
//        protected override GH_GetterResult Prompt_Plural(ref List<Vec3_GH> values)
//        {
//            values = new List<Vec3_GH>();
//            return GH_GetterResult.success;
//        }





//    }
//}