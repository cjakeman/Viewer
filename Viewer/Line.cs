using System.Drawing;

namespace TimetableViewer
{
    public class Line
    {
        public Point Start;
        public Point End;

        public Line()
        {
        }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
