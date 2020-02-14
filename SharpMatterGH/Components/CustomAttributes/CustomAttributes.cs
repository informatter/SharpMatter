using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grasshopper;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.GUI.Canvas;
using System.Drawing;


namespace SharpMatter.SharpMatterGH.Components.CustomAttributes
{
    public class CustomAttributes : GH_ComponentAttributes
    {
       

        Color transparent = Color.FromArgb(0, 0, 0,0);
       // Color greenOpacity = Color.FromArgb(150, 38, 189, 0);
        public CustomAttributes(IGH_Component component)
          : base(component)
        { }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            

            if (channel == GH_CanvasChannel.Objects)
            {
               
                // Cache the existing style.
                GH_PaletteStyle style = GH_Skin.palette_normal_standard;
                GH_PaletteStyle styleSelected = GH_Skin.palette_normal_selected;

                // Swap out palette for normal, unselected components

                GH_Skin.palette_normal_standard = new GH_PaletteStyle(transparent, Color.Teal, Color.Black);

                // Swap out palette for normal, selected components
               // GH_Skin.palette_normal_selected = new GH_PaletteStyle(greenOpacity, Color.Teal, Color.Black);

               

                    

               
          


                base.Render(canvas, graphics, channel);

                // Put the original style back.
                GH_Skin.palette_normal_standard = style;
               // GH_Skin.palette_normal_selected = styleSelected;




            }

            else
                base.Render(canvas, graphics, channel);
        }
    }
}
