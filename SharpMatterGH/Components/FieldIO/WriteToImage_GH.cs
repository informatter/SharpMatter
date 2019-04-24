using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using SharpMatter.SharpField;


namespace SharpMatter.SharpMatterGH.Components.FieldIO
{
    public class WriteToImage_GH: GH_Component
    {
        int counter = 0;





        private imageFormat _imageFormat = imageFormat.jpg;

        public imageFormat ImageFormat
        {
            get { return _imageFormat; }
            set
            {
                _imageFormat = value;
                Message = _imageFormat.ToString();
            }
        }


        


        /// <summary>
        /// Initializes a new instance of the SaveImageSecuence_GH class.
        /// </summary>
        public WriteToImage_GH()
          : base("Write to image", "Write to image",
              "Description",
              "SharpMatter", "Field I/O")
        {
            ImageFormat = imageFormat.jpg;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("reset", "reset", "reset", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("run", "run", "save image secuence iteratively", GH_ParamAccess.item, false);
            pManager.AddGenericParameter("field", "field", "sharp field 2d", GH_ParamAccess.item);
            pManager.AddTextParameter("path", "path", "file path", GH_ParamAccess.item, "C:\\Users\nicol\\Desktop\\Material\\New folder");
            pManager.AddTextParameter("name", "name", "file name", GH_ParamAccess.item, "image");
            // pManager.AddIntegerParameter("format", "format", "image format", GH_ParamAccess.item,0);
            // pManager.AddParameter(_imageformatParam);
            pManager.AddColourParameter("colors", "colors", "list of colors", GH_ParamAccess.list);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool _reset = false;
            bool _run = false;
            SharpField2D<double> _field = new SharpField2D<double>();
            string _path = "";
            string _name = "";
       
            List<Color> _colors = new List<Color>();

            DA.GetData(0, ref _reset);
            DA.GetData(1, ref _run);
            DA.GetData(2, ref _field);
            DA.GetData(3, ref _path);
            DA.GetData(4, ref _name);
            DA.GetDataList(5, _colors);

            if (_run)
            {
                counter++;
                SharpFieldIO.SaveImageSecuence(_field, _path, _name, counter, _imageFormat, _colors);

         
                ExpireSolution(true);
            }

       
            if (_reset) counter = 0;
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
            get { return new Guid("f78e41b3-dee4-4e0d-8c16-fd27e633e090"); }
        }




        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);

            var sub = Menu_AppendItem(menu, "Image format");
            Menu_AppendItem(sub.DropDown, "Jpeg", JpegClicked, true, _imageFormat == imageFormat.jpg);
            Menu_AppendItem(sub.DropDown, "Png", PngClicked, true, _imageFormat == imageFormat.png);

        }


        private void JpegClicked(object sender, EventArgs e)
        {
            ImageFormat = imageFormat.jpg;
            //Params.Input[5] = _imageformatParam;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }


        private void PngClicked(object sender, EventArgs e)
        {
            ImageFormat = imageFormat.png;
            // Params.Input[5] = _imageformatParam;
            Params.OnParametersChanged();
            ExpireSolution(true);
        }


        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes.CustomAttributes(this);
        }


    }
}
