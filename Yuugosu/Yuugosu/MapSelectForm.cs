using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Yuugosu
{
    public partial class MapSelectForm : Form
    {

        public string mp3Path;
        public string diffPath;
        public string imgPath;

        public MapSelectForm()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void MapSelectForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirs = new DirectoryInfo("Songs");
            foreach (DirectoryInfo dir in dirs.GetDirectories())
            {
                mapListBox.Items.Add(dir.Name);
            }            
        }

        private void mapListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapDiffBox.Items.Clear();
            var x = Path.Combine("Songs", mapListBox.SelectedItem.ToString());
            DirectoryInfo diffs = new DirectoryInfo(x);

            foreach(FileInfo diff in diffs.GetFiles("*.osu"))
            {
                mapDiffBox.Items.Add(diff.Name);
            }
            mp3Path = Path.Combine(x , diffs.GetFiles("*.mp3").ElementAt(0).ToString());
            imgPath = Path.Combine(x, diffs.GetFiles("*.jpg").ElementAt(0).ToString());
        }

        private void mapDiffBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            diffPath = Path.Combine("Songs", mapListBox.SelectedItem.ToString(), mapDiffBox.SelectedItem.ToString());
        }
    }
}
 
