using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableViewer
{
    public class Content
    {
        // Cache font instead of recreating font objects each time we paint.
        private Font fnt = new Font("Arial", 10);

        private Canvas _canvas;
        public Content(Canvas canvas)
        {
            _canvas = canvas;
        }
        public void Load()
        {
            var fLine = new FLine(new FPoint(300f, 500f), new FPoint(4000f, 3000f));
            _canvas.FLineList.Add(fLine);
            fLine = new FLine(new FPoint(4000f, 3000f), new FPoint(300f, 3000f));
            _canvas.FLineList.Add(fLine);
            fLine = new FLine(new FPoint(300f, 3000f), new FPoint(300f, 500f));
            _canvas.FLineList.Add(fLine);
        }

        public void Draw(Graphics g)
        {
            using (Pen p = new Pen(Color.Black, 1))
            {
                g.DrawString("This is a diagonal line drawn on the control",
                    fnt, Brushes.Blue, new Point(30, 30));

                foreach (var canvasLine in _canvas.FLineList)
                {
                    var viewportLine = _canvas.LineToViewportLine(canvasLine);
                    g.DrawLine(Pens.Black, viewportLine.Start, viewportLine.End);
                }
            }
        }
    }
}
