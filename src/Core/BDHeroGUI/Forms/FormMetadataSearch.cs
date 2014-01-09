﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BDHero.JobQueue;

namespace BDHeroGUI.Forms
{
    public partial class FormMetadataSearch : Form
    {
        /// <summary>
        /// Gets the user's input.
        /// </summary>
        public readonly SearchQuery SearchQuery = new SearchQuery();

        /// <summary>
        /// Maximum width to auto-resize.
        /// </summary>
        private int _maxAutoResizeWidth;

        /// <summary>
        /// Determines whether the form should auto-resize itself when the value of the TextBox changes due to user input.
        /// </summary>
        private bool _autoResize = true;

        /// <summary>
        /// Flag set by <see cref="AutoResize"/> to notify <see cref="OnResize"/> to ignore resize events generated by auto-resizing the form.
        /// </summary>
        private bool _ignoreResize;

        /// <summary>
        /// Constructs a new metadata search form window and copies the specified search query into a new SearchQuery object.
        /// </summary>
        /// <param name="searchQuery"></param>
        public FormMetadataSearch(SearchQuery searchQuery)
        {
            InitializeComponent();

            SearchQuery.CopyFrom(searchQuery);

            textBoxSearchQuery.Text = searchQuery.Title;
            textBoxSearchQuery.TextChanged += TextBoxSearchQueryOnTextChanged;

            textBoxYear.Text = searchQuery.Year.HasValue ? searchQuery.Year.ToString() : "";
            textBoxYear.TextChanged += TextBoxYearOnTextChanged;

            Load += OnLoad;
            Resize += OnResize;
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            _maxAutoResizeWidth = Screen.FromControl(this).WorkingArea.Width / 2;

            AutoResize();

            MinimumSize = new Size(Width, Height);
            MaximumSize = new Size(int.MaxValue, Height);
        }

        private void OnResize(object sender, EventArgs eventArgs)
        {
            if (!_autoResize)
                return;

            // User manually resized the form; stop auto-resizing
            if (!_ignoreResize)
                _autoResize = false;

            _ignoreResize = false;
        }

        /// <summary>
        /// Automatically resizes the form to best fit the text in the TextBox.
        /// Respects upper and lower limits to prevent the form from resizing
        /// too small or taking up the entire width of the screen.
        /// </summary>
        private void AutoResize()
        {
            if (!_autoResize)
                return;

            _ignoreResize = true;

            using (Graphics g = CreateGraphics())
            {
                var before = textBoxSearchQuery.Width;
                var text = textBoxSearchQuery.Text + "MM"; // add 2 letters for padding
                var size = g.MeasureString(text, textBoxSearchQuery.Font);
                var after = (int)Math.Ceiling(size.Width);
                var delta = after - before;
                var width = Width + delta;
                Width = Math.Min(width, _maxAutoResizeWidth);
            }
        }

        private void TextBoxSearchQueryOnTextChanged(object sender, EventArgs eventArgs)
        {
            SearchQuery.Title = textBoxSearchQuery.Text;
            AutoResize();
        }

        private void TextBoxYearOnTextChanged(object sender, EventArgs eventArgs)
        {
            if (Regex.IsMatch(textBoxYear.Text, @"^[0-9]{4}$"))
                SearchQuery.Year = int.Parse(textBoxYear.Text);
            else
                SearchQuery.Year = null;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}