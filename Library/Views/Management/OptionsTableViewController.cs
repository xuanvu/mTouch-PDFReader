//****************************************//
// mTouch-PDFReader library
// Options table view controller
//
// Created by Matsibarov Alexander. 2012.
// Copyright Matsibarov Alexander 2012. All rights reserved.
//
// www.mtouch-pdfreader.com
//****************************************//

using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using mTouchPDFReader.Library.Data.Managers;
using mTouchPDFReader.Library.Data.Enums;
using mTouchPDFReader.Library.Data.Objects;

namespace mTouchPDFReader.Library.Views.Management
{
	public class OptionsTableViewController : UITableViewController
	{
		#region Constants
		
		private const int DefaultCellWidth = 600;
		
		private const int DefaultCellHeight = 50;
		
		#endregion
		
		#region Fields
		
		// Page turning
		private UITableViewCell mPageTurningTypeCell;
        // Visibility
        private UITableViewCell mToolbarVisibilityCell; 
        private UITableViewCell mBottombarVisibilityCell; 
        private UITableViewCell mPageNumberVisibilityCell; 
        private UITableViewCell mNoteBtnVisibilityCell; 
        private UITableViewCell mBookmarksBtnVisibilityCell;
        private UITableViewCell mThumbsBtnVisibilityCell; 
        // Colors
        private UITableViewCell mBackgroundColorCell; 
        // Zoom
		private UITableViewCell mZoomScaleLevelsCell;
		private UITableViewCell mZoomByDoubleTouchCell;
		// Thumbs
        private UITableViewCell mThumbsBufferSizeCell; 
        private UITableViewCell mThumbSizeCell;
        // Library info
        private UITableViewCell mLibraryReleaseDateCell;
        private UITableViewCell mLibraryVersionCell;
		
		#endregion
		
		#region Constructors
		
		public OptionsTableViewController(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public OptionsTableViewController(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		public OptionsTableViewController() : base(null, null)
		{
			Initialize();
		}

		void Initialize()
		{
		}		
		#endregion	
		
		/// <summary>
		/// Calls when view has loaded
		/// </summary>
        public override void ViewDidLoad()
        {
        	base.ViewDidLoad();
   
        	// Page turning type
        	mPageTurningTypeCell = CreateTurningTypeCell();
        	// Visibility
        	mToolbarVisibilityCell = CreateToolbarVisibilityCell();
        	mBottombarVisibilityCell = CreateBottombarVisibilityCell();
        	mPageNumberVisibilityCell = CreatePageNumberVisibilityCell();
        	mNoteBtnVisibilityCell = CreateNoteBtnVisibilityCell();
        	mBookmarksBtnVisibilityCell = CreateBookmarksBtnVisibilityCell();
        	mThumbsBtnVisibilityCell = CreateThumbsBtnVisibilityCell();
        	// Colors
        	mBackgroundColorCell = CreateBackgroundColorCell();
        	// Zoom
        	mZoomScaleLevelsCell = CreateZoomScaleLevelsCell();
        	mZoomByDoubleTouchCell = CreatemZoomByDoubleTouchCell();
        	// Thumbs
        	mThumbsBufferSizeCell = CreateThumbsBufferSizeCell();
        	mThumbSizeCell = CreateThumbSizeCell();
        	// Library info
        	mLibraryReleaseDateCell = CreateLibraryReleaseDateCell();
        	mLibraryVersionCell = CreateLibraryVersionCell();
			
			TableView = new UITableView(View.Bounds, UITableViewStyle.Grouped);
			TableView.Source = new DataSource(this);        	
        }
		
		/// <summary>
		/// Called when permission is shought to rotate
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
		
		#region Logic
		
		/// <summary>
		/// Creates table cell 
		/// </summary>
		/// <param name="id">Cell UID</param>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateCell(string id)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default, id);
			cell.Frame = new RectangleF(0, 0, DefaultCellWidth, DefaultCellHeight);
			cell.BackgroundColor = UIColor.White;
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}
		
		/// <summary>
		/// Create title label control 
		/// </summary>
		/// <param name="title">Label title</param>
		/// <returns>Label control</returns>
		private UILabel CreateTitleLabelControl(string title)
		{
			var label = new UILabel(new RectangleF(60, 15, 400, 20));
			label.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin;
			label.BackgroundColor = UIColor.Clear;
			label.Text = title;
			label.Font = UIFont.BoldSystemFontOfSize(18.0f);
			return label;
		}
		
		/// <summary>
		/// Create value label control 
		/// </summary>
		/// <param name="title">Label title</param>
		/// <returns>Label control</returns>
		private UILabel CreateValueLabelControl(string title)
		{
			int width = 250;
			var label = new UILabel(new RectangleF(DefaultCellWidth - width - 60, 15, width, 20));
			label.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			label.BackgroundColor = UIColor.Clear;
			label.TextAlignment = UITextAlignment.Right;
			label.Text = title;	
			return label;
		}
		
		/// <summary>
		/// Creates segment control 
		/// </summary>
		/// <param name="values">Segment control values</param>
		/// <param name="width">Control width</param>
		/// <returns>Segment control</returns>
		private UISegmentedControl CreateSegmentControl(string[] values, int width)
		{
			var seg = new UISegmentedControl(new RectangleF(DefaultCellWidth - width - 60, 10, width, 30));
			seg.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			for (int i = 0; i < values.Length; i++) {
				seg.InsertSegment(values[i], i, false);
			}
			return seg;
		}
		
		/// <summary>
		/// Creates switch control
		/// <param name="values">Switch control values</param>
		/// <param name="width">Control width</param>
		/// </summary>
		/// <returns>Switch control</returns>
		private UISwitch CreateSwitchControl(string[] values)
		{
			int width = 90;
			var ctrl = new UISwitch(new RectangleF(DefaultCellWidth - width - 60, 10, width, 30));
			ctrl.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			return ctrl;
		}
		
		/// <summary>
		/// Creates slider control
		/// </summary>
		/// <param name="minValue">Slider minimum value</param>
		/// <param name="maxValue">Slider maximum value</param>
		/// <returns>Slider control</returns>
		private UISlider CreateSliderControl(int minValue, int maxValue)
		{
			int width = 250;
			var slider = new UISlider(new RectangleF(DefaultCellWidth - width - 60, 10, width, 30));
			slider.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			slider.MinValue = minValue;
			slider.MaxValue = maxValue;
			return slider;
		}
		
		/// <summary>
		/// Creates turning type cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateTurningTypeCell()
		{
			var cell = CreateCell("PageTurningTypeCell");
			var label = CreateTitleLabelControl("Type".t());
			var seg = CreateSegmentControl(new string[] { "Horz.".t(),	"Vert.".t() }, 150);
			seg.SelectedSegment = (int)OptionsManager.Instance.Options.pPageTurningType;
			seg.ValueChanged += delegate {
				OptionsManager.Instance.Options.pPageTurningType = (PageTurningType)seg.SelectedSegment;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(seg);
			return cell;
		}
		
		/// <summary>
		/// Creates toolbar visibility cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateToolbarVisibilityCell()
		{
			var cell = CreateCell("ToolbarVisibilityCell");
			var label = CreateTitleLabelControl("Toolbar".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.ToolbarVisible, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.ToolbarVisible = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
		/// <summary>
		/// Creates bottombar visibility cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateBottombarVisibilityCell()
		{
			var cell = CreateCell("BottombarVisibilityCell");
			var label = CreateTitleLabelControl("Bottombar".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.BottombarVisible, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.BottombarVisible = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
        /// <summary>
		/// Creates page number visibility cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreatePageNumberVisibilityCell()
		{
			var cell = CreateCell("PageNumberVisibilityCell");
			var label = CreateTitleLabelControl("Page number".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.PageNumberVisible, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.PageNumberVisible = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
        /// <summary>
		/// Creates note button visibility cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateNoteBtnVisibilityCell()
		{
			var cell = CreateCell("NoteBtnVisibilityCell");
			var label = CreateTitleLabelControl("Button 'Note'".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.NoteBtnVisible, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.NoteBtnVisible = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
        /// <summary>
		/// Creates bookmarks button visibility cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateBookmarksBtnVisibilityCell()
		{
			var cell = CreateCell("BookmarksBtnVisibilityCell");
			var label = CreateTitleLabelControl("Button 'Bookmarks'".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.BookmarksBtnVisible, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.BookmarksBtnVisible = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
        /// <summary>
		/// Creates thumbs button visibility cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateThumbsBtnVisibilityCell()
		{
			var cell = CreateCell("ThumbsBtnVisibilityCell");
			var label = CreateTitleLabelControl("Button 'Thumbs'".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.ThumbsBtnVisible, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.ThumbsBtnVisible = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
        /// <summary>
		/// Creates background color cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateBackgroundColorCell()
		{
			var cell = CreateCell("BackgroundColorCell");
			var label = CreateTitleLabelControl("Background".t());
			cell.AddSubview(label);
			return cell;
		}
		
		/// <summary>
		/// Creates zoom scale levels cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateZoomScaleLevelsCell()
		{
			var cell = CreateCell("ZoomScaleLevelsCell");
			var label = CreateTitleLabelControl("Zoom scale levels".t());
			var slider = CreateSliderControl(Options.MinZoomScaleLevels, Options.MaxZoomScaleLevels);
			slider.SetValue(OptionsManager.Instance.Options.ZoomScaleLevels, false);
			slider.ValueChanged += delegate {
				OptionsManager.Instance.Options.ZoomScaleLevels = (int)slider.Value;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(slider);
			return cell;
		}
		
		/// <summary>
		/// Creates zoom by double touch cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreatemZoomByDoubleTouchCell()
		{
			var cell = CreateCell("ZoomByDoubleTouchCell");
			var label = CreateTitleLabelControl("Scale by double click".t());
			var switchCtrl = CreateSwitchControl(new string[] { "Yes".t(), "No".t() });
			switchCtrl.SetState(OptionsManager.Instance.Options.AllowZoomByDoubleTouch, false);
			switchCtrl.ValueChanged += delegate {
				OptionsManager.Instance.Options.AllowZoomByDoubleTouch = switchCtrl.On;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(switchCtrl);
			return cell;
		}
		
        /// <summary>
		/// Creates thumbs buffer size cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateThumbsBufferSizeCell()
		{
			var cell = CreateCell("ThumbsBufferSizeCell");
			var label = CreateTitleLabelControl("Thumbs buffer size".t());
			var slider = CreateSliderControl(Options.MinThumbsBufferSize, Options.MaxThumbsBufferSize);
			slider.SetValue(OptionsManager.Instance.Options.ThumbsBufferSize, false);
			slider.ValueChanged += delegate {
				OptionsManager.Instance.Options.ThumbsBufferSize = (int)slider.Value;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(slider);
			return cell;
		}
		
		/// <summary>
		/// Creates thumbs size cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateThumbSizeCell()
		{
			var cell = CreateCell("ThumbSizeCell");
			var label = CreateTitleLabelControl("Thumbs size".t());
			var slider = CreateSliderControl(Options.MinThumbSize, Options.MaxThumbSize);
			slider.SetValue(OptionsManager.Instance.Options.ThumbSize, false);
			slider.ValueChanged += delegate {
				OptionsManager.Instance.Options.ThumbSize = (int)slider.Value;
				OptionsManager.Instance.Save();
			};
			cell.AddSubview(label);
			cell.AddSubview(slider);
			return cell;
		}
		
        /// <summary>
		/// Creates library release date cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateLibraryReleaseDateCell()
		{
			var cell = CreateCell("LibraryReleaseDateCell");
			var label = CreateTitleLabelControl("Release date".t());
			var labelInfo = CreateValueLabelControl(OptionsManager.Instance.Options.LibraryReleaseDate.ToShortDateString());			
			cell.AddSubview(label);
			cell.AddSubview(labelInfo);
			return cell;
		}
		
        /// <summary>
		/// Creates library version cell 
		/// </summary>
		/// <returns>Table cell</returns>
		private UITableViewCell CreateLibraryVersionCell()
		{
			var cell = CreateCell("LibraryVersionCell");
			var label = CreateTitleLabelControl("Version".t());
			var labelInfo = CreateValueLabelControl(OptionsManager.Instance.Options.LibraryVersion);
			cell.AddSubview(label);
			cell.AddSubview(labelInfo);
			return cell;
		}
		
		#endregion
		
		/// <summary>
		/// TableView datasource
		/// </summary> 
		class DataSource : UITableViewSource
		{
			private const int SectionsCount = 6;
			
			private readonly int [] RowsInSections = new int[] { 1, 6, 1, 2, 2, 2 }; 
			
			private readonly string[] SectionTitles = new string[] { "Turning".t(), "Visibility".t(), "Color".t(), "Scale".t(), "Thumbs".t(), "Library information".t() };
			
			/// <summary>
			/// Parent table controller
			/// </summary>
			private OptionsTableViewController mController;

			/// <summary>
			/// Work constructor
			/// </summary>
			public DataSource(OptionsTableViewController controller)
			{
				mController = controller;
			}
		
			/// <summary>
			/// Returns numbers of groups 
			/// </summary>
			public override int NumberOfSections(UITableView tableView)
			{
				return SectionsCount;
			}
			
			/// <summary>
			/// Returns rows count
			/// </summary>
			public override int RowsInSection(UITableView tableview, int section)
			{
				return RowsInSections[section];
			}
			
			/// <summary>
			/// Returns title four group
			/// </summary>
			public override string TitleForHeader(UITableView tableView, int section)
			{
				return SectionTitles[section];
			}

			/// <summary>
			/// Returns row by id
			/// </summary>
			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				switch (indexPath.Section) 
				{
					case 0:
						return mController.mPageTurningTypeCell;
					case 1:
						switch (indexPath.Row) {
							case 0:
								return mController.mToolbarVisibilityCell;
							case 1:
								return mController.mBottombarVisibilityCell;
							case 2:
								return mController.mPageNumberVisibilityCell;
							case 3:
								return mController.mNoteBtnVisibilityCell;
							case 4:
								return mController.mBookmarksBtnVisibilityCell;
							case 5:
								return mController.mThumbsBtnVisibilityCell;
						}
						break;
					case 2:
						return mController.mBackgroundColorCell;
					case 3:
						switch (indexPath.Row) {
							case 0:
								return mController.mZoomScaleLevelsCell;
							case 1:
								return mController.mZoomByDoubleTouchCell;
						}
						break;
					case 4:
						switch (indexPath.Row) {
							case 0:
								return mController.mThumbsBufferSizeCell;
							case 1:
								return mController.mThumbSizeCell;
						}
						break;
					case 5:
						switch (indexPath.Row) {
							case 0:
								return mController.mLibraryReleaseDateCell;
							case 1:
								return mController.mLibraryVersionCell;
						}
						break;
				}
				return null;
			}

			/// <summary>
			/// Selects theme when user clicked by row
			/// </summary>
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
			}

			/// <summary>
			/// Selectes theme when user clicked by accessory button
			/// </summary>
			public override void AccessoryButtonTapped(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
			}
		}
	}
}

