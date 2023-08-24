using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TimetableViewer
{
    public partial class Viewer : Form
    {
        public Canvas Canvas;
        public Content Content;

        public Viewer()
        {
            // 1. Initialisation starts here
            InitializeComponent();
        }

        private void Viewer_SizeChanged(object sender, EventArgs e)
        {
            // 2. Initialisation continues here
            Canvas = new Canvas(this, 8000f, 6000f, 500f, 400f);
            Content = new Content(Canvas);
            Content.Load();
        }

        private void Viewer_Load(object sender, System.EventArgs e)
        {
            // 3. Initialisation continues here
            MouseWheel += new MouseEventHandler(OnMouseWheel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 4. Initialisation finishes here, the content is visible in the form and waits for mouse interactions.
            base.OnPaint(e);

            using (Graphics g = e.Graphics)
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Content.Draw(g);
            }
        }

        #region Panning
        private bool IsPanning;
        private Point StartMousePosition;
        private Point EndMousePosition;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (! IsPanning)
                {
                    StartMousePosition = e.Location;
                    IsPanning = true;
                }

                if (IsPanning)
                {
;                   EndMousePosition = e.Location;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (IsPanning)
            {
                IsPanning = false;
                Canvas.Pan(StartMousePosition, EndMousePosition);
                Invalidate();
            }
        }
        #endregion

        #region Zooming
        // Respond to the mouse wheel.
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            Canvas.SetScale(e.Delta, this);
            Invalidate();
        }
        #endregion
    }
}
