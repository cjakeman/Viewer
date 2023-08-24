using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TimetableViewer
{
    public class Canvas
    {
        public Form Form;

        // pixels per X dimension
        public double PixelsPX { get { return Form.Width / WindowMapping.Width; } }
        // pixels per Y dimension
        public double PixelsPY { get { return Form.Height / WindowMapping.Height; } }

        // Specifies the region of the Canvas mapped to the form's window
        public class Mapping
        {
            public double OffsetX;
            public double OffsetY;
            public double Width;
            public double Height;
            public double CentreX { get { return OffsetX + Width / 2; } }
            public double CentreY { get { return OffsetY + Height / 2; } }
        }

        public Mapping WindowMapping;

        public Canvas(Form form, double width, double height, double offsetX, double offsetY)
        {
            Form = form;
            WindowMapping = new Mapping();
            WindowMapping.OffsetX = offsetX;
            WindowMapping.OffsetY = offsetY;
            // Offsets allow for the height of the title bar
            WindowMapping.Width = width;
            WindowMapping.Height = height - 20;
        }

        public void Pan(Point start, Point end)
        {
            WindowMapping.OffsetX -= (end.X - start.X) / PixelsPX;
            WindowMapping.OffsetY -= (end.Y - start.Y) / PixelsPY;
        }

        public List<FLine> FLineList = new List<FLine>();

        public void SetScale(int mouseClicks, Form form)
        {
            // The aim is to adjust the Offset and Width to keep the CentreX|Y unchanged,
            // so the zoom is always about the centre of the window (and, by intention, not the mouse pointer).

            // The amount by which we adjust scale per wheel click.
            const double scale_per_delta = 0.10f / 120;

            var enlargement = 1.0f + (mouseClicks * scale_per_delta);

            var oldCentreX = WindowMapping.CentreX;
            WindowMapping.Width *= enlargement;
            WindowMapping.OffsetX = oldCentreX - (WindowMapping.Width / 2);

            var oldCentreY = WindowMapping.CentreY;
            WindowMapping.Height *= enlargement;
            WindowMapping.OffsetY = oldCentreY - (WindowMapping.Height / 2);
        }
        public Line LineToViewportLine(FLine fLine)
        {
            var viewportLine = new Line();
            viewportLine.Start.X = (int)Math.Round((fLine.Start.X - WindowMapping.OffsetX) * PixelsPX);
            viewportLine.Start.Y = (int)Math.Round((fLine.Start.Y - WindowMapping.OffsetY) * PixelsPY);
            viewportLine.End.X = (int)Math.Round((fLine.End.X - WindowMapping.OffsetX) * PixelsPX);
            viewportLine.End.Y = (int)Math.Round((fLine.End.Y - WindowMapping.OffsetY) * PixelsPY);

            return viewportLine;
        }
    }
}
