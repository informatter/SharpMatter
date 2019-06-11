using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using SharpMatter.SharpMatterGH.Types;

namespace SharpMatter.SharpMatterGH.Components.Parameters
{
    public class Vec3Param_GH : GH_PersistentParam<Vec3_GH>, IGH_PreviewObject , IGH_PreviewData
    {
        /// <summary>
        /// Initializes a new instance of the Vec3_GH class.
        /// </summary>
        public Vec3Param_GH()
           : base("Vec3d", "Vec3d", "", "SharpMatter", "Parameters")
        {
        }


        /// <inheritdoc />
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }




        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("95ef6d8f-ab88-415b-8d38-96b6254cebe3"); }
        }


        /// <inheritdoc />
        protected override GH_GetterResult Prompt_Singular(ref Vec3_GH value)
        {
            value = new Vec3_GH();
            return GH_GetterResult.success;
        }


        /// <inheritdoc />
        protected override GH_GetterResult Prompt_Plural(ref List<Vec3_GH> values)
        {
            values = new List<Vec3_GH>();
            return GH_GetterResult.success;
        }

        public BoundingBox ClippingBox
        {
            get { return Preview_ComputeClippingBox(); }
        }

        bool _hidden;
        public bool Hidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }

        public bool IsPreviewCapable
        {
            get { return true; }
        }

      

        public void DrawViewportWires(IGH_PreviewArgs args)
        {
            
            switch(args.Document.PreviewMode)
            {
                case GH_PreviewMode.Wireframe:
                    Preview_DrawWires(args);
                   
                    break;

                case GH_PreviewMode.Shaded:
                    Preview_DrawWires(args);
                    break;
                    
            }
        }

        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            if (args.Document.PreviewMode == GH_PreviewMode.Shaded && args.Display.SupportsShading)
            {
                Preview_DrawMeshes(args);
            }
        }

        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            throw new NotImplementedException();
        }

        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            throw new NotImplementedException();
        }

        //public void DrawViewportWires(GH_PreviewWireArgs args)
        //{

        //    args.Pipeline.DrawPoint((Point3d)this);
        //}



        //public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        //{
        //    throw new NotImplementedException();
        //}


    }
}