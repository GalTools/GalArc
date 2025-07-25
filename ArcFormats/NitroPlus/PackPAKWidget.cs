using GalArc.Templates;
using System;
using System.Drawing;

namespace ArcFormats.NitroPlus
{
    public partial class PackPAKWidget : WidgetTemplate
    {
        public static PackPAKWidget Instance { get; } = new PackPAKWidget();

        public NitroPlusOptions Options = new NitroPlusOptions();

        public PackPAKWidget()
        {
            InitializeComponent();
        }

        private void lbOriginalArc_SizeChanged(object sender, EventArgs e)
        {
            this.txtOriginalFilePath.Location = new Point(this.lbOriginalArc.Location.X + this.lbOriginalArc.Width + 6, this.txtOriginalFilePath.Location.Y);
            this.btSelect.Location = new Point(this.txtOriginalFilePath.Location.X + this.txtOriginalFilePath.Width + 6, this.btSelect.Location.Y);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            this.txtOriginalFilePath.Text = ChooseFile() ?? this.txtOriginalFilePath.Text;
            Options.OriginalFilePath = this.txtOriginalFilePath.Text;
        }
    }

    public class NitroPlusOptions : ArcOptions
    {
        public string OriginalFilePath { get; set; }
    }
}
