﻿// Copyright 2012-2014 Andrew C. Dvorak
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

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DotNetUtils.FS;
using UILib.Extensions;
using UILib.WinForms.Dialogs.FS;

namespace UILib.WinForms.Controls
{
    /// <summary>
    /// Simple wrapper control that contains a <see href="System.Windows.Forms.TextBox"/> and a "Browse" <see href="Button"/>
    /// to let users select files or folders from standard system dialogs.
    /// </summary>
    public partial class FileTextBox : UserControl
    {
        /// <summary>
        /// Gets or sets the value of the TextBox.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get { return TextBox.Text; }
            set { TextBox.Text = value; }
        }

        /// <summary>
        /// Gets or sets the value of the TextBox.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string SelectedPath
        {
            get { return Environment.ExpandEnvironmentVariables(TextBox.Text); }
            set { TextBox.Text = value; }
        }

        /// <summary>
        /// Gets or sets the label of the "Browse" button.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("Browse...")]
        public string ButtonText
        {
            get { return buttonBrowse.Text; }
            set { buttonBrowse.Text = value; }
        }

        /// <summary>
        /// Gets or sets whether the text box is read only (i.e., selectable but not editable).
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return TextBox.ReadOnly; }
            set
            {
                TextBox.ReadOnly = value;
                buttonBrowse.Enabled = !value;
            }
        }

        /// <summary>
        /// Gets or sets the type of dialog that will be displayed when the user clicks the "Browse" button.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(FileSystemDialogType.OpenFile)]
        public FileSystemDialogType DialogType { get; set; }

        /// <summary>
        /// Gets or sets the text that will be displayed in the dialog's title.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("Select a file:")]
        public string DialogTitle { get; set; }

        /// <summary>
        /// Gets or sets whether the "New folder" button will be visible in the dialog.
        /// Only applies when <see href="DialogType"/> is set to <see href="FileSystemDialogType.OpenDirectory"/>.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        public bool ShowNewFolderButton { get; set; }

        /// <summary>
        /// Gets or sets whether the user can select multiple files or directories.
        /// Only applies when <see href="DialogType"/> is set to <see href="FileSystemDialogType.OpenFile"/> or <see href="FileSystemDialogType.OpenDirectory"/>.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets whether the user should be prompted to overwrite an existing file.
        /// Only applies when <see href="DialogType"/> is set to <see href="FileSystemDialogType.SaveFile"/>.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        public bool OverwritePrompt { get; set; }

        /// <summary>
        /// Gets or sets a list of file extensions to allow the user to open/save.
        /// </summary>
        public FileType[] FileTypes { get; set; }

        /// <summary>
        /// Gets or sets whether the "Browse" file dialog should allow the user to open/save files with any file extension (*.*),
        /// even if the extension is not in the list of <see href="FileTypes"/>.
        /// Only applies when <see href="DialogType"/> is set to <see href="FileSystemDialogType.OpenFile"/> or <see href="FileSystemDialogType.SaveFile"/>.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        public bool AllowAnyExtension { get; set; }

        /// <summary>
        /// Gets or sets the BorderStyle of the TextBox.
        /// </summary>
        public new BorderStyle BorderStyle
        {
            get { return TextBox.BorderStyle; }
            set { TextBox.BorderStyle = value; }
        }

        /// <summary>
        /// Triggered whenever the value of the TextBox changes.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler TextChanged;

        /// <summary>
        /// Triggered when the <see href="SelectedPath"/> changes after the user selects a new path
        /// from the dialog box and presses "Select" (or "Save").
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public event EventHandler SelectedPathChanged;

        protected readonly ITextBox TextBox;

        /// <summary>
        /// Constructs a new FileTextBox component.
        /// </summary>
        public FileTextBox()
            : this(new StandardTextBox())
        {
        }

        /// <summary>
        /// Constructs a new FileTextBox component.
        /// </summary>
        protected FileTextBox(ITextBox textBox)
        {
            InitializeComponent();

            ShowNewFolderButton = true;
            OverwritePrompt = true;
            AllowAnyExtension = true;

            buttonBrowse.Click += ShowDialog;

            TextBox = textBox;
            TextBox.Control.Size = textBoxDummy.Size;
            TextBox.Control.Location = textBoxDummy.Location;
            TextBox.Control.Dock = textBoxDummy.Dock;
            TextBox.Control.Anchor = textBoxDummy.Anchor;
            TextBox.Control.Font = textBoxDummy.Font;
            TextBox.Control.TabIndex = textBoxDummy.TabIndex;

            var parent = textBoxDummy.Parent;
            parent.Controls.Remove(textBoxDummy);
            parent.Controls.Add(TextBox.Control);

            TextBox.TextChanged += (sender, args) => OnTextChanged(args);
            TextBox.PreviewKeyDown += TextBoxOnPreviewKeyDown;
        }

        /// <summary>
        /// Highlights the TextBox by changing its <see href="BorderStyle"/>.
        /// </summary>
        public void Highlight()
        {
            textBoxDummy.Highlight();
        }

        /// <summary>
        /// Removes highlighting from the TextBox by reverting its <see href="BorderStyle"/> back to its original value.
        /// </summary>
        public void UnHighlight()
        {
            textBoxDummy.UnHighlight();
        }

        /// <summary>
        /// Displays the file/folder dialog specified by <see href="DialogType"/>.
        /// </summary>
        public void Browse()
        {
            ShowDialog();
        }

        private void TextBoxOnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Proxy TextBox event to this control
            OnPreviewKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        private void OnSelectedPathChanged(EventArgs e)
        {
            if (SelectedPathChanged != null)
                SelectedPathChanged(this, e);
        }

        private void ShowDialog(object sender = null, EventArgs e = null)
        {
            var dialog = CreateDialog();

            if (!string.IsNullOrEmpty(SelectedPath))
            {
                dialog.SelectedPath = SelectedPath;
            }

            if (dialog.ShowDialog() != DialogResult.OK) return;

            var prevPath = SelectedPath;
            SelectedPath = dialog.SelectedPath;

            if (prevPath != SelectedPath)
                OnSelectedPathChanged(EventArgs.Empty);
        }

        private IFileSystemDialog CreateDialog()
        {
            switch (DialogType)
            {
                case FileSystemDialogType.OpenFile:
                    return new OpenFileDialog2
                        {
                            AllowAnyExtension = AllowAnyExtension,
                            FileTypes = FileTypes,
                            Title = DialogTitle
                        };
                case FileSystemDialogType.SaveFile:
                    return new SaveFileDialog2
                        {
                            AllowAnyExtension = AllowAnyExtension,
                            FileTypes = FileTypes,
                            OverwritePrompt = OverwritePrompt,
                            Title = DialogTitle
                        };
                default:
                    // Vista or higher
                    if (FolderBrowserDialog3.IsPlatformSupported)
                    {
                        return new FolderBrowserDialog3
                               {
                                   Title = DialogTitle,
                                   ShowNewFolderButton = ShowNewFolderButton
                               };
                    }

                    // XP or Mono
                    return new FolderBrowserDialog2
                           {
                               Title = DialogTitle,
                               ShowNewFolderButton = ShowNewFolderButton
                           };
            }
        }
    }
}
