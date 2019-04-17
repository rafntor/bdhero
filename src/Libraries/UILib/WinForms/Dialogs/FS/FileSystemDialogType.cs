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

namespace UILib.WinForms.Dialogs.FS
{
    /// <summary>
    ///     Enum containing the available <see href="IFileSystemDialog"/> implementations.
    /// </summary>
    public enum FileSystemDialogType
    {
        /// <summary>
        ///     Open a file using the <see href="OpenFileDialog2"/> class.
        /// </summary>
        OpenFile,

        /// <summary>
        ///     Save a file using the <see href="SaveFileDialog2"/> class.
        /// </summary>
        SaveFile,

        /// <summary>
        ///     Open a folder using the <see href="FolderBrowserDialog2"/> or <see href="FolderBrowserDialog3"/> class
        ///     (depending on the operating system).
        /// </summary>
        OpenDirectory
    }
}