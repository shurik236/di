using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class AppForm : Form
    {
        public AppForm(VisualizationConfig config)
        {
            ClientSize = new Size(config.Width, config.Height);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag cloud";
        }
    }
}
