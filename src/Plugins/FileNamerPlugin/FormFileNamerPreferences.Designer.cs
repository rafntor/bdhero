// Copyright 2012-2014 Andrew C. Dvorak
//
// This file is part of BDHero.
//
// BDHero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BDHero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BDHero.  If not, see <http://www.gnu.org/licenses/>.

using TextEditor.WinForms;
using UILib.WinForms.Controls;
using UILib.WinForms.Dialogs.FS;

namespace BDHero.Plugin.FileNamer
{
    partial class FormFileNamerPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Video", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Audio", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Subtitles", System.Windows.Forms.HorizontalAlignment.Left);
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.selectableLabelMoviePlaceholders = new UILib.WinForms.Controls.SelectableLabel();
            this.textBoxMovieFileNameExample = new UILib.WinForms.Controls.SelectableLabel();
            this.textBoxMovieDirectoryExample = new UILib.WinForms.Controls.SelectableLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMovieDirectory = new TextEditor.WinForms.SyntaxHighlightingFileTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hyperlinkLabelTVShowReleaseDateFormat = new UILib.WinForms.Controls.HyperlinkLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxTVShowReleaseDateFormat = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxEpisodeNumberFormat = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxSeasonNumberFormat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.selectableLabelTVShowPlaceholders = new UILib.WinForms.Controls.SelectableLabel();
            this.textBoxTVShowFileNameExample = new UILib.WinForms.Controls.SelectableLabel();
            this.textBoxTVShowDirectoryExample = new UILib.WinForms.Controls.SelectableLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxTVShowDirectory = new TextEditor.WinForms.SyntaxHighlightingFileTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxReplaceSpacesWith = new System.Windows.Forms.TextBox();
            this.checkBoxReplaceSpaces = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.listViewCodecNames = new UILib.WinForms.Controls.ListView2();
            this.columnHeaderLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCodec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDefault = new System.Windows.Forms.Button();
            this.buttonRevert = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxMovieFileName = new TextEditor.WinForms.TextEditorControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxTVShowFileName = new TextEditor.WinForms.TextEditorControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(94, 157);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Preview:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Placeholders:";
            // 
            // selectableLabelMoviePlaceholders
            // 
            this.selectableLabelMoviePlaceholders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectableLabelMoviePlaceholders.BackColor = System.Drawing.SystemColors.Window;
            this.selectableLabelMoviePlaceholders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectableLabelMoviePlaceholders.Location = new System.Drawing.Point(176, 9);
            this.selectableLabelMoviePlaceholders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectableLabelMoviePlaceholders.Name = "selectableLabelMoviePlaceholders";
            this.selectableLabelMoviePlaceholders.ReadOnly = true;
            this.selectableLabelMoviePlaceholders.Size = new System.Drawing.Size(1018, 19);
            this.selectableLabelMoviePlaceholders.TabIndex = 0;
            this.selectableLabelMoviePlaceholders.Text = "{volume} {title} {year} {res} {vcodec} {acodec} {channels} {cut} {vlang} {alang}";
            // 
            // textBoxMovieFileNameExample
            // 
            this.textBoxMovieFileNameExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMovieFileNameExample.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMovieFileNameExample.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMovieFileNameExample.Location = new System.Drawing.Point(176, 157);
            this.textBoxMovieFileNameExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMovieFileNameExample.Name = "textBoxMovieFileNameExample";
            this.textBoxMovieFileNameExample.ReadOnly = true;
            this.textBoxMovieFileNameExample.Size = new System.Drawing.Size(1018, 19);
            this.textBoxMovieFileNameExample.TabIndex = 4;
            this.textBoxMovieFileNameExample.Text = "Contact (1999) [1080p] [TrueHD 7.1].mkv";
            // 
            // textBoxMovieDirectoryExample
            // 
            this.textBoxMovieDirectoryExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMovieDirectoryExample.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMovieDirectoryExample.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMovieDirectoryExample.Location = new System.Drawing.Point(176, 85);
            this.textBoxMovieDirectoryExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMovieDirectoryExample.Name = "textBoxMovieDirectoryExample";
            this.textBoxMovieDirectoryExample.ReadOnly = true;
            this.textBoxMovieDirectoryExample.Size = new System.Drawing.Size(1014, 19);
            this.textBoxMovieDirectoryExample.TabIndex = 2;
            this.textBoxMovieDirectoryExample.Text = "C:\\temp\\CONTACT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(94, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Preview:";
            // 
            // textBoxMovieDirectory
            // 
            this.textBoxMovieDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMovieDirectory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textBoxMovieDirectory.DialogTitle = null;
            this.textBoxMovieDirectory.DialogType = UILib.WinForms.Dialogs.FS.FileSystemDialogType.OpenDirectory;
            this.textBoxMovieDirectory.FileTypes = null;
            this.textBoxMovieDirectory.Location = new System.Drawing.Point(176, 38);
            this.textBoxMovieDirectory.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.textBoxMovieDirectory.Name = "textBoxMovieDirectory";
            this.textBoxMovieDirectory.OverwritePrompt = false;
            this.textBoxMovieDirectory.SelectedPath = "C:\\Users\\tore\\AppData\\Local\\Temp\\%volume%";
            this.textBoxMovieDirectory.Size = new System.Drawing.Size(1018, 37);
            this.textBoxMovieDirectory.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1149, 120);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = ".mkv";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "File name template:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory template:";
            // 
            // hyperlinkLabelTVShowReleaseDateFormat
            // 
            this.hyperlinkLabelTVShowReleaseDateFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hyperlinkLabelTVShowReleaseDateFormat.DisabledColor = System.Drawing.Color.Empty;
            this.hyperlinkLabelTVShowReleaseDateFormat.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.hyperlinkLabelTVShowReleaseDateFormat.HoverColor = System.Drawing.Color.Empty;
            this.hyperlinkLabelTVShowReleaseDateFormat.Location = new System.Drawing.Point(360, 277);
            this.hyperlinkLabelTVShowReleaseDateFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.hyperlinkLabelTVShowReleaseDateFormat.Name = "hyperlinkLabelTVShowReleaseDateFormat";
            this.hyperlinkLabelTVShowReleaseDateFormat.RegularColor = System.Drawing.Color.Empty;
            this.hyperlinkLabelTVShowReleaseDateFormat.Size = new System.Drawing.Size(194, 21);
            this.hyperlinkLabelTVShowReleaseDateFormat.TabIndex = 8;
            this.hyperlinkLabelTVShowReleaseDateFormat.Text = "Formatting help on MSDN";
            this.hyperlinkLabelTVShowReleaseDateFormat.Url = "http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.100).aspx";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 277);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(158, 20);
            this.label15.TabIndex = 15;
            this.label15.Text = "Release date format:";
            // 
            // textBoxTVShowReleaseDateFormat
            // 
            this.textBoxTVShowReleaseDateFormat.Location = new System.Drawing.Point(176, 272);
            this.textBoxTVShowReleaseDateFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTVShowReleaseDateFormat.Name = "textBoxTVShowReleaseDateFormat";
            this.textBoxTVShowReleaseDateFormat.Size = new System.Drawing.Size(174, 26);
            this.textBoxTVShowReleaseDateFormat.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(32, 234);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 20);
            this.label14.TabIndex = 13;
            this.label14.Text = "Episode # format:";
            // 
            // comboBoxEpisodeNumberFormat
            // 
            this.comboBoxEpisodeNumberFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEpisodeNumberFormat.FormattingEnabled = true;
            this.comboBoxEpisodeNumberFormat.Items.AddRange(new object[] {
            "1",
            "01"});
            this.comboBoxEpisodeNumberFormat.Location = new System.Drawing.Point(176, 229);
            this.comboBoxEpisodeNumberFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxEpisodeNumberFormat.Name = "comboBoxEpisodeNumberFormat";
            this.comboBoxEpisodeNumberFormat.Size = new System.Drawing.Size(62, 28);
            this.comboBoxEpisodeNumberFormat.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 192);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 20);
            this.label13.TabIndex = 11;
            this.label13.Text = "Season # format:";
            // 
            // comboBoxSeasonNumberFormat
            // 
            this.comboBoxSeasonNumberFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeasonNumberFormat.FormattingEnabled = true;
            this.comboBoxSeasonNumberFormat.Items.AddRange(new object[] {
            "1",
            "01"});
            this.comboBoxSeasonNumberFormat.Location = new System.Drawing.Point(176, 188);
            this.comboBoxSeasonNumberFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxSeasonNumberFormat.Name = "comboBoxSeasonNumberFormat";
            this.comboBoxSeasonNumberFormat.Size = new System.Drawing.Size(62, 28);
            this.comboBoxSeasonNumberFormat.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(94, 157);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Preview:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Placeholders:";
            // 
            // selectableLabelTVShowPlaceholders
            // 
            this.selectableLabelTVShowPlaceholders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectableLabelTVShowPlaceholders.BackColor = System.Drawing.SystemColors.Window;
            this.selectableLabelTVShowPlaceholders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectableLabelTVShowPlaceholders.Location = new System.Drawing.Point(176, 9);
            this.selectableLabelTVShowPlaceholders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectableLabelTVShowPlaceholders.Name = "selectableLabelTVShowPlaceholders";
            this.selectableLabelTVShowPlaceholders.ReadOnly = true;
            this.selectableLabelTVShowPlaceholders.Size = new System.Drawing.Size(1018, 19);
            this.selectableLabelTVShowPlaceholders.TabIndex = 0;
            this.selectableLabelTVShowPlaceholders.Text = "{volume} {title} {date} {res} {vcodec} {acodec} {channels} {cut} {vlang} {alang} " +
    "{episodetitle} {season} {episode}";
            // 
            // textBoxTVShowFileNameExample
            // 
            this.textBoxTVShowFileNameExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTVShowFileNameExample.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTVShowFileNameExample.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTVShowFileNameExample.Location = new System.Drawing.Point(176, 157);
            this.textBoxTVShowFileNameExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTVShowFileNameExample.Name = "textBoxTVShowFileNameExample";
            this.textBoxTVShowFileNameExample.ReadOnly = true;
            this.textBoxTVShowFileNameExample.Size = new System.Drawing.Size(1018, 19);
            this.textBoxTVShowFileNameExample.TabIndex = 4;
            this.textBoxTVShowFileNameExample.Text = "s01e01 - \"My First Day\" (2001-10-02) [1080p] [DTS-HD MA 5.1].mkv";
            // 
            // textBoxTVShowDirectoryExample
            // 
            this.textBoxTVShowDirectoryExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTVShowDirectoryExample.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTVShowDirectoryExample.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTVShowDirectoryExample.Location = new System.Drawing.Point(176, 85);
            this.textBoxTVShowDirectoryExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTVShowDirectoryExample.Name = "textBoxTVShowDirectoryExample";
            this.textBoxTVShowDirectoryExample.ReadOnly = true;
            this.textBoxTVShowDirectoryExample.Size = new System.Drawing.Size(1014, 19);
            this.textBoxTVShowDirectoryExample.TabIndex = 2;
            this.textBoxTVShowDirectoryExample.Text = "C:\\temp\\SCRUBS";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(94, 85);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Preview:";
            // 
            // textBoxTVShowDirectory
            // 
            this.textBoxTVShowDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTVShowDirectory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textBoxTVShowDirectory.DialogTitle = null;
            this.textBoxTVShowDirectory.DialogType = UILib.WinForms.Dialogs.FS.FileSystemDialogType.OpenDirectory;
            this.textBoxTVShowDirectory.FileTypes = null;
            this.textBoxTVShowDirectory.Location = new System.Drawing.Point(176, 38);
            this.textBoxTVShowDirectory.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.textBoxTVShowDirectory.Name = "textBoxTVShowDirectory";
            this.textBoxTVShowDirectory.OverwritePrompt = false;
            this.textBoxTVShowDirectory.SelectedPath = "C:\\Users\\tore\\AppData\\Local\\Temp\\%volume%";
            this.textBoxTVShowDirectory.Size = new System.Drawing.Size(1018, 37);
            this.textBoxTVShowDirectory.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1149, 120);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = ".mkv";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 120);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 20);
            this.label11.TabIndex = 3;
            this.label11.Text = "File name template:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 49);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 20);
            this.label12.TabIndex = 1;
            this.label12.Text = "Directory template:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.textBoxReplaceSpacesWith);
            this.groupBox3.Controls.Add(this.checkBoxReplaceSpaces);
            this.groupBox3.Location = new System.Drawing.Point(18, 415);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(1215, 77);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General";
            // 
            // textBoxReplaceSpacesWith
            // 
            this.textBoxReplaceSpacesWith.Location = new System.Drawing.Point(298, 28);
            this.textBoxReplaceSpacesWith.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxReplaceSpacesWith.MaxLength = 3;
            this.textBoxReplaceSpacesWith.Name = "textBoxReplaceSpacesWith";
            this.textBoxReplaceSpacesWith.Size = new System.Drawing.Size(58, 26);
            this.textBoxReplaceSpacesWith.TabIndex = 1;
            // 
            // checkBoxReplaceSpaces
            // 
            this.checkBoxReplaceSpaces.AutoSize = true;
            this.checkBoxReplaceSpaces.Location = new System.Drawing.Point(18, 31);
            this.checkBoxReplaceSpaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxReplaceSpaces.Name = "checkBoxReplaceSpaces";
            this.checkBoxReplaceSpaces.Size = new System.Drawing.Size(265, 24);
            this.checkBoxReplaceSpaces.TabIndex = 0;
            this.checkBoxReplaceSpaces.Text = "&Replace spaces in filename with:";
            this.checkBoxReplaceSpaces.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(1120, 502);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 35);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(999, 502);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 35);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // listViewCodecNames
            // 
            this.listViewCodecNames.AllowColumnReorder = true;
            this.listViewCodecNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCodecNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLabel,
            this.columnHeaderCodec,
            this.columnHeaderNumber});
            this.listViewCodecNames.FullRowSelect = true;
            this.listViewCodecNames.GridLines = true;
            listViewGroup4.Header = "Video";
            listViewGroup4.Name = "listViewGroupVideo";
            listViewGroup5.Header = "Audio";
            listViewGroup5.Name = "listViewGroupAudio";
            listViewGroup6.Header = "Subtitles";
            listViewGroup6.Name = "listViewGroupSubtitles";
            this.listViewCodecNames.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
            this.listViewCodecNames.HideSelection = false;
            this.listViewCodecNames.LabelEdit = true;
            this.listViewCodecNames.Location = new System.Drawing.Point(9, 9);
            this.listViewCodecNames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewCodecNames.MultiSelect = false;
            this.listViewCodecNames.Name = "listViewCodecNames";
            this.listViewCodecNames.ShowItemToolTips = true;
            this.listViewCodecNames.Size = new System.Drawing.Size(1183, 327);
            this.listViewCodecNames.TabIndex = 0;
            this.listViewCodecNames.UseCompatibleStateImageBehavior = false;
            this.listViewCodecNames.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderLabel
            // 
            this.columnHeaderLabel.DisplayIndex = 2;
            this.columnHeaderLabel.Text = "Label";
            this.columnHeaderLabel.Width = 1057;
            // 
            // columnHeaderCodec
            // 
            this.columnHeaderCodec.Text = "Codec";
            // 
            // columnHeaderNumber
            // 
            this.columnHeaderNumber.DisplayIndex = 0;
            this.columnHeaderNumber.Text = "#";
            this.columnHeaderNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonDefault
            // 
            this.buttonDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDefault.Location = new System.Drawing.Point(140, 502);
            this.buttonDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(112, 35);
            this.buttonDefault.TabIndex = 3;
            this.buttonDefault.Text = "&Default";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // buttonRevert
            // 
            this.buttonRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRevert.Location = new System.Drawing.Point(18, 502);
            this.buttonRevert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRevert.Name = "buttonRevert";
            this.buttonRevert.Size = new System.Drawing.Size(112, 35);
            this.buttonRevert.TabIndex = 2;
            this.buttonRevert.Text = "Re&vert";
            this.buttonRevert.UseVisualStyleBackColor = true;
            this.buttonRevert.Click += new System.EventHandler(this.buttonRevert_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(18, 18);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1215, 388);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxMovieFileName);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.selectableLabelMoviePlaceholders);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxMovieFileNameExample);
            this.tabPage1.Controls.Add(this.textBoxMovieDirectoryExample);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxMovieDirectory);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1207, 355);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Movies";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxMovieFileName
            // 
            this.textBoxMovieFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMovieFileName.ConvertTabsToSpaces = false;
            this.textBoxMovieFileName.CutCopyWholeLine = false;
            this.textBoxMovieFileName.Location = new System.Drawing.Point(176, 114);
            this.textBoxMovieFileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMovieFileName.Multiline = false;
            this.textBoxMovieFileName.Name = "textBoxMovieFileName";
            this.textBoxMovieFileName.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxMovieFileName.ShowColumnRuler = false;
            this.textBoxMovieFileName.ShowLineNumbers = false;
            this.textBoxMovieFileName.ShowWhiteSpace = false;
            this.textBoxMovieFileName.Size = new System.Drawing.Size(964, 22);
            this.textBoxMovieFileName.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxTVShowFileName);
            this.tabPage2.Controls.Add(this.hyperlinkLabelTVShowReleaseDateFormat);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.selectableLabelTVShowPlaceholders);
            this.tabPage2.Controls.Add(this.textBoxTVShowReleaseDateFormat);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.comboBoxEpisodeNumberFormat);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.comboBoxSeasonNumberFormat);
            this.tabPage2.Controls.Add(this.textBoxTVShowDirectory);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.textBoxTVShowDirectoryExample);
            this.tabPage2.Controls.Add(this.textBoxTVShowFileNameExample);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(1207, 355);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TV Shows";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxTVShowFileName
            // 
            this.textBoxTVShowFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTVShowFileName.ConvertTabsToSpaces = false;
            this.textBoxTVShowFileName.CutCopyWholeLine = false;
            this.textBoxTVShowFileName.Location = new System.Drawing.Point(176, 114);
            this.textBoxTVShowFileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxTVShowFileName.Multiline = false;
            this.textBoxTVShowFileName.Name = "textBoxTVShowFileName";
            this.textBoxTVShowFileName.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxTVShowFileName.ShowColumnRuler = false;
            this.textBoxTVShowFileName.ShowLineNumbers = false;
            this.textBoxTVShowFileName.ShowWhiteSpace = false;
            this.textBoxTVShowFileName.Size = new System.Drawing.Size(964, 22);
            this.textBoxTVShowFileName.TabIndex = 3;
            this.textBoxTVShowFileName.Text = "s{season}e{episode} - {title} ({date}) [{res}] [{acodec} {channels}]";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewCodecNames);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Size = new System.Drawing.Size(1207, 355);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Codecs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // FormFileNamerPreferences
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1251, 555);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonRevert);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1264, 585);
            this.Name = "FormFileNamerPreferences";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BDHero File Namer Preferences";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SyntaxHighlightingFileTextBox textBoxMovieDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private SelectableLabel textBoxMovieFileNameExample;
        private SelectableLabel textBoxMovieDirectoryExample;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private SelectableLabel selectableLabelMoviePlaceholders;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private SelectableLabel selectableLabelTVShowPlaceholders;
        private SelectableLabel textBoxTVShowFileNameExample;
        private SelectableLabel textBoxTVShowDirectoryExample;
        private System.Windows.Forms.Label label9;
        private SyntaxHighlightingFileTextBox textBoxTVShowDirectory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxEpisodeNumberFormat;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxSeasonNumberFormat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxReplaceSpacesWith;
        private System.Windows.Forms.CheckBox checkBoxReplaceSpaces;
        private ListView2 listViewCodecNames;
        private System.Windows.Forms.ColumnHeader columnHeaderLabel;
        private System.Windows.Forms.ColumnHeader columnHeaderCodec;
        private System.Windows.Forms.ColumnHeader columnHeaderNumber;
        private HyperlinkLabel hyperlinkLabelTVShowReleaseDateFormat;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxTVShowReleaseDateFormat;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.Button buttonRevert;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private TextEditor.WinForms.TextEditorControl textBoxMovieFileName;
        private TextEditor.WinForms.TextEditorControl textBoxTVShowFileName;
    }
}