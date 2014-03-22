﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DotNetUtils.Extensions;
using NativeAPI.Win.UXTheme;
using UILib.Extensions;

namespace TextEditor.WinForms
{
    [DefaultProperty("Text")]
    [DefaultEvent("TextChanged")]
    public class TextEditorControl : Control
    {
        /// <summary>
        ///     Gets the underlying editor for the control.
        /// </summary>
        [Browsable(false)]
        public ITextEditor Editor { get; private set; }

        private bool _isMouseOver;
        private Padding _borderPadding;

        public TextEditorControl()
        {
            Editor = TextEditorFactory.CreateMultiLineTextEditor();
            Editor.Multiline = true;
            Editor.Control.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
            Controls.Add(Editor.Control);

            SetStyle(ControlStyles.Selectable, false);

            BindEvents();

            // Default values
            BorderStyle = BorderStyle.Fixed3D;
            StandardContextMenu = true;
        }

        #region Event binding

        private void BindEvents()
        {
            Editor.TextChanged += (s, e) => OnTextChanged(e);
            Editor.FontSizeChanged += (s, e) => OnFontChanged(e);
            Editor.MultilineChanged += (s, e) => OnMultilineChanged(e);

            HandleCreated += (s, e) => BindMouseEvents();
            HandleCreated += (s, e) => BindFocusEvents();
            HandleCreated += (s, e) => AdjustRects(true);

            PaddingChanged += (s, e) => OnPaddingChanged();
            BorderStyleChanged += (s, e) => AdjustBorderPadding();

            // Prevent border artifacts
            Resize += (s, e) => Invalidate();

            PaintBackground += (s, e) => PaintBorder(e);
        }

        private void BindMouseEvents()
        {
            MouseEnter += ControlOnMouseEnter;
            MouseLeave += ControlOnMouseLeave;

            this.Descendants().ForEach(control => control.MouseEnter += ControlOnMouseEnter);
            this.Descendants().ForEach(control => control.MouseLeave += ControlOnMouseLeave);
        }

        private void BindFocusEvents()
        {
            GotFocus += OnGotFocus;
            LostFocus += OnLostFocus;

            this.Descendants().ForEach(control => control.GotFocus += OnGotFocus);
            this.Descendants().ForEach(control => control.LostFocus += OnLostFocus);
        }

        #endregion

        #region Browsable properties and events

#if true

        #region Context menu

        /// <summary>
        ///     Gets or sets whether a standard context menu (cut, copy, paste, etc.) is available.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether the user can enter multiple lines of text.")]
        [DefaultValue(true)]
        public bool StandardContextMenu
        {
            get { return base.ContextMenuStrip != null; }
            set
            {
                if (value == StandardContextMenu)
                    return;

                ContextMenuStrip = value ? CreateContextMenuStrip() : null;
            }
        }

        private static ToolStripSeparator CreateContextMenuSeparator()
        {
            return new ToolStripSeparator();
        }

        private void OptionsMenuItemOnClick(Action action)
        {
            Console.WriteLine("OptionsMenuItemOnClick()");
            action();
        }

        private void OptionsDropDownOnClosing(object sender, ToolStripDropDownClosingEventArgs args)
        {
            Console.WriteLine("OptionsDropDownOnClosing({0}) - args = {1}", sender, args);
            if (args.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                args.Cancel = true;
            }
        }

        private ContextMenuStrip CreateContextMenuStrip()
        {
            #region Edit commands

            var undo = new ToolStripMenuItem("&Undo", null, (sender, args) => Editor.Undo());
            var redo = new ToolStripMenuItem("&Redo", null, (sender, args) => Editor.Redo());

            var cut = new ToolStripMenuItem("Cu&t", null, (sender, args) => Editor.Cut());
            var copy = new ToolStripMenuItem("&Copy", null, (sender, args) => Editor.Copy());
            var paste = new ToolStripMenuItem("&Paste", null, (sender, args) => Editor.Paste());
            var delete = new ToolStripMenuItem("&Delete", null, (sender, args) => Editor.Delete());

            var selectAll = new ToolStripMenuItem("Select &All", null, (sender, args) => Editor.SelectAll());

            #endregion

            #region Options

            var optionsDivider = CreateContextMenuSeparator();
            var options = new ToolStripMenuItem("&Options");
            options.DropDown.Closing += OptionsDropDownOnClosing;

            var showLineNumbers = new ToolStripMenuItem("Show &Line Numbers");
            showLineNumbers.Click += (sender, args) => OptionsMenuItemOnClick(() => Editor.Options.ShowLineNumbers = !Editor.Options.ShowLineNumbers);

            var showWhiteSpace = new ToolStripMenuItem("Show &Whitespace");
            showWhiteSpace.Click += (sender, args) => OptionsMenuItemOnClick(() => Editor.Options.ShowSpaces = Editor.Options.ShowTabs = !(Editor.Options.ShowSpaces && Editor.Options.ShowTabs));

            var wordWrap = new ToolStripMenuItem("Word &Wrap");
            wordWrap.Click += (sender, args) => OptionsMenuItemOnClick(() => Editor.Options.WordWrap = !Editor.Options.WordWrap);

            #region Ruler

            var ruler = new ToolStripMenuItem("&Ruler");
            ruler.DropDown.Closing += OptionsDropDownOnClosing;

            var none             = CreateRulerMenuItem("None");
            var seventy          = CreateRulerMenuItem(70);
            var seventyEight     = CreateRulerMenuItem(78);
            var eighty           = CreateRulerMenuItem(80);
            var oneHundred       = CreateRulerMenuItem(100);
            var oneHundredTwenty = CreateRulerMenuItem(120);

            ruler.DropDownItems.AddRange(new ToolStripItem[]
                                         {
                                             none,
                                             new ToolStripSeparator(),
                                             seventy,
                                             seventyEight,
                                             eighty,
                                             oneHundred,
                                             oneHundredTwenty,
                                         });

            #endregion

            options.DropDownItems.AddRange(new ToolStripItem[]
                                           {
                                               showLineNumbers,
                                               showWhiteSpace,
                                               CreateContextMenuSeparator(),
                                               wordWrap,
                                               ruler,
                                           });

            #endregion

            #region Menu creation

            var menu = new ContextMenuStrip();
            menu.Items.AddRange(new ToolStripItem[]
                                {
                                    undo,
                                    redo,
                                    CreateContextMenuSeparator(),
                                    cut,
                                    copy,
                                    paste,
                                    delete,
                                    CreateContextMenuSeparator(),
                                    selectAll,
                                    optionsDivider,
                                    options,
                                });

            menu.Opened += delegate
                           {
                               undo.Enabled    = Editor.CanUndo   && !Editor.ReadOnly;
                               redo.Enabled    = Editor.CanRedo   && !Editor.ReadOnly;
                               cut.Enabled     = Editor.CanCut    && !Editor.ReadOnly;
                               copy.Enabled    = Editor.CanCopy;
                               paste.Enabled   = Editor.CanPaste  && !Editor.ReadOnly;
                               delete.Enabled  = Editor.CanDelete && !Editor.ReadOnly;

                               optionsDivider.Visible = Editor.Multiline;
                               options.Visible        = Editor.Multiline;
                                   showLineNumbers.Checked = Editor.Options.ShowLineNumbers;
                                   showWhiteSpace.Checked  = Editor.Options.ShowSpaces && Editor.Options.ShowTabs;
                                   wordWrap.Checked        = Editor.Options.WordWrap;
                                       none.Checked             = !Editor.Options.ShowColumnRuler;
                                       seventy.Checked          = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 70;
                                       seventyEight.Checked     = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 78;
                                       eighty.Checked           = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 80;
                                       oneHundred.Checked       = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 100;
                                       oneHundredTwenty.Checked = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 120;
                           };

            menu.RenderMode = ToolStripRenderMode.Professional;

            #endregion

            return menu;
        }

        private ToolStripMenuItem CreateRulerMenuItem(string text)
        {
            var item = new ToolStripMenuItem(string.Format("&{0}", text));
            item.Click += (sender, args) => OptionsMenuItemOnClick(() => SetRulerColumn(0));
            return item;
        }

        private ToolStripMenuItem CreateRulerMenuItem(int col)
        {
            var item = new ToolStripMenuItem(string.Format("&{0}", col));
            item.Click += (sender, args) => OptionsMenuItemOnClick(() => SetRulerColumn(col));
            return item;
        }

        private void SetRulerColumn(int col)
        {
            if (col > 0)
            {
                Editor.Options.ShowColumnRuler = true;
                Editor.Options.ColumnRulerPosition = col;
            }
            else
            {
                Editor.Options.ShowColumnRuler = false;
            }
        }

        #endregion

#else

        #region Context menu

        /// <summary>
        ///     Gets or sets whether a standard context menu (cut, copy, paste, etc.) is available.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether the user can enter multiple lines of text.")]
        [DefaultValue(true)]
        public bool StandardContextMenu
        {
            get { return base.ContextMenu != null; }
            set
            {
                if (value == StandardContextMenu)
                    return;

                ContextMenu = value ? CreateContextMenu() : null;
            }
        }

        private ContextMenu CreateContextMenu()
        {
            #region Edit commands

            var undo = new MenuItem("&Undo", (sender, args) => Editor.Undo());
            var redo = new MenuItem("&Redo", (sender, args) => Editor.Redo());

            var cut = new MenuItem("Cu&t", (sender, args) => Editor.Cut());
            var copy = new MenuItem("&Copy", (sender, args) => Editor.Copy());
            var paste = new MenuItem("&Paste", (sender, args) => Editor.Paste());
            var delete = new MenuItem("&Delete", (sender, args) => Editor.Delete());

            var selectAll = new MenuItem("Select &All", (sender, args) => Editor.SelectAll());

            #endregion

            #region Options

            var optionsDivider = new MenuItem("-");
            var options = new MenuItem("&Options");

            var showLineNumbers = new MenuItem("Show &Line Numbers");
            showLineNumbers.Click += (sender, args) => Editor.Options.ShowLineNumbers = !Editor.Options.ShowLineNumbers;

            var showWhiteSpace = new MenuItem("Show &Whitespace");
            showWhiteSpace.Click += (sender, args) => Editor.Options.ShowSpaces = Editor.Options.ShowTabs = !(Editor.Options.ShowSpaces && Editor.Options.ShowTabs);

            var wordWrap = new MenuItem("Word &Wrap");
            wordWrap.Click += (sender, args) => Editor.Options.WordWrap = !Editor.Options.WordWrap;

            #region Ruler

            var ruler = new MenuItem("&Ruler");

            var none             = CreateRulerMenuItem("None");
            var seventy          = CreateRulerMenuItem(70);
            var seventyEight     = CreateRulerMenuItem(78);
            var eighty           = CreateRulerMenuItem(80);
            var oneHundred       = CreateRulerMenuItem(100);
            var oneHundredTwenty = CreateRulerMenuItem(120);

            ruler.MenuItems.AddRange(new[]
                                     {
                                         none,
                                         new MenuItem("-"),
                                         seventy,
                                         seventyEight,
                                         eighty,
                                         oneHundred,
                                         oneHundredTwenty,
                                     });

            #endregion

            options.MenuItems.AddRange(new[]
                                       {
                                           showLineNumbers,
                                           showWhiteSpace,
                                           new MenuItem("-"),
                                           wordWrap,
                                           ruler,
                                       });

            #endregion

            #region Menu creation

            var menu = new ContextMenu(new[]
                                       {
                                           undo,
                                           redo,
                                           new MenuItem("-"),
                                           cut,
                                           copy,
                                           paste,
                                           delete,
                                           new MenuItem("-"),
                                           selectAll,
                                           optionsDivider,
                                           options,
                                       });

            menu.Popup += delegate
                          {
                              undo.Enabled    = Editor.CanUndo   && !Editor.ReadOnly;
                              redo.Enabled    = Editor.CanRedo   && !Editor.ReadOnly;
                              cut.Enabled     = Editor.CanCut    && !Editor.ReadOnly;
                              copy.Enabled    = Editor.CanCopy;
                              paste.Enabled   = Editor.CanPaste  && !Editor.ReadOnly;
                              delete.Enabled  = Editor.CanDelete && !Editor.ReadOnly;

                              optionsDivider.Visible = Editor.Multiline;
                              options.Visible        = Editor.Multiline;
                                  showLineNumbers.Checked = Editor.Options.ShowLineNumbers;
                                  showWhiteSpace.Checked  = Editor.Options.ShowSpaces && Editor.Options.ShowTabs;
                                  wordWrap.Checked        = Editor.Options.WordWrap;
                                      none.Checked             = !Editor.Options.ShowColumnRuler;
                                      seventy.Checked          = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 70;
                                      seventyEight.Checked     = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 78;
                                      eighty.Checked           = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 80;
                                      oneHundred.Checked       = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 100;
                                      oneHundredTwenty.Checked = Editor.Options.ShowColumnRuler && Editor.Options.ColumnRulerPosition == 120;
                          };

            #endregion

            return menu;
        }

        private MenuItem CreateRulerMenuItem(string text)
        {
            var item = new MenuItem(string.Format("&{0}", text)) { RadioCheck = true };
            item.Click += (sender, args) => SetRulerColumn(0);
            return item;
        }

        private MenuItem CreateRulerMenuItem(int col)
        {
            var item = new MenuItem(string.Format("&{0}", col)) { RadioCheck = true };
            item.Click += (sender, args) => SetRulerColumn(col);
            return item;
        }

        private void SetRulerColumn(int col)
        {
            if (col > 0)
            {
                Editor.Options.ShowColumnRuler = true;
                Editor.Options.ColumnRulerPosition = col;
            }
            else
            {
                Editor.Options.ShowColumnRuler = false;
            }
        }

        #endregion

#endif

        #region Text property

        [Browsable(true)]
        [DefaultValue("")]
        public override string Text
        {
            get { return Editor.Text; }
            set { Editor.Text = value; }
        }

        #endregion

        #region BorderStyle property + event

        /// <summary>
        ///     Gets or sets the border style for the control.
        /// </summary>
        [Browsable(true)]
        [Description("Indicates the border style for the control.")]
        [DefaultValue(BorderStyle.Fixed3D)]
        public BorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                if (value == BorderStyle)
                    return;

                _borderStyle = value;

                OnBorderStyleChanged();
                Invalidate();
            }
        }

        private BorderStyle _borderStyle;

        /// <summary>
        ///     Triggered whenever the value of the <see cref="BorderStyle"/> property changes.
        /// </summary>
        [Browsable(true)]
        [Description("Triggered whenever the value of the BorderStyle property changes.")]
        public event EventHandler BorderStyleChanged;

        protected virtual void OnBorderStyleChanged(EventArgs args = null)
        {
            if (BorderStyleChanged != null)
                BorderStyleChanged(this, args ?? EventArgs.Empty);
        }

        #endregion

        #region Options

        /// <summary>
        ///     Gets or sets whether the user can change the contents of the editor.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether the user can change the contents of the editor.")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return Editor.ReadOnly; }
            set { Editor.ReadOnly = value; }
        }

        /// <summary>
        ///     Gets or sets whether line numbers are displayed in the gutter.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether line numbers are displayed in the gutter.")]
        [DefaultValue(true)]
        public bool ShowLineNumbers
        {
            get { return Editor.Options.ShowLineNumbers; }
            set { Editor.Options.ShowLineNumbers = value; }
        }

        /// <summary>
        ///     Gets or sets whether tab and space characters are visualized.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether tab and space characters are visualized.")]
        [DefaultValue(true)]
        public bool ShowWhiteSpace
        {
            get { return Editor.Options.ShowTabs && Editor.Options.ShowSpaces; }
            set { Editor.Options.ShowTabs = Editor.Options.ShowSpaces = value; }
        }

        /// <summary>
        ///     Gets or sets whether a column ruler is displayed.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether a column ruler is displayed.")]
        [DefaultValue(true)]
        public bool ShowColumnRuler
        {
            get { return Editor.Options.ShowColumnRuler; }
            set { Editor.Options.ShowColumnRuler = value; }
        }

        /// <summary>
        ///     Gets or sets the position of the column ruler.
        /// </summary>
        [Browsable(true)]
        [Description("Controls the position of the column ruler.")]
        [DefaultValue(80)]
        public int ColumnRulerPosition
        {
            get { return Editor.Options.ColumnRulerPosition; }
            set { Editor.Options.ColumnRulerPosition = value; }
        }

        /// <summary>
        ///     Gets or sets whether the tab key inserts spaces instead of a tab character.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether the tab key inserts spaces instead of a tab character.")]
        [DefaultValue(true)]
        public bool ConvertTabsToSpaces
        {
            get { return Editor.Options.ConvertTabsToSpaces; }
            set { Editor.Options.ConvertTabsToSpaces = value; }
        }

        /// <summary>
        ///     Gets or sets whether cutting or copying with nothing selected cuts or copies the whole line.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether cutting or copying with nothing selected cuts or copies the whole line.")]
        [DefaultValue(true)]
        public bool CutCopyWholeLine
        {
            get { return Editor.Options.CutCopyWholeLine; }
            set { Editor.Options.CutCopyWholeLine = value; }
        }

        /// <summary>
        ///     Gets or sets the indentation (tab/space) width.
        /// </summary>
        [Browsable(true)]
        [Description("Controls the indentation (tab/space) width.")]
        [DefaultValue(4)]
        public int IndentationSize
        {
            get { return Editor.Options.IndentationSize; }
            set { Editor.Options.IndentationSize = value; }
        }

        #endregion

        #endregion

        #region Border styles

        private void PaintBorder(PaintEventArgs e)
        {
            if (BorderStyle == BorderStyle.None)
                return;

            var state = !Enabled      ? TextBoxState.Disabled :
                        ContainsFocus ? TextBoxState.Focused :
                        _isMouseOver  ? TextBoxState.Hot :
                                        TextBoxState.Normal;

            ThemeAPI.DrawThemedTextBoxBorder(Handle, e.Graphics, e.ClipRectangle, state);
        }

        #region Padding

        protected virtual Padding CalculatedPadding
        {
            get
            {
                return new Padding(Padding.Left   + _borderPadding.Left,
                                   Padding.Top    + _borderPadding.Top,
                                   Padding.Right  + _borderPadding.Right,
                                   Padding.Bottom + _borderPadding.Bottom);
            }
        }

        private void AdjustBorderPadding()
        {
            _borderPadding = (BorderStyle == BorderStyle.None) ? new Padding(0) : new Padding(1);
            AdjustChildRect();
        }

        private void OnPaddingChanged()
        {
            AdjustChildRect();
            Invalidate();
        }

        #endregion

        #region Mouse enter/leave

        private void ControlOnMouseEnter(object sender, EventArgs eventArgs)
        {
            _isMouseOver = IsThisTextEditorControl(sender);
            Invalidate();
        }

        private void ControlOnMouseLeave(object sender, EventArgs eventArgs)
        {
            _isMouseOver = false;
            Invalidate();
        }

        private bool IsThisTextEditorControl(object sender)
        {
            if (sender == this)
                return true;

            var source = sender as Control;
            if (source == null)
                return false;

            var control = source;
            while (control != null)
            {
                if (control == this)
                    return true;

                control = control.Parent;
            }

            return false;
        }

        #endregion

        #region Focus/blur

        private void OnGotFocus(object sender, EventArgs eventArgs)
        {
            Invalidate();
        }

        private void OnLostFocus(object sender, EventArgs eventArgs)
        {
            Invalidate();
        }

        #endregion

        #region Enable/disable

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnParentEnabledChanged(EventArgs e)
        {
            base.OnParentEnabledChanged(e);
            Invalidate();
        }

        #endregion

        #endregion

        #region Multiline

        /// <summary>
        ///     Gets or sets whether the user can enter multiple lines of text.
        /// </summary>
        [Browsable(true)]
        [Description("Determines whether the user can enter multiple lines of text.")]
        [DefaultValue(true)]
        public virtual bool Multiline
        {
            get { return Editor.Multiline; }
            set
            {
                if (value == Multiline)
                    return;

                Editor.Multiline = value;
            }
        }

        /// <summary>
        ///     Event raised when the value of the <see cref="Multiline"/> property is changed on this <see cref="TextEditorControl"/>.
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when the value of the Multiline property changes.")]
        [RefreshProperties(RefreshProperties.All)]
        public event EventHandler MultilineChanged;

        protected virtual void OnMultilineChanged(EventArgs args)
        {
            if (!Multiline)
            {
                Editor.Options.ConvertTabsToSpaces = false;
                Editor.Options.CutCopyWholeLine = false;
                Editor.Options.ShowColumnRuler = false;
                Editor.Options.ShowLineNumbers = false;
                Editor.Options.ShowSpaces = false;
                Editor.Options.ShowTabs = false;
            }

            SetStyle(ControlStyles.FixedHeight, !Multiline);

            RecreateHandle();

            if (IsHandleCreated)
                AdjustRects(false);

            if (MultilineChanged != null)
                MultilineChanged(this, args);
        }

        #endregion

        #region Sizing

        protected override Size DefaultSize
        {
            get { return new Size(500, 300); }
        }

        /// <summary>
        ///     Automatically adjusts the height of this control and its child edit control.
        ///     For singleline (non-multiline) controls, the height is calculated from the child control's font size.
        /// </summary>
        private void AdjustRects(bool returnIfAnchored)
        {
            // TODO: Always adjust child rect properly, even when returnIfAnchored is true

            var rects = GetAdjustedRects(returnIfAnchored);

            AdjustParentRect(rects.ParentRect);
            AdjustChildRect(rects.ChildRect);
        }

        private void AdjustParentRect(Rect rect)
        {
            Size = rect.Size;
        }

        private void AdjustChildRect(Rect rect)
        {
            Editor.Control.Size = rect.Size;
            Editor.Control.Location = rect.Location;
        }

        private void AdjustChildRect()
        {
            AdjustChildRect(GetAdjustedRects(false).ChildRect);
        }

        private RectSet GetAdjustedRects(bool returnIfAnchored)
        {
            var parentSize = Size;
            var parentLocation = Location;

            var childSize = Editor.Control.Size;
            var childLocation = new Point(CalculatedPadding.Left, CalculatedPadding.Top);

            // --- Anchored

            // If we're anchored to two opposite sides of the form, don't adjust the size because
            // we'll lose our anchored size by resetting to the requested width.
            if (returnIfAnchored &&
                (Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
            {
                return new RectSet(new Rect(parentSize, parentLocation), new Rect(childSize, childLocation));
            }

            var lrPad = CalculatedPadding.Left + CalculatedPadding.Right;
            var tbPad = CalculatedPadding.Top  + CalculatedPadding.Bottom;

            // --- Multiline

            if (Multiline)
            {
                childSize = new Size(parentSize.Width - lrPad,
                                     parentSize.Height - tbPad);

                return new RectSet(new Rect(parentSize, parentLocation), new Rect(childSize, childLocation));
            }

            // --- Singleline

            var fontSize = FontSizeConverter.GetWinFormsFontSize(Editor.FontSize);
            var font = new Font(Font.FontFamily, (float)fontSize, Font.Style, GraphicsUnit.Point, Font.GdiCharSet, Font.GdiVerticalFont);

            Size measuredTextSize;

            using (var g = CreateGraphics())
            {
                var size = g.MeasureString("MQ", font);
                measuredTextSize = new Size((int) Math.Ceiling(size.Width), (int) Math.Ceiling(size.Height));
            }

            var parentWidth = parentSize.Width;
            var parentHeight = (int) Math.Ceiling((double) measuredTextSize.Height);

            parentSize = new Size(parentWidth,
                                  parentHeight + tbPad);

            childSize = new Size(parentWidth + Editor.VerticalScrollBarWidth - lrPad,
                                 parentHeight + Editor.HorizontalScrollBarHeight);

            return new RectSet(new Rect(parentSize, parentLocation), new Rect(childSize, childLocation));
        }

        private class RectSet
        {
            public readonly Rect ParentRect;
            public readonly Rect ChildRect;

            public RectSet(Rect parentRect, Rect childRect)
            {
                ParentRect = parentRect;
                ChildRect = childRect;
            }
        }

        private class Rect
        {
            public readonly Size Size;
            public readonly Point Location;

            public Rect(Size size, Point location)
            {
                Size = size;
                Location = location;
            }
        }

        #endregion

        #region Font

        // TODO: Implement this
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
//            AdjustHeight(false);
        }

        #endregion

        #region Painting

        private event PaintEventHandler PaintBackground;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (PaintBackground != null)
                PaintBackground(this, e);
        }

        #endregion
    }
}
