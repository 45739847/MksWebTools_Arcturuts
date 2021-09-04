using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MksDecode_WebTools___Arcturus
{
    public partial class SelectProxyType : Form
    {
        public static string returnType { get; set; }
        public SelectProxyType()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSpecAny2_Click(object sender, EventArgs e)
        {

        }

        private void openKey_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(box_select.ToString())) { returnType = box_select.SelectedItem.ToString(); }
            this.Close();
        }
        public string returnSelected()
        {
            return returnType;
        }
    }
}
