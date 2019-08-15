using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grasshopper.Kernel;
using Rhino.Geometry;

using SharpMatter.SharpMatterGH.Types;

namespace SharpMatter.SharpMatterGH.Components.Parameters
{
    public class SharpColorParam_GH : GH_PersistentParam<SharpColor_GH>, IGH_PreviewObject, IGH_PreviewData
    {
        /// <summary>
        /// Initializes a new instance of the Vec3_GH class.
        /// </summary>
        public SharpColorParam_GH()
           : base("Sharp Color", "Sharp Color", "", "SharpMatter", "Parameters")
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
            get { return new Guid("fdebc9e5-9595-401c-957e-a3902e8cad04"); }
        }


        /// <inheritdoc />
        protected override GH_GetterResult Prompt_Singular(ref SharpColor_GH value)
        {
            value = new SharpColor_GH();
            return GH_GetterResult.success;
        }


        /// <inheritdoc />
        protected override GH_GetterResult Prompt_Plural(ref List<SharpColor_GH> values)
        {
            values = new List<SharpColor_GH>();
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

            switch (args.Document.PreviewMode)
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
