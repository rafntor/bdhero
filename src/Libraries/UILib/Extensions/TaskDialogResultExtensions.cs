// Copyright 2014 Andrew C. Dvorak
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

using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace UILib.Extensions
{
    /// <summary>
    ///     Extension methods for the <see href="TaskDialogResult"/> enum.
    /// </summary>
    public static class TaskDialogResultExtensions
    {
        /// <summary>
        ///     Converts a <see href="TaskDialogResult"/> value to its equivalent <see href="DialogResult"/> value.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static DialogResult ToDialogResult(this TaskDialogResult result)
        {
            if (result == TaskDialogResult.Ok)
                return DialogResult.OK;

            if (result == TaskDialogResult.Yes)
                return DialogResult.Yes;

            if (result == TaskDialogResult.No)
                return DialogResult.No;

            if (result == TaskDialogResult.Retry)
                return DialogResult.Retry;

            if (result == TaskDialogResult.Cancel)
                return DialogResult.Cancel;

            return DialogResult.None;
        }
    }
}