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
using System.Linq;
using System.Windows.Forms;
using BDHero.BDROM;
using I18N;
using UILib.Extensions;

namespace BDHeroGUI.Forms
{
    public partial class FormTrackFilter : Form
    {
        private readonly TrackFilter _filter;
        private readonly Language[] _languages; 

        public FormTrackFilter(TrackFilter filter)
        {
            _filter = filter;
            _languages = Language.AllLanguages.OrderBy(language => language.ISO_639_2).ToArray();

            InitializeComponent();

            Load += OnLoad;
            Shown += OnShown;
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            var values = Enum.GetValues(typeof(TrackType)).OfType<object>().ToArray();
            checkedListBoxTypes.Items.AddRange(values);
            this.EnableSelectAll();
        }

        private void OnShown(object sender, EventArgs eventArgs)
        {
            comboBoxPreferredLanguage.Items.AddRange(_languages.Select(language => language.UIDisplayName as object).ToArray());
            var curLangIndex = Array.IndexOf(_languages, _filter.PreferredLanguage);
            if (curLangIndex > -1)
            {
                comboBoxPreferredLanguage.SelectedIndex = curLangIndex;
            }

            var i = 0;
            var trackTypes = checkedListBoxTypes.Items.OfType<TrackType>().ToArray();
            foreach (var trackType in trackTypes)
            {
                checkedListBoxTypes.SetItemChecked(i, _filter.TrackTypes.Contains(trackType));
                i++;
            }

            checkBoxHideHidden.Checked = _filter.HideHiddenTracks;
            checkBoxHideUnsupportedCodecs.Checked = _filter.HideUnsupportedCodecs;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _filter.PreferredLanguage = _languages[comboBoxPreferredLanguage.SelectedIndex];
            _filter.TrackTypes = checkedListBoxTypes.CheckedItems.OfType<TrackType>().ToList();

            _filter.HideHiddenTracks = checkBoxHideHidden.Checked;
            _filter.HideUnsupportedCodecs = checkBoxHideUnsupportedCodecs.Checked;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
