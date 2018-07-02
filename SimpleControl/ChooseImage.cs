using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleControl
{
    public partial class ChooseImage : UserControl
    {
        bool OpenRandom = false;
        bool Ticked = false;
        int ShowCount = 0;
        int ImageSize = 0;
        public ChooseImage()
        {
            InitializeComponent();
        }

        private void barToggleSwitchItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
