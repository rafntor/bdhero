﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UILib.Extensions;

namespace UILib.WinForms.Controls
{
    /// <summary>
    ///     More robust <kbd>TAB</kbd> key support for the <see href="StatusStrip"/> class.
    ///     Allows child controls to be focused and iterated over using the <kbd>TAB</kbd> key.
    /// </summary>
    public class SelectableStatusStrip : StatusStrip
    {
        private const string DummyName = "DUMMY";

        /// <summary>
        ///     Gets or sets whether the user can tab <b><i>into</i></b> this status strip from outside.
        ///     Defaults to <c>true</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <b>NOTE:</b> This property <i>only</i> applies to child <see href="ToolStripControlHost"/> items;
        ///         other item types (e.g., <see href="ToolStripSplitButton"/>) are not supported due to limitations
        ///         in the .NET framework.
        ///     </para>
        ///     <para>
        ///         Regardless of this property's value, the user will <b>always</b> be able to tab <b><i>within</i></b>
        ///         this control (provided there are at least two focusable child controls).
        ///     </para>
        /// </remarks>
        [Browsable(true)]
        [Description("Gets or sets whether the user can tab into this status strip's hosted child controls from outside.  " +
                     "Regardless of this property's value, the user will always be able to tab between hosted controls " +
                     "within the status strip as long as TabStop is true.")]
        [DefaultValue(true)]
        // ReSharper disable once MemberCanBePrivate.Global
        public bool CanTabInto { get; set; }

        private ToolStripTextBox _dummy1;
        private ToolStripTextBox _dummy2;
        private ToolStripTextBox _dummy3;
        private ToolStripTextBox _dummy4;

        private readonly bool _initialized;

        /// <summary>
        ///     Constructs a new <see href="SelectableStatusStrip"/> instance.
        /// </summary>
        public SelectableStatusStrip()
        {
            TabStop = true;
            CanTabInto = true;

            PrependDummies();
            AppendDummies();

            _initialized = true;
        }

        #region Dummy management

        #region Append/Prepend

        private void PrependDummies()
        {
            // Remove existing dummies
            if (_dummy1 != null) { base.Items.Remove(_dummy1); }
            if (_dummy2 != null) { base.Items.Remove(_dummy2); }

            // Get all other items
            var otherItems = ItemsOfType<ToolStripItem>();

            // Clear all other items
            base.Items.Clear();

            // Create new dummies
            _dummy1 = CreateDummyTextBox(FocusFirstItem);
            _dummy2 = CreateDummyTextBox(FocusPrev);

            // Add new dummies
            base.Items.Add(_dummy1);
            base.Items.Add(_dummy2);

            // Add all other items back to the list
            base.Items.AddRange(otherItems);
        }

        private void AppendDummies()
        {
            // Remove existing dummies
            if (_dummy3 != null) { base.Items.Remove(_dummy3); }
            if (_dummy4 != null) { base.Items.Remove(_dummy4); }

            // Create new dummies
            _dummy3 = CreateDummyTextBox(FocusNext);
            _dummy4 = CreateDummyTextBox(FocusLastItem);

            var allItems = ItemsOfType<ToolStripItem>();
            var displayedItems = DisplayedItemsOfType<ToolStripItem>();

            // Insert dummies after the last *displayed* item.
            // This is necessary because .NET focuses the last *displayed* control
            // when SHIFT+TAB'ing *into* the StatusStrip from outside,
            // However, TAB'ing *within* the StatusStrip behaves differently:
            // *all* child controls can be focused, even if they are not displayed.
            if (displayedItems.Length < allItems.Length)
            {
                var insertIndex = displayedItems.Length + 1;
                base.Items.Insert(insertIndex, _dummy3);
                base.Items.Insert(insertIndex + 1, _dummy4);
            }
            // All items are displayed; add dummies to the end of the list
            else
            {
                base.Items.Add(_dummy3);
                base.Items.Add(_dummy4);
            }
        }

        #endregion

        #region Event handlers

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            if (!_initialized)
                return;

            if (IsDummy(e.Item))
                return;

            base.OnItemAdded(e);

            // We need to remove the existing dummies and create new ones to prevent the tab index from getting out of order.
            // Otherwise, the old dummies will be tabbed to BEFORE the new non-dummy items that were just added.
            AppendDummies();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AppendDummies();
        }

        #endregion

        #endregion

        #region Focus logic

        private void FocusPrev()
        {
            Parent.SelectNextControl(this, false, true, true, true);
        }

        private void FocusNext()
        {
            Parent.SelectNextControl(this, true, true, true, true);
        }

        private void FocusFirstItem()
        {
            if (!CanTabInto)
            {
                // Skip over this StatusStrip and focus the next sibling control
                FocusNext();
                return;
            }
            FocusItem(EndPoint.First);
        }

        private void FocusLastItem()
        {
            if (!CanTabInto)
            {
                // Skip over this StatusStrip and focus the previous sibling control
                FocusPrev();
                return;
            }
            FocusItem(EndPoint.Last);
        }

        private void FocusItem(EndPoint endPoint)
        {
            // Check if any control hosts have focusable child (descendant) controls
            var hosts = ItemsOfType<ToolStripControlHost>();
            var controls = hosts.Select(host => host.Control)
                                .OrderBy(control => control.TabIndex)
                                .SelectMany(control =>
                                            control.Descendants()
                                                   .Where(IsFocusable)
                                                   .OrderBy(descendant =>
                                                            descendant.TabIndex))
                                .ToArray();
            if (controls.Any())
            {
                if (endPoint == EndPoint.First)
                    controls.First().Select();
                else
                    controls.Last().Select();
                return;
            }

            // If the status strip doesn't contain anything that can receive focus, move focus to the status strip's nearest sibling.
            if (endPoint == EndPoint.First)
                FocusNext();
            else
                FocusPrev();
        }

        #endregion

        #region Utilities

        #region Method invocation

        /// <summary>
        ///     Executes the specified delegate on the thread that owns the control's underlying window handle.
        ///     Allows the caller to pass a lambda expression; the base <see href="Control.Invoke(System.Delegate)"/>
        ///     method requires an explicit delegate.
        /// </summary>
        /// <param name="action">
        ///     An action to be called in the control's thread context.
        /// </param>
        private void Invoke(Action action)
        {
            base.Invoke(action);
        }

        #endregion

        #region Dummy creation

        private ToolStripTextBox CreateDummyTextBox(Action onFocus)
        {
            var dummy = new ToolStripTextBox
                        {
                            Size = new Size(0, 0),
                            BorderStyle = BorderStyle.None,
                            Padding = new Padding(0),
                            Margin = new Padding(0),
                            Name = DummyName,
                            AccessibleRole = AccessibleRole.None
                        };

            dummy.Control.Name = DummyName;
            dummy.Control.TabStop = true;
            dummy.GotFocus += (sender, args) => Invoke(onFocus);

            return dummy;
        }

        #endregion

        #region Dummy detection

        private static bool IsDummy(ToolStripItem item)
        {
            return item.Name == DummyName;
        }

        private static bool IsDummy(Control control)
        {
            return control.Name == DummyName;
        }

        #endregion

        #region IsFocusable

        private static bool IsFocusable(Control control)
        {
            return !control.IsDisposed
                   && control.Visible
                   && control.CanSelect
                   && !IsDummy(control)
                ;
        }

        #endregion

        #region Item getters

        private T[] ItemsOfType<T>()
        {
            return Items.OfType<T>().ToArray();
        }

        private T[] DisplayedItemsOfType<T>()
        {
            return DisplayedItems.OfType<T>().ToArray();
        }

        #endregion

        #endregion

        #region Enums

        private enum EndPoint
        {
            First, Last
        }

        #endregion
    }
}
