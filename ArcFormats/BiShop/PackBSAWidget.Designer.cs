namespace ArcFormats.BiShop
{
    partial class PackBSAWidget
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackBSAWidget));
            this.combVersion = new System.Windows.Forms.ComboBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // combVersion
            // 
            this.combVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combVersion.FormattingEnabled = true;
            resources.ApplyResources(this.combVersion, "combVersion");
            this.combVersion.Name = "combVersion";
            this.combVersion.SelectedIndexChanged += new System.EventHandler(this.combVersion_SelectedIndexChanged);
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.Name = "lbVersion";
            // 
            // PackBSAOptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combVersion);
            this.Controls.Add(this.lbVersion);
            this.Name = "PackBSAOptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combVersion;
        private System.Windows.Forms.Label lbVersion;
    }
}
